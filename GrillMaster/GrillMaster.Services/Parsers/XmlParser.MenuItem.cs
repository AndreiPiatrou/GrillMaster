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
        /// <summary>The parse grill menu items.</summary>
        /// <param name="xmlDocument">The xml document.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<GrillMenuItem> ParseGrillMenuItems(XmlDocument xmlDocument)
        {
            var menuItemsNodes = xmlDocument.LastChild.ChildNodes.Cast<XmlNode>().Where(node => node.Name == "entry").ToList();
            menuItemsNodes.AddRange(xmlDocument.ChildNodes.Cast<XmlNode>().Where(node => node.Name == "entry"));
            var manager = CreateXmlManager(xmlDocument);

            return (from XmlNode node in menuItemsNodes select ParseGrillMenuItem(node, manager)).ToList();
        }

        /// <summary>The parse grill menu item.</summary>
        /// <param name="menuNode">The menu node.</param>
        /// <param name="manager">The manager.</param>
        /// <returns>The <see cref="GrillMenuItem"/>.</returns>
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
    }
}
