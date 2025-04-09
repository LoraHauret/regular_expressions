using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
/*
            1. Найти все слова в тексте, состоящие из 4 букв 
            2. Найти в тексте все междометия, обозначающие смех («хихи», «хаха», «хохо» и тд.) https://git.io/fjtI2 
            3. Найти в тексте время в формате «ЧЧ:ММ» 
            4. Выделить слова, в которых есть названия нот. Например, «рекурсия» 
             */
namespace TextTasks
{
    class Tasks
    {
        public void Task1()
        {
            string path = @"..\..\laughter.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                string text;
                while ((text = sr.ReadLine()) != null)
                {
                    string pattern = @"\b\w{4}\b";
                    MatchCollection matches = Regex.Matches(text, pattern);
                    foreach (Match match in matches)
                    {
                        Console.WriteLine(match);
                    }
                }
            }
        }

        public void Task2()
        {
            string path = @"..\..\laughter.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                string text;
                while ((text = sr.ReadLine()) != null)
                {
                    string pattern = @"\b(hi|ho|he|ha)(\s*-\s*(hi|ho|he|ha))*\b";
                    MatchCollection matches = Regex.Matches(text, pattern);
                    foreach (Match match in matches)
                    {
                        Console.WriteLine(match);
                    }
                }

            }
        }

        public void Task3()
        {
            string path = @"..\..\laughter.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                string text;
                while ((text = sr.ReadLine()) != null)
                {
                    string pattern = @"\b\d{1,2}:\d{2}\b";
                    MatchCollection matches = Regex.Matches(text, pattern);
                    foreach (Match match in matches)
                    {
                        Console.WriteLine(match);
                    }
                }
            }
        }
        public void Task4()
        {
            string path = @"..\..\Notes.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                string text = sr.ReadToEnd();
                if(text.Length > 0)
                {
                    string pattern = @"до|ре|ми|фа|соль|ля|си";
                    MatchCollection matches = Regex.Matches(text, pattern);
                    string[] notes = { "до", "ре", "ми", "фа", "соль", "ля", "си" };

                    for(int i = 0; i < notes.Length; i++)
                    {
                        text = text.Replace(notes[i], notes[i].ToUpper());
                    }
                    Console.WriteLine(text);

                    Console.WriteLine("\nНота   Индекс");
                    foreach (Match match in matches)
                    {
                        Console.WriteLine(match  + "\t" + match.Index.ToString());
                    }

                }
            }
        }
    }

        internal class Program
    {
        static void Main(string[] args)
        {
             Tasks task = new Tasks();
           // task.Task1();
           // task.Task2();
            //task.Task3();
            task.Task4();
        }
    }
}
