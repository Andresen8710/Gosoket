using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AzureFunctionXml
{
    class GetInfoXml
    {
        public static List<XElement> GetListArea(XDocument Xml)
        {
            return Xml.Descendants("area").ToList();
        }

        public static List<XElement> GetListEmployee(XElement Xml)
        {
            return Xml.Descendants("employee").ToList();
        }

    }
}
