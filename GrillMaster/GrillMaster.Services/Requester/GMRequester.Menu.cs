#region [Imports]

using System.Collections.Generic;
using System.Linq;

using GrillMaster.Core.Entities;
using GrillMaster.Services.Parsers;

#endregion

namespace GrillMaster.Services.Requester
{
    /// <summary>The grill master requester.</summary>
    public partial class GMRequester
    {
        /// <summary>
        /// The load grill menus.
        /// </summary>
        /// <returns>
        /// The <see cref="List{T}"/>.
        /// </returns>
        public static List<GrillMenu> LoadGrillMenus()
        {
            var doc = MakeRequest("GrillMenus");

            return XmlParser.ParseGrillMenus(doc).OrderBy(m => m.Name).ToList();
        }
    }
}
