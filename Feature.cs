using System.Collections.Generic;
using TravelerInfoMapServices.Geometry;

namespace TravelerInfoMapServices
{
	public class Feature
	{
		public GeometryBase geometry { get; set; }
		public object attributes { get; set; }
	}
}