using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using RevitMyDrone.DroneBase.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RevitMyDrone.DroneBase.Models
{
	/// <summary>
	/// A drone shot defined by a Revit perspective views
	/// </summary>
	public class DroneShot
	{
		private View3D definingView;

		public String Name
		{
			get
			{
				return definingView.Name;
			}
		}

		public Double CameraLatitude
		{
			get
			{
				Document dbDoc = definingView.Document;
				return GeoRefUtils.GetLatitude(definingView.Origin, dbDoc);
			}
		}

		public Double CameraLongitude
		{
			get
			{
				Document dbDoc = definingView.Document;
				return GeoRefUtils.GetLongitude(definingView.Origin, dbDoc);
			}
		}

		public Double CameraAltitude
		{
			get
			{
				Document dbDoc = definingView.Document;
				return GeoRefUtils.GetAltitude(definingView.Origin, dbDoc);
			}
		}

		public Double RoiLatitude
		{
			get
			{
				Document dbDoc = definingView.Document;
				//ViewDirection is normalized vector, we will multiply so ROI is 50 feet away
				XYZ roiXYZ = new XYZ(
					definingView.Origin.X - definingView.ViewDirection.X * 50,
					definingView.Origin.Y - definingView.ViewDirection.Y * 50,
					definingView.Origin.Z - definingView.ViewDirection.Z * 50);
				return GeoRefUtils.GetLatitude(roiXYZ, dbDoc); ;
			}
		}

		public Double RoiLongitude
		{
			get
			{
			Document dbDoc = definingView.Document;
			//ViewDirection is normalized vector, we will multiply so ROI is 50 feet away
			XYZ roiXYZ = new XYZ(
				definingView.Origin.X - definingView.ViewDirection.X * 50,
				definingView.Origin.Y - definingView.ViewDirection.Y * 50,
				definingView.Origin.Z - definingView.ViewDirection.Z * 50);
			return GeoRefUtils.GetLongitude(roiXYZ, dbDoc);
			}
		}

		public Double RoiAltitude
		{
			get
			{
				Document dbDoc = definingView.Document;
				//ViewDirection is normalized vector, we will multiply so ROI is 50 feet away
				XYZ roiXYZ = new XYZ(
					definingView.Origin.X - definingView.ViewDirection.X * 50,
					definingView.Origin.Y - definingView.ViewDirection.Y * 50,
					definingView.Origin.Z - definingView.ViewDirection.Z * 50);
				return GeoRefUtils.GetAltitude(roiXYZ, dbDoc);
			}
		}

		public DroneShot(View3D dView3DParam)
		{
			definingView = dView3DParam;
		}

		public String GetDesc()
		{
			StringBuilder desc = new StringBuilder();

			desc.AppendFormat("***** {0}", Name);
			desc.AppendFormat("\nCam Lat: {0}\nCam Long: {1}\nCam Alt: {2}", CameraLatitude, CameraLongitude, CameraAltitude);
			desc.AppendFormat("\nROI Lat: {0}\nROI Long: {1}\nROI Alt: {2}", RoiLatitude, RoiLongitude, RoiAltitude);

			return desc.ToString();
		}
	}
}
