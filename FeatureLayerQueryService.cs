using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using TravelerInfoMapServices.Geometry;
using Wsdot.Traffic;

namespace TravelerInfoMapServices
{
	public class FeatureLayerQueryService: Service
	{
		const int _cameraLayerId = 0;
		readonly static string _trafficApiCode = ConfigurationManager.AppSettings["apiKey"];

		// TODO: Restrict to get and post.
		public object Any(FeatureLayerQuery request)
		{
			// Parse the object IDs into ints, if provided. Otherwise the value will be null.
			var objectIds = !string.IsNullOrWhiteSpace(request.objectIds) ?
				from s in Regex.Split(request.objectIds, @"\s*,\s*")
				select int.Parse(s) 
				: null;

			IEnumerable<Feature> features;

			if (request.LayerId == _cameraLayerId)
			{
				features = GetCameras(objectIds).Select(c => c.ToFeature());
			}
			else
			{
				throw new ArgumentException(string.Format("The specified LayerID is invalid: {0}.", request.LayerId));
			}


			if (request.returnCountOnly == true && objectIds == null)
			{

			}
			else if (request.returnIdsOnly == true && objectIds == null)
			{

			}
			else
			{

			}
			throw new NotImplementedException();
		}

		private static IEnumerable<Camera> GetCameras(IEnumerable<int> objectIds=null)
		{
			IEnumerable<Camera> cameras;
			var client = new HighwayCamerasClient();
			if (objectIds == null || objectIds.Count() > 0)
			{
				cameras = client.GetCameras(_trafficApiCode);
				if (objectIds != null)
				{
					cameras = cameras.Where(c => objectIds.Contains(c.CameraID));
				}
			}
			else
			{
				Camera camera = null;
				try
				{
					camera = client.GetCamera(_trafficApiCode, objectIds.First());
				}
				catch
				{
					camera = null;
				}
				if (camera != null)
				{
					cameras = new Camera[] { camera };
				}
				else
				{
					cameras = new Camera[0];
				}
			}
			return cameras;
		}

	}
}