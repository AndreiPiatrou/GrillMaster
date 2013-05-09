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
    public class Program
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

        private const int GrillWidth = 30;

        private const int GrillHeigth = 20;

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
            var menuItems = GetAllMenuItems(menus.First()).OrderBy(i => i.Square).ThenBy(i => i.PrepareDuration).ToList();
            do
            {
                grill = FillGrill(grill, menuItems);
                grill.PrintGrill();
                var preparedItems = grill.GetFirstPreparedItems();
                foreach (var firstPreparedItem in preparedItems)
                {
                    menuItems.Remove(firstPreparedItem);
                }
                Console.WriteLine("Press any key to go to the next step...");
                Console.ReadKey();

            } while (menuItems.Any());

            Console.ReadKey();
        }

        private static Grill FillGrill(Grill grill, IEnumerable<GrillMenuItem> menuItems)
        {
            foreach (var grillMenuItem in menuItems)
            {
                if (grill.AddMenuItemIfCan(grillMenuItem))
                {
                    Console.WriteLine("{0} added. ({1},{2})", grillMenuItem.Name, grillMenuItem.X, grillMenuItem.Y);
                    Console.ReadKey();
                }
            }

            return grill;
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
                Console.WriteLine();
            }
        }

        #endregion
    }
}
