﻿// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

using AudioSwitcher.Interop;

namespace AudioSwitcher.Presentation.UI.Interop
{
    internal class DllImports
    {
        [DllImport(ExternalDll.User32, ExactSpelling = true)]
        public static extern bool SetForegroundWindow(HandleRef hWnd);

        [DllImport(ExternalDll.Uxtheme)]
        public extern static int GetThemeMargins(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, int iPropId, IntPtr rect, out MARGINS pMargins);

        [DllImport(ExternalDll.User32)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport(ExternalDll.User32)]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport(ExternalDll.Gdi32)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

    }
}
