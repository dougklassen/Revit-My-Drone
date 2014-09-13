using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RevitMyDrone.DroneBase.Utils
{
	/// <summary>
	/// utilities for extracting geo-location information for Revit views
	/// </summary>
	public static class GeoRefUtils
	{
		public static Double GetCameraLatitude(this View3D definingView)
		{
			Document dbDoc = definingView.Document;
			return GetLatitude(definingView.Origin, dbDoc);
		}

		public static Double GetCameraLongitude(this View3D definingView)
		{
			Double longitude = 0;

			return longitude;
		}

		public static Double GetCameraElevation()
		{
			Double elevation = 0;

			return elevation;
		}

		private static Double GetLatitude(XYZ xyz, Document dbDoc)
		{		
			Double latitude = 0;
			Double baseLatitude = DroneUtils.RadToDegrees(dbDoc.SiteLocation.Latitude);

			return latitude;
		}

		private static XYZ TransformToBasePointCoords(XYZ coords, Document dbDoc)
		{
			Double newX, newY, newZ;
			XYZ inBaseCoords;

			ProjectPosition projPosition = dbDoc.ActiveProjectLocation.get_ProjectPosition(new XYZ(0,0,0));

			newX = coords.X + projPosition.EastWest;
			newY = coords.Y + projPosition.NorthSouth;
			newZ = coords.Z + projPosition.Elevation;

			inBaseCoords = new XYZ(newX, newY, newZ);

			return inBaseCoords;
		}
	}
}
