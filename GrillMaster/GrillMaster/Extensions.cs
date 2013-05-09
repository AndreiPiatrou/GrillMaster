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
            Console.WriteLine("--------------------------------");
            for (int i = 0; i < 20; i++)
            {
                Console.Write("|");
                for (int j = 0; j < 30; j++)
                {
                    Console.Write(grill.IsBusyPoint(j, i) ? "x" : ".");
                }

                Console.Write("|\n");
            }

            Console.WriteLine("--------------------------------");
        }
    }
}
