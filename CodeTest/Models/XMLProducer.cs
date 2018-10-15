using System.IO;
using System.Xml.Linq;

namespace CodeTest.Models
{
    public class XMLProducer
    {

        public string FolderPath = @"..\..\Data\Computers";
       
        public string XDocumentToStringConverter(string file)
        {

            XDocument documuent = XDocument.Load(file);
            string xmlString = documuent.ToString();
            return xmlString;
        }

        private void AddXmlStringToQueue(string xmlString)
        {

        }

        private void GetFilesFromFolder()
        {
            foreach (string file in Directory.EnumerateFiles(@"..\..\Data\Computers"))
            {
                string xmlString = XDocumentToStringConverter(file);
                AddXmlStringToQueue(xmlString);
            }
        }
    }
}
