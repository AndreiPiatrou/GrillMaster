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
        public static List<GrillMenu> ParseGrillMenus(XmlDocument xmlDocument)
        {
            var menuesNode = xmlDocument.LastChild.ChildNodes.Cast<XmlNode>().Where(node => node.Name == "entry").ToList();
            var manager = CreateXmlManager(xmlDocument);

            return (from XmlNode node in menuesNode select ParseMenu(node, manager)).ToList();
        }

        public static List<GrillMenuItem> ParseGrillMenuItems(XmlDocument xmlDocument)
        {
            var menuItemsNodes = xmlDocument.LastChild.ChildNodes.Cast<XmlNode>().Where(node => node.Name == "entry").ToList();
            var manager = CreateXmlManager(xmlDocument);

            return (from XmlNode node in menuItemsNodes select ParseGrillMenuItem(node, manager)).ToList();
        }

        private static GrillMenuItem ParseGrillMenuItem(XmlNode menuNode, XmlNamespaceManager manager)
        {
            var menuName = string.Empty;
            var guid = Guid.Empty;
            var length = 0;
            var width = 0;
            var duration = TimeSpan.Zero;

            foreach (XmlNode childNode in menuNode.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "content":
                        menuName = childNode.SelectSingleNode("m:properties/d:Name", manager).InnerText;
                        guid = XmlConvert.ToGuid(childNode.SelectSingleNode("m:properties/d:Id", manager).InnerText);
                        length = Convert.ToInt32(childNode.SelectSingleNode("m:properties/d:Length", manager).InnerText);
                        width = Convert.ToInt32(childNode.SelectSingleNode("m:properties/d:Width", manager).InnerText);
                        duration = XmlConvert.ToTimeSpan(childNode.SelectSingleNode("m:properties/d:Duration", manager).InnerText);
                        break;
                }
            }

            return new GrillMenuItem(guid, menuName, length, width, duration);
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
