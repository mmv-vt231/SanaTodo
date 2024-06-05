using System.Xml.Linq;

namespace App.XMLStorage
{
    public class XMLFactory : IXMLFactory
    {
        public XDocument Load()
        {
            XDocument xmlDoc = XDocument.Load("XMLStorage/Storage.xml");

            return xmlDoc;
        }

        public void Save(XDocument xmlDoc)
        {
            xmlDoc.Save("XMLStorage/Storage.xml");
        }
    }
}
