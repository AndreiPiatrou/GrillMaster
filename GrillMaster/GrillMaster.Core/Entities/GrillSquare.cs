namespace GrillMaster.Core.Entities
{
    public class GrillRectangle
    {
        private readonly GrillPoint _point = new GrillPoint();

        public int X
        {
            get { return _point.X; }
            set { _point.X = value; }
        }

        public int Y
        {
            get { return _point.Y; }
            set { _point.Y = value; }
        }

        public int XLength;
        public int YLength;
    }
}