using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgExtension.GUI.ViewModels;

public class SQLBoxViewModel:ViewModelBase
{
    public BindableReactiveProperty<string> Title { get; }
    public ICSharpCode.AvalonEdit.Document.TextDocument SQLDocument { get; }
    public BindableReactiveProperty<string> SQLText { get; }
    public SQLBoxViewModel(int index) : base()
    {
        this.Title = new BindableReactiveProperty<string>($"SQL-{index}");
        this.SQLDocument = new ICSharpCode.AvalonEdit.Document.TextDocument();
        this.SQLText = new BindableReactiveProperty<string>();
        this.SQLDocument.TextChanged += (sender, e) => this.SQLText.Value = this.SQLDocument.Text;
    }
}
