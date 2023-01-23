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

namespace WSAFileLink
{
    public sealed partial class WSAFileLink : Page
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public WSAFileLink()
        {
            this.InitializeComponent();

            FileFolderPath.Text = localSettings.Values["FileFolderPath"] as string;
            ToAndroidPath.Text = localSettings.Values["ToAndroidPath"] as string;
            if (ToAndroidPath.Text == "") { ToAndroidPath.Text = "/storage/emulated/0/Download/"; }
            PullFolderPath.Text = localSettings.Values["PullFolderPath"] as string;
            if (PullFolderPath.Text == "") { PullFolderPath.Text = "/storage/emulated/0/Download/"; }
            ToWindowsPath.Text = localSettings.Values["ToWindowsPath"] as string;
        }

        public void FileFolderPath_TextChanged(object sender, TextChangedEventArgs e) { localSettings.Values["FileFolderPath"] = FileFolderPath.Text; }
        public void ToAndroidPath_TextChanged(object sender, TextChangedEventArgs e) { localSettings.Values["ToAndroidPath"] = ToAndroidPath.Text; }
        public void PullFolderPath_TextChanged(object sender, TextChangedEventArgs e) { localSettings.Values["PullFolderPath"] = PullFolderPath.Text; }
        public void ToWindowsPath_TextChanged(object sender, TextChangedEventArgs e) { localSettings.Values["ToWindowsPath"] = ToWindowsPath.Text; }
        private void pushButton_Click(object sender, RoutedEventArgs e)
        {
            String adbPath = localSettings.Values["adb"] as string;
            Process process = new Process();
            process.StartInfo.FileName = "PowerShell.exe";
            process.StartInfo.Arguments = adbPath + " connect 127.0.0.1:58526; " + adbPath + " push '" + FileFolderPath.Text + "' '" + ToAndroidPath.Text + "'";
            //�Ƿ�ʹ�ò���ϵͳshell����
            process.StartInfo.UseShellExecute = false;
            //�Ƿ����´����������ý��̵�ֵ (����ʾ���򴰿�)
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            //�ȴ�����ִ�����˳�����
            process.WaitForExit();
            process.Close();
        }

        private void pullButton_Click(object sender, RoutedEventArgs e)
        {
            String adbPath = localSettings.Values["adb"] as string;
            Process process = new Process();
            process.StartInfo.FileName = "PowerShell.exe";
            process.StartInfo.Arguments = adbPath + " connect 127.0.0.1:58526; " + adbPath + " pull '" + PullFolderPath.Text + "' '" + ToWindowsPath.Text + "'; ";
            //�Ƿ�ʹ�ò���ϵͳshell����
            process.StartInfo.UseShellExecute = false;
            //�Ƿ����´����������ý��̵�ֵ (����ʾ���򴰿�)
            process.StartInfo.CreateNoWindow = false;
            process.Start();
            //�ȴ�����ִ�����˳�����
            process.WaitForExit();
            process.Close();
        }

        private async void toAndroidPickerButton_Click(object sender, RoutedEventArgs e)
        {
            // ����һ��FilePicker
            var filePicker = new FileOpenPicker();
            // ͨ������App.m_window�����ȡ��ǰ���ڵ�HWND
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            // �� HWND ���ļ�ѡ���������
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);
            // ʹ���ļ�ѡ����
            filePicker.FileTypeFilter.Add("*");
            var file = await filePicker.PickSingleFileAsync();
            // ���file��Ϊ�գ���Path����FileFolderPath.Text
            if (file != null)
            {
                FileFolderPath.Text = file.Path;
            }
        }
        private async void toWindowsPickerButton_Click(object sender, RoutedEventArgs e)
        {
            // ����һ��FolderPicker
            var folderPicker = new FolderPicker();
            // ͨ������App.m_window�����ȡ��ǰ���ڵ�HWND
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            // �� HWND ���ļ���ѡ���������
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);
            // ʹ���ļ���ѡ����
            var folder = await folderPicker.PickSingleFolderAsync();
            // ���folder��Ϊ�գ���Path����ToWindowsPath.Text
            if (folder != null)
            {
                ToWindowsPath.Text = folder.Path;
            }
        }
    }
}