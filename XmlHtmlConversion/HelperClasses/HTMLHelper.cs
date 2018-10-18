using CodeTest.HelperClasses.Interfaces;
using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace XmlHtmlConversion.HelperClasses
{
    public class HTMLHelper : IHTMLHelper
    {
        public string FilePathXslt = @"..\..\Resources\Computer.xslt";
        private string outputFolderPath = @"..\..\Data\Output\";
        private string styleSheetPath = @"..\..\Resources\Style.css";

        /// <summary>
        /// Function to call other function to convert XML string to HTML file
        /// </summary>
        /// <param name="xmlString">xml string that taken form the queue</param>
        public void ConvertXmlStringToHtml(string xmlString)
        {
            try
            {
                // Loads the XSLT file
                string xsltString = LoadXsltFile();
                // Transforms XML string to HTML string using xslt file
                string htmlString = TransformXMLToHTML(xmlString, xsltString);

                // Save the HTML string into file
                SaveHtmlFile(htmlString);
            }
            catch(Exception)
            {
                throw new Exception("Converting XML to String and storing into file failed");
            }
            
        }

        /// <summary>
        /// Function to load XSLT file
        /// </summary>
        /// <returns>Returns the XSLT file in string format</returns>
        private string LoadXsltFile()
        {
            XDocument xsltDocument = XDocument.Load(FilePathXslt);
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
            // XML reader setting to parse the xml to html
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
            string outputStyleSheetPath = outputFolderPath + "Style.css";

            // Create the folder, if it is not exist.
            Directory.CreateDirectory(outputFolderPath);

            // Check for the style sheet present in the output folder
            if (!File.Exists(outputStyleSheetPath))
            {
                // Copy the style sheet from resource folder to output folder
                File.Copy(styleSheetPath, outputStyleSheetPath);
            }

            // Unique value for the file name
            Guid guid = Guid.NewGuid();
            string fileName = outputFolderPath + guid + ".html";


            //Write new HTML string to file
            File.WriteAllText(fileName, htmlString);
        }
    }
}
