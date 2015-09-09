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
    readonly byte[] feld;
    #endregion

    #region # // --- Methoden ---
    /// <summary>
    /// überprüft das Spielfeld auf Gültigkeit
    /// </summary>
    /// <returns>true, wenn das Spielfeld gültig ist sonst false</returns>
    bool Validate()
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

      if (!Validate()) throw new Exception("ungültiges Spielfeld!");
    }
    #endregion
  }
}
