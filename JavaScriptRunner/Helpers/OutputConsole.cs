using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaScriptRunner.Helpers
{
  internal interface OutputConsole
  {
    void WriteString(string s);
    void Clear();
  }
}
