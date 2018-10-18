using System;
using System.IO;
using System.Xml.Linq;

namespace XmlHtmlConversion.Models
{
    public class XMLParser
    {
        public string FolderPath = @"..\..\Data\Computers";
        private XMLStringQueue stringQueue;

        public XMLParser(XMLStringQueue xmlStringQueue)
        {
            stringQueue = xmlStringQueue;
        }

        /// <summary>
        /// Read each files from the folder
        /// </summary>
        public void GetFilesFromFolder()
        {
            try
            {
                foreach (string file in Directory.EnumerateFiles(FolderPath))
                {
                    string xmlString = XDocumentToStringConverter(file);
                    stringQueue.xmlConverterQueue.Enqueue(xmlString);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error in parsing the files.");
            }
        }

        /// <summary>
        /// Converts the file into string format
        /// </summary>
        /// <param name="file">File name</param>
        /// <returns></returns>
        private string XDocumentToStringConverter(string file)
        {

            XDocument documuent = XDocument.Load(file);
            string xmlString = documuent.ToString();
            return xmlString;
        }
    }
}
