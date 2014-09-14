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
				return definingView.GetCameraLatitude();
			}
		}

		public Double CameraLongitude
		{
			get
			{
				return definingView.GetCameraLongitude();
			}
		}

		public Double CameraAltitude
		{
			get
			{
				return definingView.GetCameraElevation();
			}
		}

		public Double RoiLatitude
		{
			get
			{
				return definingView.GetRoiLatitude();
			}
		}

		public Double RoiLongitude
		{
			get
			{
				return definingView.GetRoiLongitude();
			}
		}

		public Double RoiAltitude
		{
			get
			{
				return definingView.GetRoiElevation();
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
