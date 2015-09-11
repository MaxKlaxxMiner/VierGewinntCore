#region # using *.*

using System;
using System.Linq;
using System.Text;
// ReSharper disable MemberCanBePrivate.Global

#endregion

namespace VierGewinntCore
{
  /// <summary>
  /// merkt sich ein 4-Gewinnt Spielfeld
  /// </summary>
  public sealed class SpielFeld
  {
    #region # // --- Konstanten ---
    /// <summary>
    /// gibt die Breite des Spielfeldes an (Default: 7)
    /// </summary>
    public const int FeldBreite = 7;

    /// <summary>
    /// gibt die Höhe des Spielfeldes an (Default: 6)
    /// </summary>
    public const int FeldHöhe = 6;

    /// <summary>
    /// gibt die Anzahl der Spielfelder an (7 * 6 = 42)
    /// </summary>
    public const int FeldAnzahl = FeldBreite * FeldHöhe;
    #endregion

    #region # // --- Variablen ---
    /// <summary>
    /// das eigentliche Spielfeld
    /// </summary>
    internal readonly byte[] feld;

    /// <summary>
    /// merkt sich den aktuellen Spielstatus
    /// </summary>
    public SpielStatus Status { get; private set; }

    /// <summary>
    /// merkt sich die Aktuelle Zugnummer (0 = Start, 42 = Ende)
    /// </summary>
    public int ZugNummer { get; private set; }
    #endregion

    #region # // --- Methoden ---

    /// <summary>
    /// prüft, ob eine Kette zusammenhängend gewinnt
    /// </summary>
    /// <param name="f">Spielerfarbe (1 oder 2)</param>
    /// <param name="x1">X-Pos vom ersten prüfenden Feld</param>
    /// <param name="y1">Y-Pos vom ersten prüfenden Feld</param>
    /// <param name="x2">X-Pos vom zweiten prüfenden Feld</param>
    /// <param name="y2">Y-Pos vom zweiten prüfenden Feld</param>
    /// <param name="x3">X-Pos vom dritten prüfenden Feld</param>
    /// <param name="y3">Y-Pos vom dritten prüfenden Feld</param>
    /// <returns>true, wenn die Spielerfarbe mit allen Feldern übereinstimmt</returns>
    bool CheckGewinnKette(int f, int x1, int y1, int x2, int y2, int x3, int y3)
    {
      if (x1 < 0 || x2 < 0 || x3 < 0 || y1 < 0 || y2 < 0 || y3 < 0) return false;
      if (x1 >= FeldBreite || x2 >= FeldBreite || x3 >= FeldBreite || y1 >= FeldHöhe || y2 >= FeldHöhe || y3 >= FeldHöhe) return false;

      return feld[x1 + y1 * FeldBreite] == f &&
             feld[x2 + y2 * FeldBreite] == f &&
             feld[x3 + y3 * FeldBreite] == f;
    }

    /// <summary>
    /// prüft, ob ein bestimmter Spielzug zum Gewinn geführt hat
    /// </summary>
    /// <param name="pos">Position des letzten gesetzten Steines, welche geprüft werden soll</param>
    /// <returns>true, wenn gewonnen wurde</returns>
    internal bool CheckGewinn(int pos)
    {
      int f = feld[pos];

      int x = pos % FeldBreite;
      int y = pos / FeldBreite;

      return CheckGewinnKette(f, x - 3, y, x - 2, y, x - 1, y) ||
             CheckGewinnKette(f, x - 2, y, x - 1, y, x + 1, y) ||
             CheckGewinnKette(f, x - 1, y, x + 1, y, x + 2, y) ||
             CheckGewinnKette(f, x + 1, y, x + 2, y, x + 3, y) ||

             CheckGewinnKette(f, x, y - 3, x, y - 2, x, y - 1) ||
             CheckGewinnKette(f, x, y - 2, x, y - 1, x, y + 1) ||
             CheckGewinnKette(f, x, y - 1, x, y + 1, x, y + 2) ||
             CheckGewinnKette(f, x, y + 1, x, y + 2, x, y + 3) ||

             CheckGewinnKette(f, x - 3, y - 3, x - 2, y - 2, x - 1, y - 1) ||
             CheckGewinnKette(f, x - 2, y - 2, x - 1, y - 1, x + 1, y + 1) ||
             CheckGewinnKette(f, x - 1, y - 1, x + 1, y + 1, x + 2, y + 2) ||
             CheckGewinnKette(f, x + 1, y + 1, x + 2, y + 2, x + 3, y + 3) ||

             CheckGewinnKette(f, x + 3, y - 3, x + 2, y - 2, x + 1, y - 1) ||
             CheckGewinnKette(f, x + 2, y - 2, x + 1, y - 1, x - 1, y + 1) ||
             CheckGewinnKette(f, x + 1, y - 1, x - 1, y + 1, x - 2, y + 2) ||
             CheckGewinnKette(f, x - 1, y + 1, x - 2, y + 2, x - 3, y + 3) ||
        false
      ;
    }

    /// <summary>
    /// überprüft das Spielfeld auf Gültigkeit und setzt die Felder "Status" und "ZugNummer"
    /// </summary>
    /// <returns>true, wenn das Spielfeld gültig ist sonst false</returns>
    bool UpdateValidate()
    {
      // --- Felder-Anzahl und Inhalt prüfen ---

      if (feld.Length != FeldAnzahl) return false;
      if (feld.Any(b => b > 2)) return false;


      // --- Spieler Anzahl prüfen ---

      int spielerX = feld.Count(b => b == 1);
      int spielerO = feld.Count(b => b == 2);

      if (spielerX != spielerO && spielerX - 1 != spielerO) return false;


      // --- auf ungültige Lücken prüfen ---

      for (int x = 0; x < FeldBreite; x++)
      {
        bool find = false;
        for (int y = 0; y < FeldHöhe; y++)
        {
          if (feld[x + y * FeldBreite] != 0)
          {
            find = true;
          }
          else
          {
            if (find) return false;
          }
        }
      }


      // --- alles soweit ok ---

      //ZugNummer = spielerX + spielerO;
      //Status = SpielStatus.EndeGleichstand;

      //for (int x = 0; x < FeldBreite; x++)
      //{
      //  for (int y = 0; y < FeldHöhe; y++)
      //  {
      //    if (feld[x + y * FeldBreite] == 0) continue;

      //    if (CheckGewinn(x + y * FeldBreite))
      //    {
      //      var neuStatus = feld[x + y * FeldBreite] == 1 ? SpielStatus.EndeGewinnX : SpielStatus.EndeGewinnO;

      //      if (Status != SpielStatus.EndeGleichstand && Status != neuStatus) return false;

      //      Status = neuStatus;
      //    }
      //  }
      //}

      return true;
    }

    /// <summary>
    /// gibt das Spielfeld als Zeichenkette zurück
    /// </summary>
    /// <returns>sichbares Spielfeld als Zeichenkette</returns>
    public override string ToString()
    {
      var ausgabe = new StringBuilder(FeldAnzahl + FeldBreite * 2);

      for (int y = 0; y < FeldHöhe; y++)
      {
        for (int x = 0; x < FeldBreite; x++)
        {
          switch (feld[x + y * FeldBreite])
          {
            case 1: ausgabe.Append('X'); break;
            case 2: ausgabe.Append('O'); break;
            default: ausgabe.Append('.'); break;
          }
        }
        ausgabe.AppendLine();
      }

      return ausgabe.ToString();
    }
    #endregion

    #region # // --- Konstruktor ---
    /// <summary>
    /// Konstruktor
    /// </summary>
    /// <param name="spielfeldString">SpielfeldDaten</param>
    public SpielFeld(string spielfeldString)
    {
      feld = spielfeldString.ToCharArray().Select(c =>
      {
        switch (c)
        {
          case ' ':
          case '-':
          case '.':
          case '\r':
          case '\n':
          case '\t':
          return (byte)0;

          case 'x':
          case 'X':
          case '1':
          case '+':
          return (byte)1;

          case 'o':
          case 'O':
          case '0':
          case '2':
          return (byte)2;

          default: return (byte)255;
        }
      }).Where(f => f < 255).ToArray();

      if (!UpdateValidate()) throw new Exception("ungültiges Spielfeld!");
    }
    #endregion
  }
}
