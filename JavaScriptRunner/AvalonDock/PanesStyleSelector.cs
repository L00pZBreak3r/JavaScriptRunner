/************************************************************************

   AvalonDock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the New BSD
   License (BSD) as published at http://avalondock.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up AvalonDock in Extended WPF Toolkit Plus at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like facebook.com/datagrids

  **********************************************************************/

using System.Windows.Controls;
using System.Windows;

using JavaScriptRunner.ViewModels;

namespace JavaScriptRunner.AvalonDock
{
    internal class PanesStyleSelector : StyleSelector
    {
        public Style FileStyle
        {
            get;
            set;
        }

        public Style ToolStyle
        {
            get;
            set;
        }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is ScriptFileViewModel)
                return FileStyle;

            if (item is ToolViewModel)
                return ToolStyle;

            return base.SelectStyle(item, container);
        }
    }
}
