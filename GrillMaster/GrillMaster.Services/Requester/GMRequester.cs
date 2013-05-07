#region [Imports]

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using GrillMaster.Core.Entities;
using GrillMaster.Services.Parsers;

#endregion

namespace GrillMaster.Services.Requester
{
    /// <summary>
    ///     Server requester.
    /// </summary>
    public class GMRequester
    {
        /// <summary>
        ///     Server base uri.
        /// </summary>
        private static readonly Uri ServerBaseUri = new Uri(Properties.Settings.Default.ServerBaseUri);

        /// <summary>
        ///     User credentials.
        /// </summary>
        private static CredentialCache _cache;

        #region [Constructors]

        public GMRequester(string userName, string userPassword)
        {
            InitProperties(userName, userPassword);
        }

        #endregion

        #region [Public methods]

        /// <summary>
        /// The initialize requester.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        public static void InitRequester(string userName, string password)
        {
            InitProperties(userName, password);
        }

        /// <summary>
        /// The load grill menus.
        /// </summary>
        /// <returns>
        /// The <see cref="List{T}"/>.
        /// </returns>
        public static List<GrillMenu> LoadGrillMenus()
        {
            var doc = MakeRequest("GrillMenus");

            return XmlParser.ParseGrillMenus(doc);
        }

        public static List<GrillMenuItem> LoadGrillMenuItems()
        {
            var doc = MakeRequest("GrillMenuItems");

            return XmlParser.ParseGrillMenuItems(doc);
        }

        public static List<GrillMenuItem> LoadGrillMenuItems(string menuItenLink)
        {
            var doc = MakeRequest(menuItenLink);

            return XmlParser.ParseGrillMenuItems(doc);
        }

        public static List<GrillMenuQuantity> LoadGrillMenuQuantities(string quantitiesLink)
        {
            var doc = MakeRequest(quantitiesLink);

            return XmlParser.ParseGrillMenuQuantities(doc);
        }

        /// <summary>
        ///     Make request.
        /// </summary>
        /// <param name="entityUriParameter">Entity name.</param>
        /// <returns>Xml response.</returns>
        private static XmlDocument MakeRequest(string entityUriParameter)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(CreateRequest(entityUriParameter));

            return xmlDocument;
        }

        #endregion

        #region [Private methods]

        /// <summary>
        ///     Initiate necessary properties for requests.
        /// </summary>
        /// <param name="userName">User name(login).</param>
        /// <param name="userPassword">User password.</param>
        private static void InitProperties(string userName, string userPassword)
        {
            var serviceCreds = new NetworkCredential(userName, userPassword);
            _cache = new CredentialCache { { ServerBaseUri, "Basic", serviceCreds } };
        }

        /// <summary>
        ///     Create web request.
        /// </summary>
        /// <param name="entityUriParameter">Uri parameter for entities request,</param>
        /// <returns>Responce stream.</returns>
        private static Stream CreateRequest(string entityUriParameter)
        {
            var resultUri = new Uri(string.Format("{0}/{1}", ServerBaseUri, entityUriParameter));
            var httpRequest = (HttpWebRequest)WebRequest.Create(resultUri);
            httpRequest.Credentials = _cache;

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            return httpResponse.GetResponseStream();
        }

        #endregion
    }
}
