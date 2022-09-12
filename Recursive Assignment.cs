//Author: Lyam Katz
//File Name: main.cs
//Project Name: MP2 - Grade 12
//Creation Date: 2021-05-29
//Modified Date: 2021-06-03
//Description: This program is designed to check if each word in a text file of words is a monkey word or not
/*
Monkey Word Rules
There are 2 rules that determine if a word is a valid Monkey Word or not:
1. A Monkey Word is a special word called an A Word, which may be optionally followed by the letter “N” and
then followed by a Monkey Word
2. An A-Word is either only a single letter “A”, OR the letter “B” followed by a Monkey Word followed by the
letter “S”
*/

using System;
using System.IO;
using System.Diagnostics;

class MainClass
{
    static Stopwatch stopWatch = new Stopwatch();

    public static void Main (string[] args)
    {
        try
        {
            int numMonkeyWords = 0;
       
            Console.Clear();

            StreamReader inFile = File.OpenText("input.txt");
            StreamWriter outFile = File.CreateText("Katz_L.txt");

            stopWatch.Reset();
            stopWatch.Start();

            while(!inFile.EndOfStream)
            {
                string word = inFile.ReadLine();

                if (MonkeyWord(word))
                {
                    outFile.WriteLine($"{word}:YES");
                    Console.WriteLine($"{word}:YES");
                    numMonkeyWords++;
                }
                else
                {
                    outFile.WriteLine($"{word}:NO");
                    Console.WriteLine($"{word}:NO");
                }
            }

            outFile.Write(numMonkeyWords);
            Console.WriteLine(numMonkeyWords);

            stopWatch.Stop();

            inFile.Close();
            outFile.Close();

            Console.Write(GetTimeOutput(stopWatch));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }    
    }

    private static bool MonkeyWord(string word)
    {
        if (AWord(word))
        {
            return true;
        }

        for (int i = 0; i < word.Length; i++)
        {
            if (word[i] == 'N' && AWord(word.Substring(0, i)) && MonkeyWord(word.Substring(i + 1)))
            {
                return true;
            }            
        }

        return false;
    }

    private static bool AWord(string word)
    {
        if (word == "A")
        {
            return true;
        }

        if (word.Length > 2 && word[0] == 'B' && word[word.Length - 1] == 'S' && MonkeyWord(word.Substring(1, word.Length - 2)))
        {
            return true;
        }

        return false;
    }

    public static string GetTimeOutput(Stopwatch timer)
    {
        TimeSpan ts = timer.Elapsed;
        int millis = ts.Milliseconds;
        int seconds = ts.Seconds;
        int minutes = ts.Minutes;
        int hours = ts.Hours;
        int days = ts.Days;

        return "Time- Days:Hours:Minutes:Seconds.Milliseconds:" + days + ":" + hours + ":" + minutes + ":" + seconds + "." + millis;
    }
}
