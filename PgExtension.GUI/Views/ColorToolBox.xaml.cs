using PgExtension.GUI.ViewModels;

namespace PgExtension.GUI.Views
{
    public partial class ColorToolBox
    {
        public ColorToolBox()
        {
            InitializeComponent();
            this.DataContext = App.GetService<ColorToolBoxViewModel>();
        }
    }
}
