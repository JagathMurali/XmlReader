using System;
using System.Threading.Tasks;
using XmlHtmlConversion.Models;

namespace XmlHtmlConversion
{
    class Program
    {

        static void Main()
        {
            // Class that have string queue to which the xml will be stored
            XMLStringQueue xmlStringQueue = new XMLStringQueue();

            // Initializing the xml parser and html convertor by passing the xml string queue class instance
            XMLParser xmlparser = new XMLParser(xmlStringQueue);
            HTMLConvertor htmlConverter = new HTMLConvertor(xmlStringQueue);

            /*  XMLparser will run on the main thread and this will trigge the call to read xml file 
             *  and convert it into string and place it into the queue */
            xmlparser.GetFilesFromFolder();

            /* Creating a second thread that reads the string from the queue
             * and converts it into html file using xslt file*/
            Action action = () =>
            {
                htmlConverter.ReadXmlStringAndConverttoHTML();
            };
            Parallel.Invoke(action);
        }
    }   
}
