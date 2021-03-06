﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelerInfoMapServices
{
	public abstract class FeatureLayerQueryResponseBase
	{

	}

	public abstract class FeatureLayerQueryObjectIdResponseBase : FeatureLayerQueryResponseBase
	{
		public string objectIdFieldName { get; set; }
	}

	public class FeatureLayerQueryResponse : FeatureLayerQueryObjectIdResponseBase
	{
		public string globalIdFieldName { get; set; }
		public string geometryType { get; set; }
		public object spatialReference { get; set; }
		public bool? hasZ { get; set; }
		public bool? hasM { get; set; }
		public IEnumerable<Field> fields { get; set; }
		public IEnumerable<Feature> features { get; set; }
	}

	public class FeatureLayerQueryCountResponse: FeatureLayerQueryResponseBase
	{
		public int count { get; set; }
	}

	public class FeatureLayerQueryObjectIdsResponse: FeatureLayerQueryObjectIdResponseBase
	{
		public IEnumerable<int> objectIds { get; set; }
	}
}