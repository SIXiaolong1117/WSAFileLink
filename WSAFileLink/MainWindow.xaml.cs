// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WSAFileLink
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            // Hide default title bar.
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            contentFrame.Navigate(typeof(WSAFileLink));
        }
        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                contentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                var selectedItem = (NavigationViewItem)args.SelectedItem;
                if ((string)selectedItem.Tag == "WSAFileLink")
                {
                    contentFrame.Navigate(typeof(WSAFileLink));
                }
            }
            //else if ((string)selectedItem.Tag == "SamplePage2") contentFrame.Navigate(typeof(SamplePage2));
            //else if ((string)selectedItem.Tag == "SamplePage3") contentFrame.Navigate(typeof(SamplePage3));
            //else if ((string)selectedItem.Tag == "SamplePage4") contentFrame.Navigate(typeof(SamplePage4));

        }
    }
}