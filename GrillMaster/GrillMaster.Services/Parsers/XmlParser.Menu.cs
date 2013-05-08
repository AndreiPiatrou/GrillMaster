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
        /// <summary>
        /// The parse menu.
        /// </summary>
        /// <param name="xmlDocument">
        /// The xml document.
        /// </param>
        /// <returns>
        /// The <see cref="GrillMenu"/>.
        /// </returns>
        public static List<GrillMenu> ParseGrillMenus(XmlDocument xmlDocument)
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
    }
}
