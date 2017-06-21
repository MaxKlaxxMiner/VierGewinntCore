using System;
using System.Collections.Generic;
using System.Linq;

namespace Research
{
  static partial class Program
  {
    static IEnumerable<string> RowCounterBase(int len)
    {
      var cc = new byte[len];
      var chars = new char[len];
      for (; ; )
      {
        for (int i = 0; i < chars.Length; i++) chars[i] = FieldChars[cc[i]];
        yield return new string(chars);
        int p = cc.Length - 1;
        while (++cc[p] == FieldChars.Length)
        {
          cc[p] = 1;
          p--;
          if (p < 0) yield break;
        }
      }
    }

    /// <summary>
    /// Prüfmethode zum zählen, wieviel Varianten pro Spalte möglich sind
    /// </summary>
    static void RowCounter()
    {
      int count = 0;
      foreach (var row in RowCounterBase(6).Where(ValidRow))
      {
        Console.WriteLine("{0,3} : \"{1}\"", count, row);
        count++;
      }
      Console.WriteLine();
      Console.WriteLine("Total-Count: " + count);
    }
  }
}
