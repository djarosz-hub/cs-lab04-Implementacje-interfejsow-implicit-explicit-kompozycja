using System;
using System.Collections.Generic;
using System.Text;
using ver1;

namespace ClassLibrary1
{
    public class MultifunctionalDevice : Copier, IFax
    {
        public int FaxCounter { get; private set; }
        public List<string> RecieversList { get; private set; }
        public MultifunctionalDevice()
        {
            RecieversList = new List<string>();
        }
        public void Fax(string reciever)
        {

            if (string.IsNullOrEmpty(reciever))
                throw new ArgumentNullException();
            if (state == IDevice.State.on)
            {
                Scan(out IDocument document);
                SendFaxTo(reciever, document);
                FaxCounter++;
            }
        }
        public void SendFaxTo(string reciever, IDocument document)
        {
            if(!RecieversList.Contains(reciever))
                RecieversList.Add(reciever);
            Console.WriteLine($"Sending {document.GetFileName()} to {reciever}.");
        }
        public void GetRecieversList()
        {
            int len = RecieversList.Count;
            for (int i = 0; i < len; i++)
                Console.WriteLine($"{i + 1}. {RecieversList[i]}");
        }
    }
}
