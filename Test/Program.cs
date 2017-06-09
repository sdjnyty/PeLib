using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTY.PeLib
{
  class Program
  {
    static void Main(string[] args)
    {
      var pe = new PeFile();
      pe.Load(@"c:\windows\notepad.exe");
      Console.ReadKey();
    }
  }
}
