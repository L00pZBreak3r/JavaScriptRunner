using System.Windows;

using JavaScriptRunner.ViewModels;

namespace JavaScriptRunner
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      DataContext = new MainWindowViewModel();
      InitializeComponent();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }

    private void OpenFileButton_Click(object sender, RoutedEventArgs e)
    {
      var m = DataContext as MainWindowViewModel;
      if (m != null)
        m.OpenFile();
    }
    
    private void NewFileButton_Click(object sender, RoutedEventArgs e)
    {
      var m = DataContext as MainWindowViewModel;
      if (m != null)
        m.NewFile();
    }

    private void RunButton_Click(object sender, RoutedEventArgs e)
    {
      var m = DataContext as MainWindowViewModel;
      if (m != null)
        m.RunFile();
    }

    private void DebugButton_Click(object sender, RoutedEventArgs e)
    {
      var m = DataContext as MainWindowViewModel;
      if (m != null)
        m.RunFile(true);
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
      var m = DataContext as MainWindowViewModel;
      if (m != null)
        m.ClearConsole();
    }
  }
}
