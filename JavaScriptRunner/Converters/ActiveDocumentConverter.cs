using System;
using System.Globalization;
using System.Windows.Data;

using JavaScriptRunner.ViewModels;
using JavaScriptRunner.AvalonDock;

namespace JavaScriptRunner.Converters
{
  [ValueConversion(typeof(ScriptFileViewModel), typeof(PaneViewModel))]
  internal sealed class ActiveDocumentConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is ScriptFileViewModel)
        return value;

      return Binding.DoNothing;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is ScriptFileViewModel)
        return value;

      return Binding.DoNothing;
    }
  }
}
