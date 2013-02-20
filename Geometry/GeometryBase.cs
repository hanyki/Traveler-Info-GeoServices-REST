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
		public double x { get; set; }
		public double y { get; set; }
		public double? z { get; set; }
		public double? m { get; set; }
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
		public List<List<double>> points { get; set; }
	}

	public class Polyline: MultiplePointGeometry
	{
		public List<List<List<double>>> paths { get; set; }
	}

	public class Polygon: MultiplePointGeometry
	{
		public List<List<List<double>>> rings { get; set; }
	}

	public class Envelope: GeometryBase
	{
		public double xmin { get; set; }
		public double ymin { get; set; }
		public double xmax { get; set; }
		public double ymax { get; set; }
		public double? zmin { get; set; }
		public double? zmax { get; set; }
		public double? mmin { get; set; }
		public double? mmax { get; set; }
	}


}