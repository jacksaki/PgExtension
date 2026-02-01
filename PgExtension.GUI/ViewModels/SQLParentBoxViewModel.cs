using ObservableCollections;
using R3;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgExtension.GUI.ViewModels;

public class SQLParentBoxViewModel:BoxViewModelBase
{
    public ObservableCollection<SQLBoxViewModel> SQL { get; }
    public ReactiveCommand AddCommand { get; }
    public ReactiveCommand RemoveSelectedCommand { get; }
    public BindableReactiveProperty<SQLBoxViewModel> SelectedSQL { get; }
    private int _index;
    public SQLParentBoxViewModel() : base()
    {
        this.SQL=new ObservableCollection<SQLBoxViewModel>();
        this.SelectedSQL = new BindableReactiveProperty<SQLBoxViewModel>();
        this.AddCommand = new ReactiveCommand();
        this.RemoveSelectedCommand = this.SelectedSQL.Select(x => x != null).ToReactiveCommand();
        this.AddCommand.Subscribe(_ =>
        {
            _index++;
            var sql = new SQLBoxViewModel(_index);
            this.SQL.Add(sql);
            this.SelectedSQL.Value = sql;
            RaisePropertyChanged(nameof(SQL));
        });
        this.RemoveSelectedCommand.Subscribe(_ =>
        {
            var index = this.SQL.IndexOf(this.SelectedSQL.Value);
            this.SQL.Remove(this.SelectedSQL.Value);
            RaisePropertyChanged(nameof(SQL));
        });
    }
}
