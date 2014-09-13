using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Reflection;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitMyDrone.DroneBase
{
	public static class FileLocations
	{
		//AddInDirectory is initialized at runtime
		public static String AddInDirectory;
		public static String AssemblyName;
		public static readonly String imperialTemplateDirectory = @"C:\ProgramData\Autodesk\RVT 2014\Family Templates\English_I\";
		public static readonly String ResourceNameSpace = @"RevitMyDrone.DroneBase.Resources";
	}

	public class StartUpApp : IExternalApplication
	{
		Result IExternalApplication.OnStartup(UIControlledApplication application)
		{
			//initialize AssemblyName using reflection
			FileLocations.AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
			//initialize AddInDirectory. The addin should be stored in a directory named after the assembly
			FileLocations.AddInDirectory = application.ControlledApplication.AllUsersAddinsLocation + "\\" + FileLocations.AssemblyName + "\\";

			//load image resources
			BitmapImage largeIcon = GetEmbeddedImageResource("iconLarge.png");
			BitmapImage smallIcon = GetEmbeddedImageResource("iconSmall.png");

			PushButtonData getShotsCommandPushButtonData = new PushButtonData(
					"GetShotsButton", //name of the button
					"Get Shots", //text on the button
					FileLocations.AddInDirectory + FileLocations.AssemblyName + ".dll",
					"RevitMyDrone.DroneBase.Commands.GetShotsCommand");
			getShotsCommandPushButtonData.LargeImage = largeIcon;

			RibbonPanel DroneBaseRibbonPanel = application.CreateRibbonPanel("DroneBase");
			DroneBaseRibbonPanel.AddItem(getShotsCommandPushButtonData);

			return Result.Succeeded;
		}

		Result IExternalApplication.OnShutdown(UIControlledApplication application)
		{
			return Result.Succeeded;
		}

		/// <summary>
		/// Utility method to retrieve an embedded image resource from the assembly
		/// </summary>
		/// <param name="resourceName">The name of the image, corresponding to the filename of the embedded resouce added to the solution</param>
		/// <returns>The loaded image represented as a BitmapImage</returns>
		BitmapImage GetEmbeddedImageResource(String resourceName)
		{
			Assembly asm = Assembly.GetExecutingAssembly();
			Stream str = asm.GetManifestResourceStream(FileLocations.ResourceNameSpace + "." + resourceName);

			BitmapImage bmp = new BitmapImage();
			bmp.BeginInit();
			bmp.StreamSource = str;
			bmp.EndInit();

			return bmp;
		}
	}
}
