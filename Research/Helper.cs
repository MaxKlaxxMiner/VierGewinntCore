using System;
using System.Collections.Generic;

namespace Research
{
  static partial class Program
  {
    /// <summary>
    /// generiert alle Basis-Varianten, welche pro Spalte vorkommen können
    /// </summary>
    /// <param name="height">Höhe der Spalte (Default = 6)</param>
    /// <returns>genertierte Spalten</returns>
    static IEnumerable<string> RowCounterBase(int height = RowHeight)
    {
      var cc = new byte[height];
      var chars = new char[height];
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
    /// prüft, ob eine Spalte im aktiven Spielfeld gültig ist (bereits gewonnen Spiele sollten nicht vorkommen)
    /// </summary>
    /// <param name="row">Spalte, welche geprüft werden soll</param>
    /// <returns>true wenn die Spalte gültig ist</returns>
    static bool ValidRow(string row)
    {
      return !row.Contains("xxxx") && !row.Contains("oooo");
    }

    /// <summary>
    /// berechnet eine bestimmte ID mit einer bestimmten Funktion aus und gibt das Ergebnis zurück
    /// </summary>
    /// <param name="id">Quell-ID, welche berechnet werden soll</param>
    /// <param name="calc1">Berechnungsfunktion</param>
    /// <param name="val1">der zu berechnende Wert</param>
    /// <returns>das entsprechende Ergebnis</returns>
    static int CalcRowId(int id, Calc calc1, int val1)
    {
      switch (calc1)
      {
        case Calc.And: return id & val1;
        case Calc.Or: return id | val1;
        case Calc.Xor: return id ^ val1;
        default: throw new NotSupportedException();
      }
    }
  }
}
