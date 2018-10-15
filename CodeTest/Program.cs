using CodeTest.Models;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace CodeTest
{
    class Program
    {

        static void Main(string[] args)
        {
            ConcurrentQueue<string> xmlConverterQueue = new ConcurrentQueue<string>();
            XMLProducer xmlProducer = new XMLProducer();
            HTMLCreator htmlCreator = new HTMLCreator();
            foreach (string file in Directory.EnumerateFiles(@"..\..\Data\Computers"))
            {
                string xmlString = xmlProducer.XDocumentToStringConverter(file);
                xmlConverterQueue.Enqueue(xmlString);
            }
            Action action = () =>
            {
                string xmlStringfromQueue;
                while (xmlConverterQueue.TryDequeue(out xmlStringfromQueue))
                {
                    if (!string.IsNullOrEmpty(xmlStringfromQueue))
                    {
                        htmlCreator.ConvertXmlStringToHtml(xmlStringfromQueue);
                    }
                }
            };
            Parallel.Invoke(action);
        }

        //public static string TransformXMLToHTML(string inputXml, string xsltString)
        //{
        //    XslCompiledTransform transform = new XslCompiledTransform();
        //    XmlReaderSettings setting = new XmlReaderSettings();
        //    setting.DtdProcessing = DtdProcessing.Parse;
        //    using (XmlReader reader = XmlReader.Create(new StringReader(xsltString), setting))
        //    {
        //        transform.Load(reader);
        //    }
        //    StringWriter results = new StringWriter();
        //    using (XmlReader reader = XmlReader.Create(new StringReader(inputXml)))
        //    {
        //        transform.Transform(reader, null, results);
        //    }
        //    return results.ToString();
        //}

        //public static void SaveHtmlFile(string htmlString, string filename)
        //{
        //    //TODO Create directory check if folder is created or not.
        //    string fileName = @"..\..\Data\"+ filename+".html";
          
        //    //Write new HTML string to file
        //    File.WriteAllText(fileName, htmlString);

        //    //Show it in the default application for handling .html files
        //    //Process.Start(fileName);
        //}


    }   
}
