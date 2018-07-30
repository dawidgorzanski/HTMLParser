using HTMLParser.Model;
using System;
using System.Diagnostics;
using System.IO;

namespace HTMLParserConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var html = File.ReadAllText(@"C:\Users\dawid\Desktop\chartexample.html");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var document = HtmlDocument.Load(html);
            watch.Stop();
            Console.WriteLine($"Parsing time: {watch.ElapsedMilliseconds} \n\n{document}");
            File.WriteAllText(@"C:\Users\dawid\Desktop\chartexample2.html", document.ToString());
            //Console.ReadKey();
        }
    }
}
