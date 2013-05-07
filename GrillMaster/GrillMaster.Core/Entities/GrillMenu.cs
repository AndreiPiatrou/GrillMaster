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

        private List<GrillMenuItem> menuItems;
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
            : this(id, name, new List<GrillMenuItem>())
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
        public GrillMenu(Guid id, string name, IEnumerable<GrillMenuItem> menuItems)
        {
            this.id = id;
            this.name = name;
            this.menuItems = new List<GrillMenuItem>(menuItems);
        }

        #endregion

        #region [Properties]

        public List<GrillMenuItem> MenuItems
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

        #endregion
    }
}
