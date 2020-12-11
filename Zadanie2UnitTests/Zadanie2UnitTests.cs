using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using ver1;
using ClassLibrary1;

namespace Zadanie2UnitTests
{
    [TestClass]
    public class Zadanie2UnitTests
    {
        [TestMethod]
        public void MultiFunctionalDevice_GetState_StateOff()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOff();

            Assert.AreEqual(IDevice.State.off, fax.GetState());
        }
        [TestMethod]
        public void MultiFunctionalDevice_GetState_StateOn()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOn();

            Assert.AreEqual(IDevice.State.on, fax.GetState());
        }
        // weryfikacja, czy po wywo³aniu metody `Print` i w³¹czonej kopiarce w napisie pojawia siê s³owo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultiFunctionalDevice_Print_DeviceOn()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                fax.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `Print` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultiFunctionalDevice_Print_DeviceOff()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                fax.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `Scan` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultiFunctionalDevice_Scan_DeviceOff()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                fax.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `Scan` i wy³¹czonej kopiarce w napisie pojawia siê s³owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultiFunctionalDevice_Scan_DeviceOn()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                fax.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywo³anie metody `Scan` z parametrem okreœlaj¹cym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultiFunctionalDevice_Scan_FormatTypeDocument()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                fax.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                fax.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                fax.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywo³aniu metody `ScanAndPrint` i wy³¹czonej kopiarce w napisie pojawiaj¹ siê s³owa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultiFunctionalDevice_ScanAndPrint_DeviceOn()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                fax.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `ScanAndPrint` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Print`
        // ani s³owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultiFunctionalDevice_ScanAndPrint_DeviceOff()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                fax.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiFunctionalDevice_PrintCounter()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            fax.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            fax.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            fax.Print(in doc3);

            fax.PowerOff();
            fax.Print(in doc3);
            fax.Scan(out doc1);
            fax.PowerOn();

            fax.ScanAndPrint();
            fax.ScanAndPrint();

            // 5 wydruków, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(5, fax.PrintCounter);
        }

        [TestMethod]
        public void MultiFunctionalDevice_ScanCounter()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOn();

            IDocument doc1;
            fax.Scan(out doc1);
            IDocument doc2;
            fax.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            fax.Print(in doc3);

            fax.PowerOff();
            fax.Print(in doc3);
            fax.Scan(out doc1);
            fax.PowerOn();

            fax.ScanAndPrint();
            fax.ScanAndPrint();

            // 4 skany, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(4, fax.ScanCounter);
        }

        [TestMethod]
        public void MultiFunctionalDevice_PowerOnCounter()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOn();
            fax.PowerOn();
            fax.PowerOn();

            IDocument doc1;
            fax.Scan(out doc1);
            IDocument doc2;
            fax.Scan(out doc2);

            fax.PowerOff();
            fax.PowerOff();
            fax.PowerOff();
            fax.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            fax.Print(in doc3);

            fax.PowerOff();
            fax.Print(in doc3);
            fax.Scan(out doc1);
            fax.PowerOn();

            fax.ScanAndPrint();
            fax.ScanAndPrint();

            // 3 w³¹czenia
            Assert.AreEqual(3, fax.Counter);
        }
        [TestMethod]
        public void MultifunctionalDevice_Adding_Recievers_On_Recievers_List()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOn();
            fax.Fax("Test1");
            fax.Fax("Test2");
            fax.Fax("Test3");
            fax.Fax("Test4");
            Assert.AreEqual(4, fax.RecieversList.Count);
        }
        [TestMethod]
        public void MultifunctionalDevice_NotDoubling_Recievers_On_Recievers_List()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOn();
            fax.Fax("Test1");
            fax.Fax("Test1");
            fax.Fax("Test1");
            Assert.AreEqual(1, fax.RecieversList.Count);
        }
        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MultifunctionalDevice_Trying_Not_To_Type_Reciever_Of_Fax(string reciever)
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOn();
            fax.Fax(reciever);
        }
        [TestMethod]
        public void MultifunctionalDevice_Correct_FaxCounter()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOn();
            fax.Fax("test1");
            fax.Fax("test1");
            fax.Fax("test1");
            fax.Fax("test1");
            fax.Fax("test1");
            Assert.AreEqual(5, fax.FaxCounter);
        }
        [TestMethod]
        public void MultiFunctionalDevice_Fax_DeviceOff()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                fax.Fax("test");
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Sending"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
        [TestMethod]
        public void MultiFunctionalDevice_Fax_DeviceOn()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                fax.Fax("test");
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
