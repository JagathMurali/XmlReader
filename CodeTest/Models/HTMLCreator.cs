using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace CodeTest.Models
{
    public class HTMLCreator
    {
        private string filePathXslt = @"..\..\Resources\Computer.xslt";

        private string LoadXsltFile()
        {
            XDocument xsltDocument = XDocument.Load(filePathXslt);
            return xsltDocument.ToString();
        }

        public void ConvertXmlStringToHtml(string xmlString)
        {
            string xslString = LoadXsltFile();
            string htmlString = TransformXMLToHTML(xmlString, xslString);
            SaveHtmlFile(htmlString);
        }

        private string TransformXMLToHTML(string inputXml, string xsltString)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            XmlReaderSettings setting = new XmlReaderSettings();
            setting.DtdProcessing = DtdProcessing.Parse;
            using (XmlReader reader = XmlReader.Create(new StringReader(xsltString), setting))
            {
                transform.Load(reader);
            }
            StringWriter results = new StringWriter();
            using (XmlReader reader = XmlReader.Create(new StringReader(inputXml)))
            {
                transform.Transform(reader, null, results);
            }
            return results.ToString();
        }

        public void SaveHtmlFile(string htmlString)
        {
            Guid guid = Guid.NewGuid();
            //TODO Create directory check if folder is created or not.
            string fileName = @"..\..\Data\" + guid + ".html";

            //Write new HTML string to file
            File.WriteAllText(fileName, htmlString);

            //Show it in the default application for handling .html files
            //Process.Start(fileName);
        }      
    }
}
