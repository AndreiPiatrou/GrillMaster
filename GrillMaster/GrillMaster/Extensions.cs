#region [Imports]

using System;
using GrillMaster.Core.Entities;

#endregion

namespace GrillMaster
{
    public static class Extensions
    {
        public static void PrintGrill(this Grill grill)
        {
            Console.WriteLine(@"/------------------------------\");
            for (var i = 0; i < Grill.Heigth; i++)
            {
                Console.Write("|");
                for (var j = 0; j < Grill.Width; j++)
                {
                    Console.Write(grill.IsBusyPoint(j, i) ? "x" : ".");
                }

                Console.Write("|\n");
            }

            Console.WriteLine(@"\------------------------------/");
        }
    }
}
