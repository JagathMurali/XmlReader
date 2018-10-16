using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace CodeTest.Models
{
    public class HTMLConvertor
    {
        public XMLStringQueue stringQueue;
        private string filePathXslt = @"..\..\Resources\Computer.xslt";

        public HTMLConvertor(XMLStringQueue _xmlStringQueue)
        {
            stringQueue = _xmlStringQueue;
        }   

        public void ReadXmlStringAndConverttoHTML()
        {
            string xmlStringfromQueue;
            while (stringQueue.xmlConverterQueue.TryDequeue(out xmlStringfromQueue))
            {
                if (!string.IsNullOrEmpty(xmlStringfromQueue))
                {
                    ConvertXmlStringToHtml(xmlStringfromQueue);
                }
            }
        }

        /// <summary>
        /// Function to call other function to convert XML string to HTML file
        /// </summary>
        /// <param name="xmlString">xml string that taken form the queue</param>
        private void ConvertXmlStringToHtml(string xmlString)
        {
            string xslString = LoadXsltFile();
            string htmlString = TransformXMLToHTML(xmlString, xslString);
            SaveHtmlFile(htmlString);
        }

        /// <summary>
        /// Function to load XSLT file
        /// </summary>
        /// <returns>Returns the XSLT file in string format</returns>
        private string LoadXsltFile()
        { 
            XDocument xsltDocument = XDocument.Load(filePathXslt);
            return xsltDocument.ToString();
        }

        /// <summary>
        /// Function transforms the XML to HTML using XSLT
        /// </summary>
        /// <param name="inputXml">XML string which will be transformed</param>
        /// <param name="xsltString">XSLT file that used to transform XML to HTML</param>
        /// <returns>Returns HTML in string format</returns>
        private string TransformXMLToHTML(string inputXml, string xsltString)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            // XMl reader setting to parse the xml to html
            XmlReaderSettings setting = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse
            };
            using (XmlReader xmlReader = XmlReader.Create(new StringReader(xsltString), setting))
            {
                transform.Load(xmlReader);
            }

            // Output of the Transform function with be available in the output variable
            StringWriter results = new StringWriter();
            using (XmlReader xmlReader = XmlReader.Create(new StringReader(inputXml)))
            {
                transform.Transform(xmlReader, null, results);
            }
            return results.ToString();
        }

        /// <summary>
        /// Create the HTML file in the specified folder
        /// </summary>
        /// <param name="htmlString">String that needs to be saved as a HTML file</param>
        private void SaveHtmlFile(string htmlString)
        {
            string outputFolderPath = @"..\..\Data\Output\";
            Directory.CreateDirectory(outputFolderPath);

            Guid guid = Guid.NewGuid();
            string fileName = outputFolderPath + guid + ".html";


            //Write new HTML string to file
            File.WriteAllText(fileName, htmlString);
        }      
    }
}
