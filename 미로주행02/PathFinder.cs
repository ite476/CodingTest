using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace 미로주행02
{
    public class PathFinder
    {
        public static Coordinate Boundary { get; set; }

        Dictionary<Coordinate, Car> ExploredCoordinates { get; set; } = new Dictionary<Coordinate, Car>();

        Queue<Car> SearchQueue { get; set; } = new Queue<Car>();
        HashSet<Coordinate> ToSearch { get; set; } = new HashSet<Coordinate>();

        HashSet<Coordinate> NoMoreSearch { get; set; } = new HashSet<Coordinate>();

        public PathFinder(Car Start)
        {
            Enqueue_IfValid(Start);
        }

        int Threshold = 1000_000_000;
        int count = 0;
        public Dictionary<Coordinate, Car> GetExploredCoordinates()
        {
            Car? currentCar;
            while (TryDequeue(out currentCar))
            {
                if (count++ > Threshold) break;

                if (ExploredCoordinates.Explore(currentCar))
                {
                    EnqueueAdjacentCars(currentCar);
                }
            }

            return ExploredCoordinates;
        }

        public bool IsValid_ToSearch(Car car)
        {
            return NoMoreSearch.Contains(car.Coordinate) == false
                && ToSearch.Contains(car.Coordinate) == false
                && car.IsInsideOfBoundary();
        }

        private void Enqueue_IfValid(Car car)
        {
            if (car.IsValid_ToEnQueue(this))
            {
                ToSearch.Add(car.Coordinate);
                SearchQueue.Enqueue(car);
            }            
        }

        private void EnqueueAdjacentCars(Car? currentCar)
        {
            foreach (var car in currentCar.GetAdjacentValidCars(this))
            {
                Enqueue_IfValid(car);
            }
        }

        private bool TryDequeue(out Car? car)
        {
            car = null;

            if (SearchQueue.Count > 0)
            {
                car = SearchQueue.Dequeue();
                ToSearch.Remove(car.Coordinate);
                NoMoreSearch.Add(car.Coordinate);
                return true;
            }

            return false;
        }

        
    }

    
    
}
