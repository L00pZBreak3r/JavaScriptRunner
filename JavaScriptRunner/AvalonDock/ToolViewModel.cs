/************************************************************************

   AvalonDock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the New BSD
   License (BSD) as published at http://avalondock.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up AvalonDock in Extended WPF Toolkit Plus at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like facebook.com/datagrids

  **********************************************************************/

namespace JavaScriptRunner.AvalonDock
{
  internal class ToolViewModel : PaneViewModel
  {
    public ToolViewModel(string title, bool isVisible)
    {
      Title = ContentId = title;
      _isVisible = isVisible;
    }

    #region IsVisible

    private bool _isVisible;
    public bool IsVisible
    {
      get { return _isVisible; }
      set
      {
        if (_isVisible != value)
        {
          _isVisible = value;
          RaisePropertyChanged();
        }
      }
    }

    #endregion
  }
}
