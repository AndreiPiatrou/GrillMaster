#region {Imports]

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using VARDESC = System.Runtime.InteropServices.ComTypes.VARDESC;

#endregion

namespace GrillMaster.Core.Entities
{
    /// <summary>
    ///     Grill entity.
    /// </summary>
    public class Grill
    {
        #region [Constants]

        private const int Heigth = 20;
        private const int Width = 30;

        #endregion

        /// <summary>
        ///     Items on grill.
        /// </summary>
        private readonly List<GrillMenuItem> _menuItems = new List<GrillMenuItem>();

        #region [Public methods]

        /// <summary>
        ///     Add menu item on first fit place.
        /// </summary>
        /// <param name="menuItem">Menu item to add on grill.</param>
        /// <returns>Add result.</returns>
        public bool AddMenuItemIfCan(GrillMenuItem menuItem)
        {
            var pointForItem = GetFirstFreeRectangle(menuItem.Height, menuItem.Width);
            if (pointForItem == null)
            {
                pointForItem = GetFirstFreeRectangle(menuItem.Width, menuItem.Height);
                if (pointForItem == null)
                {
                    return false;
                }

                menuItem.IsInverted = true;
                menuItem.SetItemPositionOnGrill(pointForItem.X, pointForItem.Y);
                _menuItems.Add(menuItem);
                return true;
            }

            menuItem.SetItemPositionOnGrill(pointForItem.X, pointForItem.Y);
            _menuItems.Add(menuItem);
            return true;
        }

        /// <summary>
        ///     Check if point is busy by menu item.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <returns>Chexk result,</returns>
        public bool IsBusyPoint(int x, int y)
        {
            return _menuItems.Any(item => item.IsBusyPoint(x, y));
        }

        /// <summary>
        ///     Get all items from griil, that can be prepared.
        /// </summary>
        /// <returns>Prepared items.</returns>
        public IEnumerable<GrillMenuItem> GetFirstPreparedItems()
        {
            var firstPrepareTime = _menuItems.Min(i => i.PrepareDuration);// Get first prepare time.
            foreach (var grillMenuItem in _menuItems)
            {
                grillMenuItem.PrepareDuration -= firstPrepareTime;// Decreace for all items prepare time.
            }

            return _menuItems.Where(i => i.IsPrepared);
        }

        #endregion

        #region [Private methods]

        private GrillPoint GetFirstFreeRectangle(int heigth, int width)
        {
            for (var i = 0; i < Heigth - heigth; i++)
            {
                for (var j = 0; j < Width - width; j++)
                {
                    if (IsBusyPoint(j, i))
                    {
                        continue;
                    }

                    for (int k = i; k < i + heigth; k++)
                    {
                        for (int l = j; l < j + width; l++)
                        {
                            if (IsBusyPoint(l, k)) continue;
                            return new GrillPoint(l, k);
                        }
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
