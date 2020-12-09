using ClassLibrary1;
using System;
using ver1;

namespace Zadanie1
{
    class Program
    {
        static void Main(string[] args)
        {
            var xerox = new Copier();
            Console.WriteLine(xerox.Counter);
            Console.WriteLine(xerox.GetState());
            xerox.PowerOn();
            Console.WriteLine(xerox.Counter);
            Console.WriteLine(xerox.GetState());

            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2, IDocument.FormatType.PDF);

            xerox.ScanAndPrint();
            System.Console.WriteLine(xerox.Counter);
            System.Console.WriteLine(xerox.PrintCounter);
            System.Console.WriteLine(xerox.ScanCounter);
        }
    }
}
