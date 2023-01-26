// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Diagnostics;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.ViewManagement;

namespace WSAFileLink
{
    public sealed partial class WSAFileLink : Page
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        DataPackage dataPackage = new DataPackage();
        private void refreshCMD()
        {
            String adbPath = localSettings.Values["adb"] as string;
            adbPushCMD.Text = adbPath + " connect 127.0.0.1:58526; " + adbPath + " push '" + FileFolderPath.Text + "' '" + ToAndroidPath.Text + "';";
            adbPullCMD.Text = adbPath + " connect 127.0.0.1:58526; " + adbPath + " pull '" + PullFolderPath.Text + "' '" + ToWindowsPath.Text + "';";
        }
        public WSAFileLink()
        {
            this.InitializeComponent();

            FileFolderPath.Text = localSettings.Values["FileFolderPath"] as string;
            ToAndroidPath.Text = localSettings.Values["ToAndroidPath"] as string;
            if (ToAndroidPath.Text == "") { ToAndroidPath.Text = "/storage/emulated/0/Download/"; }
            PullFolderPath.Text = localSettings.Values["PullFolderPath"] as string;
            if (PullFolderPath.Text == "") { PullFolderPath.Text = "/storage/emulated/0/Download/"; }
            ToWindowsPath.Text = localSettings.Values["ToWindowsPath"] as string;

            refreshCMD();
        }
        private void FileFolderPath_DragOver(object sender, DragEventArgs e)
        {
        }
        private void FileFolderPath_Drop(object sender, DragEventArgs e)
        {
        }
        public void FileFolderPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            localSettings.Values["FileFolderPath"] = FileFolderPath.Text;
            refreshCMD();
        }
        public void ToAndroidPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            localSettings.Values["ToAndroidPath"] = ToAndroidPath.Text;
            refreshCMD();
        }
        public void PullFolderPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            localSettings.Values["PullFolderPath"] = PullFolderPath.Text;
            refreshCMD();
        }
        public void ToWindowsPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            localSettings.Values["ToWindowsPath"] = ToWindowsPath.Text;
            refreshCMD();
        }
        private void toAndroidCopyButton_Click(object sender, RoutedEventArgs e)
        {
            String adbPath = localSettings.Values["adb"] as string;
            dataPackage.SetText(adbPath + " connect 127.0.0.1:58526; " + adbPath + " push '" + FileFolderPath.Text + "' '" + ToAndroidPath.Text + "';");
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            Clipboard.SetContent(dataPackage);
        }
        private void toWindowsCopyButton_Click(object sender, RoutedEventArgs e)
        {
            String adbPath = localSettings.Values["adb"] as string;
            dataPackage.SetText(adbPath + " connect 127.0.0.1:58526; " + adbPath + " pull '" + PullFolderPath.Text + "' '" + ToWindowsPath.Text + "';");
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            Clipboard.SetContent(dataPackage);
        }
        private void pushButton_Click(object sender, RoutedEventArgs e)
        {
            String adbPath = localSettings.Values["adb"] as string;
            Process process = new Process();
            process.StartInfo.FileName = "PowerShell.exe";
            process.StartInfo.Arguments = adbPath + " connect 127.0.0.1:58526; " + adbPath + " push '" + FileFolderPath.Text + "' '" + ToAndroidPath.Text + "';";
            //是否使用操作系统shell启动
            process.StartInfo.UseShellExecute = false;
            //是否在新窗口中启动该进程的值 (不显示程序窗口)
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            //等待程序执行完退出进程
            process.WaitForExit();
            process.Close();
        }
        private void pullButton_Click(object sender, RoutedEventArgs e)
        {
            String adbPath = localSettings.Values["adb"] as string;
            Process process = new Process();
            process.StartInfo.FileName = "PowerShell.exe";
            process.StartInfo.Arguments = adbPath + " connect 127.0.0.1:58526; " + adbPath + " pull '" + PullFolderPath.Text + "' '" + ToWindowsPath.Text + "';";
            //是否使用操作系统shell启动
            process.StartInfo.UseShellExecute = false;
            //是否在新窗口中启动该进程的值 (不显示程序窗口)
            process.StartInfo.CreateNoWindow = false;
            process.Start();
            //等待程序执行完退出进程
            process.WaitForExit();
            process.Close();
        }

        private async void toAndroidPickerButton_Click(object sender, RoutedEventArgs e)
        {
            // 创建一个FilePicker
            var filePicker = new FileOpenPicker();
            // 通过传入App.m_window对象获取当前窗口的HWND
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            // 将 HWND 与文件选择器相关联
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);
            // 使用文件选择器
            filePicker.FileTypeFilter.Add("*");
            var file = await filePicker.PickSingleFileAsync();
            // 如果file不为空，则将Path填入FileFolderPath.Text
            if (file != null)
            {
                FileFolderPath.Text = file.Path;
            }
        }
        private async void toWindowsPickerButton_Click(object sender, RoutedEventArgs e)
        {
            // 创建一个FolderPicker
            var folderPicker = new FolderPicker();
            // 通过传入App.m_window对象获取当前窗口的HWND
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            // 将 HWND 与文件夹选择器相关联
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);
            // 使用文件夹选择器
            var folder = await folderPicker.PickSingleFolderAsync();
            // 如果folder不为空，将Path填入ToWindowsPath.Text
            if (folder != null)
            {
                ToWindowsPath.Text = folder.Path;
            }
        }
    }
}
