using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

static class Extensions
{
    public static bool IsOdd(this int n) => n % 2 > 0;
    public static bool IsEven(this int n) => !n.IsOdd();
}

class Result
{

    /*
     * Complete the 'bomberMan' function below.
     *
     * The function is expected to return a STRING_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. STRING_ARRAY grid
     */

    public static List<string> bomberMan(int n, List<string> grid)
    {
        var Field = new MineField(grid);
        for(int i = 0; i < n; i++)
        {
            Field.TimeTick();
        }

        return Field.ToAnswer();
    }

}

class MineField
{
    private Cell[,] Field { get; set; }
    private int Width { get; set; }
    private int Height { get; set; }
    private int Tick { get; set; } = 0;


    public Cell this[int x, int y] 
    { 
        get => Field[x, y]; 
        set => Field[x, y] = value;
    }


    public MineField(List<string> grid)
    {
        Height = grid.Count;
        Width = grid[0].Length;

        Field = new Cell[Width, Height];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (grid[y][x] == 'O') this[x, y] = Cell.Bomb;
                else this[x, y] = Cell.Nothing;
            }
        }
    }


    public void TimeTick()
    {
        Tick++;
        BombsExplode();
        PlantBombs();
    }
    private void BombsExplode()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                var cell = this[x, y];
                if (cell.TimeTick() == Cell.Action.Explode)
                {
                    ExplodeAdjacent(x, y);
                }
            }
        }
    }
    private void PlantBombs()
    {
        if (Tick.IsEven())
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var cell = this[x, y];
                    cell.PlantBomb();
                }
            }
        }
    }

    

    private void ExplodeAdjacent(int x, int y)
    {
        DeathNote = new bool[Width, Height];

        Explode(x, y);
        Explode(x - 1, y);
        Explode(x, y - 1);
        Explode(x + 1, y);
        Explode(x, y + 1);

        ExecuteDeathNote();
    }

    private void ExecuteDeathNote()
    {
        for(int y = 0; y < Height; y++)
        {
            for(int x = 0; x < Width; x++)
            {
                if (DeathNote[x, y])
                {
                    this[x, y] = Cell.Nothing;
                }
            }
        }
    }

    bool[,] DeathNote { get; set; }

    private void Explode(int x, int y)
    {
        if (IsInsideBoundary(x, y)) { DeathNote[x, y] = true; }
    }
    private bool IsInsideBoundary(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;

    private void ExplodeChecking(int x, int y)
    {
        if (IsInsideBoundary(x, y) && this[x,y].IsAboutToExplode() == false)
        { this[x,y] = Cell.Nothing; }
    }


    public List<string> ToAnswer()
    {
        List<string> answer = new List<string>();
        for (int y = 0; y < Height; y++)
        {
            string now = "";
            for (int x = 0; x < Width; x++)
            {
                now += this[x, y].ToLetter();
            }
            Console.WriteLine(now);
            answer.Add(now);
        }
        return answer;
    }
}

class Cell
{
    public enum Action
    {
        Explode, Nothing
    }

    public enum CellObject
    {
        Bomb, Nothing
    }

    public CellObject Thing { get; set; }
    public int CountDown { get; set; }


    public static Cell Bomb => new Cell(CellObject.Bomb, 3);
    public static Cell Nothing => new Cell(CellObject.Nothing, 0);


    Cell() : this(CellObject.Nothing, 0) { }
    Cell(CellObject thing, int countDown)
    {
        Thing = thing;
        CountDown = countDown;
    }

    public string ToLetter() => (this.Thing == CellObject.Bomb) ? "O" : ".";
    public Action TimeTick()
    {
        if (this.Thing == CellObject.Bomb)
        {
            CountDown--;
            if (CountDown == 0) { return Action.Explode; }
            else { return Action.Nothing; }
        }

        return Action.Nothing;
    }
    public bool IsAboutToExplode() => this.Thing == CellObject.Bomb && CountDown == 1;
    public void PlantBomb()
    {
        if (this.Thing == CellObject.Nothing)
        {
            this.Thing = CellObject.Bomb;
            this.CountDown = 3;
        }
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int r = Convert.ToInt32(firstMultipleInput[0]);

        int c = Convert.ToInt32(firstMultipleInput[1]);

        int n = Convert.ToInt32(firstMultipleInput[2]);

        List<string> grid = new List<string>();

        for (int i = 0; i < r; i++)
        {
            string gridItem = Console.ReadLine();
            grid.Add(gridItem);
        }

        List<string> result = Result.bomberMan(n, grid);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
