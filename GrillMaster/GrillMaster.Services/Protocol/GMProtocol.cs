#region [Imports]

using System;

#endregion

namespace GrillMaster.Services.Protocol
{
    public partial class GMProtocol
    {
        private static Uri serverBaseUri;

        #region [Constructors]

        static GMProtocol()
        {
            serverBaseUri = new Uri(Properties.Settings.Default.ServerBaseUri);
        }

        #endregion
    }
}
