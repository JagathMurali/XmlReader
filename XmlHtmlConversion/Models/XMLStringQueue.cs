using System.Collections.Concurrent;

namespace XmlHtmlConversion.Models
{
    public class XMLStringQueue
    {
        // Concrrent queue which will be used by the XML parser and HTML converter
        public ConcurrentQueue<string> xmlConverterQueue = new ConcurrentQueue<string>();
    }
}
