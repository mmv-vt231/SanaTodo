using System.Xml.Linq;

namespace App.XMLStorage
{
    public interface IXMLFactory
    {
        XDocument Load();
        void Save(XDocument xmlDoc);
    }
}
