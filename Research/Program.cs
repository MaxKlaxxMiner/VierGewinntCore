using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Research
{
  static partial class Program
  {
    /// <summary>
    /// 127 Varianten, die in einer Senkrechten Reihe vorkommen können (6 Felder hoch)
    /// </summary>
    static readonly string[] AllRows = RowCounterBase().ToArray();
    /// <summary>
    /// 103 Varianten, die in einer Senkrechten Reihe ohne direkten Gewinn vorkommen können (6 Felder hoch)
    /// </summary>
    static readonly string[] AllValidRows = AllRows.Where(ValidRow).ToArray();

    static readonly string[][] XMap = Enumerable.Range(0, RowHeight).Select(i => AllRows.Where(r => r[RowHeight - 1 - i] == 'x' && ValidRow(r)).ToArray()).ToArray();
    static readonly string[][] OMap = Enumerable.Range(0, RowHeight).Select(i => AllRows.Where(r => r[RowHeight - 1 - i] == 'o' && ValidRow(r)).ToArray()).ToArray();

    struct Solution
    {
      public int id;
      public int result;
    }

    static void BruteBitScan1(Calc calc1, int val1)
    {
      var sln = new Solution[AllRows.Length];
      int slnCount = 0;

      int id = 0;
      int result = 0;
      if (Compare1(id, calc1, val1, result))
      {
        
      }
    }

    static void BruteBitScans()
    {
      BruteBitScan1(Calc.And, 0);
    }

    static void Main(string[] args)
    {
      // TestRowCounter();

      BruteBitScans();

      Console.ReadLine();
    }
  }
}
