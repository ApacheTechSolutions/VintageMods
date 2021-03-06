﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace VintageMods.Core.MemoryAdaptor.Native.Types
{
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public struct LUID
    {
        public int LowPart;
        public int HighPart;
    }
}