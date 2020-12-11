using System;
using ClassLibrary3;

namespace Zadanie3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---Copier test---\n");
            Copier x = new Copier();
            Console.WriteLine($"copier counter: {x.Counter}");
            Console.WriteLine($"print counter: {x.PrintCounter}");
            Console.WriteLine(x.GetState());
            x.PowerOn();
            Console.WriteLine(x.GetState());
            Console.WriteLine("printing doc1.pdf");
            IDocument doc1 = new PDFDocument("doc1.pdf");
            x.Print(doc1);
            Console.WriteLine("scanning doc.txt");
            x.Scan(IDocument.FormatType.TXT);
            Console.WriteLine("scanning doc.jpg");
            x.Scan(IDocument.FormatType.JPG);

            Console.WriteLine($"copier counter: {x.Counter}");
            Console.WriteLine($"print counter: {x.PrintCounter}");
            Console.WriteLine($"scan counter: {x.ScanCounter}");

            Console.WriteLine("scanning and printing doc.pdf");
            x.ScanAndPrint(IDocument.FormatType.PDF);
            Console.WriteLine($"print counter: {x.PrintCounter}");
            Console.WriteLine($"scan counter: {x.ScanCounter}\n");
            Console.WriteLine("---MultidimensionalDevice test---\n");
            MultidimensionalDevice multi = new MultidimensionalDevice();
            Console.WriteLine(multi.GetState());
            multi.PowerOn();
            Console.WriteLine(multi.GetState());
            Console.WriteLine($"multi print counter: {multi.PrintCounter}");
            Console.WriteLine($"multi scan counter: {multi.ScanCounter}");
            multi.Print(doc1);
            Console.WriteLine("scanning doc.txt");
            multi.Scan(IDocument.FormatType.TXT);
            Console.WriteLine("scanning doc.jpg");
            multi.Scan(IDocument.FormatType.JPG);
            Console.WriteLine($"multi print counter: {multi.PrintCounter}");
            Console.WriteLine($"multi scan counter: {multi.ScanCounter}");
            Console.WriteLine("faxing .txt document to testRecievier");
            multi.FaxDocument("testReciever", IDocument.FormatType.TXT);
            Console.WriteLine("faxing .pdf document to testRecievier2");
            multi.FaxDocument("testReciever2", IDocument.FormatType.PDF);
            Console.WriteLine($"multi counter: {multi.Counter}");
            Console.WriteLine($"multi print counter: {multi.PrintCounter}");
            Console.WriteLine($"multi scan counter: {multi.ScanCounter}");
            Console.WriteLine($"multi fax counter: {multi.FaxCounter}");
        }
    }
}
