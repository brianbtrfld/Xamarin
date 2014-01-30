using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace TablesDemo
{
	public class SessionsTableSource : UITableViewSource
	{
		static readonly string sessionCellId = "SessionCell";
		List<Session> data;
		IGrouping<int, Session>[] grouping;
		// sub-group of speakers in each index
		public SessionsTableSource (List<Session> sessions)
		{
			data = sessions;
			grouping = GetSessionsGroupedByDate ();
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return grouping [section].Count ();
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return grouping.Count ();
		}

		// TODO: Step 3a: uncomment to add a title to the header over each section
		public override string TitleForHeader (UITableView tableView, int section)
		{
			return grouping [section].ElementAt (0).Begins.Date.ToString ("dd MMM yyyy");
		}

		public override string TitleForFooter (UITableView tableView, int section)
		{
			return string.Format ("{0} total sessions.", grouping [section].Count ());
		}

		// CUSTOMIZE header.
//		public override UIView GetViewForHeader (UITableView tableView, int section)
//		{
//			// NOTE: Don't call the base implementation on a Model class
//			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
//		
//			return new UIView (new RectangleF (0, 0, tableView.Bounds.Width, 50)) { BackgroundColor = UIColor.Red};
//		}

		// CUSTOMIZE footer.
//		public override UIView GetViewForFooter (UITableView tableView, int section)
//		{
//			// NOTE: Don't call the base implementation on a Model class
//			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
//			throw new NotImplementedException ();
//		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var sessionGroup = grouping [indexPath.Section];
			var session = sessionGroup.ElementAt (indexPath.Row);
			
			new UIAlertView ("Session Selected", session.Title, null, "OK", null).Show ();
			
			tableView.DeselectRow (indexPath, true);
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (sessionCellId);
			
			var sessionGroup = grouping [indexPath.Section];
			var session = sessionGroup.ElementAt (indexPath.Row);
			
			if (cell == null)
			{
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, sessionCellId);	
			}

			cell.TextLabel.Text = session.Title;
			cell.DetailTextLabel.Text = session.Location + "; " + session.Speaker;
			
			return cell;
		}

		// This method groups the Sessions by date
		IGrouping<int, Session>[] GetSessionsGroupedByDate ()
		{
			var sessionsGrouped = (from s in data
			                       group s by s.Begins.Day into g
			                       select g).ToArray ();

			return sessionsGrouped;
		}
	}
}

