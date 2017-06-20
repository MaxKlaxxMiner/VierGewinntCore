using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Research
{
  static partial class Program
  {
    static readonly char[] FieldChars = { ' ', 'x', 'o' };

    static void Main(string[] args)
    {
      RowCounter();
      Console.ReadLine();
    }
  }
}
