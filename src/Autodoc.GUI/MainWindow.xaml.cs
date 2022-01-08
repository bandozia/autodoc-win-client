using Andozias.Autodoc.Service.Managers;
using Autodoc.GUI.Controls;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Autodoc.GUI;

public partial class MainWindow : MetroWindow
{    
    public MainWindow(SettingsControl settingsControl, TessDataListControl tessDataListControl)
    {
        InitializeComponent();
                
        settingsFlyout.Content = settingsControl;
        tessDataListContent.Content = tessDataListControl;
    }
    
    private void OpenSettings_Click(object sender, RoutedEventArgs e) => settingsFlyout.IsOpen = true;
}

