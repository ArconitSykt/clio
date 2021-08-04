using System;
using System.Net;
using HtmlAgilityPack;
using System.IO;

namespace klio
{
    class Program
    {
        static void Main(string[] args)
        {

            System.Console.WriteLine("Путь до SRC.txt (например C:\\work)");
            var link = Console.ReadLine();
            System.Console.WriteLine("Путь: " + link + "\\SRC.txt");
            System.Console.WriteLine("Начать? ДА - Y, нет - n");
            if (Console.ReadLine() == "Y")
            {
                rFile(link, "\\SRC.txt");
            }
            Console.WriteLine("==Finish==");


            async void rFile(string linkFile, string fix)
            {
                string[] lines = System.IO.File.ReadAllLines(linkFile + fix);
                System.Console.WriteLine("Start...");
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                    using StreamWriter file = new(linkFile + "\\RES.txt", append: true);
                    await file.WriteLineAsync(getTitle(line).Trim(new Char[] { ' ', '\n', '\r' }));
                }
            }

            string getTitle(string url)
            {
                var html = url;
                HtmlWeb web = new HtmlWeb();
                try
                {
                    var htmlDoc = web.Load("http://" + html);
                    var headlineText = htmlDoc.DocumentNode.SelectSingleNode("(//head//title)[1]").InnerText;
                    return (html + ";" + headlineText);
                }
                catch (WebException)
                {
                    return html;
                }
                catch (UriFormatException)
                {
                    return html;
                }
                catch (NullReferenceException)
                {
                    return html;
                }
            }

        }
    }
}
