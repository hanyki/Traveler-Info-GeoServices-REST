using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace TravelerInfoMapServices
{
	[Route("/TravelerInfo/FeatureServer/{LayerId}/query")]
	public class FeatureLayerQuery
	{
		public int LayerId { get; set; }
		public string f { get; set; }
		public string where { get; set; }
		// TODO: check to see if format is correct.  This parameter should not require the user to put brackets around object ID list.
		/// <summary>
		/// A comma separated list of <see cref="int"/>. Optional.
		/// </summary>
		public string objectIds { get; set; }

		#region Spatial Relation query parameters
		public object geometry { get; set; }
		public string geometryType { get; set; }
		/// <summary>
		/// Either an integer or a spatialReference object.
		/// </summary>
		public string inSR { get; set; }
		public string spatialRel { get; set; }
		public string relationParam { get; set; } 
		#endregion

		/// <summary>
		/// This should either be an integer or a comma separated list containing two integers.
		/// </summary>
		public string time { get; set; }

		public string outFields { get; set; }

		public bool? returnGeoemtry { get; set; }
		public int? maxAllowableOffset { get; set; }

		public int? geometryPrecision { get; set; }

		/// <summary>
		/// Either an <see cref="int"/> or a spatialReference object.
		/// </summary>
		public int? outSR { get; set; }

		public bool? returnIdsOnly { get; set; }

		public bool? returnCountOnly { get; set; }

		public string orderByFields { get; set; }

		public bool? returnZ { get; set; }
		public bool? returnM { get; set; }

	}
}