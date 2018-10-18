using System;
using XmlHtmlConversion.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class XMLParserUnitTest
    {
        [TestMethod]
        public void CheckXmlFileIsAddedIntoQueueAfterReadingAndParsing()
        {
            // Arrange
            XMLStringQueue sampleQueue = new XMLStringQueue();
            XMLParser xmlparser = new XMLParser(sampleQueue);
            xmlparser.FolderPath = @"..\..\SampleInput\";

            // Act
            xmlparser.GetFilesFromFolder();

            // Assert
            Assert.IsFalse(sampleQueue.xmlConverterQueue.IsEmpty);
            Assert.AreEqual(sampleQueue.xmlConverterQueue.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CheckWhetherExceptionIsThrownWhenErrorOccursInPasing()
        {
            // Arrange
            XMLStringQueue sampleQueue = new XMLStringQueue();
            XMLParser xmlparser = new XMLParser(sampleQueue);

            // Act
            xmlparser.GetFilesFromFolder();

            // Assert
            Assert.ThrowsException<Exception>(() => xmlparser.GetFilesFromFolder());
        }
    }
}
