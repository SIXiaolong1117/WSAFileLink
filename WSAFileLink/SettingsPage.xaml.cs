// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WSAFileLink
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public SettingsPage()
        {
            this.InitializeComponent();

            adbPath.Text = localSettings.Values["adb"] as string;
            if (adbPath.Text == "")
            {
                adbPath.Text = "adb";
            }
        }

        private void saveSettingButton_Click(object sender, RoutedEventArgs e)
        {
            //    localSettings.Values["adb"] = adbPath.Text;
        }

        public void adbPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            localSettings.Values["adb"] = adbPath.Text;
        }
    }
}
