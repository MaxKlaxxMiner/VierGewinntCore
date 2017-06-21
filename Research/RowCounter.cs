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

    static int GetRowId(string row)
    {
      int lenBits = row.LastIndexOf(' ') + 2;
      int dataBits = 0;

      for (int b = 0; b < row.Length; b++)
      {
        if (row[b] == 'x') dataBits |= 1 << (row.Length - b - 1);
      }

      for (int len = 0; len < lenBits; len++)
      {
        dataBits |= 1 << (row.Length - len + 1);
      }

      if ((row.Trim() + "x").First() == 'x') dataBits -= 2 << row.Length;

      return dataBits;
    }

    /// <summary>
    /// Prüfmethode zum zählen, wieviel Varianten pro Spalte möglich sind
    /// </summary>
    static void RowCounter()
    {
      int count = 0;
      foreach (var row in RowCounterBase(6).Where(ValidRow))
      {
        Console.WriteLine("{0,3} : \"{1}\"  {2}", count, row, Convert.ToString(GetRowId(row), 2).PadLeft(row.Length + 2, '0'));
        count++;
      }
      Console.WriteLine();
      Console.WriteLine("Total-Count: " + count);
    }
  }
}
