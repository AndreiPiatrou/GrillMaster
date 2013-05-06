#region [Imports]

using System;

#endregion

namespace GrillMaster.Core.Entities
{
    /// <summary>
    ///     Introduce entity for menu item.
    /// </summary>
    public class GrillMenuItem : IIdentify
    {
        #region [Private fields]

        private readonly Guid id;
        private readonly string name;
        private readonly int height;
        private readonly int width;
        private readonly TimeSpan prepareDuration;

        #endregion

        #region [Constructors]

        public GrillMenuItem(Guid id, string name, int height, int width, TimeSpan prepareDuration)
        {
            this.id = id;
            this.name = name;
            this.height = height;
            this.width = width;
            this.prepareDuration = prepareDuration;
        }

        #endregion

        #region [Properties]

        public Guid Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public TimeSpan PrepareDuration
        {
            get { return prepareDuration; }
        }

        #endregion
    }
}
