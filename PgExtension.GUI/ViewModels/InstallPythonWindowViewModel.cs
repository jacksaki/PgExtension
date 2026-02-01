using MahApps.Metro.Controls.Dialogs;
using ObservableCollections;
using R3;
using SQLFormatter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PgExtension.GUI.ViewModels;

public class InstallPythonWindowViewModel:ViewModelBase
{
    public IDialogCoordinator? DialogCoordinator
    {
        get;
        set;
    }

    public BindableReactiveProperty<string> Title { get; }
    public ICollectionView LogsView { get; }
    public LibraryInstaller Installer { get; }
    public ObservableCollection<string> Logs { get; }
    public InstallPythonWindowViewModel() : base()
    {
        this.Logs = new ObservableCollection<string>();
        this.Installer = new LibraryInstaller();
        this.Installer.Logged += (sender, e) =>
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                this.Logs.Insert(0, $"{e.Date:HH:mm:ss}\t{e.Type}\t{e.Message}");
                RaisePropertyChanged(nameof(Logs));
                RaisePropertyChanged(nameof(LogsView));
            });
        };

        this.DialogCoordinator = MahApps.Metro.Controls.Dialogs.DialogCoordinator.Instance;
        this.Title = new BindableReactiveProperty<string>();
        this.LogsView = CollectionViewSource.GetDefaultView(this.Logs);
    }

    public async Task InstallAsync()
    {
        try
        {
            this.Installer.Title.Subscribe(x => this.Title.Value = x);

            await this.Installer.InstallAsync();
        }
        catch (Exception ex)
        {
            await DialogCoordinator.ShowMessageAsync(this, "エラー", ex.Message, MessageDialogStyle.Affirmative);
        }
    }
}
