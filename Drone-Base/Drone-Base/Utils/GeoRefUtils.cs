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
		public static readonly Double METERS_PER_FOOT = 0.3048;

		public static Double GetLatitude(XYZ projCoordsXYZ, Document dbDoc)
		{		
			Double latitude;
			Double baseLatitude = DroneUtils.RadToDegrees(dbDoc.SiteLocation.Latitude);
			XYZ baseCoordsXYZ = TransformToBasePointCoords(projCoordsXYZ, dbDoc);
			latitude = baseLatitude + DroneUtils.FeetToLatitude(baseCoordsXYZ.Y, baseLatitude);

			return latitude;
		}

		public static Double GetLongitude(XYZ projCoordXYZ, Document dbDoc)
		{
			Double longitude;
			Double baseLongitude = DroneUtils.RadToDegrees(dbDoc.SiteLocation.Longitude);
			XYZ baseCoordsXYZ = TransformToBasePointCoords(projCoordXYZ, dbDoc);
			longitude = baseLongitude + DroneUtils.FeetToLongitude(baseCoordsXYZ.X, baseLongitude);

			return longitude;
		}

		public static Double GetAltitude(XYZ projCoordXYZ, Document dbDoc)
		{
			return projCoordXYZ.Z * METERS_PER_FOOT;
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
