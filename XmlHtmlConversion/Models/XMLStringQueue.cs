using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlHtmlConversion.Models
{
    public class XMLStringQueue
    {
        public ConcurrentQueue<string> xmlConverterQueue = new ConcurrentQueue<string>();
    }
}
