using System;
using System.Collections.Generic;

public class Solution
{
    Cell[,] Cells { get; } = new Cell[50, 50];
    Cell GetCell(int r, int c) => Cells[r - 1, c - 1];

    public string[] solution(string[] commands)
    {
        for (int r = 0; r < 50; r++)
        {
            for (int c = 0; c < 50; c++)
            {
                Cells[r, c] = new Cell(r, c);
            }
        }

        List<string> answer = new List<string>();

        for (int i = 0; i < commands.Length; i++)
        {
            DoCommand(commands[i], ref answer);
        }

        return answer.ToArray();

        //50x50 셀, 비어있음
        /// UPDATE r c value     -> 좌표 값 변경
        /// UPDATE value1 value2 -> 모든 값 변경
        /// MERGE r1 c1 r2 c2    -> 두 위치의 셀을 병합, 한 값으로 통일 -> 주소 레퍼런스 합침 (첫 값 우선)
        /// UNMERGE r c          -> 해당 위치의 모든 병합 해제
        /// PRINT r c            -> r, c위치의 값 출력

    }

    private void DoCommand(string command, ref List<string> answer)
    {
        string[] commands = command.Split(' ');
        string mainCommand = commands[0];
        int mainHash = mainCommand.GetHashCode();

        if (mainHash.IsHashCodeOf("UPDATE"))
        {
            Update(commands);
            return;
        }

        if (mainHash.IsHashCodeOf("MERGE"))
        {
            Merge(commands);
            return;
        }

        if (mainHash.IsHashCodeOf("UNMERGE"))
        {
            Unmerge(commands);
            return;
        }

        if (mainHash.IsHashCodeOf("PRINT"))
        {
            answer.Add(Print(commands));
            return;
        }

        throw new Exception("Undefined Command Recieved");
    }


    void Update(string[] commands)
    {
        switch (commands.Length)
        {
            case 4:
                Update(int.Parse(commands[1]),
                       int.Parse(commands[2]),
                       commands[3]);
                return;
            case 3:
                Update(commands[1], commands[2]);
                return;
            default:
                throw new Exception($"Wrong number of Commands For Update : {commands.Length}");
        }
    }
    void Update(int r, int c, string value) => GetCell(r, c).Value = value;
    void Update(string value1, string value2)
        => SelectAllCells(cell => cell.Value == value1).ChangeData(value2);
    private List<Cell> SelectAllCells(Func<Cell, bool> condition)
    {
        List<Cell> result = new List<Cell>();

        foreach (var cell in Cells)
        {
            if (cell.IsSelfPointing
                && condition(cell))
            {
                result.Add(cell);
            }
        }

        return result;
    }
    void Merge(string[] commands) => Merge(int.Parse(commands[1]),
                                           int.Parse(commands[2]),
                                           int.Parse(commands[3]),
                                           int.Parse(commands[4]));
    void Merge(int r1, int c1, int r2, int c2)
    {
        var cell1 = GetCell(r1, c1);
        var cell2 = GetCell(r2, c2);


        if (cell1.IsEmpty && cell2.IsEmpty == false)
        {
            cell1.SetCursorAs(cell2);
        }
        else
        {
            cell2.SetCursorAs(cell1);
        }
    }
    void Unmerge(string[] commands) => Unmerge(int.Parse(commands[1]),
                                               int.Parse(commands[2]));
    void Unmerge(int r, int c)
    {
        Console.WriteLine($"Unmerging {r},{c}...");
        var cell = GetCell(r, c);
        string data = cell.Value;
        Console.WriteLine(data);
        SelectAllCells(cl => cl.HasSameCursor(cell)).Reset();
        Console.WriteLine(data);
        cell.Value = data;
        Console.WriteLine(data);
    }
    string Print(string[] commands) => Print(int.Parse(commands[1]),
                                             int.Parse(commands[2]));
    string Print(int r, int c)
    {
        string answer = GetCell(r, c).Value;

        if (answer == null || answer.Length == 0)
        {
            answer = "EMPTY";
        }

        return answer;
    }
}

public static class SolutionExtensions
{
    public static void ChangeData(this List<Cell> list, string value)
    {
        foreach (var cell in list)
        {
            cell.Value = value;
        }
    }

    public static bool IsHashCodeOf(this int hashcode, string command)
    {
        return hashcode == command.GetHashCode();
    }

    public static void Reset(this List<Cell> list)
    {
        Console.WriteLine(list.Count);
        for (int i = 0; i < list.Count; i++)
        {
            Cell cell = list[i];
            Console.WriteLine($"{cell.CursorR},{cell.CursorC} : {cell.R}, {cell.C}");
            cell.Reset();
            Console.WriteLine($"{cell.CursorR},{cell.CursorC} : {cell.R}, {cell.C}");
        }
    }
}

public class Cell
{
    public int R { get; }
    public int C { get; }

    public string Value
    {
        get => DataBase.Data[CursorR, CursorC];
        set => DataBase.Data[CursorR, CursorC] = value;
    }

    public int CursorR { get; set; }
    public int CursorC { get; set; }
    public bool IsSelfPointing
    {
        get => R == CursorR && C == CursorC;
    }

    public bool IsEmpty
    {
        get => Value == null || Value == string.Empty;
    }

    public Cell(int r, int c)
    {
        this.R = r;
        this.C = c;
        Reset();
    }

    public void Reset()
    {
        this.CursorR = R;
        this.CursorC = C;
        //Console.WriteLine($"Cell[{R},{C}] now Pointing [{CursorR},{CursorC}] -> {string.Empty}");
        this.Value = string.Empty;
    }

    public void SetCursorAs(Cell cell) {
        this.CursorR = cell.CursorR;
        this.CursorC = cell.CursorC;
    }
    public bool HasSameCursor(Cell cell) => (this.CursorR == cell.CursorR) && (this.CursorC == cell.CursorC);
}

public static class DataBase
{
    public static string[,] Data { get; set; } = new string[50, 50];
}