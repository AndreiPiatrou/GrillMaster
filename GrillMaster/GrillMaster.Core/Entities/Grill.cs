#region {Imports]

using System.Collections.Generic;

#endregion

namespace GrillMaster.Core.Entities
{
    public class Grill
    {
        #region [Constants]

        private const int Heigth = 20;
        private const int Width = 30;

        #endregion

        private List<GrillMenuItem> menuItems;

        public GrillSquare BiggestFreeSquare
        {
            get
            {
                foreach (var grillMenuItem in menuItems)
                {

                }
            }
        }

    }
}
