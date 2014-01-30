using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace TablesDemo 
{
	public class Application 
	{
		public static void Main (string[] args)
		{
			try 
			{
				// AppDelegate class name needs to match Register attribute below.
				UIApplication.Main (args, null, "AppDelegate");
			} 
			catch (Exception e) 
			{
				Console.WriteLine (e.ToString ());
			}
		}
	}

	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			window.MakeKeyAndVisible ();

			// Always need one RootViewController.
			window.RootViewController = new SpeakersViewController ();
			return true;
		}
	}
}