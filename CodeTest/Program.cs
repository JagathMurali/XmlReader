using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Diagnostics;

namespace CodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO config the path name to config file.
            string filePathXslt = @"..\..\Resources\Computer.xslt";
            XDocument xsltDocument = XDocument.Load(filePathXslt);

            foreach (string file in Directory.EnumerateFiles(@"..\..\Data\Computers"))
            {
                XDocument documuent = XDocument.Load(file);
                string htmlString = TransformXMLToHTML(documuent.ToString(), xsltDocument.ToString());
                Guid guid =  Guid.NewGuid();
                SaveHtmlFile(htmlString, guid.ToString());
            }
        }

        public static string TransformXMLToHTML(string inputXml, string xsltString)
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

        public static void SaveHtmlFile(string htmlString, string filename)
        {
            //TODO Create directory check if folder is created or not.
            string fileName = @"..\..\Data\"+ filename+".html";
          
            //Write new HTML string to file
            File.WriteAllText(fileName, htmlString);

            //Show it in the default application for handling .html files
            //Process.Start(fileName);
        }


    }   
}
