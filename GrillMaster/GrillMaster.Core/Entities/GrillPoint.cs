namespace GrillMaster.Core.Entities
{
    public class GrillPoint
    {
        public GrillPoint()
            : this(0, 0)
        {
        }

        public GrillPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;
    }
}