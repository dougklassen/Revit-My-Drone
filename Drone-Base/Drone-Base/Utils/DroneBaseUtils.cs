using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RevitMyDrone.DroneBase.Utils
{
	public static class DroneBaseUtils
	{
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
	}
}
