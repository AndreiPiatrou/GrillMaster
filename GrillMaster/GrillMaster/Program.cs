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
            var grill = new Grill();
            LoadMenusAndPrintThem(menus);

            bool requestadToExit;
            do
            {
                var menuToPrepare = ChooseGrillMenu(menus); // Choose menu.
                var menuItems = GetAllMenuItems(menuToPrepare).ToList(); // Get all menu items.

                PrepareMenu(grill, menuItems); // Prepare menu.
                grill.ClearMenuItemsOnGrill();

                requestadToExit = RequestToExit();  // Ask to exit.

            } while (!requestadToExit);
        }

        private static bool RequestToExit()
        {
            while (true)
            {
                Console.WriteLine("Do you want to repeat? [y/n]");
                var key = Console.ReadKey();
                Console.Clear();
                if (key.Key == ConsoleKey.Y)
                {
                    return false;
                }

                if (key.Key == ConsoleKey.N)
                {
                    return true;
                }

                Console.Clear();
                Console.WriteLine("Unknown input. Please retry.");
            }
        }

        private static GrillMenu ChooseGrillMenu(List<GrillMenu> menus)
        {
            while (true)
            {
                Console.WriteLine("Enter menu number to prepare.");
                var menuNumder = Console.ReadLine();
                var menuToPrepare = menus.FirstOrDefault(m => m.Name == menuNumder || m.Name == string.Format("Menu {0}", menuNumder));
                if (menuToPrepare != null)
                {
                    return menuToPrepare;
                }

                Console.WriteLine("There is no menu with '{0}' name. Enter correct menu number.", menuNumder);
            }
        }

        /// <summary>
        ///     Prepare one menu.
        /// </summary>
        /// <param name="grill"></param>
        /// <param name="menuItems"></param>
        private static void PrepareMenu(Grill grill, List<GrillMenuItem> menuItems)
        {
            var totalPrepareTime = TimeSpan.Zero;
            do
            {
                TimeSpan prepareTime;
                PrintNextStepInfo();
                var addedOnGrillItemsCount = grill.FillGrillItems(menuItems).Count(); // Fill items on grill.
                Console.WriteLine("Added {0} from {1}", addedOnGrillItemsCount, menuItems.Count);

                grill.PrintGrill(); // Print grill in console.

                grill.GetNextPreparedItems(out prepareTime); // Do next prepare iteration.
                totalPrepareTime += prepareTime; // Sum all prepare time.
                var preparedItemsCount = menuItems.RemoveAll(i => i.IsPrepared); // Remove prepared items from main collection.

                grill.ClearMenuItemsOnGrill(); // Clear grill items for next iteration.
                Console.WriteLine("Prepared items {0} from {1}. Prepare time: {2}", preparedItemsCount, addedOnGrillItemsCount, prepareTime);

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
            Console.WriteLine("Total menu prepare time:{0}", totalPrepareTime);
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
        private static void LoadMenusAndPrintThem(IEnumerable<GrillMenu> menus)
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

                Console.WriteLine();
            }
        }

        #endregion
    }
}
