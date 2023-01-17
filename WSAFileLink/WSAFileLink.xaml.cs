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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WSAFileLink
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WSAFileLink : Page
    {
        public WSAFileLink()
        {
            this.InitializeComponent();
            ToAndroidPath.Text = "/storage/emulated/0/Download/";
            PullFolderPath.Text = "/storage/emulated/0/Download/";
        }

        private void pushButton_Click(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "PowerShell.exe";
            process.StartInfo.Arguments = "adb connect 127.0.0.1:58526; adb push '" + FileFolderPath.Text + "' '" + ToAndroidPath.Text + "'";
            process.StartInfo.UseShellExecute = false; //是否使用操作系统shell启动
            process.StartInfo.CreateNoWindow = true; //是否在新窗口中启动该进程的值 (不显示程序窗口)
            process.Start();
            process.WaitForExit(); //等待程序执行完退出进程
            process.Close();
        }

        private void pullButton_Click(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "PowerShell.exe";
            process.StartInfo.Arguments = "adb connect 127.0.0.1:58526; adb pull '" + PullFolderPath.Text + "' '" + ToWindowsPath.Text + "'; ";
            process.StartInfo.UseShellExecute = false; //是否使用操作系统shell启动
            process.StartInfo.CreateNoWindow = false; //是否在新窗口中启动该进程的值 (不显示程序窗口)
            process.Start();
            process.WaitForExit(); //等待程序执行完退出进程
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
