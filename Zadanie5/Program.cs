using System;
using ClassLibrary5;
namespace Zadanie5
{
    class Program
    {
        static void Main(string[] args)
        {
            Copier copier = new Copier();
            Console.WriteLine($"Current copier state: {copier.GetState()}");
            copier.PowerOn();
            Console.WriteLine($"Current copier state: {copier.GetState()}");
            var doc = new PDFDocument("doc.pdf");
            copier._Printer.Print(doc);
            copier._Printer.Print(doc);
            copier._Printer.Print(doc);
            //cooling down after 3 prints
            copier._Printer.Print(doc);
            copier._Scanner.Scan();
            copier._Scanner.Scan();
            //cooling down after 2 scans
            copier._Scanner.Scan();
            copier.GetCounter();
            Console.WriteLine($"Current copier state: {copier.GetState()}");
            copier.PowerOff();
            Console.WriteLine($"Current copier state: {copier.GetState()}");

        }
    }
}
