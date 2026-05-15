using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace ModernPad;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
        CreateNewTab("Untitled");
    }

    private void CreateNewTab(string title)
    {
        var tab = new TabViewItem
        {
            Header = title,
            Content = new TextBlock
            {
                Text = "Editor will be here (Monaco + WebView2 coming soon)",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            }
        };

        MainTabView.TabItems.Add(tab);
        MainTabView.SelectedItem = tab;
    }

    // === File Menu ===
    private void Menu_New_Click(object sender, RoutedEventArgs e)
    {
        CreateNewTab("Untitled");
    }

    private async void Menu_Open_Click(object sender, RoutedEventArgs e)
    {
        var picker = new FileOpenPicker();
        picker.FileTypeFilter.Add("*");

        // WinUI 3 requires this for the picker to work
        var hwnd = WindowNative.GetWindowHandle(this);
        InitializeWithWindow.Initialize(picker, hwnd);

        StorageFile file = await picker.PickSingleFileAsync();
        if (file != null)
        {
            CreateNewTab(file.Name);
            // TODO: Load file content into the editor
        }
    }

    private void Menu_Save_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement save
    }

    private void Menu_SaveAs_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement Save As
    }

    private void Menu_Exit_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    // === Language / Syntax Menu ===
    private void Menu_Language_Click(object sender, RoutedEventArgs e)
    {
        if (sender is MenuFlyoutItem item && MainTabView.SelectedItem is TabViewItem currentTab)
        {
            string lang = item.Tag?.ToString() ?? "plaintext";
            currentTab.Header = $"{currentTab.Header} ({lang})";
            // TODO: Change syntax highlighting in the editor
        }
    }

    // === UI Language (Localization) ===
    private void Menu_UILanguage_Click(object sender, RoutedEventArgs e)
    {
        if (sender is MenuFlyoutItem item)
        {
            string lang = item.Tag?.ToString() ?? "en";

            if (lang == "ru")
            {
                // TODO: Switch to Russian resources
                this.Title = "ModernPad (Русский)";
            }
            else
            {
                this.Title = "ModernPad";
            }

            // In future: Load .resw resources and refresh UI
        }
    }

    private void MainTabView_AddTabButtonClick(TabView sender, object args)
    {
        CreateNewTab("Untitled");
    }

    private void MainTabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
    {
        sender.TabItems.Remove(args.Tab);
    }
}