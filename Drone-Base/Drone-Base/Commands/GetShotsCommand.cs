using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using RevitMyDrone.DroneBase.Utils;
using RevitMyDrone.DroneBase.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
			if (0 == droneViews.Count())
			{
				TaskDialog.Show("Found shots", "No Drone shots found");
				return Result.Failed;
			}

			DroneMission mission = new DroneMission(droneViews);

			TaskDialog.Show("Mission Plan", mission.GetDescription());

			SaveFileDialog saveDlg = new SaveFileDialog();
			saveDlg.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
			DialogResult result = saveDlg.ShowDialog();
			if (DialogResult.Cancel == result)
			{
				return Result.Cancelled;
			}

			try
			{
				using(StreamWriter sw = new StreamWriter(saveDlg.FileName))
				{
					sw.Write(mission.GetMissionCommandList());
				}
			}
			catch (Exception)
			{
				TaskDialog.Show("Error", "Could not write " + saveDlg.FileName);
				return Result.Failed;
			}

			return Result.Succeeded;
		}
	}
}
