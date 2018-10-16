using System.IO;
using System.Xml.Linq;

namespace CodeTest.Models
{
    public class XMLParser
    {
        public string FolderPath = @"..\..\Data\Computers";
        XMLStringQueue stringQueue;

        public XMLParser(XMLStringQueue _xmlStringQueue)
        {
            stringQueue = _xmlStringQueue;
        }
        private string XDocumentToStringConverter(string file)
        {

            XDocument documuent = XDocument.Load(file);
            string xmlString = documuent.ToString();
            return xmlString;
        }
       
        public void GetFilesFromFolder()
        {
            foreach (string file in Directory.EnumerateFiles(@"..\..\Data\Computers"))
            {
                string xmlString = XDocumentToStringConverter(file);
                stringQueue.xmlConverterQueue.Enqueue(xmlString);
            }
        }
    }
}
