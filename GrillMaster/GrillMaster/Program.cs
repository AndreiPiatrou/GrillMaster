#region [Imports]

using System;
using System.Collections.Generic;
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
        /// <summary>
        /// The user name.
        /// </summary>
        private const string UserName = "a.petrov@itransition.com";

        /// <summary>
        /// The password.
        /// </summary>
        private const string Password = "Pjxi6";

        /// <summary>
        /// The main.
        /// </summary>
        private static void Main()
        {
            Console.WriteLine("User name: {0}", UserName);
            Console.WriteLine("Password:  {0}", Password);
            GMRequester.InitRequester(UserName, Password);
            var menus = GMRequester.LoadGrillMenus();

            LoadAllMenus(menus);

            Console.ReadKey();
        }

        /// <summary>Load all menus` info, fill and print it.</summary>
        /// <param name="menus">The menus.</param>
        private static void LoadAllMenus(IEnumerable<GrillMenu> menus)
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
    }
}
