using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 미로주행03
{
    public static partial class PathFinderExtension
    {
        public static Car MoveUp(this Car car) => new Car(car.Coordinate + (0, 1), car.Fuel - 1);
        public static Car MoveDown(this Car car) => new Car(car.Coordinate - (0, 1), car.Fuel - 1);
        public static Car MoveLeft(this Car car) => new Car(car.Coordinate - (1, 0), car.Fuel - 1);
        public static Car MoveRight(this Car car) => new Car(car.Coordinate + (1, 0), car.Fuel - 1);

        public static bool IsValid_ToEnQueue(this Car car, PathFinder pathFinder)
        {
            throw new NotImplementedException();
        }

        public static Queue<Car> GetAdjacentValidCars(this Car car, PathFinder pathFinder)
        {
            var Q = new Queue<Car>();

            Q.EnqueueIfValid(pathFinder, car.MoveUp());
            Q.EnqueueIfValid(pathFinder, car.MoveDown());
            Q.EnqueueIfValid(pathFinder, car.MoveLeft());
            Q.EnqueueIfValid(pathFinder, car.MoveRight());

            return Q;
        }

        public static void EnqueueIfValid(this Queue<Car> Q, PathFinder pathFinder, Car currentCar)
        {
            if (currentCar.IsValid_ToEnQueue(pathFinder))
            {
                Q.Enqueue(currentCar);
            }
        }


        public static bool Explore(this HashSet<Coordinate> ExploredCoordinates, Car car) => Explore(ExploredCoordinates, car.Coordinate);
        public static bool Explore(this HashSet<Coordinate> ExploredCoordinates, Coordinate coordinate)
        {
            if (ExploredCoordinates.Contains(coordinate) == false)
            {
                ExploredCoordinates.Add(coordinate);
                return true;
            }

            return false;
        }

                

        public static bool HasLessFuelThan(this Dictionary<Coordinate, Car> ExploredCoordinates, Car car) 
            => ExploredCoordinates[car.Coordinate].Fuel < car.Fuel;
        public static bool IsInsideOfBoundary(this Car car) => car.Coordinate.IsInsideOf(PathFinder.Boundary);
    }
}
