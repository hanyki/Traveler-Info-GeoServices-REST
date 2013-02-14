using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelerInfoMapServices
{
	public class Field
	{
		public string name { get; set; }
		public string type { get; set; }
		public string alias { get; set; }
		public int? length { get; set; }
	}
}