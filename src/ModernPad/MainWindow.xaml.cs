using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

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

    private void MainTabView_AddTabButtonClick(TabView sender, object args)
    {
        CreateNewTab("Untitled");
    }

    private void MainTabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
    {
        sender.TabItems.Remove(args.Tab);
    }
}