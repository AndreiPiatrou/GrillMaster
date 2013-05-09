#region [Imports]

using System;
using System.Security.Cryptography.X509Certificates;

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
        private int x;
        private int y;

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

        public bool IsPrepared
        {
            get { return PrepareDuration == TimeSpan.Zero; }
        }

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

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        #endregion
    }
}
