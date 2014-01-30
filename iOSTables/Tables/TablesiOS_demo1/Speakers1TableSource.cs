using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace TablesDemo
{
	// TODO: Step 1b: uncomment to implement SpeakersTableSource
	public class SpeakersTableSource : UITableViewSource
	{		
		static readonly string speakerCellId = "SpeakerCell";

		string[] data;

		public SpeakersTableSource (string[] speakers)
		{
			data = speakers;
		}
		
		public override int RowsInSection (UITableView tableview, int section)
		{
			// REQUIRED override.
			// Groups or Sections, logic group. 
			// section == 0 indexed.

			return data.Length; // only one section
		}
		
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// REQUIRED override.
			// indexPath == 
			// objective-c does not support a concept of Namespace
			// 2-3 letter class prefix name is supposed to be used to define origin
			// UI prefix = UIKit classes
			// NS prefix = Next Step OS classes
			// MC prefix = Mac classes

			var cell = tableView.DequeueReusableCell (speakerCellId);

			if (cell == null) 
			{
				// Avoid creating brand new cells for all possible number of rows, using
				// DequeueReusableCell to grab existing cell that might have been scrolled
				// off the screen.
				cell = new UITableViewCell (UITableViewCellStyle.Default, speakerCellId);
			}

			cell.TextLabel.Text = data [indexPath.Row];
			return cell;
		}
		
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var speaker = data [indexPath.Row];
			
			new UIAlertView ("Speaker Selected", speaker, null, "OK", null).Show ();
			
			tableView.DeselectRow (indexPath, true);
		}
	}
}

