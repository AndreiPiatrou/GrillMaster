using System;

namespace GrillMaster.Core.Entities
{
    public class GrillMenuQuantity
    {
        #region [Private fields]

        private const string GrillMenuString = "Menu";
        private const string GrillMenuItemString = "GrillMenuItem";
        private const string GrillMenuQuantityString = "GrillMenuItemQuantities";
        private readonly Guid id;
        private readonly int quantity;

        #endregion

        #region [Constructors]

        public GrillMenuQuantity(Guid guid, int quantity)
        {
            id = guid;
            this.quantity = quantity;
        }

        #endregion

        #region [Properties]

        public int Quantity
        {
            get { return quantity; }
        }

        public string GetMenuLink
        {
            get { return string.Format("{0}(guid'{1}')/{2}", GrillMenuQuantityString, id, GrillMenuString); }
        }

        public string GetMenuItemLink
        {
            get { return string.Format("{0}(guid'{1}')/{2}", GrillMenuQuantityString, id, GrillMenuItemString); }
        }

        #endregion
    }
}
