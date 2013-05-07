#region [Imports]

using System.Collections.Generic;
using System.Linq;
using System.Xml;
using GrillMaster.Core.Entities;

#endregion

namespace GrillMaster.Services.Parsers
{
    using System;

    /// <summary>
    /// The xml parser.
    /// </summary>
    public class XmlParser
    {
        /// <summary>
        /// The parse menu.
        /// </summary>
        /// <param name="xmlDocument">
        /// The xml document.
        /// </param>
        /// <returns>
        /// The <see cref="GrillMenu[]"/>.
        /// </returns>
        public static List<GrillMenu> ParseMenu(XmlDocument xmlDocument)
        {
            var menuesNode = xmlDocument.LastChild.ChildNodes.Cast<XmlNode>().Where(node => node.Name == "entry").ToList();
            var manager = CreateXmlManager(xmlDocument);

            return (from XmlNode node in menuesNode select ParseMenu(node, manager)).ToList();
        }

        /// <summary>
        /// The parse menu.
        /// </summary>
        /// <param name="menuNode">
        /// The menu node.
        /// </param>
        /// <param name="manager">
        /// The manager.
        /// </param>
        /// <returns>
        /// The <see cref="GrillMenu"/>.
        /// </returns>
        private static GrillMenu ParseMenu(XmlNode menuNode, XmlNamespaceManager manager)
        {
            var menuName = string.Empty;
            var guid = Guid.Empty;

            foreach (XmlNode childNode in menuNode.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "content":
                        menuName = childNode.SelectSingleNode("m:properties/d:Name", manager).InnerText;
                        guid = XmlConvert.ToGuid(childNode.SelectSingleNode("m:properties/d:Id", manager).InnerText);
                        break;
                }
            }

            return new GrillMenu(guid, menuName);
        }

        /// <summary>
        /// The create xml manager.
        /// </summary>
        /// <param name="xmlDocument">
        /// The xml document.
        /// </param>
        /// <returns>
        /// The <see cref="XmlNamespaceManager"/>.
        /// </returns>
        private static XmlNamespaceManager CreateXmlManager(XmlDocument xmlDocument)
        {
            var manager = new XmlNamespaceManager(xmlDocument.NameTable);
            manager.AddNamespace("m", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
            manager.AddNamespace("d", "http://schemas.microsoft.com/ado/2007/08/dataservices");

            return manager;
        }
    }
}
