using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelerInfoMapServices.Properties;

namespace TravelerInfoMapServices
{
    [Route("/TravelerInfo/FeatureServer/{LayerId}")]
    public class FeatureLayerInfoQuery
    {
        /// <summary>
        /// The output format.  Choices are: "json".
        /// </summary>
        public string f { get; set; }

        public int LayerId { get; set; }
    }

    public class FeatureLayerInfoService: Service
    {
        public object Any(FeatureLayerInfoQuery request)
        {
            Response.ContentType = "application/json";
            return Resources.GroupedCamerasLayer;
        }
    }
}