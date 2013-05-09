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


            Console.ReadKey();
        }

        private void Optimize(GrillMenu menu)
        {
            var resultCollection = GetAllMenuItems(menu);

        }
        
        #region [Help methods]

        private IEnumerable<GrillMenuItem> FindFIrstCollection(List<GrillMenuItem> items)
        {
            
        }

        /// <summary>
        ///     Get all menu items.
        /// </summary>
        /// <param name="menu">Grill menu.</param>
        /// <returns>All items in menu.</returns>
        private List<GrillMenuItem> GetAllMenuItems(GrillMenu menu)
        {
            var resultCollection = new List<GrillMenuItem>();
            foreach (var menuItem in menu.MenuItems)
            {
                for (int i = 0; i < menuItem.Item1; i++)
                {
                    resultCollection.Add(menuItem.Item2);
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

                Console.WriteLine();
            }
        }

        #endregion
    }
}
