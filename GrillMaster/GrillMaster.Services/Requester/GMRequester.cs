#region [Imports]

using System;
using System.IO;
using System.Net;
using System.Xml;

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
        private static readonly Uri serverBaseUri = new Uri(Properties.Settings.Default.ServerBaseUri);
        /// <summary>
        ///     User credantials.
        /// </summary>
        private static CredentialCache Cache;

        #region [Constructors]

        public GMRequester(string userName, string userPassword)
        {
            InitProperties(userName, userPassword);
        }

        #endregion

        #region [Public methods]

        /// <summary>
        ///     Make request.
        /// </summary>
        /// <param name="entityUriParameter">Entity name.</param>
        /// <returns>Xml responce.</returns>
        public static XmlDocument MakeRequest(string entityUriParameter)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(CreateRequest(entityUriParameter));

            return xmlDocument;
        }

        public static void InitRequester(string userName, string password)
        {
            InitProperties(userName, password);
        }

        #endregion

        #region [Private methods]

        /// <summary>
        ///     Initiate necessary properties for requests.
        /// </summary>
        /// <param name="userName">User name(login).</param>
        /// <param name="password">User password.</param>
        private static void InitProperties(string userName, string userPassword)
        {
            var serviceCreds = new NetworkCredential(userName, userPassword);
            Cache = new CredentialCache { { serverBaseUri, "Basic", serviceCreds } };
        }

        /// <summary>
        ///     Create web request.
        /// </summary>
        /// <param name="entityUriParameter">Uri parameter for entities request,</param>
        /// <returns>Responce stream.</returns>
        private static Stream CreateRequest(string entityUriParameter)
        {
            var resultUri = new Uri(string.Format("{0}/{1}", serverBaseUri, entityUriParameter));
            var httpRequest = (HttpWebRequest)WebRequest.Create(resultUri);
            httpRequest.Credentials = Cache;

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            return httpResponse.GetResponseStream();
        }

        #endregion
    }
}
