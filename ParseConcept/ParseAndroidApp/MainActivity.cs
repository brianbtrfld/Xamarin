using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Parse;

namespace ParseConcept
{
	[Activity (Label = "ParseConcept", MainLauncher = true)]
	public class MainActivity : Activity
	{
		int count = 1;

		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);


			// Initialize the Parse client with your Application ID and Windows Key found on
			// your Parse dashboard
			ParseClient.Initialize("6jx4xfirogzRggfF3Tuh4awVsWj19LtlCDBLEzyU",
				                   "BkEwlXjH6GAr3hhfrkcF7jq5N8sjgiQL5pYZfYn8");


			var testObject = new ParseObject ("TestObject");
			testObject ["foo"] = "bar";
			await testObject.SaveAsync ();


			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};
		}
	}
}


