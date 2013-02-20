using ServiceStack.ServiceInterface;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
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


			if (request.LayerId == _cameraLayerId)
			{
				return GetCameras(objectIds, request.outSR).ToResponse(request);
			}
			else
			{
				// throw new ArgumentException(string.Format("The specified LayerID is invalid: {0}.", request.LayerId));
				return new
				{
					code = 500,
					error = string.Format("The specified LayerID is invalid: {0}.", request.LayerId)
				};
			}
		}

		private static IEnumerable<Camera> GetCameras(IEnumerable<int> objectIds=null, int? outSR=null)
		{

			IEnumerable<Camera> cameras;
			////var client = new HighwayCamerasClient();
			var client = new ServiceStack.ServiceClient.Web.JsonServiceClient("http://www.wsdot.wa.gov/traffic/api/HighwayCameras/HighwayCamerasREST.svc/");
			if (objectIds == null || objectIds.Count() > 0)
			{
				////cameras = client.GetCameras(_trafficApiCode);
				cameras = client.Get<Camera[]>("GetCamerasAsJson?AccessCode=" + _trafficApiCode);
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
					////camera = client.GetCamera(_trafficApiCode, objectIds.First());
					camera = client.Get<Camera>("GetCameraAsJson?AccessCode=" + _trafficApiCode);
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