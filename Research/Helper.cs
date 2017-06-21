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
    /// prüft, ob ein bestimmtes Ergebnis mit "einer" bestimmten Berechnung erhalten werden kann
    /// </summary>
    /// <param name="id">Quell-ID, welche geprüft werden soll</param>
    /// <param name="calc1">Berechnungsfunktion</param>
    /// <param name="val1">der zu berechnende Wert</param>
    /// <param name="result">das zu erwartende Ergebnis</param>
    /// <returns>true, wenn dasa Ergebnis gepasst hat</returns>
    static bool Compare1(int id, Calc calc1, int val1, int result)
    {
      switch (calc1)
      {
        case Calc.And: return (id & val1) == result;
        case Calc.Or: return (id | val1) == result;
        case Calc.Xor: return (id ^ val1) == result;
        default: throw new NotSupportedException();
      }
    }
  }
}
