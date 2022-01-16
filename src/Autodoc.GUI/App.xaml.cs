using Andozias.Autodoc.Service.Managers;
using Andozias.Autodoc.Service.Ocr;
using Autodoc.GUI.Controls;
using Autodoc.GUI.View;
using Autodoc.GUI.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace Autodoc.GUI;

public partial class App : Application
{
    private readonly IServiceProvider serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton<HttpClient>();
        services.AddScoped<IRemoteManager, RemoteManager>();
        services.AddScoped<ILocalFileManager, LocalFileManager>();
        services.AddSingleton<ITesseractService, TesseractService>();
        services.AddSingleton<IOcrService, OcrService>();

        services.AddSingleton<MainWindow>();
                
        services.AddSingleton<SettingsViewModel>();
        services.AddSingleton<SettingsControl>();

        services.AddSingleton<TessDataListViewModel>();
        services.AddSingleton<TessDataListControl>();

        services.AddScoped<AddFilesViewModel>();
        services.AddScoped<AddFilesControl>();
                        
        services.AddScoped<DecodeViewModel>();
        services.AddScoped<DecodeView>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}

