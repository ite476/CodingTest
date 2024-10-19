using System;
using System.Collections.Generic;
using System.Linq;

namespace _Lv._2__삼각_달팽이
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sol = new Solution();

            var answer = sol.solution(5);

            Console.WriteLine(answer.Select(x => $"{x}").Aggregate((a, b) => $"{a}, {b}"));
            Console.ReadLine();

        }
    }

    public class Solution
    {
        public int N { get; set; }
        public List<List<int>> Pyramid { get; set; }

        public int[] solution(int n)
        {
            N = n;

            Pyramid = new List<List<int>>();

            foreach (var rank in Enumerable.Range(0, n))
            {
                var row = new List<int>();
                
                foreach (var i in Enumerable.Range(0, rank + 1))
                {
                    row.Add(0);
                }

                Pyramid.Add(row);
            }

            var cursor = new Coordinate()
            {
                Depth = 0,
                Index = 0,
            };

            int value = 1;
            int lastValue = Pyramid.Sum(x => x.Count);
            var direction = HeadingDirection.Down;

            while (value <= lastValue) 
            {
                if (Pyramid[cursor.Depth][cursor.Index] == 0)
                {
                    Pyramid[cursor.Depth][cursor.Index] = value;
                    value += 1;
                }

                if (value > lastValue) break;

                cursor = NextCursor(cursor, ref direction);
            }

            var answer = Pyramid.SelectMany(x => x).ToArray();

            return answer;
        }

        public Coordinate NextCursor(
            Coordinate cursor, 
            ref HeadingDirection direction)
        {
            while (true)
            {
                Coordinate newCursor;

                switch (direction)
                {
                    case HeadingDirection.Down:
                        newCursor = new Coordinate()
                        {
                            Depth = cursor.Depth + 1,
                            Index = cursor.Index,
                        };

                        if (Validate(newCursor)) return newCursor;
                        
                        break;

                    case HeadingDirection.Right:
                        newCursor = new Coordinate()
                        {
                            Depth = cursor.Depth,
                            Index = cursor.Index + 1,
                        };

                        if (Validate(newCursor)) return newCursor;

                        break;

                    case HeadingDirection.Up:
                        newCursor = new Coordinate()
                        {
                            Depth = cursor.Depth - 1,
                            Index = cursor.Index - 1,
                        };

                        if (Validate(newCursor)) return newCursor;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                direction = NextDirection(direction);
            }
            
        }

        public bool Validate(Coordinate coordinate)
        {
            return coordinate.Depth < N
                && coordinate.Index <= coordinate.Depth
                && coordinate.Depth >= 0
                && coordinate.Index >= 0
                && Pyramid[coordinate.Depth][coordinate.Index] == 0;
        }

        public HeadingDirection NextDirection(HeadingDirection direction)
        {
            return (HeadingDirection)((int)(direction + 1) % 3);
        }


        public class Coordinate
        {
            public int Depth { get; set; }
            public int Index { get; set; }
        }

        public enum HeadingDirection
        {
            Down = 0,
            Right = 1,
            Up = 2,
        }
    }

    /*
    public class Solution
    {
        public int[] solution(int n)
        {
            List<List<Tile>> tileRowList = new List<List<Tile>>()
            {
                new List<Tile> { new Tile() },
            };

            foreach (var depth in Enumerable.Range(0, n - 1))
            {
                var thisRow = tileRowList[depth];
                var nextRow = new List<Tile>();
                tileRowList.Add(nextRow);

                var firstOne = thisRow[0];
                nextRow.Add(firstOne.CreateChild(toLeft: true));

                foreach (var tile in thisRow)
                {
                    nextRow.Add(tile.CreateChild(toLeft: false));
                }
            }

            foreach (var row in tileRowList)
            {
                var cursor = row[0];
                foreach (var tile in row.Skip(1))
                {
                    cursor.AddFriend(tile);
                    cursor = tile;
                }
            }

            var root = tileRowList[0][0];
            var totalTiles = tileRowList.Sum(r => r.Count);
            root.Snail(totalTiles);

            var answer = tileRowList.SelectMany(x => x.Select(t => t.Value ?? totalTiles)).ToArray();

            return answer;
        }
    }

    public enum Direction
    {
        LeftLower,
        Right,
        LeftUpper,

        RightUpper,
        RightLower,
    }

    public class Tile
    {
        public Dictionary<Direction, Tile> TileFamily { get; set; } = new Dictionary<Direction, Tile>();

        public int? Value { get; set; } = null;

        public Tile CreateChild(bool toLeft)
        {
            var child = new Tile();

            if (toLeft)
            {
                TileFamily[Direction.LeftLower] = child;
                child.TileFamily[Direction.RightUpper] = this;
            }
            else
            {
                TileFamily[Direction.RightLower] = child;
                child.TileFamily[Direction.LeftUpper] = this;
            }

            return child;
        }

        public void AddFriend(Tile tile)
        {
            TileFamily[Direction.Right] = tile;
        }

        public void Snail(int goal)
        {
            var value = 1;
            Value = value;
            value++;

            var direction = Direction.LeftLower;

            Tile cursor = this;

            while (value < goal)
            {
                if (cursor.TileFamily.TryGetValue(direction, out var nextTile))
                {
                    if (nextTile.Value == null)
                    {
                        nextTile.Value = value;
                        cursor = nextTile;
                        value++;
                    }
                    else
                    {
                        direction = NextDirection(direction);
                    }
                }
                else
                {
                    direction = NextDirection(direction);
                }
            }
        }

        public Direction NextDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.LeftLower:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.LeftUpper;
                case Direction.LeftUpper:
                    return Direction.LeftLower;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        
    }
    */
}
