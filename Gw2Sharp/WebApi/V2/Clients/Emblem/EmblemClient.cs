using System;

namespace Gw2Sharp.WebApi.V2.Clients
{
    /// <summary>
    /// A client of the Guild Wars 2 API v2 emblem endpoint.
    /// </summary>
    [EndpointPath("emblem")]
    public class EmblemClient : BaseClient, IEmblemClient
    {
        private readonly IEmblemBackgroundsClient backgrounds;
        private readonly IEmblemForegroundsClient foregrounds;

        /// <summary>
        /// Creates a new <see cref="EmblemClient"/> that is used for the API v2 emblem endpoint.
        /// </summary>
        /// <param name="connection">The connection used to make requests, see <see cref="IConnection"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="connection"/> is <c>null</c>.</exception>
        public EmblemClient(IConnection connection) :
            base(connection)
        {
            this.backgrounds = new EmblemBackgroundsClient(connection);
            this.foregrounds = new EmblemForegroundsClient(connection);
        }

        /// <inheritdoc />
        public virtual IEmblemBackgroundsClient Backgrounds => this.backgrounds;

        /// <inheritdoc />
        public virtual IEmblemForegroundsClient Foregrounds => this.foregrounds;
    }
}