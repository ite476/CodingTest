using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 미로주행03
{
    public static class CoordinateOperator 
    {
        
    }

    public class Coordinate
    {
        public int X { get; }
        public int Y { get; }

        public Coordinate(int x, int y) 
        {
            X = x;
            Y = y;
        }

        public Coordinate((int,int) tuple)
        {
            X = tuple.Item1;
            Y = tuple.Item2;
        }

        public Coordinate(Coordinate coordinate)
        {
            X = coordinate.X;
            Y = coordinate.Y;
        }

        public bool IsInsideOf(Coordinate boundary) => (X >= 0) && (Y >= 0) && (X <= boundary.X) && (Y <= boundary.Y);

        public static Coordinate operator +(Coordinate coord) => coord;
        public static Coordinate operator -(Coordinate coord) => new Coordinate(-coord.X, -coord.Y);
        
        public static Coordinate operator +(Coordinate left, Coordinate right) => new Coordinate(left.X + right.X, left.Y + right.Y);
        public static Coordinate operator -(Coordinate left, Coordinate right) => left + (-right);

        public static Coordinate operator +(Coordinate left, (int, int) right) => left + new Coordinate(right);
        public static Coordinate operator -(Coordinate left, (int, int) right) => left - new Coordinate(right);

        public static bool operator ==(Coordinate left, Coordinate right) => (left.X == right.X) && (left.Y == right.Y);
        public static bool operator !=(Coordinate left, Coordinate right) => !(left == right);


        public override string ToString() => $"{{Coordinate : {this.X}, {this.Y}}}";

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17; // 임의의 초기값 선택 (어떤 정수도 가능)

                // X와 Y 값을 조합하여 해시 코드 생성
                hash = hash * 23 + X;
                hash = hash * 29 + Y;

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Coordinate)
            {
                return this == (Coordinate)obj;
            }
            

            throw new NotImplementedException();
        }
    }
}
