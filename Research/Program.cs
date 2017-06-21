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
    /// mögliche Spielfelder
    /// ' ' = frei
    /// 'x' = Spieler 1
    /// 'o' = Spieler 2
    /// </summary>
    static readonly char[] FieldChars = { ' ', 'x', 'o' };
    /// <summary>
    /// Anzahl der Felder, welche übeinander in einer Reihe liegen können
    /// </summary>
    const int RowHeight = 6;
    /// <summary>
    /// 127 Varianten, die in einer Senkrechten Reihe vorkommen können (6 Felder hoch)
    /// </summary>
    static readonly string[] AllRows = RowCounterBase(RowHeight).ToArray();

    /// <summary>
    /// prüft, ob eine Spalte im aktiven Spielfeld gültig ist (bereits gewonnen Spiele sollten nicht vorkommen)
    /// </summary>
    /// <param name="row">Spalte, welche geprüft werden soll</param>
    /// <returns>true wenn die Spalte gültig ist</returns>
    static bool ValidRow(string row)
    {
      return !row.Contains("xxxx") && !row.Contains("oooo");
    }

    /// <summary>
    /// Methode zum analysieren und prüfen, ob bestimmte Felder direkt mit Bit-Mustern abgefragt werden können
    /// </summary>
    static void BitChecker()
    {
      var rowDict = AllRows.ToDictionary(r => r, GetRowId);

      var xMap = Enumerable.Range(0, RowHeight).Select(i => new { rows = AllRows.Where(r => r[RowHeight - 1 - i] == 'x' && ValidRow(r)).Select(r => new{row = r,id = rowDict[r]}).ToArray(), keys = new List<KeyValuePair<int, int>>() } ).ToArray();
      var oMap = Enumerable.Range(0, RowHeight).Select(i => new { rows = AllRows.Where(r => r[RowHeight - 1 - i] == 'o' && ValidRow(r)).Select(r => new{row = r,id = rowDict[r]}).ToArray(), keys = new List<KeyValuePair<int, int>>() } ).ToArray();

      for (int k = 1; k < 256; k++)
      {
        for (int v = 0; v < 256; v++)
        {
          foreach (var map in xMap)
          {
            if (map.rows.All(m => (m.id & k) == v)) map.keys.Add(new KeyValuePair<int, int>(k, v));
          }

          foreach (var map in oMap)
          {
            if (map.rows.All(m => (m.id & k) == v)) map.keys.Add(new KeyValuePair<int, int>(k, v));
          }
        }
      }

      int stop = 0;
    }

    static bool Compare1(int src, Calc calc1, int val1, int result)
    {
      switch (calc1)
      {
        case Calc.And: return (src & val1) == result;
        case Calc.Or: return (src | val1) == result;
        case Calc.Xor: return (src ^ val1) == result;
        default: throw new NotSupportedException();
      }
    }

    static void BruteBitScan()
    {

    }

    static void Main(string[] args)
    {
      // RowCounter();

      // BitChecker();

      Console.ReadLine();
    }
  }
}
