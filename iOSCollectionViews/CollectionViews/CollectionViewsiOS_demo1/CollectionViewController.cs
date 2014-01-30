using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CollectionViewDemo
{
	public class CollectionViewController : UICollectionViewController
	{
		static readonly NSString cellId = new NSString ("ImageCell");
		static readonly NSString headerId = new NSString ("Header");

		// used to keep the cell on top of other cells when scaled while highlighting
		int cellZIndex = 1;

		public Speakers Speakers { get; private set; }

		public CollectionViewController (UICollectionViewLayout layout) : base (layout)
		{
			Speakers = new Speakers ();

			// TODO: Step1b: set size and color of the UICollectionView
			CollectionView.ContentSize = UIScreen.MainScreen.Bounds.Size;
			CollectionView.BackgroundColor = UIColor.White;

		}
            
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
             
			// TODO: Step1c: register the ImageCell so it can be created from a DequeueReusableCell call 
			// NOTE:  System will create ImageCell for us.
			CollectionView.RegisterClassForCell (typeof(ImageCell), cellId); // Now we need a custom cell class! (step 1d)
		}
        

		// TODO: Step1e: implement the UICollectionViewSource methods
		public override int GetItemsCount (UICollectionView collectionView, int section)
		{
			// Equivalent to GetRowsCount on TableViews.
			return Speakers.Count;
		}
           
		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			// get an ImageCell from the pool. DequeueReusableCell will create one if necessary
			ImageCell imageCell = (ImageCell)collectionView.DequeueReusableCell (cellId, indexPath);

			// NOTE: there is no creating of the cell like table view cell.

			// update the image for the speaker
			imageCell.UpdateImage (Speakers [indexPath.Row].ImageFile);
                
			return imageCell;
		} 

		public override void ItemSelected (UICollectionView collectionView, NSIndexPath indexPath)
		{
			// Equivalent to RowSelected in TableViews.

			// do something when the item is selected (eg a new ViewController)
			Console.WriteLine ("selected " + indexPath.Row);
		}
	}          
}

