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
using Xceed.Wpf.AvalonDock.Layout;

using JavaScriptRunner.ViewModels;

namespace JavaScriptRunner.AvalonDock
{
    internal class PanesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FileViewTemplate
        {
            get;
            set;
        }

        public DataTemplate ToolViewTemplate
        {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var itemAsLayoutContent = item as LayoutContent;

            if (item is ScriptFileViewModel)
                return FileViewTemplate;

            if (item is ToolWindowViewModel)
                return ToolViewTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
