using System;
using System.Linq;

namespace Research
{
  static partial class Program
  {
    /// <summary>
    /// Prüfmethode zum zählen, wieviel Varianten pro Spalte möglich sind
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    static void TestRowCounter()
    {
      int count = 0;
      foreach (var row in RowCounterBase())
      {
        Console.WriteLine("{0,3} : \"{1}\"", count, row);
        count++;
      }
      Console.WriteLine();
      Console.WriteLine("Total-Count: " + count);
    }
  }
}
