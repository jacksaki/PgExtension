using PgExtension.GUI.ViewModels;

namespace PgExtension.GUI.Views
{
    public partial class ThemeSettingsBox
    {
        public ThemeSettingsBox()
        {
            InitializeComponent();
            DataContext = App.GetService<ThemeSettingsViewModel>();
        }
    }
}
