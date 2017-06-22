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

    static readonly string[][] XMap = Enumerable.Range(0, RowHeight).Select(i => AllValidRows.Where(r => r[RowHeight - 1 - i] == 'x').ToArray()).ToArray();
    static readonly string[][] OMap = Enumerable.Range(0, RowHeight).Select(i => AllValidRows.Where(r => r[RowHeight - 1 - i] == 'o').ToArray()).ToArray();

    public struct MapElement
    {
      public readonly int[] trueValues;
      public readonly int[] falseValues;
      public readonly char player;
      public readonly int playerPos;
      public MapElement(char player, int playerPos)
      {
        this.player = player;
        this.playerPos = playerPos;
        trueValues = AllValidRows.Select((r, i) => new { r, i }).Where(r => r.r[RowHeight - 1 - playerPos] == player).Select(r => r.i).ToArray();
        falseValues = AllValidRows.Select((r, i) => new { r, i }).Where(r => r.r[RowHeight - 1 - playerPos] != player).Select(r => r.i).ToArray();
      }
      public override string ToString()
      {
        return (new { player, playerPos, trueValues = string.Join(",", trueValues.Select(i => AllValidRows[i])), falseValues = "int[" + falseValues.Length + "]" }).ToString();
      }
    }

    static readonly MapElement[] XMap2 = Enumerable.Range(0, RowHeight).Select(i => new MapElement('x', i)).ToArray();
    static readonly MapElement[] OMap2 = Enumerable.Range(0, RowHeight).Select(i => new MapElement('o', i)).ToArray();

    static void FullRowScan()
    {
      for (int i = 0; i < RowHeight; i++)
      {
        Console.WriteLine(XMap2[i]);
        Console.WriteLine();
      }
      for (int i = 0; i < RowHeight; i++)
      {
        Console.WriteLine(OMap2[i]);
        Console.WriteLine();
      }
    }


    static void Main(string[] args)
    {
      // TestRowCounter();

      FullRowScan();

      Console.ReadLine();
    }
  }
}
