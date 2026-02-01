using Cysharp.Diagnostics;
using R3;
using SQLFormatter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PgExtension.GUI.ViewModels;

public class SQLBoxViewModel:BoxViewModelBase
{
    public BindableReactiveProperty<string> Title { get; }
    public ICSharpCode.AvalonEdit.Document.TextDocument SQLDocument { get; }
    public BindableReactiveProperty<string> SQLText { get; }
    public ReactiveCommand ExecuteCommand { get; }
    public ICollectionView LogsView { get; }
    public BindableReactiveProperty<string> LogText { get; }
    public ReactiveCommand BeautifyCommand { get; }
    public ObservableCollection<LogItem> Logs { get; }
    public SQLBoxViewModel(int index) : base()
    {
        ProcessX.AcceptableExitCodes = new List<int>() { 0, 1 }.AsReadOnly();
        this.Logs = new ObservableCollection<LogItem>();
        this.LogsView = CollectionViewSource.GetDefaultView(this.Logs);
        this.LogText = new BindableReactiveProperty<string>();
        this.Title = new BindableReactiveProperty<string>($"SQL-{index}");
        this.SQLDocument = new ICSharpCode.AvalonEdit.Document.TextDocument();
        this.SQLText = new BindableReactiveProperty<string>();
        this.SQLDocument.TextChanged += (sender, e) => this.SQLText.Value = this.SQLDocument.Text;
        this.ExecuteCommand = this.SQLText.Select(x => !string.IsNullOrEmpty(x)).ToReactiveCommand();
        this.BeautifyCommand = this.SQLText.Select(x => !string.IsNullOrEmpty(x)).ToReactiveCommand();
        this.BeautifyCommand.SubscribeAwait(async (x, ct) =>
        {
            var formatter = new SQLFluffFormatter();
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                formatter.Logged += async (sender, e) =>
                {
                    await App.Current.Dispatcher.InvokeAsync(() =>
                    {
                        this.Logs.Add(e);
                    });
                };
                try
                {
                    await formatter.ExecuteAsync(this.SQLText.Value);
                    Clipboard.SetText(string.Join("\r\n", this.Logs.Where(x => x.Type == InstallLogItemType.Output).Select(x => x.Message)));
                    OnSnackBarMessage(new Models.SnackBarMessageEventArgs("コピーしました"));
                }
                catch (Exception ex)
                {
                    OnErrorOccurred(new Models.ErrorOccurredEventArgs("エラー", ex.Message));
                }
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        });
    }
}
