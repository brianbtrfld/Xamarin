using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace TablesDemo
{
	// TODO: Step 5a: This custom cell has been pre-built.
	public class SessionCell : UITableViewCell
	{
		UILabel titleLabel, timeLabel, speakerLabel;
		UIImageView favoriteImageView;

		public Session Session
		{
			get;
			set;
		}
		// reuseIdentifier is used by DequeueResuableCell method call.
		public SessionCell(string reuseIdentifier) : base(UITableViewCellStyle.Default, reuseIdentifier)
		{
			// NOTE:  {} is "initializer" syntax for object.
			titleLabel = new UILabel { Font = UIFont.FromName("Avenir-Light", 16.0f), BackgroundColor = UIColor.Clear  };

			timeLabel = new UILabel
			{
				Font = UIFont.FromName("Avenir-Light", 14.0f),
				TextColor = UIColor.DarkGray,
				BackgroundColor = UIColor.Clear
			};

			speakerLabel = new UILabel { Font = UIFont.FromName("Avenir-Light", 14.0f), BackgroundColor = UIColor.Clear  };

			ContentView.AddSubview(titleLabel);
			ContentView.AddSubview(timeLabel);
			ContentView.AddSubview(speakerLabel);

			favoriteImageView = new UIImageView();
			favoriteImageView.Image = UIImage.FromBundle("images/favorited");

//			ContentView.AddSubview (favoriteImageView);
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			float padding = 5.0f;

			titleLabel.Text = Session.Title;
			timeLabel.Text = String.Format("{0} - {1}", Session.Begins.ToString("HH:mm"), Session.Ends.ToString("HH:mm"));
			speakerLabel.Text = (Session.Speaker != null) ? Session.Speaker : "";

			// Bounds is relative to the view itself.
			// (0,0) based upper-left origin

			// Frame is relative to the parent or parents coordinates of parent.

			RectangleF b = ContentView.Bounds; // ContentView.Bounds can change! see Accessories or Editing mode

			var titleRect = new RectangleF(b.Left + padding, b.Top + padding, b.Width - 2 * padding, (b.Height / 3) + 4);
			titleLabel.Frame = titleRect;

			var speakerRect = new RectangleF(b.Left + padding, titleRect.Bottom + padding, b.Width / 2 - 2 * padding, b.Height / 3);
			speakerLabel.Frame = speakerRect;	

			var timeRect = new RectangleF(speakerRect.Right + 5 * padding, titleRect.Bottom + padding, b.Width / 2 - 2 * padding, b.Height / 3);
			timeLabel.Frame = timeRect;

			if (Session.IsFavorite == true)
			{
				ContentView.AddSubview(favoriteImageView);
				favoriteImageView.Frame = new RectangleF(b.Width - 42, 5, 38, 38);
			}
			else
			{
				favoriteImageView.RemoveFromSuperview();
			}

		}
	}
}

