using Autodoc.GUI.Controls;
using Autodoc.GUI.View;
using MahApps.Metro.Controls;
using System.Windows;

namespace Autodoc.GUI;

public partial class MainWindow : MetroWindow
{    
    public MainWindow(SettingsControl settingsControl, DecodeView decodeView)
    {
        InitializeComponent();
                
        settingsFlyout.Content = settingsControl;
        
        mainContent.Content = decodeView;
    }
    
    private void OpenSettings_Click(object sender, RoutedEventArgs e) => settingsFlyout.IsOpen = true;
}

