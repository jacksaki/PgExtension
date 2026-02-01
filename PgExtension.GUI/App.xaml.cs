using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PgExtension.GUI.Models;
using PgExtension.GUI.Services;
using PgExtension.GUI.ViewModels;
using PgExtension.GUI.Views;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows;
using System.Windows.Media;

namespace PgExtension.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly IHost _host = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!); })
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<ApplicationHostService>();
                services.AddSingleton<AppConfig>(AppConfig.Load());
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<SQLParentBox>();
                services.AddSingleton<SQLParentBoxViewModel>();
                services.AddTransient(typeof(Lazy<>), typeof(LazyResolver<>));
                services.AddSingleton<ColorSettingsBox>();
                services.AddSingleton<ColorSettingsBoxViewModel>();
            }).Build();

        internal FlowDirection InitialFlowDirection { get; set; }
        internal BaseTheme InitialTheme { get; set; }

        public static T? GetService<T>()
            where T : class
        {
            return _host.Services.GetService(typeof(T)) as T;
        }

        public static object? GetService(Type contentType)
        {
            return _host.Services.GetService(contentType);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var config = GetService<AppConfig>()!;
            //Timeline.DesiredFrameRateProperty.OverrideMetadata(
            //    typeof(Timeline), 
            //    new FrameworkPropertyMetadata { DefaultValue = config.FrameRate });

            this.InitialTheme = BaseTheme.Inherit;
            this.InitialFlowDirection = FlowDirection.LeftToRight;
            InitTheme();
            _host.Start();
        }

        private void InitTheme()
        {
            var conf = App.GetService<AppConfig>()!;
            var paletteHelper = new PaletteHelper();
            var themeConf = conf.Theme;
            if (themeConf == null)
            {
                return;
            }

            Theme theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(themeConf.IsDarkTheme ? BaseTheme.Dark : BaseTheme.Light);
            if (themeConf.PrimaryColor.HasValue)
            {
                paletteHelper.ChangePrimaryColor(themeConf.PrimaryColor.Value);
            }
            if (themeConf.SecondaryColor.HasValue)
            {
                paletteHelper.ChangeSecondaryColor(themeConf.SecondaryColor.Value);
            }
            paletteHelper.SetTheme(theme);
        }

        private async void Application_Exit(object sender, ExitEventArgs e)
        {
            var paletteHelper = new PaletteHelper();
            paletteHelper.GetTheme();
            //per();
            var config = App.GetService<AppConfig>()!;
            config.SetTheme(paletteHelper.GetConfig());
            config.Save();
            await _host.StopAsync();

            _host.Dispose();
        }
    }
}