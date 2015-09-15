#region # using *.*

using System;
using System.Linq;
using System.Text;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeInternal

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

    #region # // --- bool CheckGewinn() - prüft, ob ein bestimmter Spielzug zum Gewinn geführt hat ---
    /// <summary>
    /// merkt sich die statische Map, welche alle direkten Prüfpositionen des Spielfeldes enthält
    /// </summary>
    static readonly byte[] GewinnMap = GeneriereGewinnMap();

    /// <summary>
    /// generiert die "GewinnMap"
    /// </summary>
    /// <returns>fertig erstellte Gewinn-Map</returns>
    static byte[] GeneriereGewinnMap()
    {
      var map = new byte[FeldAnzahl * 64];

      for (int py = 0; py < FeldHöhe; py++)
      {
        for (int px = 0; px < FeldBreite; px++)
        {
          int pOfs = (px + py * FeldBreite) * 64;
          int p = pOfs + 1;

          // --- diagonal lo -> ru ---

          if (px > 2 && px < FeldBreite && py > 2 && py < FeldHöhe)
          {
            map[p++] = (byte)(px - 3 + (py - 3) * FeldBreite);
            map[p++] = (byte)(px - 2 + (py - 2) * FeldBreite);
            map[p++] = (byte)(px - 1 + (py - 1) * FeldBreite);
          }

          if (px > 1 && px < FeldBreite - 1 && py > 1 && py < FeldHöhe - 1)
          {
            map[p++] = (byte)(px - 2 + (py - 2) * FeldBreite);
            map[p++] = (byte)(px - 1 + (py - 1) * FeldBreite);
            map[p++] = (byte)(px + 1 + (py + 1) * FeldBreite);
          }

          if (px > 0 && px < FeldBreite - 2 && py > 0 && py < FeldHöhe - 2)
          {
            map[p++] = (byte)(px - 1 + (py - 1) * FeldBreite);
            map[p++] = (byte)(px + 1 + (py + 1) * FeldBreite);
            map[p++] = (byte)(px + 2 + (py + 2) * FeldBreite);
          }

          if (px > -1 && px < FeldBreite - 3 && py > -1 && py < FeldHöhe - 3)
          {
            map[p++] = (byte)(px + 1 + (py + 1) * FeldBreite);
            map[p++] = (byte)(px + 2 + (py + 2) * FeldBreite);
            map[p++] = (byte)(px + 3 + (py + 3) * FeldBreite);
          }


          // --- diagonal lu -> ro

          if (px > 2 && px < FeldBreite && py > -1 && py < FeldHöhe - 3)
          {
            map[p++] = (byte)(px - 3 + (py + 3) * FeldBreite);
            map[p++] = (byte)(px - 2 + (py + 2) * FeldBreite);
            map[p++] = (byte)(px - 1 + (py + 1) * FeldBreite);
          }

          if (px > 1 && px < FeldBreite - 1 && py > 0 && py < FeldHöhe - 2)
          {
            map[p++] = (byte)(px - 2 + (py + 2) * FeldBreite);
            map[p++] = (byte)(px - 1 + (py + 1) * FeldBreite);
            map[p++] = (byte)(px + 1 + (py - 1) * FeldBreite);
          }

          if (px > 0 && px < FeldBreite - 2 && py > 1 && py < FeldHöhe - 1)
          {
            map[p++] = (byte)(px - 1 + (py + 1) * FeldBreite);
            map[p++] = (byte)(px + 1 + (py - 1) * FeldBreite);
            map[p++] = (byte)(px + 2 + (py - 2) * FeldBreite);
          }

          if (px > -1 && px < FeldBreite - 3 && py > 2 && py < FeldHöhe)
          {
            map[p++] = (byte)(px + 1 + (py - 1) * FeldBreite);
            map[p++] = (byte)(px + 2 + (py - 2) * FeldBreite);
            map[p++] = (byte)(px + 3 + (py - 3) * FeldBreite);
          }


          // --- waagerecht ---

          if (px > 2 && px < FeldBreite)
          {
            map[p++] = (byte)(px - 3 + py * FeldBreite);
            map[p++] = (byte)(px - 2 + py * FeldBreite);
            map[p++] = (byte)(px - 1 + py * FeldBreite);
          }

          if (px > 1 && px < FeldBreite - 1)
          {
            map[p++] = (byte)(px - 2 + py * FeldBreite);
            map[p++] = (byte)(px - 1 + py * FeldBreite);
            map[p++] = (byte)(px + 1 + py * FeldBreite);
          }

          if (px > 0 && px < FeldBreite - 2)
          {
            map[p++] = (byte)(px - 1 + py * FeldBreite);
            map[p++] = (byte)(px + 1 + py * FeldBreite);
            map[p++] = (byte)(px + 2 + py * FeldBreite);
          }

          if (px > -1 && px < FeldBreite - 3)
          {
            map[p++] = (byte)(px + 1 + py * FeldBreite);
            map[p++] = (byte)(px + 2 + py * FeldBreite);
            map[p++] = (byte)(px + 3 + py * FeldBreite);
          }


          // --- senkrecht ---

          if (py < FeldHöhe - 3)
          {
            map[p++] = (byte)(px + (py + 1) * FeldBreite);
            map[p++] = (byte)(px + (py + 2) * FeldBreite);
            map[p++] = (byte)(px + (py + 3) * FeldBreite);
          }

          map[pOfs] = (byte)(p - pOfs); // Länge abspeichern
        }
      }

      return map;
    }

    /// <summary>
    /// prüft, ob ein bestimmter Spielzug zum Gewinn geführt hat
    /// </summary>
    /// <param name="pos">Position des letzten gesetzten Steines, welche geprüft werden soll</param>
    /// <returns>true, wenn gewonnen wurde</returns>
    internal bool CheckGewinn(int pos)
    {
      int mapPos = pos * 64;
      int len = mapPos + GewinnMap[mapPos];
      int f = feld[pos];

      if (f == 0) return false;

      for (int p = mapPos + 1; p < len; p += 3)
      {
        int i1 = GewinnMap[p];
        int i2 = GewinnMap[p + 1];
        int i3 = GewinnMap[p + 2];

        if (feld[i1] == f && feld[i2] == f && feld[i3] == f) return true;
      }

      return false;
    }

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
    bool CheckGewinnKette_Old(int f, int x1, int y1, int x2, int y2, int x3, int y3)
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
    // ReSharper disable once UnusedMember.Global
    internal bool CheckGewinn_Old(int pos)
    {
      int f = feld[pos];
      if (f == 0) return false;

      int x = pos % FeldBreite;
      int y = pos / FeldBreite;

      return CheckGewinnKette_Old(f, x - 3, y, x - 2, y, x - 1, y) ||
             CheckGewinnKette_Old(f, x - 2, y, x - 1, y, x + 1, y) ||
             CheckGewinnKette_Old(f, x - 1, y, x + 1, y, x + 2, y) ||
             CheckGewinnKette_Old(f, x + 1, y, x + 2, y, x + 3, y) ||

             CheckGewinnKette_Old(f, x, y - 3, x, y - 2, x, y - 1) ||
             CheckGewinnKette_Old(f, x, y - 2, x, y - 1, x, y + 1) ||
             CheckGewinnKette_Old(f, x, y - 1, x, y + 1, x, y + 2) ||
             CheckGewinnKette_Old(f, x, y + 1, x, y + 2, x, y + 3) ||

             CheckGewinnKette_Old(f, x - 3, y - 3, x - 2, y - 2, x - 1, y - 1) ||
             CheckGewinnKette_Old(f, x - 2, y - 2, x - 1, y - 1, x + 1, y + 1) ||
             CheckGewinnKette_Old(f, x - 1, y - 1, x + 1, y + 1, x + 2, y + 2) ||
             CheckGewinnKette_Old(f, x + 1, y + 1, x + 2, y + 2, x + 3, y + 3) ||

             CheckGewinnKette_Old(f, x + 3, y - 3, x + 2, y - 2, x + 1, y - 1) ||
             CheckGewinnKette_Old(f, x + 2, y - 2, x + 1, y - 1, x - 1, y + 1) ||
             CheckGewinnKette_Old(f, x + 1, y - 1, x - 1, y + 1, x - 2, y + 2) ||
             CheckGewinnKette_Old(f, x - 1, y + 1, x - 2, y + 2, x - 3, y + 3);
    }
    #endregion

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
    public SpielFeld() : this(new string(' ', FeldAnzahl)) { }

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
