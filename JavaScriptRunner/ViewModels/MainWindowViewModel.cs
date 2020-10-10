using System;
using System.Windows;
using System.Collections.ObjectModel;

using JavaScriptRunner.Helpers;

namespace JavaScriptRunner.ViewModels
{
  internal class MainWindowViewModel : ViewModelBase
  {
    public const string PROGRAM_CAPTION = "Java script runner";

    public MainWindowViewModel()
    {
      ToolWindows.Add(new ToolWindowViewModel("Консоль вывода", false));
      SetWindowText();
    }

    #region OpenDocuments

    private readonly ObservableCollection<ScriptFileViewModel> mOpenDocuments = new ObservableCollection<ScriptFileViewModel>();

    public ObservableCollection<ScriptFileViewModel> OpenDocuments
    {
      get { return mOpenDocuments; }
    }

    #endregion

    #region ToolWindows

    private readonly ObservableCollection<ToolWindowViewModel> mToolWindows = new ObservableCollection<ToolWindowViewModel>();

    public ObservableCollection<ToolWindowViewModel> ToolWindows
    {
      get { return mToolWindows; }
    }

    #endregion

    #region ActiveDocument

    private ScriptFileViewModel mActiveDocument;

    public ScriptFileViewModel ActiveDocument
    {
      get { return mActiveDocument; }
      set
      {
        if (mActiveDocument != value)
        {
          mActiveDocument = value;
          RaisePropertyChanged();
          RaisePropertyChanged("IsDocumentOpen");
          SetWindowText();
        }
      }
    }

    #endregion

    #region IsDocumentOpen

    //private bool mIsDocumentOpen;

    public bool IsDocumentOpen
    {
      get { return mActiveDocument != null; }
      /*set
      {
        if (mIsDocumentOpen != value)
        {
          mIsDocumentOpen = value;
          RaisePropertyChanged();
        }
      }*/
    }

    #endregion
    
    #region WindowText

    private string mWindowText;

    public string WindowText
    {
      get { return mWindowText; }
      set
      {
        if (mWindowText != value)
        {
          mWindowText = value;
          RaisePropertyChanged();
        }
      }
    }

    #endregion
    
    #region ObjectName

    private string mObjectName;

    public string ObjectName
    {
      get { return mObjectName; }
      set
      {
        if (mObjectName != value)
        {
          mObjectName = value;
          RaisePropertyChanged();
        }
      }
    }

    #endregion
    
    #region ConnectionDate

    private DateTime mConnectionDate;

    public DateTime ConnectionDate
    {
      get { return mConnectionDate; }
      set
      {
        if (mConnectionDate != value)
        {
          mConnectionDate = value;
          RaisePropertyChanged();
        }
      }
    }

    #endregion
    
    #region ShowOutputWindow

    //private bool mShowOutputWindow;

    public bool ShowOutputWindow
    {
      get { return ToolWindows[0].IsVisible; }
      set
      {
        if (ToolWindows[0].IsVisible != value)
        {
          ToolWindows[0].IsVisible = value;
          RaisePropertyChanged();
        }
      }
    }

    #endregion
    
    #region ShowInternalMessages

    private bool mShowInternalMessages;

    public bool ShowInternalMessages
    {
      get { return mShowInternalMessages; }
      set
      {
        if (mShowInternalMessages != value)
        {
          mShowInternalMessages = value;
          RaisePropertyChanged();
        }
      }
    }

    #endregion

    private void FileViewModel_WindowClosed(object sender, EventArgs e)
    {
      var m = sender as ScriptFileViewModel;
      if (m != null)
      {
        mOpenDocuments.Remove(m);
      }
      if (mOpenDocuments.Count <= 0)
        ActiveDocument = null;
      m?.Dispose();
    }

    private void FileViewModel_RunComplete(object sender, EventArgs e)
    {
      SetWindowText();
    }

    private void SetWindowText(bool bRunning = false)
    {
      string winText = PROGRAM_CAPTION;
      string objName = null;
      if (!string.IsNullOrEmpty(mActiveDocument?.Title))
      {
        objName = mActiveDocument.Title;
        winText += " - " + objName;
      }
      if (!string.IsNullOrEmpty(objName) && bRunning)
        objName += " - Running...";
      WindowText = winText;
      ObjectName = objName;
      ConnectionDate = DateTime.Now;
    }
    
    public void NewFile()
    {
      ScriptFileViewModel cf = new ScriptFileViewModel(ToolWindows[0]);
      cf.WindowClosed += FileViewModel_WindowClosed;
      cf.RunComplete += FileViewModel_RunComplete;
      mOpenDocuments.Add(cf);
    }

    public void OpenFile()
    {
      ScriptFileViewModel cf = new ScriptFileViewModel(ToolWindows[0]);
      cf.OpenFile();
      string s = cf.Text;
      if (!string.IsNullOrEmpty(s))
      {
        cf.WindowClosed += FileViewModel_WindowClosed;
        cf.RunComplete += FileViewModel_RunComplete;
        mOpenDocuments.Add(cf);
      }
    }

    public void SaveFile(bool saveAs = false)
    {
      if (mActiveDocument != null)
      {
        mActiveDocument.SaveFile(saveAs);
        SetWindowText();
      }
    }

    public void RunFile(bool doDebug = false)
    {
      if (mActiveDocument != null)
      {
        mActiveDocument.RunFile(doDebug);
        SetWindowText(true);
      }
    }

    public void ClearConsole()
    {
      ToolWindows[0].Clear();
    }
  }
}
