#region [Imports]

using GrillMaster.Core.Entities;
using GrillMaster.Services.Parsers;
using System.Collections.Generic;

#endregion

namespace GrillMaster.Services.Requester
{
    public partial class GMRequester
    {
        /// <summary>The load grill menu quantities.</summary>
        /// <param name="quantitiesLink">The quantities link.</param>
        /// <returns>The <see cref="List"/>.</returns>
        public static List<GrillMenuQuantity> LoadGrillMenuQuantities(string quantitiesLink)
        {
            var doc = MakeRequest(quantitiesLink);

            return XmlParser.ParseGrillMenuQuantities(doc);
        }
    }
}
