using System;
using System.IO.MemoryMappedFiles;
using System.Net.Sockets;
using System.Text.Json;
using Gw2Sharp.Json;
using Gw2Sharp.Models;
using Gw2Sharp.Mumble.Models;

namespace Gw2Sharp.Mumble
{
    /// <summary>
    /// A client for the Guild Wars 2 Mumble Link API service.
    /// </summary>
    public class Gw2MumbleClient : BaseClient, IGw2MumbleClient
    {
        /// <summary>
        /// The settings that are used when deserializing JSON objects.
        /// </summary>
        private static readonly JsonSerializerOptions deserializerSettings = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = SnakeCaseNamingPolicy.SnakeCase
        };

        internal const string MUMBLE_LINK_MAP_NAME = "MumbleLink";
        internal const string MUMBLE_LINK_GAME_NAME_GUILD_WARS_2 = "Guild Wars 2";
        internal static readonly char[] mumbleLinkGameName = new[] { 'G', 'u', 'i', 'l', 'd', ' ', 'W', 'a', 'r', 's', ' ', '2', '\0' };
        private const string EMPTY_IDENTITY = "{}";

        private string rawIdentity = EMPTY_IDENTITY;
        private readonly object identityLock = new object();
        private readonly object serverAddressLock = new object();

        private readonly Lazy<MemoryMappedFile> memoryMappedFile;
        private readonly Lazy<MemoryMappedViewAccessor> memoryMappedViewAccessor;

        private Gw2LinkedMem linkedMem;

        /// <summary>
        /// Creates a new <see cref="Gw2MumbleClient"/>.
        /// </summary>
        /// <param name="connection">The connection used to make requests, see <see cref="IConnection"/>.</param>
        /// <param name="gw2Client">The Guild Wars 2 client.</param>
        /// <exception cref="ArgumentNullException"><paramref name="connection"/> or <paramref name="gw2Client"/> is <c>null</c>.</exception>
        protected internal Gw2MumbleClient(IConnection connection, IGw2Client gw2Client) : base(connection, gw2Client)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));
            if (gw2Client == null)
                throw new ArgumentNullException(nameof(gw2Client));

            this.memoryMappedFile = new Lazy<MemoryMappedFile>(
                () => MemoryMappedFile.CreateOrOpen(MUMBLE_LINK_MAP_NAME, Gw2LinkedMem.SIZE, MemoryMappedFileAccess.ReadWrite), true);
            this.memoryMappedViewAccessor = new Lazy<MemoryMappedViewAccessor>(
                () => this.memoryMappedFile.Value.CreateViewAccessor(), true);
        }

        private unsafe void UpdateIdentityIfNeeded()
        {
            // Check if we're in the same frame update
            if (this.linkedMem.identity[0] == '\0')
                return;

            // Thread-safety
            lock (this.identityLock)
            {
                // Check again
                if (this.linkedMem.identity[0] != '\0')
                {
                    var cacheSpan = this.rawIdentity.AsSpan();
                    fixed (char* ptr = this.linkedMem.identity)
                    {
                        // Check if the identity is different from last update
                        var span = new ReadOnlySpan<char>(ptr, this.rawIdentity.Length);
                        if (!span.SequenceEqual(cacheSpan))
                        {
                            // Update cache
                            this.rawIdentity = new string(ptr);
                            try
                            {
                                this.identity = JsonSerializer.Deserialize<CharacterIdentity>(this.rawIdentity, deserializerSettings);
                            }
                            catch (JsonException)
                            {
                                this.identity = null;
                            }
                        }
                    }
                    this.linkedMem.identity[0] = '\0';
                }
            }
        }

        private CharacterIdentity? identity;
        private unsafe CharacterIdentity? Identity
        {
            get
            {
                this.UpdateIdentityIfNeeded();
                return this.identity;
            }
        }


        /// <inheritdoc />
        public bool IsAvailable { get; private set; }

        /// <inheritdoc />
        public int Version =>
            this.IsAvailable ? (int)this.linkedMem.uiVersion : default;

        /// <inheritdoc />
        public int Tick =>
            this.IsAvailable ? (int)this.linkedMem.uiTick : default;

        /// <inheritdoc />
        public string Name { get; private set; } = string.Empty;

        /// <inheritdoc />
        public unsafe Coordinates3 AvatarPosition =>
            this.IsAvailable ? new Coordinates3(this.linkedMem.fAvatarPosition[0], this.linkedMem.fAvatarPosition[1], this.linkedMem.fAvatarPosition[2]) : default;

        /// <inheritdoc />
        public unsafe Coordinates3 AvatarFront =>
            this.IsAvailable ? new Coordinates3(this.linkedMem.fAvatarFront[0], this.linkedMem.fAvatarFront[1], this.linkedMem.fAvatarFront[2]) : default;

        /// <inheritdoc />
        public unsafe Coordinates3 CameraPosition =>
            this.IsAvailable ? new Coordinates3(this.linkedMem.fCameraPosition[0], this.linkedMem.fCameraPosition[1], this.linkedMem.fCameraPosition[2]) : default;

        /// <inheritdoc />
        public unsafe Coordinates3 CameraFront =>
            this.IsAvailable ? new Coordinates3(this.linkedMem.fCameraFront[0], this.linkedMem.fCameraFront[1], this.linkedMem.fCameraFront[2]) : default;

        private readonly byte[] serverAddressCache = new byte[Gw2Context.SOCKET_ADDRESS_SIZE];
        private bool serverAddressCacheDirty = true;
        private string? serverAddress;
        /// <inheritdoc />
        public unsafe string ServerAddress
        {
            get
            {
                if (!this.IsAvailable)
                    return string.Empty;

                // Check if we're in the same frame update
                if (!this.serverAddressCacheDirty)
                    return this.serverAddress ?? string.Empty;

                // Thread-safety
                lock (this.serverAddressLock)
                {
                    var cacheSpan = this.serverAddressCache.AsSpan();
                    fixed (byte* ptr = this.linkedMem.context.socketAddress)
                    {
                        // Check if the server address is different from last update
                        var span = new ReadOnlySpan<byte>(ptr, Gw2Context.SOCKET_ADDRESS_SIZE);
                        if (!span.SequenceEqual(cacheSpan))
                        {
                            // Update server address
                            span.CopyTo(cacheSpan);
                            this.serverAddressCacheDirty = false;
                            this.serverAddress = this.linkedMem.context.socketAddressFamily switch
                            {
                                AddressFamily.InterNetwork =>
                                    $"{this.linkedMem.context.socketAddress4[0]}." +
                                    $"{this.linkedMem.context.socketAddress4[1]}." +
                                    $"{this.linkedMem.context.socketAddress4[2]}." +
                                    $"{this.linkedMem.context.socketAddress4[3]}",
                                AddressFamily.InterNetworkV6 =>
                                    $"{this.linkedMem.context.socketAddress6[0]:X4}:" +
                                    $"{this.linkedMem.context.socketAddress6[1]:X4}:" +
                                    $"{this.linkedMem.context.socketAddress6[2]:X4}:" +
                                    $"{this.linkedMem.context.socketAddress6[3]:X4}:" +
                                    $"{this.linkedMem.context.socketAddress6[4]:X4}:" +
                                    $"{this.linkedMem.context.socketAddress6[5]:X4}:" +
                                    $"{this.linkedMem.context.socketAddress6[6]:X4}:" +
                                    $"{this.linkedMem.context.socketAddress6[7]:X4}",
                                _ => string.Empty
                            };
                        }
                    }
                }
                return this.serverAddress ?? string.Empty;
            }
        }

        /// <inheritdoc />
        public ushort ServerPort =>
            this.IsAvailable ? this.linkedMem.context.socketPort : default;

        /// <inheritdoc />
        public int BuildId =>
            this.IsAvailable ? (int)this.linkedMem.context.buildId : default;

        /// <inheritdoc />
        public bool IsMapOpen =>
            this.IsAvailable ? this.linkedMem.context.uiState.HasFlag(UiState.IsMapOpen) : default;

        /// <inheritdoc />
        public bool IsCompassTopRight =>
            this.IsAvailable ? this.linkedMem.context.uiState.HasFlag(UiState.IsCompassTopRight) : default;

        /// <inheritdoc />
        public bool IsCompassRotationEnabled =>
            this.IsAvailable ? this.linkedMem.context.uiState.HasFlag(UiState.IsCompassRotationEnabled) : default;

        /// <inheritdoc />
        public bool DoesGameHaveFocus =>
            this.IsAvailable ? this.linkedMem.context.uiState.HasFlag(UiState.DoesGameHaveFocus) : default;

        /// <inheritdoc />
        public bool IsCompetitiveMode =>
            this.IsAvailable ? this.linkedMem.context.uiState.HasFlag(UiState.IsCompetitiveMode) : default;

        /// <inheritdoc />
        public bool DoesAnyInputHaveFocus =>
            this.IsAvailable ? this.linkedMem.context.uiState.HasFlag(UiState.DoesAnyInputHaveFocus) : default;

        /// <inheritdoc />
        public Size Compass =>
            this.IsAvailable ? new Size(this.linkedMem.context.compassWidth, this.linkedMem.context.compassHeight) : default;

        /// <inheritdoc />
        public double CompassRotation =>
            this.IsAvailable ? this.linkedMem.context.compassRotation : default;

        /// <inheritdoc />
        public Coordinates2 PlayerLocationMap =>
            this.IsAvailable ? new Coordinates2(this.linkedMem.context.playerMapX, this.linkedMem.context.playerMapY) : default;

        /// <inheritdoc />
        public Coordinates2 MapCenter =>
            this.IsAvailable ? new Coordinates2(this.linkedMem.context.mapCenterX, this.linkedMem.context.mapCenterY) : default;

        /// <inheritdoc />
        public double MapScale =>
            this.IsAvailable ? this.linkedMem.context.mapScale : default;


        /// <inheritdoc />
        public int MapId =>
            this.IsAvailable ? (int)this.linkedMem.context.mapId : default;

        /// <inheritdoc />
        public MapType MapType =>
            this.IsAvailable ? (MapType)this.linkedMem.context.mapType : default;

        /// <inheritdoc />
        public uint ShardId =>
            this.IsAvailable ? this.linkedMem.context.shardId : default;

        /// <inheritdoc />
        public uint Instance =>
            this.IsAvailable ? this.linkedMem.context.instance : default;

        /// <inheritdoc />
        public unsafe string RawIdentity
        {
            get
            {
                if (!this.IsAvailable)
                    return string.Empty;

                this.UpdateIdentityIfNeeded();
                return this.rawIdentity;
            }
        }

        /// <inheritdoc />
        public string CharacterName =>
            this.IsAvailable && this.Identity != null ? this.Identity.Name : string.Empty;

        /// <inheritdoc />
        public ProfessionType Profession =>
            this.IsAvailable && this.Identity != null ? this.Identity.Profession : 0;

        /// <inheritdoc />
        public int Specialization =>
            this.IsAvailable && this.Identity != null ? this.Identity.Spec : 0;

        /// <inheritdoc />
        public RaceType Race =>
            this.IsAvailable && this.Identity != null ? this.Identity.Race : 0;

        /// <inheritdoc />
        public int TeamColorId =>
            this.IsAvailable && this.Identity != null ? this.Identity.TeamColorId : default;

        /// <inheritdoc />
        public bool IsCommander =>
            this.IsAvailable && this.Identity != null ? this.Identity.Commander : default;

        /// <inheritdoc />
        public double FieldOfView =>
            this.IsAvailable && this.Identity != null ? this.Identity.Fov : default;

        /// <inheritdoc />
        public UiSize UiSize =>
            this.IsAvailable && this.Identity != null ? this.Identity.Uisz : default;


        /// <inheritdoc />
        public unsafe void Update()
        {
            if (this.isDisposed)
                throw new ObjectDisposedException(nameof(Gw2MumbleClient));



            this.memoryMappedViewAccessor.Value.Read<Gw2LinkedMem>(0, out var linkedMem);
            int oldTick = this.Tick;

            if (linkedMem.uiTick != oldTick)
            {
                var gameNameSpan = new ReadOnlySpan<char>(mumbleLinkGameName);
                var linkedNameSpan = new ReadOnlySpan<char>(linkedMem.name, mumbleLinkGameName.Length);
                this.IsAvailable = gameNameSpan.SequenceEqual(linkedNameSpan);

                if (this.IsAvailable)
                {
                    this.Name = MUMBLE_LINK_GAME_NAME_GUILD_WARS_2;
                    this.serverAddressCacheDirty = true;
                }
                else
                {
                    this.Name = new string(linkedMem.name);
                }
            }

            this.linkedMem = linkedMem;
        }

        #region IDisposable Support

        private bool isDisposed = false; // To detect redundant calls

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">Dispose managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    if (this.memoryMappedViewAccessor.IsValueCreated)
                        this.memoryMappedViewAccessor.Value.Dispose();
                    if (this.memoryMappedFile.IsValueCreated)
                        this.memoryMappedFile.Value.Dispose();
                }

                this.isDisposed = true;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}