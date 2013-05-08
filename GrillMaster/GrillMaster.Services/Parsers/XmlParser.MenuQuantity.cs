#region [Imports]

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using GrillMaster.Core.Entities;

#endregion

namespace GrillMaster.Services.Parsers
{
    /// <summary>The xml parser.</summary>
    public partial class XmlParser
    {
        /// <summary>The parse grill menu quantities.</summary>
        /// <param name="xmlDocument">The xml document.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<GrillMenuQuantity> ParseGrillMenuQuantities(XmlDocument xmlDocument)
        {
            var menuItemsNodes = xmlDocument.LastChild.ChildNodes.Cast<XmlNode>().Where(node => node.Name == "entry").ToList();
            var manager = CreateXmlManager(xmlDocument);

            return (from XmlNode node in menuItemsNodes select ParseGrillMenuQuantity(node, manager)).ToList();
        }

        /// <summary>The parse grill menu quantity.</summary>
        /// <param name="menuNode">The menu node.</param>
        /// <param name="manager">The manager.</param>
        /// <returns>The <see cref="GrillMenuQuantity"/>.</returns>
        private static GrillMenuQuantity ParseGrillMenuQuantity(XmlNode menuNode, XmlNamespaceManager manager)
        {
            var guid = Guid.Empty;
            var quantity = 0;

            foreach (XmlNode childNode in menuNode.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "content":
                        guid = XmlConvert.ToGuid(childNode.SelectSingleNode("m:properties/d:Id", manager).InnerText);
                        quantity = Convert.ToInt32(childNode.SelectSingleNode("m:properties/d:Quantity", manager).InnerText);
                        break;
                }
            }

            return new GrillMenuQuantity(guid, quantity);
        }
    }
}
