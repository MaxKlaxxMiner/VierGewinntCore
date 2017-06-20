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
    /// 127 Varianten, die in einer Senkrechten Reihe vorkommen können (6 Felder hoch)
    /// </summary>
    static readonly string[] Rows = RowCounterBase(6).ToArray();

    /// <summary>
    /// Methode zum analysieren und prüfen, ob bestimmte Felder direkt mit Bit-Mustern abgefragt werden können
    /// </summary>
    static void BitChecker()
    {

    }

    static void Main(string[] args)
    {
      // RowCounter();

      BitChecker();

      Console.ReadLine();
    }
  }
}
