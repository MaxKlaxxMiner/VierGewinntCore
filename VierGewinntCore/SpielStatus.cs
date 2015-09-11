using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinntCore
{
  /// <summary>
  /// gibt den aktuellen Spielstatus an
  /// </summary>
  public enum SpielStatus
  {
    /// <summary>
    /// Spieler 1 ist am Zug - X
    /// </summary>
    ZugX,

    /// <summary>
    /// Spieler 2 ist am Zug - O
    /// </summary>
    ZugO,

    /// <summary>
    /// Ende des Spiels (Gleichstand)
    /// </summary>
    EndeGleichstand,

    /// <summary>
    /// Ende des Spiels (Spieler 1 hat gewonnen - X)
    /// </summary>
    EndeGewinnX,

    /// <summary>
    /// Ende des Spiels (Spieler 2 hat gewonnen - O)
    /// </summary>
    EndeGewinnO,
  }
}
