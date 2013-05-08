#region [Imports]

using System.Xml;

#endregion

namespace GrillMaster.Services.Parsers
{
    /// <summary>
    /// The xml parser.
    /// </summary>
    public partial class XmlParser
    {
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
