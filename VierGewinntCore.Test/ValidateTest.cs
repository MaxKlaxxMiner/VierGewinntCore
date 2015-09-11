#region # using *.*

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable UnusedVariable

#endregion

namespace VierGewinntCore.Test
{
  [TestClass]
  public sealed class ValidateTests
  {
    #region # // --- Helper-Methoden ---
    /// <summary>
    /// gibt das Spielfeld als Zeichenkette zurück (gleicher Aufbau wie die Test-Felder)
    /// </summary>
    /// <param name="feld">Spielfeld, was zurück gegeben werden soll</param>
    /// <returns>gefilterte Zeichenkette des Spielfeldes</returns>
    static string GetStr(SpielFeld feld)
    {
      return feld.ToString().Replace("\r", "").Replace("\n", "").ToLower();
    }
    #endregion

    #region # // --- Test1 - Leer ---
    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Test1F_Leer()
    {
      const string Feld =
        "......." +
        "......." +
        "......" +
        "......." +
        "......." +
        ".......";

      var feld = new SpielFeld(Feld);
    }

    [TestMethod]
    public void Test1R_Leer()
    {
      const string Feld =
        "......." +
        "......." +
        "......." +
        "......." +
        "......." +
        ".......";

      var feld = new SpielFeld(Feld);

      Assert.AreEqual(Feld, GetStr(feld));
    }
    #endregion

    #region # // --- Test2 - Spieler-Anzahl ---
    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Test2Fa_SpielerAnzahl()
    {
      const string Feld =
        "......." +
        "......." +
        "......." +
        "......." +
        "......." +
        "...o...";

      var feld = new SpielFeld(Feld);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Test2Fb_SpielerAnzahl()
    {
      const string Feld =
        "......." +
        "......." +
        "......." +
        "....x.." +
        "....o.." +
        ".x..x..";

      var feld = new SpielFeld(Feld);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Test2Fc_SpielerAnzahl()
    {
      const string Feld =
        "......." +
        "......." +
        "......." +
        "......." +
        "....o.." +
        ".o..x..";

      var feld = new SpielFeld(Feld);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Test2Fd_SpielerAnzahl()
    {
      const string Feld =
        "xxoxoxo" +
        "oxoxoxo" +
        "oxoxoxo" +
        "xoxoxox" +
        "xoxoxox" +
        "xoxoxox";

      var feld = new SpielFeld(Feld);
    }

    [TestMethod]
    public void Test2Ra_SpielerAnzahl()
    {
      const string Feld =
        "......." +
        "......." +
        "......." +
        "......." +
        "......." +
        "...x...";

      var feld = new SpielFeld(Feld);

      Assert.AreEqual(Feld, GetStr(feld));
    }

    [TestMethod]
    public void Test2Rb_SpielerAnzahl()
    {
      const string Feld =
        "......." +
        "......." +
        "....o.." +
        "....x.." +
        ".o..o.." +
        ".x..x..";

      var feld = new SpielFeld(Feld);

      Assert.AreEqual(Feld, GetStr(feld));
    }

    [TestMethod]
    public void Test2Rc_SpielerAnzahl()
    {
      const string Feld =
        "......." +
        "......." +
        "......." +
        "....x.." +
        ".x..o.." +
        ".o..x..";

      var feld = new SpielFeld(Feld);

      Assert.AreEqual(Feld, GetStr(feld));
    }

    [TestMethod]
    public void Test2Rd_SpielerAnzahl()
    {
      const string Feld =
        "oxoxoxo" +
        "oxoxoxo" +
        "oxoxoxo" +
        "xoxoxox" +
        "xoxoxox" +
        "xoxoxox";

      var feld = new SpielFeld(Feld);

      Assert.AreEqual(Feld, GetStr(feld));
    }
    #endregion

    #region # // --- Test3 - Feld-Lücken ---
    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Test3Fa_FeldLücken()
    {
      const string Feld =
        "......." +
        "......." +
        "......." +
        "......." +
        ".x....." +
        ".......";

      var feld = new SpielFeld(Feld);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Test3Fb_FeldLücken()
    {
      const string Feld =
        "......." +
        "......." +
        ".o....." +
        "......." +
        ".x....." +
        ".x.....";

      var feld = new SpielFeld(Feld);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Test3Fc_FeldLücken()
    {
      const string Feld =
        "......o" +
        "......." +
        "......." +
        "......." +
        ".xox..." +
        ".xxo...";

      var feld = new SpielFeld(Feld);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Test3Fd_FeldLücken()
    {
      const string Feld =
        "oxoxoxo" +
        "oxoxoxo" +
        "oxox.xo" +
        "xoxoxox" +
        "xoxoxox" +
        "xoxoxox";

      var feld = new SpielFeld(Feld);
    }

    [TestMethod]
    public void Test3Ra_FeldLücken()
    {
      const string Feld =
        "......." +
        "......." +
        "......." +
        "......." +
        "......." +
        ".x.....";

      var feld = new SpielFeld(Feld);

      Assert.AreEqual(Feld, GetStr(feld));
    }

    [TestMethod]
    public void Test3Rb_FeldLücken()
    {
      const string Feld =
        "......." +
        "......." +
        ".o....." +
        ".o....." +
        ".x....." +
        ".x.....";

      var feld = new SpielFeld(Feld);

      Assert.AreEqual(Feld, GetStr(feld));
    }

    [TestMethod]
    public void Test3Rc_FeldLücken()
    {
      const string Feld =
        "......." +
        "......." +
        "......x" +
        "...o..x" +
        ".xox..x" +
        ".xxoooo";

      var feld = new SpielFeld(Feld);

      Assert.AreEqual(Feld, GetStr(feld));
    }

    [TestMethod]
    public void Test3Rd_FeldLücken()
    {
      const string Feld =
        "oxox.xo" +
        "oxoxoxo" +
        "oxoxoxo" +
        "xoxoxox" +
        "xoxoxox" +
        "xoxoxox";

      var feld = new SpielFeld(Feld);

      Assert.AreEqual(Feld, GetStr(feld));
    }
    #endregion
  }
}
