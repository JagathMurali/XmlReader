using System;
using CodeTest.HelperClasses.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using XmlHtmlConversion.Models;

namespace UnitTest
{
    [TestClass]
    public class HTMLConverterUnitTest
    {
        [TestMethod]
        public void CheckWhetherXMLStringConvertToHtml()
        {
            // Arrange
            string sampleString = "<Overview><Machine><ComputerName>Comp_AMS0100013</ComputerName><InventoryDate>2006-12-14T09:26:42+00:00</InventoryDate><Memory>504</Memory><IPAddress>10.31.1.135</IPAddress><Macaddress>000E7FFB3550</Macaddress><OperatingSystem>Windows XP Professional, Version 5.1, Service Pack 2 (Build 2600), Dutch (Netherlands)</OperatingSystem><ScreenResolution>1024x768</ScreenResolution><TempDirSize>12</TempDirSize><UserIsAdmin>0</UserIsAdmin><Manufacturer>Hewlett-Packard</Manufacturer><Product>HP d530 SFF(DC578AV)</Product><SerialNumber>FRB4110FJT</SerialNumber><ProcessorVersion>Intel(R) Pentium(R) 4 CPU 2.60GHz</ProcessorVersion></Machine><Assigned><AssignedCount>1</AssignedCount></Assigned></Overview>";
            XMLStringQueue sampleQueue = new XMLStringQueue();
            Mock<IHTMLHelper> mockHTMLHelper = new Mock<IHTMLHelper>();
            sampleQueue.xmlConverterQueue.Enqueue(sampleString);
            HTMLConverter htmlConverter = new HTMLConverter(sampleQueue, mockHTMLHelper.Object);

            // Act
            htmlConverter.ReadXmlStringAndConverttoHTML();

            // Assert
            mockHTMLHelper.Verify(mock => mock.ConvertXmlStringToHtml(sampleString));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CheckWhetherErrorIsCaughtWhenExceptionOccur()
        {
            // Arrange
            string sampleString = "<Overview><Machine><ComputerName>Comp_AMS0100013</ComputerName><InventoryDate>2006-12-14T09:26:42+00:00</InventoryDate><Memory>504</Memory><IPAddress>10.31.1.135</IPAddress><Macaddress>000E7FFB3550</Macaddress><OperatingSystem>Windows XP Professional, Version 5.1, Service Pack 2 (Build 2600), Dutch (Netherlands)</OperatingSystem><ScreenResolution>1024x768</ScreenResolution><TempDirSize>12</TempDirSize><UserIsAdmin>0</UserIsAdmin><Manufacturer>Hewlett-Packard</Manufacturer><Product>HP d530 SFF(DC578AV)</Product><SerialNumber>FRB4110FJT</SerialNumber><ProcessorVersion>Intel(R) Pentium(R) 4 CPU 2.60GHz</ProcessorVersion></Machine><Assigned><AssignedCount>1</AssignedCount></Assigned></Overview>";
            XMLStringQueue sampleQueue = new XMLStringQueue();
            Mock<IHTMLHelper> mockHTMLHelper = new Mock<IHTMLHelper>();
            sampleQueue.xmlConverterQueue.Enqueue(sampleString);
            mockHTMLHelper.Setup(mock => mock.ConvertXmlStringToHtml(It.IsAny<string>())).Throws(new Exception());
            HTMLConverter htmlConverter = new HTMLConverter(sampleQueue, mockHTMLHelper.Object);

            // Act
            htmlConverter.ReadXmlStringAndConverttoHTML();

            // Assert
            Assert.ThrowsException<Exception>(() => htmlConverter.ReadXmlStringAndConverttoHTML());
        }
    }
}
