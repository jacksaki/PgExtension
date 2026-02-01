using PgExtension.GUI.ViewModels;
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

namespace PgExtension.GUI.Views
{
    /// <summary>
    /// SQLParentBox.xaml の相互作用ロジック
    /// </summary>
    public partial class SQLParentBox : UserControl
    {
        public SQLParentBox()
        {
            InitializeComponent();
            this.DataContext = App.GetService<SQLParentBoxViewModel>();
        }
    }
}
