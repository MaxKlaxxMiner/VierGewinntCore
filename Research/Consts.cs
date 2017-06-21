
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
    /// Anzahl der Spalten
    /// </summary>
    const int RowCount = 7;

    /// <summary>
    /// Anzahl der Felder, welche übereinander in einer Reihe liegen können
    /// </summary>
    const int RowHeight = 6;

    /// <summary>
    /// maximale Bits, welche pro Spalte benutzt werden können (Default: 9 Bits)
    /// </summary>
    const int MaxBitsPerRow = sizeof(ulong) * 8 / RowCount;

    /// <summary>
    /// höchste ID, welche eine Spalte annehmen kann
    /// </summary>
    const int MaxRowId = 1 << MaxBitsPerRow;
  }
}
