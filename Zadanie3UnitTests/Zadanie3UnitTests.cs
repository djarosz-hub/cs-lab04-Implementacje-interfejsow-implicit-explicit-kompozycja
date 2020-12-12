using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary3;
using System.IO;
using System;

namespace Zadanie3UnitTests
{

    [TestClass]
    public class Zadanie3UnitTests
    {
        [TestMethod]
        public void Copier_GetState_StateOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            Assert.AreEqual(IDevice.State.off, copier.GetState());
        }

        [TestMethod]
        public void Copier_GetState_StateOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            Assert.AreEqual(IDevice.State.on, copier.GetState());
        }


        // weryfikacja, czy po wywo³aniu metody `Print` i w³¹czonej kopiarce w napisie pojawia siê s³owo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Print_DeviceOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.Print(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `Print` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Print_DeviceOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.Print(doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `Scan` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Scan_DeviceOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(IDocument.FormatType.PDF);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `Scan` i w³¹czonej kopiarce w napisie pojawia siê s³owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Scan_DeviceOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywo³anie metody `Scan` z parametrem okreœlaj¹cym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void Copier_Scan_FormatTypeDocument()
        {
            var copier = new Copier();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                copier.Scan(IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                copier.Scan(IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywo³aniu metody `ScanAndPrint` i wy³¹czonej kopiarce w napisie pojawiaj¹ siê s³owa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_ScanAndPrint_DeviceOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                copier.ScanAndPrint(IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `ScanAndPrint` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Print`
        // ani s³owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_ScanAndPrint_DeviceOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                copier.ScanAndPrint(IDocument.FormatType.PDF);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void Copier_PrintCounter()
        {
            var copier = new Copier();
            copier.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            copier.Print(doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            copier.Print(doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(doc3);

            copier.PowerOff();
            copier.Print(doc3);
            copier.Scan(IDocument.FormatType.PDF);
            copier.PowerOn();

            copier.ScanAndPrint(IDocument.FormatType.JPG);
            copier.ScanAndPrint(IDocument.FormatType.TXT);

            // 5 wydruków, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(5, copier.PrintCounter);
        }

        [TestMethod]
        public void Copier_ScanCounter()
        {
            var copier = new Copier();
            copier.PowerOn();

            IDocument doc1;
            copier.Scan(IDocument.FormatType.PDF);
            IDocument doc2;
            copier.Scan(IDocument.FormatType.JPG);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(doc3);

            copier.PowerOff();
            copier.Print(doc3);
            copier.Scan(IDocument.FormatType.PDF);
            copier.PowerOn();

            copier.ScanAndPrint(IDocument.FormatType.TXT);
            copier.ScanAndPrint(IDocument.FormatType.PDF);

            // 4 skany, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(4, copier.ScanCounter);
        }

        [TestMethod]
        public void Copier_PowerOnCounter()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.PowerOn();
            copier.PowerOn();

            IDocument doc1;
            copier.Scan(IDocument.FormatType.JPG);
            IDocument doc2;
            copier.Scan(IDocument.FormatType.PDF);

            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(doc3);

            copier.PowerOff();
            copier.Print(doc3);
            copier.Scan(IDocument.FormatType.PDF);
            copier.PowerOn();

            copier.ScanAndPrint(IDocument.FormatType.TXT);
            copier.ScanAndPrint(IDocument.FormatType.JPG);

            // 3 w³¹czenia
            Assert.AreEqual(3, copier.Counter);
        }
        [TestMethod]
        public void MultidimensionalDevice_GetState_StateOff()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOff();

            Assert.AreEqual(IDevice.State.off, fax.GetState());
        }
        [TestMethod]
        public void MultidimensionalDevice_GetState_StateOn()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();

            Assert.AreEqual(IDevice.State.on, fax.GetState());
        }
        // weryfikacja, czy po wywo³aniu metody `Print` i w³¹czonej kopiarce w napisie pojawia siê s³owo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Print_DeviceOn()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                fax.Print(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `Print` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Print_DeviceOff()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                fax.Print(doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `Scan` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Scan_DeviceOff()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                fax.Scan(IDocument.FormatType.JPG);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `Scan` i w³¹czonej kopiarce w napisie pojawia siê s³owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Scan_DeviceOn()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                fax.Scan(IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywo³anie metody `Scan` z parametrem okreœlaj¹cym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultidimensionalDevice_Scan_FormatTypeDocument()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                fax.Scan(IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                fax.Scan(IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                fax.Scan(IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywo³aniu metody `ScanAndPrint` i w³¹czonej kopiarce w napisie pojawiaj¹ siê s³owa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_ScanAndPrint_DeviceOn()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                fax.ScanAndPrint(IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `ScanAndPrint` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Print`
        // ani s³owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_ScanAndPrint_DeviceOff()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                fax.ScanAndPrint(IDocument.FormatType.JPG);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_PrintCounter()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            fax.Print(doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            fax.Print(doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            fax.Print(doc3);

            fax.PowerOff();
            fax.Print(doc3);
            fax.Scan(IDocument.FormatType.JPG);
            fax.PowerOn();

            fax.ScanAndPrint(IDocument.FormatType.TXT);
            fax.ScanAndPrint(IDocument.FormatType.TXT);

            // 5 wydruków, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(5, fax.PrintCounter);
        }

        [TestMethod]
        public void MultidimensionalDevice_ScanCounter()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();

            IDocument doc1;
            fax.Scan(IDocument.FormatType.TXT);
            IDocument doc2;
            fax.Scan(IDocument.FormatType.TXT);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            fax.Print(doc3);

            fax.PowerOff();
            fax.Print(doc3);
            fax.Scan(IDocument.FormatType.TXT);
            fax.PowerOn();

            fax.ScanAndPrint(IDocument.FormatType.TXT);
            fax.ScanAndPrint(IDocument.FormatType.TXT);

            // 4 skany, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(4, fax.ScanCounter);
        }

        [TestMethod]
        public void MultidimensionalDevice_PowerOnCounter()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();
            fax.PowerOn();
            fax.PowerOn();

            IDocument doc1;
            fax.Scan(IDocument.FormatType.TXT);
            IDocument doc2;
            fax.Scan(IDocument.FormatType.TXT);

            fax.PowerOff();
            fax.PowerOff();
            fax.PowerOff();
            fax.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            fax.Print(doc3);

            fax.PowerOff();
            fax.Print(doc3);
            fax.Scan(IDocument.FormatType.TXT);
            fax.PowerOn();

            fax.ScanAndPrint(IDocument.FormatType.TXT);
            fax.ScanAndPrint(IDocument.FormatType.TXT);

            // 3 w³¹czenia
            Assert.AreEqual(3, fax.Counter);
        }
        [TestMethod]
        public void MultidimensionalDevice_Adding_Recievers_On_Recievers_List()
        {
            var multiDev = new MultidimensionalDevice();
            multiDev.PowerOn();
            multiDev.FaxDocument("testReciever1", IDocument.FormatType.TXT);
            multiDev.FaxDocument("testReciever2", IDocument.FormatType.TXT);
            multiDev.FaxDocument("testReciever3", IDocument.FormatType.TXT);
            multiDev.FaxDocument("testReciever4", IDocument.FormatType.TXT);
            Assert.AreEqual(4, multiDev._Fax.RecieversList.Count);
        }
        [TestMethod]
        public void MultidimensionalDevice_NotDoubling_Recievers_On_Recievers_List()
        {
            var multiDev = new MultidimensionalDevice();
            multiDev.PowerOn();
            multiDev.FaxDocument("testReciever", IDocument.FormatType.TXT);
            multiDev.FaxDocument("testReciever", IDocument.FormatType.TXT);
            multiDev.FaxDocument("testReciever", IDocument.FormatType.TXT);
            Assert.AreEqual(1, multiDev._Fax.RecieversList.Count);
        }
        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MultidimensionalDevice_Trying_Not_To_Type_Reciever_Of_Fax(string reciever)
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();
            fax.FaxDocument(reciever,IDocument.FormatType.JPG);
        }
        [TestMethod]
        public void MultidimensionalDevice_Correct_FaxCounter()
        {
            var multiDev = new MultidimensionalDevice();
            multiDev.PowerOn();
            multiDev.FaxDocument("testReciever", IDocument.FormatType.TXT);
            multiDev.FaxDocument("testReciever", IDocument.FormatType.TXT);
            multiDev.FaxDocument("testReciever", IDocument.FormatType.TXT);
            multiDev.FaxDocument("testReciever", IDocument.FormatType.TXT);
            multiDev.FaxDocument("testReciever", IDocument.FormatType.TXT);
            Assert.AreEqual(5, multiDev.FaxCounter);
        }
        [TestMethod]
        public void MultidimensionalDevice_Fax_DeviceOff()
        {
            var multiDev = new MultidimensionalDevice();
            multiDev.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multiDev.FaxDocument("testReciever", IDocument.FormatType.TXT);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Sending"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
        [TestMethod]
        public void MultidimensionalDevice_Fax_DeviceOn()
        {
            var multiDev = new MultidimensionalDevice();
            multiDev.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multiDev.FaxDocument("testReciever",IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Sending"));

            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

    }
    public class ConsoleRedirectionToStringWriter : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }
}
