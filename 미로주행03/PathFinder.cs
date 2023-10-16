using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace 미로주행03
{
    public class PathFinder
    {
        public static Coordinate Boundary { get; set; }
        HashSet<Coordinate> ExploredCoordinates { get; set; } = new HashSet<Coordinate>();        
        Car Start { get; set; }

        BitArray[] PossibleFlags { get; set; }

        public PathFinder(Car Start, Coordinate Boundary)
        {
            PathFinder.Boundary = Boundary;
            bitArr =  new BitArray2D(Boundary.X + 1, Boundary.Y + 1);
            this.Start = Start;
        }

        BitArray2D bitArr;
        public BitArray2D GetExploredCoordinates()
        {
            int startY = Math.Max(0, Start.Coordinate.X - Start.Fuel);
            int endY = Math.Min(Boundary.X, Start.Coordinate.X + Start.Fuel);

            for(int y = startY ; y <= endY ; y++)
            {
                int FuelLeft = Start.Fuel - Math.Abs(Start.Coordinate.X - y);
                int startX = Math.Max(0, Start.Coordinate.Y - FuelLeft);
                int endX = Math.Min(Boundary.Y, Start.Coordinate.Y + FuelLeft);

                bitArr.SetRowTrue(startX, endX, y);
            }

            return bitArr;
        }
        
    }

    
    
}
