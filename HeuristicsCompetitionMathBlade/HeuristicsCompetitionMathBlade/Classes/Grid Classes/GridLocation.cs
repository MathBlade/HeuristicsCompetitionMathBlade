using System;
using System.Collections.Generic;
using System.Text;

namespace HeuristicsCompetitionMathBlade.Classes.Grid_Classes
{
    public class GridLocation
    {

        
        int _x;
        int _y;
        public GridLocation(int x, int y)
        {
            _x = x;
            _y = y;

        }



        public override bool Equals(object obj)
        {
            GridLocation other = (GridLocation)obj;
            if (other == null)
            {
                return false;
            }
            return (X == other.X && Y == other.Y);
        }

        public override int GetHashCode()
        {
            //return base.GetHashCode();
            return string.Format("{0}-{1}", X, Y).GetHashCode();
        }

        public static GridLocation Subtract(GridLocation start, GridLocation destination)
        {
            return new GridLocation(start.X - destination.X, start.Y - destination.Y);
        }

        public int X {  get { return _x; } }
        public int Y {  get { return _y; } }
    }
}
