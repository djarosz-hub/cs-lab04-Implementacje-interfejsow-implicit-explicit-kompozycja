using System;
using ver1;
using ClassLibrary1;

namespace Zadanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            var fax = new MultifunctionalDevice();
            Console.WriteLine(fax.Counter);
            Console.WriteLine(fax.GetState());
            fax.PowerOn();
            Console.WriteLine(fax.Counter);
            Console.WriteLine(fax.GetState());

            IDocument doc1 = new PDFDocument("aaa.pdf");
            fax.Print(in doc1);

            IDocument doc2;
            fax.Scan(out doc2, IDocument.FormatType.PDF);

            fax.ScanAndPrint();

            fax.Fax("testReciever");


            Console.WriteLine($"Fax on/off counter: {fax.Counter}");
            Console.WriteLine($"Print counter: {fax.PrintCounter}");
            Console.WriteLine($"Scan counter: {fax.ScanCounter}");
            Console.WriteLine($"Fax counter: {fax.FaxCounter}");
            Console.WriteLine("Fax recievers list:");
            fax.GetRecieversList();
            Console.WriteLine(fax.RecieversList.Count);

        }
    }
}
