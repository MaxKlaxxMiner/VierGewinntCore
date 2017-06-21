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
      public readonly int id;
      public readonly int result;
      public Solution(int id, int result)
      {
        this.id = id;
        this.result = result;
      }
    }

    static void BruteBitScan1Rec(Calc calc1, int val1, Solution[] sln, int slnCount)
    {
      for (int id = 0; id < MaxRowId; id++)
      {
        int result = CalcRowId(id, calc1, val1);
        sln[slnCount] = new Solution(id, result);
        slnCount++;
      }
    }

    static void BruteBitScan1(Calc calc1, int val1)
    {
      var sln = new Solution[AllRows.Length];

      BruteBitScan1Rec(calc1, val1, sln, 0);
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
