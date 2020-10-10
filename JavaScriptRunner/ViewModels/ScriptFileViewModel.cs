using System;
using System.IO;
using System.ComponentModel;
using Microsoft.Win32;
using System.Threading;

using Jint;

using JavaScriptRunner.Helpers;
using JavaScriptRunner.AvalonDock;

namespace JavaScriptRunner.ViewModels
{
  internal class ScriptFileViewModel : PaneViewModel, IDisposable
  {
    private readonly OutputConsole mOutputConsole;

    private readonly BackgroundWorker mWorker;

    private volatile bool mDebug;
    private volatile bool mDebugWait;
    private volatile bool mDoStep;

    public event EventHandler RunComplete;

    public ScriptFileViewModel(OutputConsole outputConsole)
    {
      mOutputConsole = outputConsole;
      mWorker = new BackgroundWorker();
      mWorker.DoWork += worker_DoWork;
      mWorker.RunWorkerCompleted += worker_RunWorkerCompleted;
    }

    #region FilePath

    private string mFilePath;

    public string FilePath
    {
      get { return mFilePath; }
      set
      {
        if (mFilePath != value)
        {
          mFilePath = value;
          RaisePropertyChanged();
        }
      }
    }

    #endregion

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

    public void OpenFile()
    {
      var openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "Script files|*.script";

      var dr = openFileDialog.ShowDialog();

      if (dr.HasValue && dr.Value)
      {
        Text = File.ReadAllText(openFileDialog.FileName);
        FilePath = openFileDialog.FileName;
        Title = Path.GetFileNameWithoutExtension(FilePath);
      }
    }

    public void SaveFile(bool saveAs = false)
    {
      if (!string.IsNullOrWhiteSpace(mText))
      {
        string filePath = mFilePath;
        if (saveAs)
          filePath = null;
        if (string.IsNullOrWhiteSpace(filePath))
        {
          var saveFileDialog = new SaveFileDialog();
          saveFileDialog.Filter = "Script files|*.script";
          saveFileDialog.DefaultExt = ".script";

          var dr = saveFileDialog.ShowDialog();

          if (dr.HasValue && dr.Value)
            filePath = saveFileDialog.FileName;
        }
        if (!string.IsNullOrWhiteSpace(filePath))
        {
          FilePath = filePath;
          Title = Path.GetFileNameWithoutExtension(FilePath);

          File.WriteAllText(mFilePath, mText);
        }
      }
    }

    public void RunFile(bool doDebug = false)
    {
      if (mDebug)
      {
        mDoStep = true;
        mDebugWait = doDebug;
      }
      else if ((mOutputConsole != null) && !string.IsNullOrWhiteSpace(mText))
      {
        if (!mWorker.IsBusy)
        {
          mDebug = doDebug;
          mDebugWait = doDebug;
          mWorker.RunWorkerAsync();
        }
      }
    }

    private Jint.Runtime.Debugger.StepMode C_Step(object sender, Jint.Runtime.Debugger.DebugInformation e)
    {
      if (mOutputConsole != null)
        mOutputConsole.WriteString(string.Format("Line {1}: {0}\n", e.CurrentStatement.ToString(), e.CurrentStatement.Location.Start.Line));
      while (mDebugWait && !mDoStep)
        Thread.Sleep(200);
      mDoStep = false;
      return Jint.Runtime.Debugger.StepMode.Over;
    }

    private void worker_DoWork(object sender, DoWorkEventArgs e)
    {
      // run all background tasks here
      if ((mOutputConsole != null) && !string.IsNullOrWhiteSpace(mText))
      {
        var engine = new Engine((cfg) => { cfg.AllowClr(); cfg.DebugMode(mDebug); });
        var c = engine.SetValue("inputtap", new Action<string>(mOutputConsole.WriteString));

        if (mDebug)
        {
          c.Step += C_Step;
        }
        c.Execute(mText);
      }
    }

    private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      //update ui once worker complete his work
      mDebug = false;
      mDebugWait = false;
      mDoStep = false;
      RunComplete?.Invoke(this, EventArgs.Empty);
      System.Windows.MessageBox.Show("Расчет окончен!", MainWindowViewModel.PROGRAM_CAPTION, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
    }

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          // TODO: dispose managed state (managed objects).
          if (mWorker != null)
            mWorker.Dispose();
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.

        disposedValue = true;
      }
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~VectorChartViewModel() {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      // GC.SuppressFinalize(this);
    }
    #endregion
  }
}
