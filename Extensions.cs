using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using TravelerInfoMapServices.Geometry;
using Wsdot.Traffic;

namespace TravelerInfoMapServices
{
	public static class Extensions
	{
		const int _wkid = 4326;
		readonly static SpatialReference _spatialReference = new WkidBasedSpatialReference { wkid = _wkid };

		public static Point ToPoint(this RoadwayLocation location)
		{
			return new Point
			{
				x = location.Longitude,
				y = location.Latitude,
				spatialReference = _spatialReference
			};
		}

		public static Feature ToFeature(this ITrafficFeature trafficFeature)
		{
			Type type = trafficFeature.GetType();
			// Get a list of all of the properties of the traffic feature.
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly);
			var attributes = (from prop in properties
						  where prop.PropertyType.IsValueType
						  select prop).ToDictionary(k => k.Name, pi => pi.GetValue(trafficFeature, null));
			var roadwayLocation = properties.First(prop => prop.PropertyType == typeof(RoadwayLocation)).GetValue(trafficFeature, null) as RoadwayLocation;

			return new Feature
			{
				attributes = attributes,
				geometry = roadwayLocation != null ? roadwayLocation.ToPoint() : null
			};
		}
	}
}