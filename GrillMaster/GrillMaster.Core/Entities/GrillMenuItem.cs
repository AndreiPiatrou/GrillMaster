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
        private readonly GrillRectangle _rectangleOnGrill = new GrillRectangle();
        private TimeSpan prepareDuration;
        private bool isInverted;

        #endregion

        #region [Constructors]

        public GrillMenuItem(Guid id, string name, int height, int width, TimeSpan prepareDuration)
        {
            this.id = id;
            this.name = name;
            _rectangleOnGrill.YLength = height;
            _rectangleOnGrill.XLength = width;
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
            get { return !isInverted ? _rectangleOnGrill.XLength : _rectangleOnGrill.YLength; }
        }

        public int Height
        {
            get { return !isInverted ? _rectangleOnGrill.XLength : _rectangleOnGrill.YLength; }
        }

        public TimeSpan PrepareDuration
        {
            get { return prepareDuration; }
            set { prepareDuration = value; }
        }

        public int X
        {
            get { return _rectangleOnGrill.X; }
            set { _rectangleOnGrill.X = value; }
        }

        public int Y
        {
            get { return _rectangleOnGrill.Y; }
            set { _rectangleOnGrill.Y = value; }
        }

        public bool IsInverted
        {
            get { return isInverted; }
            set { isInverted = value; }
        }

        #endregion

        public GrillMenuItem Clone()
        {
            var clone = new GrillMenuItem(id, name, Height, Width, TimeSpan.FromSeconds(prepareDuration.TotalSeconds));

            return clone;
        }

        public void SetItemPositionOnGrill(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int Square
        {
            get { return X * Y; }
        }

        public bool IsBusyPoint(int xCoordinate, int yCoordinate)
        {
            return (xCoordinate >= X && xCoordinate < (X + Width))
                && (yCoordinate >= Y && yCoordinate < (Y + Height));
        }

        public override string ToString()
        {
            return string.Format("{0} - ({1},{2}) - {3}x{4}", name, X, Y, Height, Width);
        }
    }
}
