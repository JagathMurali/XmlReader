using CodeTest.HelperClasses.Interfaces;
using System;

namespace XmlHtmlConversion.Models
{
    public class HTMLConverter
    {
        public XMLStringQueue StringQueue;
        public IHTMLHelper HtmlHelper;      

        public HTMLConverter(XMLStringQueue xmlStringQueue, IHTMLHelper htmlHelper)
        {
            StringQueue = xmlStringQueue;
            HtmlHelper = htmlHelper;
        }   

        /// <summary>
        /// Reading the xml string from the Queue and passing it into the helper class
        /// </summary>
        public void ReadXmlStringAndConverttoHTML()
        {
            try
            {
                string xmlStringfromQueue;
                while (StringQueue.xmlConverterQueue.TryDequeue(out xmlStringfromQueue))
                {
                    if (!string.IsNullOrEmpty(xmlStringfromQueue))
                    {
                        HtmlHelper.ConvertXmlStringToHtml(xmlStringfromQueue);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Error in converting string into html.");
            }
        }
    }
}
