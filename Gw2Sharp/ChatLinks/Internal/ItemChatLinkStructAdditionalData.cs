#pragma warning disable CA1028 // Enum Storage should be Int32
#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable S2342 // Enumeration types should comply with a naming convention

using System;

namespace Gw2Sharp.ChatLinks.Internal
{
    /// <summary>
    /// Additional data flags for item chat links.
    /// </summary>
    [Flags]
    public enum ItemChatLinkStructAdditionalData : byte
    {
        /// <summary>
        /// No extra data.
        /// </summary>
        None = 0,

        /// <summary>
        /// The item has a custom skin.
        /// </summary>
        Skin = 1 << 7,

        /// <summary>
        /// The item has a first upgrade.
        /// </summary>
        Upgrade1 = 1 << 6,

        /// <summary>
        /// The item has a second upgrade.
        /// </summary>
        Upgrade2 = 1 << 5
    }
}
