#region # using *.*

using System;
using VierGewinntCore;

#endregion

namespace FourConsole
{
  static class Program
  {
    static void Main()
    {
      var feld = new SpielFeld();

      for (; ; )
      {
        Console.WriteLine(feld);

        switch (Console.ReadKey().Key)
        {
          case ConsoleKey.Escape: return;
        }
      }
    }
  }
}
