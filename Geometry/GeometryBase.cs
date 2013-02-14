using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelerInfoMapServices.Geometry
{
	public abstract class GeometryBase
	{
		public SpatialReference spatialReference { get; set; }
	}

	public class Point: GeometryBase
	{
		public decimal x { get; set; }
		public decimal y { get; set; }
		public decimal? z { get; set; }
		public decimal? m { get; set; }
	}

	/// <summary>
	/// An abstract class representing geometries that are composed of more than one point.
	/// </summary>
	public abstract class MultiplePointGeometry: GeometryBase
	{
		public bool? hasZ { get; set; }
		public bool? hasM { get; set; }
	}

	public class Multipoint: MultiplePointGeometry
	{
		public List<List<decimal>> points { get; set; }
	}

	public class Polyline: MultiplePointGeometry
	{
		public List<List<List<decimal>>> paths { get; set; }
	}

	public class Polygon: MultiplePointGeometry
	{
		public List<List<List<decimal>>> rings { get; set; }
	}

	public class Envelope: GeometryBase
	{
		public decimal xmin { get; set; }
		public decimal ymin { get; set; }
		public decimal xmax { get; set; }
		public decimal ymax { get; set; }
		public decimal? zmin { get; set; }
		public decimal? zmax { get; set; }
		public decimal? mmin { get; set; }
		public decimal? mmax { get; set; }
	}


}