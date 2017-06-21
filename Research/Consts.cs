
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
    /// Anzahl der Felder, welche übereinander in einer Reihe liegen können
    /// </summary>
    const int RowHeight = 6;
  }
}
