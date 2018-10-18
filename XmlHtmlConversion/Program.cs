using System;
using System.Threading.Tasks;
using XmlHtmlConversion.HelperClasses;
using XmlHtmlConversion.Models;

namespace XmlHtmlConversion
{
    class Program
    {
        static void Main()
        {
            // User interaction to start the process
            Console.WriteLine("Press a key to start the process");
            Console.ReadLine();
            Console.WriteLine("Process starting...");

            // Class that have string queue to which the xml will be stored
            XMLStringQueue xmlStringQueue = new XMLStringQueue();


            // Initializing the xml parser and html convertor by passing the xml string queue class instance
            XMLParser xmlparser = new XMLParser(xmlStringQueue);
            HTMLConverter htmlConverter = new HTMLConverter(xmlStringQueue, new HTMLHelper());

            /*  XMLparser will run on the main thread and this will trigge the call to read xml file 
             *  and convert it into string and place it into the queue */
            xmlparser.GetFilesFromFolder();

            /* Creating a second thread that reads the string from the queue
             * and converts it into html file using xslt file*/
            void action()
            {
                htmlConverter.ReadXmlStringAndConverttoHTML();
            }
            Parallel.Invoke(action);

            // User interaction to exit
            Console.WriteLine("Process completed. Press a key to exit");
            Console.ReadLine();
        }
    }
}
