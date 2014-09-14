using Autodesk.Revit.DB;

using RevitMyDrone.DroneBase.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RevitMyDrone.DroneBase.Models
{
	public class DroneMission
	{
		public DroneWaypoint Home { get; set; }

		public List<DroneShot> Shots { get; set; }

		public DroneMission(IList<View3D> shots)
		{
			Shots = new List<DroneShot>();
			foreach (View3D v in shots)
			{
				Shots.Add(new DroneShot(v));
			}
		}

		public String GetMissionCommandList()
		{
			if (null == Home)
			{
				throw new InvalidOperationException("Must set a location for Home before generating a mission command list");
			}

			StringBuilder missionPlan = new StringBuilder();

			missionPlan.AppendLine("QGC WPL 110");

			missionPlan.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t#HOME",
				"0",
				"1",
				"0",
				"16",	//waypoint
				"0",
				"0",
				"0",
				"0",
				Home.Latitude,	//latitude
				Home.Longitude,	//longitude
				"0",
				"1");
			missionPlan.AppendLine();

			missionPlan.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t#TAKEOFF",
				"1",
				"0",
				"3",
				"22",	//takeoff
				"0",
				"0",
				"0",
				"0",
				Home.Latitude,	//latitude
				Home.Longitude,	//longitude
				"50",	//elevation
				"1");
			missionPlan.AppendLine();

			Int32 i = 2;
			foreach (DroneShot s in Shots)
			{
				missionPlan.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t#{12} ROI",
					i,
					"0",
					"3",
					"201",	//ROI
					"0",
					"0",
					"0",
					"0",
					s.RoiLatitude,	//latitude
					s.RoiLongitude,	//longitude
					s.RoiAltitude,	//elevation
					"1",
					s.Name);	//comment
				missionPlan.AppendLine();

				i++;

				missionPlan.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t#{12}",
					i,
					"0",
					"3",
					"16",	//Waypoint
					"0",
					"0",
					"0",
					"0",
					s.CameraLatitude,	//latitude
					s.CameraLongitude,	//longitude
					s.CameraAltitude,	//elevation
					"1",
					s.Name);	//comment
				missionPlan.AppendLine();

				i++;
			}

			missionPlan.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t#LAND",
				i,
				"0",
				"3",
				"20",	//land
				"0",
				"0",
				"0",
				"0",
				Home.Latitude,	//latitude
				Home.Longitude,	//longitude
				"50",	//elevation
				"1");

			return missionPlan.ToString();
		}

		public String GetDescription()
		{
			StringBuilder desc = new StringBuilder();

			foreach (DroneShot s in Shots)
			{
				desc.AppendLine();
				desc.AppendLine(s.GetDesc());
			}

			return desc.ToString();
		}
	}
}
