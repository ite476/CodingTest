using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 미로주행03
{
    public class Car
    {
        public Coordinate Coordinate { get; }
        public int Fuel { get; }

        public Car(int x, int y, int fuel) : this(new Coordinate(x, y), fuel) { }
        public Car(Coordinate coordinate, int fuel)
        {
            this.Coordinate = coordinate;
            this.Fuel = fuel;
        }

        public bool hasFuelLeft() => Fuel > 0;

        public override string ToString() => $"{{Car : {this.Coordinate}, {this.Fuel}}}";
    }
}
