using System;
using ClassLibrary4;
namespace Zadanie4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Copier x = new Copier();
            Console.WriteLine($"Current copier state: {x.GetState()}");
            ((IDevice)x).PowerOn();
            Console.WriteLine($"Current copier state: {x.GetState()}");

            var doc = new PDFDocument("doc.pdf");
            ((IPrinter)x).Print(doc);
            ((IPrinter)x).Print(doc);
            ((IPrinter)x).Print(doc);
            //cooling down after 3 prints
            ((IPrinter)x).Print(doc);
            ((IPrinter)x).Print(doc);
            IDocument doc1;
            IDocument doc2;
            IDocument doc3;
            ((IScanner)x).Scan(out doc1);
            ((IScanner)x).Scan(out doc2);
            //cooling down after 2 scans
            ((IScanner)x).Scan(out doc3);
            x.GetCounter();
            Console.WriteLine($"Current copier state: {x.GetState()}");
            ((IDevice)x).PowerOff();
            Console.WriteLine($"Current copier state: {x.GetState()}");





        }
    }
}
