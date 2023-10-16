using ConsoleApp2;
using System;
using System.Drawing;
using System.Net;

namespace ConsoleApp2
{
    public class Coordinate
    {
        public int X { get; }
        public int Y { get; }

        public Coordinate(int x, int y) 
        { 
            X = x;
            Y = y;
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
        public static bool operator ==(Coordinate left, Coordinate right) => (left.X == right.X) && (left.Y == right.Y);
        public static bool operator !=(Coordinate left, Coordinate right) => !(left==right);

        public override string ToString() => $"{{Coordinate : {this.X}, {this.Y}}}";

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17; // 임의의 초기값 선택 (어떤 정수도 가능)

                // X와 Y 값을 조합하여 해시 코드 생성
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();

                return hash;
            }
        }


    }


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

    public class LogCalculator
    {
        Dictionary<Coordinate, DriveLog> ExploredCoordinates = new Dictionary<Coordinate, DriveLog>();
        Queue<DriveLog> SearchQueue { get; set; } = new Queue<DriveLog>();
        HashSet<Coordinate> SearchQueueHashCodes = new HashSet<Coordinate>();
        HashSet<Coordinate> NoMoreSearch = new HashSet<Coordinate>();
        bool hitFlag = true;

        public LogCalculator(Car StartCar, bool hitFlag)
        {
            Init(new DriveLog(StartCar));
            this.hitFlag = hitFlag;
        }

        private void Init(DriveLog Start)
        {
            ExploredCoordinates.Clear();
            SearchQueue.Clear();

            PutOrReplace_IntoDict(Start);
        }

        int calccount = 0;
        public List<Coordinate> GetPossibleCoordinateList_OfSigns()
        {
            var dict = new Dictionary<Coordinate, DriveLog>();

            // 연료 남았으면 위로 보내기 + 아래/왼쪽/오른쪽 ?? 없으면 종료
            // 위로 보낸 Log 생성
            // 이미 dict에 있으면 최소거리인지 확인해서 최소거리이면(더작으면) Replace
            // Replace되었다면 확인할 큐에 포함

            DriveLog? LogTarget;
            while (SearchQueue.TryDequeue(out LogTarget))
            {
                GetAdjacentLogs(LogTarget);
            }

            return dict.Keys.ToList();
        }

        private void GetAdjacentLogs(DriveLog logTarget)
        {
            Console.WriteLine("Called GetAdjacentLogs");
            DriveLog currentLog = logTarget.UpLog();
            if (ValidateLog(currentLog))
            {
                ExploredCoordinates[currentLog.Car.Coordinate] = currentLog;
                GetAdjacentLogs(currentLog);
            }
            currentLog = logTarget.DownLog();
            if (ValidateLog(currentLog))
            {
                ExploredCoordinates[currentLog.Car.Coordinate] = currentLog;
                GetAdjacentLogs(currentLog);
            }
            currentLog = logTarget.LeftLog();
            if (ValidateLog(currentLog))
            {
                ExploredCoordinates[currentLog.Car.Coordinate] = currentLog;
                GetAdjacentLogs(currentLog);
            }
            currentLog = logTarget.RightLog();
            if (ValidateLog(currentLog))
            {
                ExploredCoordinates[currentLog.Car.Coordinate] = currentLog;
                GetAdjacentLogs(currentLog);
            }

        }

        private bool ValidateLog(DriveLog currentLog) =>
            currentLog.Car.hasFuelLeft()
            && currentLog.IsInsideOfBoundary()
            && AlreadyExplored(currentLog) ? HasMoreFuel_ThanAlreadyExplored(currentLog) : true;

        private bool HasMoreFuel_ThanAlreadyExplored(DriveLog currentLog)
        {
            return (ExploredCoordinates[currentLog.Car.Coordinate].Car.Fuel < currentLog.Car.Fuel);
        }

        private bool AlreadyExplored(DriveLog currentLog)
        {
            return ExploredCoordinates.ContainsKey(currentLog.Car.Coordinate);
        }

        private void ValidateAdjacentLogs(DriveLog searchingTarget)
        {
            int FuelLeft = searchingTarget.Car.Fuel - 1;
            Console.WriteLine(FuelLeft.ToString());
            DriveLog log = new DriveLog(new Car(new Coordinate(searchingTarget.Car.Coordinate + new Coordinate(0, 1)), FuelLeft));
            if (log.IsInsideOfBoundary()) ValidateLog_AndEnQ(log);
            log = new DriveLog(new Car(new Coordinate(searchingTarget.Car.Coordinate - new Coordinate(0, 1)), FuelLeft));
            if (log.IsInsideOfBoundary()) ValidateLog_AndEnQ(log);
            log = new DriveLog(new Car(new Coordinate(searchingTarget.Car.Coordinate - new Coordinate(1, 0)), FuelLeft));
            if (log.IsInsideOfBoundary()) ValidateLog_AndEnQ(log);
            log = new DriveLog(new Car(new Coordinate(searchingTarget.Car.Coordinate + new Coordinate(1, 0)), FuelLeft));
            if (log.IsInsideOfBoundary()) ValidateLog_AndEnQ(log);
        }

        void ValidateLog_AndEnQ(DriveLog? logNow)
        {
            if (logNow != null
                && logNow.Car.Coordinate.IsInsideOf(DriveLog.Boundary))
            {
                if (ExploredCoordinates.ContainsKey(logNow.Car.Coordinate))
                {
                    if (ExploredCoordinates[logNow.Car.Coordinate].Car.Fuel < logNow.Car.Fuel)
                    {
                        PutOrReplace_IntoDict(logNow);
                    }
                }
                else
                {
                    PutOrReplace_IntoDict(logNow);
                }
            }
        }

        private void PutOrReplace_IntoDict(DriveLog logNow)
        {
            ExploredCoordinates[logNow.Car.Coordinate] = logNow;
            if (logNow.Car.hasFuelLeft()
                && !NoMoreSearch.Contains(logNow.Car.Coordinate)
                && !SearchQueueHashCodes.Contains(logNow.Car.Coordinate))
            {
                SearchQueue.Enqueue(logNow);
                SearchQueueHashCodes.Add(logNow.Car.Coordinate);
            }
        }

    }

    public class DriveLog
    {
        public static Coordinate Boundary { get; set; }

        public Car Car { get; }
        public bool hitFlag = false;
        public DriveLog? Previous { get; } = null;

        public static DriveLog? GoUp(DriveLog prev) => prev.Car.hasFuelLeft()? new DriveLog(new Car(prev.Car.Coordinate + new Coordinate(0, 1), prev.Car.Fuel - 1), prev) : null;
        public static DriveLog? GoDown(DriveLog prev) => prev.Car.hasFuelLeft() ? new DriveLog(new Car(prev.Car.Coordinate - new Coordinate(0, 1), prev.Car.Fuel - 1), prev) : null;
        public static DriveLog? GoRight(DriveLog prev) => prev.Car.hasFuelLeft() ? new DriveLog(new Car(prev.Car.Coordinate + new Coordinate(1, 0), prev.Car.Fuel - 1), prev) : null;
        public static DriveLog? GoLeft(DriveLog prev) => prev.Car.hasFuelLeft() ? new DriveLog(new Car(prev.Car.Coordinate - new Coordinate(1, 0), prev.Car.Fuel - 1), prev) : null;


        public bool IsInsideOfBoundary() => this.Car.Coordinate.IsInsideOf(Boundary);
        public DriveLog(Car car, DriveLog? prevRecord = null)
        {
            this.Car = car;
            this.Previous = prevRecord;
        }

        

        public override string ToString() => $"{{CarLog : " + 
                                                $"\r\n\t{Boundary}, " +
                                                $"\r\n\t{Car}, " +
                                                $"\r\n\t{hitFlag}" +
                                            $"\r\n}}";

        internal DriveLog UpLog() => new DriveLog(new Car(new Coordinate(this.Car.Coordinate + new Coordinate(0, 1)), this.Car.Fuel - 1));
        internal DriveLog DownLog() => new DriveLog(new Car(new Coordinate(this.Car.Coordinate - new Coordinate(0, 1)), this.Car.Fuel - 1));
        internal DriveLog LeftLog() => new DriveLog(new Car(new Coordinate(this.Car.Coordinate - new Coordinate(1, 0)), this.Car.Fuel - 1));
        internal DriveLog RightLog() => new DriveLog(new Car(new Coordinate(this.Car.Coordinate + new Coordinate(1, 0)), this.Car.Fuel - 1));

    }


    public class Solution
    {
        public static void Main(string[] args)
        {
            Solution sol = new Solution();
            int[,] tests = { 
                { 0,0,199997,1}, 
                { 5, 6, 7, 8 }
                };

            sol.solution(99999, 99999, tests);
        }

        public long solution(int n, int m, int[,] tests)
        {
            DriveLog.Boundary = new Coordinate(n, m);
            tests.GetLength(0); // 테스트수
            tests.GetLength(1); // 1~4 인수들..

            int index = 0;
            (Car Start, bool hitFlag) = (new Car(tests[index, 0], tests[index, 1], tests[index, 2]), tests[index, 3] == 1);

            LogCalculator calc = new LogCalculator(Start, hitFlag);
            calc.GetPossibleCoordinateList_OfSigns();
            Car test = new Car(1, 2, 3);            
            DriveLog testLog = new DriveLog(test);
            Console.WriteLine(test);
            Console.WriteLine(testLog.ToString());
            long answer = 0;
            return answer;
            /*
             * 좌표 (0,0) -> (n,m)
             * (a,b) > 표지판
             * 출발좌표, 남은연료량의 최대주행거리, 표지판도달여부
             * tests [x, y, d, flag]
             * 
             * 
             * 
             * 
             * 
            */
        }
    }
}