/*﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;*/
/*
            1. Найти все слова в тексте, состоящие из 4 букв 
            2. Найти в тексте все междометия, обозначающие смех («хихи», «хаха», «хохо» и тд.) https://git.io/fjtI2 
            3. Найти в тексте время в формате «ЧЧ:ММ» 
            4. Выделить слова, в которых есть названия нот. Например, «рекурсия» 
             */
/*namespace TextTasks
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
*/

using System;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

/*           
            5. Получить содержимое сайта sinoptik.ua, показать на экране консоли текущую температуру воздуха в вашем городе 
            6. показать на экране консоли курс валют в вашем городе

 */


/*class Program
{
    public static async Task Main(string[] args)
    {

        var playwright = await Playwright.CreateAsync(); // Запускаю браузер
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
        var page = await browser.NewPageAsync();


        await page.GotoAsync("https://sinoptik.ua/ru/pohoda/odesa"); // Переход на сайт с погодой

        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        var pageHTML = await page.ContentAsync();

        using (StreamWriter writer = new StreamWriter("output.html", false, Encoding.UTF8))
        {
            await writer.WriteAsync(pageHTML);
        }
        //Console.WriteLine(pageHTML);
        // забираю температуру только с картинки. она специфическая, формат для поиска уникальный
        string pattern = @"Одесса: \d{1,2}:\d{2}";
        // MatchCollection matches = Regex.Matches(pageHTML, pattern, RegexOptions.IgnoreCase);
        Match match = Regex.Match(pageHTML, pattern, RegexOptions.IgnoreCase);
        if (match.Success)
        {
            Console.WriteLine(match.Value);
            int index = match.Index;
            pageHTML = pageHTML.Substring(index);

            string pattern1 = @"(\+|-)\d{1,2}°C";
            match = Regex.Match(pageHTML, pattern1);
            if (match.Success)
            {
                Console.WriteLine(match.Value);
            }
        }
        await browser.CloseAsync();// Закрываю браузер
    }
}*/


class Program
{
    static async Task Main(string[] args)
    {
        string url = "https://minfin.com.ua/ua/currency/";
        string city = "Одесса";
        string[] currency = { "USD", "EUR" };
        string cont  = await GetCurrencyAsync(url, currency);
        Console.WriteLine(cont);
    }

    public static async Task<string> GetCurrencyAsync(string url, string[] currency)
    {
        using (HttpClient client = new HttpClient())
        {
            
            HttpResponseMessage response = await client.GetAsync(url);// Отправка GET запроса на сайт
            response.EnsureSuccessStatusCode();

            
            string content = await response.Content.ReadAsStringAsync();// Получение содержимого страницы

            //Console.WriteLine(content);

            //HtmlDocument doc = new HtmlDocument();
            // doc.LoadHtml(content);
            string ret = "";
            for(int i = 0; i < currency.Length; i++)
            {
                string pattern = $@"{currency[i]}";

                Match match = Regex.Match(content, pattern, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    string subcont = content.Substring(match.Index);
                    //Console.WriteLine(content);
                    pattern = @"\d{1,3}\.\d{1,4}";
                    match = Regex.Match(subcont, pattern);
                    if (match.Success)
                        ret+= currency[i] + " " + match.Value + "\n";
                }
            }            

            return ret;
        }
    }
}
