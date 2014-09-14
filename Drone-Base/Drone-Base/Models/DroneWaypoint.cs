using Autodesk.Revit.DB;

using RevitMyDrone.DroneBase.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RevitMyDrone.DroneBase.Models
{
	public class DroneWaypoint
	{
		private XYZ projectXYZ;
		private Document dbDoc;

		public String Name { get; set; }

		public Double Latitude
		{
			get
			{
				return GeoRefUtils.GetLatitude(projectXYZ, dbDoc);
			}
		}

		public Double Longitude
		{
			get
			{
				return GeoRefUtils.GetLongitude(projectXYZ, dbDoc);
			}
		}

		public Double Elevation
		{
			get
			{
				return GeoRefUtils.GetAltitude(projectXYZ, dbDoc);
			}
		}

		public DroneWaypoint(XYZ projCoordXYZParam, Document dbDocParam)
		{
			projectXYZ = projCoordXYZParam;
			dbDoc = dbDocParam;
		}
	}
}
