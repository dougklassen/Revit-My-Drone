using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RevitMyDrone.DroneBase.Utils
{
	/// <summary>
	/// utilities for managing drone info in the Revit model
	/// </summary>
	public static class DroneUtils
	{
		private static readonly Double FEET_PER_LAT = 364776.5736209741;
		private static readonly Double FEET_PER_LONG = 246328.36747889998;

		private static Regex droneViewRegex = new Regex(@"^DRONE_(?<index>\d?\d\d)$");

		public static IList<View3D> GetDroneViews(this Document dbDoc)
		{
			List<View3D> droneViews = null;

			droneViews = new FilteredElementCollector(dbDoc)
				.OfClass(typeof(View3D))
				.AsEnumerable()
				.Cast<View3D>()
				.Where(v => droneViewRegex.IsMatch(v.Name))
				.OrderBy(v =>
					Int32.Parse(droneViewRegex.Match(v.Name).Groups["index"].ToString()))
				.ToList();

			return droneViews;
		}

		///// <summary>
		///// Retrieve the base point in the model that is geolocated by the SiteLocation of the project
		///// todo: ensure that the correct base point is returned when there are multiple
		///// </summary>
		//public static BasePoint GetGeoLocatedBasePoint(this Document dbDoc)
		//{
		//	//just find the basepoint that's furthest from the origin
		//	BasePoint bp = new FilteredElementCollector(dbDoc)
		//		.OfClass(typeof(BasePoint))
		//		.AsEnumerable()
		//		.Cast<BasePoint>()
		//		.OrderBy(p => p.GetBasePointXYZ().X + p.GetBasePointXYZ().Y)
		//		.Last();

		//	return bp;
		//}

		//public static XYZ GetBasePointXYZ(this BasePoint bp)
		//{
		//	Parameter p;
		//	p = bp.get_Parameter("E/W");
		//	Double xCoord = p.AsDouble();
		//	p = bp.get_Parameter("N/S");
		//	Double yCoord = p.AsDouble();
		//	p = bp.get_Parameter("Elev");
		//	Double zCoord = p.AsDouble();
		//	return new XYZ(xCoord, yCoord, zCoord);
		//}

		public static Double RadToDegrees(Double inRads)
		{
			return 360 * (inRads / (2 * Math.PI));
		}

		/// <summary>
		/// Only valid for 
		/// </summary>
		/// <param name="feet"></param>
		/// <param name="latitude"></param>
		/// <returns></returns>
		public static Double FeetToLatitude(Double feet, Double latitude)
		{
			return feet / FEET_PER_LAT;
		}

		public static Double FeetToLongitude(Double feet, Double latitude)
		{
			return feet / FEET_PER_LONG;
		}
	}
}
