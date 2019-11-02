using System.Runtime.InteropServices;

namespace Gw2Sharp.ChatLinks.Structs
{
    /// <summary>
    /// Represents a Guild Wars 2 point of interest chat link.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct WvwObjectiveChatLinkStruct
    {
        /// <summary>
        /// The objective id.
        /// </summary>
        [FieldOffset(0)]
        public uint ObjectiveId;

        /// <summary>
        /// The map id.
        /// </summary>
        [FieldOffset(4)]
        public uint MapId;
    }
}
