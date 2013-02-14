namespace TravelerInfoMapServices.Geometry
{
	public abstract class SpatialReference
	{

	}

	public class WkidBasedSpatialReference: SpatialReference
	{
		public int wkid { get; set; }
		public int? latestWkid { get; set; }
		public int? vcsWkid { get; set; }
		public int? latestVcsWkid { get; set; }
	}

	public class WktBasedSpatialReference : SpatialReference
	{
		public string wkt { get; set; }
	}
}
