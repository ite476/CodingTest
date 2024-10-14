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

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Text.Json;

class Result
{

    /*
     * Complete the 'getTotalGoals' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. STRING team
     *  2. INTEGER year
     */
    


    public static int getTotalGoals(string team, int year)
    {
        // https://jsonmock.hackerrank.com/api/football_matches?year=<year>&team1=<team>&page=<page>
        // https://jsonmock.hackerrank.com/api/football_matches?year=<year>&team2=<team>&page=<page>

        // page 
        // per_page
        // total  : matches
        // total_pages
        // data
        //{ "competition":"UEFA Champions League",
        //  "year":2011,"round":"GroupH",
        //  "team1":"Barcelona",
        //  "team2":"AC Milan",
        //  "team1goals":"2",
        //  "team2goals":"2"}
        // competition
        // year
        // team1
        // team2
        // team1goals
        // team2goals        

        int wincount = 0;

        int totalPages01 = TotalPages01(team, year);        
        for (int i = 1; i <= totalPages01; i++)
        {
            wincount += WinCount01(i, team, year);
        }
        int totalPages02 = TotalPages02(team, year);
        for (int i = 1; i <= totalPages02; i++)
        {
            wincount += WinCount02(i, team, year);
        }

        return wincount;
    }

    private static int WinCount02(int i, string team, int year) => WinCounts02(RestURL(year, i, null, team));
    private static int WinCounts02(string url)
    {
        int wincount = 0;        

        var root = GetJsonElement(url);
        int count = CountThisPage(ref root);
        var data = Data(ref root);
        for (int x = 0; x < count; x++)
        {
            var now = data[x];
            if (WinsTeam2(ref now)) { wincount++; }
        }

        return wincount;
    }

    private static JsonElement GetJsonElement(string url)
    {
        string jsonString01 = callWebRequest(url);
        JsonDocument doc = JsonDocument.Parse(jsonString01);
        JsonElement root = doc.RootElement;
        return root;
    }

    private static int WinCount01(int i, string team, int year) => WinCounts01(RestURL(year, i, team, null));
    private static int WinCounts01(string url)
    {
        int wincount = 0;
        var root = GetJsonElement(url);

        int count = CountThisPage(ref root);
        var data = Data(ref root);
        for (int x = 0; x < count; x++)
        {
            var now = data[x];
            if (WinsTeam1(ref now)) { wincount++; }
        }

        return wincount;
    }

    private static bool WinsTeam2(ref JsonElement now) => T1Goals(ref now) < T2Goals(ref now);
    private static bool WinsTeam1(ref JsonElement now) => T1Goals(ref now) > T2Goals(ref now);
    private static int T1Goals(ref JsonElement now) => now.GetProperty("team1goals").GetInt32();
    private static int T2Goals(ref JsonElement now) => now.GetProperty("team2goals").GetInt32();    

    private static int CountThisPage(ref JsonElement json) => (Page(ref json) < TotalPages(ref json))? 10 : (Total(ref json) % 10);
    private static int Page(ref JsonElement json) => json.GetProperty("page").GetInt32();
    private static int TotalPages(ref JsonElement json) => json.GetProperty("total_pages").GetInt32();
    private static int Total(ref JsonElement json) => json.GetProperty("total").GetInt32();
    private static JsonElement[] Data(ref JsonElement json) => json.GetProperty("data").EnumerateArray().ToArray();
    


    private static int TotalPages01(string team, int year)
    {
        var root = GetJsonElement(RestURL(year, 1, team, null));
        return TotalPages(ref root);
    }

    private static int TotalPages02(string team, int year)
    {
        var root = GetJsonElement(RestURL(year, 1, null, team));
        return TotalPages(ref root);
    }

    public static string callWebRequest(string targetURL)
    {
        string responseFromServer = string.Empty;

        try
        {

            WebRequest request = WebRequest.Create(targetURL);
            request.Method = "GET";
            request.ContentType = "application/json";

            using (WebResponse response = request.GetResponse())
            using (Stream dataStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(dataStream))
            {
                responseFromServer = reader.ReadToEnd();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        return responseFromServer;
    }


    private static string RestURL(int year, int page, string team1 = null, string team2 = null)  
        => @"https://jsonmock.hackerrank.com/api/football_matches?"
        + $"year={year}"
        + ((team1 != null)? $"&team1={team1}" : "")
        + ((team2 != null)? $"&team2={team2}" : "")
        + $"&page={page}" ;

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string team = Console.ReadLine();

        int year = Convert.ToInt32(Console.ReadLine().Trim());

        int result = Result.getTotalGoals(team, year);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
