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

		public static Point ToPoint(this RoadwayLocation location, bool includeSpatialReference = false)
		{
			return new Point
			{
				x = Convert.ToDouble(location.Longitude),
				y = Convert.ToDouble(location.Latitude),
				spatialReference = includeSpatialReference ? _spatialReference : null
			};
		}

		////public static Feature ToFeature(this ITrafficFeature trafficFeature)
		////{
		////	Type type = trafficFeature.GetType();
		////	// Get a list of all of the properties of the traffic feature.
		////	PropertyInfo[] properties = type.GetProperties();
		////	var attributes = (from prop in properties
		////				  where prop.PropertyType.IsValueType
		////				  select prop).ToDictionary(k => k.Name, pi => pi.GetValue(trafficFeature, null));
		////	var roadwayLocation = properties.First(prop => prop.PropertyType == typeof(RoadwayLocation)).GetValue(trafficFeature, null) as RoadwayLocation;

		////	return new Feature
		////	{
		////		attributes = attributes,
		////		geometry = roadwayLocation != null ? roadwayLocation.ToPoint() : null
		////	};
		////}

		public static FeatureLayerQueryResponseBase ToResponse(this IEnumerable<Camera> trafficFeatures, FeatureLayerQuery request)
		{
			// Parse the object IDs into ints, if provided. Otherwise the value will be null.
			var objectIds = !string.IsNullOrWhiteSpace(request.objectIds) ?
				from s in Regex.Split(request.objectIds, @"\s*,\s*")
				select int.Parse(s)
				: null;


			if (request.returnCountOnly == true && objectIds == null)
			{
				return new FeatureLayerQueryCountResponse
				{
					count = trafficFeatures.Count()
				};
			}
			else if (request.returnIdsOnly == true && objectIds == null)
			{
				return new FeatureLayerQueryObjectIdsResponse
				{
					objectIdFieldName = "CameraID",
					objectIds = from c in trafficFeatures select c.CameraID
				};
			}
			else
			{
				return new FeatureLayerQueryResponse
				{
					features = trafficFeatures.Select(c => c.ToFeature(outSR: request.outSR)),
				};
			}
		}
	}
}