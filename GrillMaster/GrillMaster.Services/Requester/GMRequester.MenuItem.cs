#region [Imports]

using System.Collections.Generic;

using GrillMaster.Core.Entities;
using GrillMaster.Services.Parsers;

#endregion

namespace GrillMaster.Services.Requester
{
    /// <summary>The grill menu requester.</summary>
    public partial class GMRequester
    {
        /// <summary>The load grill menu items.</summary>
        /// <returns>The <see cref="List"/>.</returns>
        public static List<GrillMenuItem> LoadGrillMenuItems()
        {
            var doc = MakeRequest("GrillMenuItems");

            return XmlParser.ParseGrillMenuItems(doc);
        }

        /// <summary>The load grill menu items.</summary>
        /// <param name="menuItenLink">The menu iten link.</param>
        /// <returns>The <see cref="List"/>.</returns>
        public static List<GrillMenuItem> LoadGrillMenuItems(string menuItenLink)
        {
            var doc = MakeRequest(menuItenLink);

            return XmlParser.ParseGrillMenuItems(doc);
        }

    }
}
