#region [Imports]

using System;
using System.Collections.Generic;

#endregion

namespace GrillMaster.Core.Entities
{
    /// <summary>
    ///     Introduce grill menu.
    /// </summary>
    public class GrillMenu : IIdentify
    {
        #region [Private fields]

        private List<Tuple<int, GrillMenuItem>> menuItems;
        private Guid id;
        private string name;

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="GrillMenu"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        public GrillMenu(Guid id, string name)
            : this(id, name, new List<Tuple<int, GrillMenuItem>>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GrillMenu"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="menuItems">
        /// The menu items.
        /// </param>
        public GrillMenu(Guid id, string name, IEnumerable<Tuple<int, GrillMenuItem>> menuItems)
        {
            this.id = id;
            this.name = name;
            this.menuItems = new List<Tuple<int, GrillMenuItem>>(menuItems);
        }

        #endregion

        #region [Properties]

        public List<Tuple<int, GrillMenuItem>> MenuItems
        {
            get { return menuItems; }
            set { menuItems = value; }
        }

        public Guid Id
        {
            get
            {
                return id;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string GetMenuQuantitiesLink
        {
            get { return string.Format("GrillMenus(guid'{0}')/GrillMenuItemQuantity", id); }
        }

        #endregion
    }
}
