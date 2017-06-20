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
    static readonly string[] Rows = RowCounterBase(RowHeight).ToArray();

    /// <summary>
    /// prüft, ob eine Spalte im aktiven Spielfeld gültig ist 
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    static bool ValidRow(string row)
    {
      return !row.Contains("xxxx") && !row.Contains("oooo");
    }

    /// <summary>
    /// Methode zum analysieren und prüfen, ob bestimmte Felder direkt mit Bit-Mustern abgefragt werden können
    /// </summary>
    static void BitChecker()
    {
      var xMap = Enumerable.Range(0, RowHeight).Select(i => Rows.Where(r => r[RowHeight - 1 - i] == 'x').ToArray()).ToArray();
      var oMap = Enumerable.Range(0, RowHeight).Select(i => Rows.Where(r => r[RowHeight - 1 - i] == 'o').ToArray()).ToArray();

      int stop = 0;
    }

    static void Main(string[] args)
    {
      // RowCounter();

      BitChecker();

      Console.ReadLine();
    }
  }
}
