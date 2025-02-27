﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using ColorPicker.Helpers;
using ColorPicker.Mouse;
using ColorPickerUI;

namespace ColorPicker
{
    public static class Program
    {
        private static string[] _args;

        [STAThread]
        public static void Main(string[] args)
        {
            _args = args;
            Logger.LogInfo($"Color Picker started with pid={Environment.ProcessId}");
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            try
            {
                using (var application = new App())
                {
                    application.InitializeComponent();
                    application.Run();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Unhandled exception", ex);
                CursorManager.RestoreOriginalCursors();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                Logger.LogError("Unhandled exception", ex);
            }
            else
            {
                Logger.LogError("Unhandled exception");
            }

            CursorManager.RestoreOriginalCursors();
        }
    }
}
