#region # using *.*

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace VierGewinntCore.Test
{
  [TestClass]
  public sealed class GewinnCheckTests
  {
    const string Feld =
      "......." +
      "......." +
      "......." +
      "......." +
      "......." +
      ".......";

    static void ScanFalse(SpielFeld feld)
    {
      for (int p = 0; p < SpielFeld.FeldAnzahl; p++)
      {
        Assert.IsFalse(feld.CheckGewinn(p));
      }
    }

    static void ScanTrue(SpielFeld feld, int p1, int p2, int p3, int p4)
    {
      Assert.IsTrue(feld.CheckGewinn(p1));
      Assert.IsTrue(feld.CheckGewinn(p2));
      Assert.IsTrue(feld.CheckGewinn(p3));
      Assert.IsTrue(feld.CheckGewinn(p4));

      for (int p = 0; p < SpielFeld.FeldAnzahl; p++)
      {
        if (p == p1 || p == p2 || p == p3 || p == p4) continue;

        Assert.IsFalse(feld.CheckGewinn(p));
      }
    }

    static void ScanTrueOptional(SpielFeld feld, int p1, int o1, int o2, int o3)
    {
      Assert.IsTrue(feld.CheckGewinn(p1));

      for (int p = 0; p < SpielFeld.FeldAnzahl; p++)
      {
        if (p == p1 || p == o1 || p == o2 || p == o3) continue;

        Assert.IsFalse(feld.CheckGewinn(p));
      }
    }

    [TestMethod]
    public void Test_0_Leer()
    {
      var feld = new SpielFeld(Feld);

      ScanFalse(feld);
    }

    [TestMethod]
    public void Test_1_EinerVoll()
    {
      var feld = new SpielFeld(Feld);

      for (int p1 = 0; p1 < SpielFeld.FeldAnzahl; p1++)
      {
        feld.feld[p1] = 1;

        ScanFalse(feld);

        feld.feld[p1] = 0;
      }
    }

    [TestMethod]
    public void Test_2_ZweierVoll()
    {
      var feld = new SpielFeld(Feld);

      for (int p2 = 0; p2 < SpielFeld.FeldAnzahl; p2++)
      {
        feld.feld[p2] = 1;
        for (int p1 = 0; p1 < p2; p1++)
        {
          feld.feld[p1] = 1;

          ScanFalse(feld);

          feld.feld[p1] = 0;
        }
        feld.feld[p2] = 0;
      }
    }

    [TestMethod]
    public void Test_3_DreierVoll()
    {
      var feld = new SpielFeld(Feld);

      for (int p3 = 0; p3 < SpielFeld.FeldAnzahl; p3++)
      {
        feld.feld[p3] = 1;
        for (int p2 = 0; p2 < p3; p2++)
        {
          feld.feld[p2] = 1;
          for (int p1 = 0; p1 < p2; p1++)
          {
            feld.feld[p1] = 1;

            ScanFalse(feld);

            feld.feld[p1] = 0;
          }
          feld.feld[p2] = 0;
        }
        feld.feld[p3] = 0;
      }
    }

    [TestMethod]
    public void Test_4_ViererVoll()
    {
      var feld = new SpielFeld(Feld);

      for (int p4 = 0; p4 < SpielFeld.FeldAnzahl; p4++)
      {
        feld.feld[p4] = 1;
        for (int p3 = 0; p3 < p4; p3++)
        {
          feld.feld[p3] = 1;
          for (int p2 = 0; p2 < p3; p2++)
          {
            feld.feld[p2] = 1;
            for (int p1 = 0; p1 < p2; p1++)
            {
              feld.feld[p1] = 1;

              if (p1 + 1 == p2 && p2 + 1 == p3 && p3 + 1 == p4 && p1 % SpielFeld.FeldBreite < p4 % SpielFeld.FeldBreite)
              {
                ScanTrue(feld, p1, p2, p3, p4);
              }
              else if (p1 + SpielFeld.FeldBreite == p2 && p2 + SpielFeld.FeldBreite == p3 && p3 + SpielFeld.FeldBreite == p4)
              {
                ScanTrueOptional(feld, p1, p2, p3, p4);
              }
              else if (p1 + SpielFeld.FeldBreite + 1 == p2 && p2 + SpielFeld.FeldBreite + 1 == p3 && p3 + SpielFeld.FeldBreite + 1 == p4 && p1 % SpielFeld.FeldBreite < p4 % SpielFeld.FeldBreite)
              {
                ScanTrue(feld, p1, p2, p3, p4);
              }
              else if (p1 + SpielFeld.FeldBreite - 1 == p2 && p2 + SpielFeld.FeldBreite - 1 == p3 && p3 + SpielFeld.FeldBreite - 1 == p4 && p1 % SpielFeld.FeldBreite > p4 % SpielFeld.FeldBreite)
              {
                ScanTrue(feld, p1, p2, p3, p4);
              }
              else ScanFalse(feld);

              feld.feld[p1] = 0;
            }
            feld.feld[p2] = 0;
          }
          feld.feld[p3] = 0;
        }
        feld.feld[p4] = 0;
      }
    }

  }
}
