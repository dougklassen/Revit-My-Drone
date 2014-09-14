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
			Document dbDoc = definingView.Document;
			return GetLongitude(definingView.Origin, dbDoc);
		}

		public static Double GetCameraElevation(this View3D definingView)
		{
			return definingView.Origin.Z;
		}

		public static Double GetRoiLatitude(this View3D definingView)
		{
			Document dbDoc = definingView.Document;
			//ViewDirection is normalized vector, we will multiply so ROI is 50 feet away
			XYZ roiXYZ = new XYZ(
				definingView.Origin.X - definingView.ViewDirection.X * 50,
				definingView.Origin.Y - definingView.ViewDirection.Y * 50,
				definingView.Origin.Z - definingView.ViewDirection.Z * 50);
			return GetLatitude(roiXYZ, dbDoc);
		}

		public static Double GetRoiLongitude(this View3D definingView)
		{
			Document dbDoc = definingView.Document;
			//ViewDirection is normalized vector, we will multiply so ROI is 50 feet away
			XYZ roiXYZ = new XYZ(
				definingView.Origin.X - definingView.ViewDirection.X * 50,
				definingView.Origin.Y - definingView.ViewDirection.Y * 50,
				definingView.Origin.Z - definingView.ViewDirection.Z * 50);
			return GetLongitude(roiXYZ, dbDoc);
		}

		public static Double GetRoiElevation(this View3D definingView)
		{
			Document dbDoc = definingView.Document;
			//ViewDirection is normalized vector, we will multiply so ROI is 50 feet away
			XYZ roiXYZ = new XYZ(
				definingView.Origin.X - definingView.ViewDirection.X * 50,
				definingView.Origin.Y - definingView.ViewDirection.Y * 50,
				definingView.Origin.Z - definingView.ViewDirection.Z * 50);
			return roiXYZ.Z;
		}

		private static Double GetLatitude(XYZ projCoordsXYZ, Document dbDoc)
		{		
			Double latitude;
			Double baseLatitude = DroneUtils.RadToDegrees(dbDoc.SiteLocation.Latitude);
			XYZ baseCoordsXYZ = TransformToBasePointCoords(projCoordsXYZ, dbDoc);
			latitude = baseLatitude + DroneUtils.FeetToLatitude(baseCoordsXYZ.Y, baseLatitude);

			return latitude;
		}

		private static Double GetLongitude(XYZ projCoordXYZ, Document dbDoc)
		{
			Double longitude;
			Double baseLongitude = DroneUtils.RadToDegrees(dbDoc.SiteLocation.Longitude);
			XYZ baseCoordsXYZ = TransformToBasePointCoords(projCoordXYZ, dbDoc);
			longitude = baseLongitude + DroneUtils.FeetToLongitude(baseCoordsXYZ.X, baseLongitude);

			return longitude;
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
