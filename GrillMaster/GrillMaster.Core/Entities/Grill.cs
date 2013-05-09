#region {Imports]

using System;
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

        #region [Private fields]

        /// <summary>
        ///     Items on grill.
        /// </summary>
        private List<GrillMenuItem> _menuItemsOnGrill = new List<GrillMenuItem>();

        #endregion

        #region [Properties]

        public List<GrillMenuItem> MenuItemsOnGrill
        {
            get { return _menuItemsOnGrill; }
        }

        #endregion

        #region [Public methods]

        /// <summary>
        ///     Check if point is busy by menu item.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <returns>Chexk result,</returns>
        public bool IsBusyPoint(int x, int y)
        {
            return _menuItemsOnGrill.Any(item => item.IsBusyPoint(x, y));
        }

        /// <summary>
        ///     Fill menu items on grill.
        /// </summary>
        /// <param name="menuItems">All items to prepare.</param>
        /// <returns>Added on grill items.</returns>
        public IEnumerable<GrillMenuItem> FillGrillItems(IEnumerable<GrillMenuItem> menuItems)
        {
            _menuItemsOnGrill = new List<GrillMenuItem>();
            foreach (var grillMenuItem in menuItems.OrderBy(i => i.Square))
            {
                AddMenuItemIfCan(grillMenuItem);
            }

            return _menuItemsOnGrill;
        }

        /// <summary>
        ///     Clear menu items on grill.
        /// </summary>
        public void ClearMenuItemsOnGrill()
        {
            _menuItemsOnGrill.Clear();
        }

        /// <summary>
        ///     Do prepare iteration. Returns prepared items and prepare time.
        /// </summary>
        /// <param name="prepareTime">Time to prepare items items on grill with minimum prepare time.</param>
        /// <returns>Prepared Items.</returns>
        public IEnumerable<GrillMenuItem> GetNextPreparedItems(out TimeSpan prepareTime)
        {
            prepareTime = _menuItemsOnGrill.Min(i => i.PrepareDuration);
            foreach (var grillMenuItem in _menuItemsOnGrill)
            {
                grillMenuItem.PrepareDuration -= prepareTime;
            }

            return _menuItemsOnGrill.Where(i => i.IsPrepared);
        }

        #endregion

        #region [Private methods]

        /// <summary>
        ///     Add menu item on first fit place.
        /// </summary>
        /// <param name="menuItem">Menu item to add on grill.</param>
        /// <returns>Add result.</returns>
        private bool AddMenuItemIfCan(GrillMenuItem menuItem)
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
                _menuItemsOnGrill.Add(menuItem);
                return true;
            }

            menuItem.SetItemPositionOnGrill(pointForItem.X, pointForItem.Y);
            _menuItemsOnGrill.Add(menuItem);
            return true;
        }

        private GrillPoint GetFirstFreeRectangle(int heigth, int width)
        {
            for (var i = 0; i < Heigth - heigth + 1; i++)
            {
                for (var j = 0; j < Width - width + 1; j++)
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
