using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using RevitMyDrone.DroneBase.Utils;
using RevitMyDrone.DroneBase.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RevitMyDrone.DroneBase.Commands
{
	[Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
	public class GetShotsCommand : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			UIDocument uiDoc = commandData.Application.ActiveUIDocument;
			Document dbDoc = commandData.Application.ActiveUIDocument.Document;

			IList<View3D> droneViews = dbDoc.GetDroneViews();
			List<DroneShot> shots = new List<DroneShot>();
			foreach(View3D v in droneViews)
			{
				shots.Add(new DroneShot(v));
			}

			StringBuilder msg = new StringBuilder();

			if (droneViews.Count() > 0)
			{
				foreach (DroneShot s in shots)
				{
					msg.AppendLine();
					msg.AppendLine(s.GetDesc());
				}

				TaskDialog.Show("Found shots", msg.ToString()); 
			}
			else
			{
				TaskDialog.Show("Found shots", "No Drone shots found");
				return Result.Failed;
			}

			return Result.Succeeded;
		}
	}
}
