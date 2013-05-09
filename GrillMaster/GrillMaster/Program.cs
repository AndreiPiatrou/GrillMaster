#region [Imports]

using System;
using System.Collections.Generic;
using System.Linq;
using GrillMaster.Core.Entities;
using GrillMaster.Services.Requester;

#endregion

namespace GrillMaster
{
    /// <summary>
    /// The program.
    /// </summary>
    public static class Program
    {
        #region [Constants]

        /// <summary>
        /// The user name.
        /// </summary>
        private const string UserName = "a.petrov@itransition.com";

        /// <summary>
        /// The password.
        /// </summary>
        private const string Password = "Pjxi6";

        #endregion

        /// <summary>
        /// The main.
        /// </summary>
        private static void Main()
        {
            Console.WriteLine("User name: {0}", UserName);
            Console.WriteLine("Password:  {0}", Password);
            GMRequester.InitRequester(UserName, Password);
            var menus = GMRequester.LoadGrillMenus();
            LoadMenusAndPrintIt(menus);

            var grill = new Grill();
            var menuItems = GetAllMenuItems(menus.First()).ToList();
            var totalPrepareTime = TimeSpan.Zero;
            do
            {
                TimeSpan prepareTime;
                PrintNextStepInfo();

                var addedOnGrillItemsCount = grill.FillGrillItems(menuItems).Count(); // Fill items on grill.
                Console.WriteLine("Added {0} from {1}", addedOnGrillItemsCount, menuItems.Count);
                grill.PrintGrill(); // Print grill in console.

                grill.GetNextPreparedItems(out prepareTime); // Get next prepare iteration.
                totalPrepareTime += prepareTime; // Sum all prepare time.
                var preparedItemsCount = menuItems.RemoveAll(i => i.IsPrepared); // Remove prepared items from main collection.
                
                grill.ClearMenuItemsOnGrill(); // Clear grill items for next iteration.
                Console.WriteLine("Prepared items {0} from {1}", preparedItemsCount, addedOnGrillItemsCount);

            } while (menuItems.Any());

            PrintFinishInfo(totalPrepareTime);
        }

        private static void PrintNextStepInfo()
        {
            Console.WriteLine("Press any key to go to the next step...");
            Console.ReadKey();
            Console.Clear();
        }

        private static void PrintFinishInfo(TimeSpan totalPrepareTime)
        {
            Console.WriteLine("Menu is prepared! Congratulations! Have a good dinner!");
            Console.WriteLine("Total prepare time:{0}", totalPrepareTime);
            Console.ReadKey();
        }

        #region [Help methods]

        /// <summary>
        ///     Get all menu items.
        /// </summary>
        /// <param name="menu">Grill menu.</param>
        /// <returns>All items in menu.</returns>
        private static IEnumerable<GrillMenuItem> GetAllMenuItems(GrillMenu menu)
        {
            var resultCollection = new List<GrillMenuItem>();
            foreach (var menuItem in menu.MenuItems)
            {
                for (var i = 0; i < menuItem.Item1; i++)
                {
                    resultCollection.Add(menuItem.Item2.Clone());
                }
            }

            return resultCollection;
        }

        /// <summary>Load all menus` info, fill and print it.</summary>
        /// <param name="menus">The menus.</param>
        private static void LoadMenusAndPrintIt(IEnumerable<GrillMenu> menus)
        {
            foreach (var grillMenu in menus)
            {
                Console.WriteLine("{0}.", grillMenu.Name);
                var quantities = GMRequester.LoadGrillMenuQuantities(grillMenu.GetMenuQuantitiesLink);

                foreach (var grillMenuQuantity in quantities)
                {
                    var menuItem = GMRequester.LoadGrillMenuItems(grillMenuQuantity.GetMenuItemLink)[0];
                    grillMenu.MenuItems.Add(new Tuple<int, GrillMenuItem>(grillMenuQuantity.Quantity, menuItem));
                    Console.WriteLine(
                        "{0} * {1} ({2}x{3}. Duration:{4})",
                        grillMenuQuantity.Quantity,
                        menuItem.Name,
                        menuItem.Height,
                        menuItem.Width,
                        menuItem.PrepareDuration);
                }
                // todo: remove after whole implementation.
                return;
                // Console.WriteLine();
            }
        }

        #endregion
    }
}
