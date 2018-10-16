using System.IO;
using System.Xml.Linq;

namespace XmlHtmlConversion.Models
{
    public class XMLParser
    {
        public string FolderPath = @"..\..\Data\Computers";
        XMLStringQueue stringQueue;

        public XMLParser(XMLStringQueue _xmlStringQueue)
        {
            stringQueue = _xmlStringQueue;
        }
        public void GetFilesFromFolder()
        {
            foreach (string file in Directory.EnumerateFiles(FolderPath))
            {
                string xmlString = XDocumentToStringConverter(file);
                stringQueue.xmlConverterQueue.Enqueue(xmlString);
            }
        }
        private string XDocumentToStringConverter(string file)
        {

            XDocument documuent = XDocument.Load(file);
            string xmlString = documuent.ToString();
            return xmlString;
        }
    }
}
