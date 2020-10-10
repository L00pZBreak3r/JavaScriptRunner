using System.Globalization;
using System.IO;
using System.Windows;
using Microsoft.Win32;

using JavaScriptRunner.Helpers;
using JavaScriptRunner.AvalonDock;

namespace JavaScriptRunner.ViewModels
{
  internal class ToolWindowViewModel : ToolViewModel, OutputConsole
  {
    public ToolWindowViewModel(string title, bool isVisible)
      : base(title, isVisible)
    {

    }

    #region Text

    private string mText;

    public string Text
    {
      get { return mText; }
      set
      {
        if (mText != value)
        {
          mText = value;
          RaisePropertyChanged();
        }
      }
    }

    #endregion

    public void WriteString(string s)
    {
      Text += s;
    }

    public void Clear()
    {
      Text = "";
    }
  }
}
