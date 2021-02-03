using System.Collections.Generic;
using System.IO;
using System.Linq;
using FormsConcept.Model;
using Xamarin.Forms;

namespace FormsConcept
{
    public class NotesPage : ContentPage
    {
        private ListView _notesListView;

        public NotesPage()
        {
            Title = "Notes";

            ToolbarItem toolbarAdd = new ToolbarItem();
            toolbarAdd.Text = "+";
            toolbarAdd.Clicked += OnNoteAdd_Clicked;

            ToolbarItems.Add(toolbarAdd);


            _notesListView = new ListView();

            _notesListView.Margin = new Thickness(20);
            _notesListView.ItemSelected += OnNoteSelected;

            _notesListView.ItemTemplate = new DataTemplate(() =>
            {
                TextCell textCell = new TextCell();
                textCell.SetBinding(TextCell.TextProperty, "Text");
                textCell.SetBinding(TextCell.DetailProperty, "Date");

                return textCell;
 
            });

            Content = _notesListView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var notes = new List<Note>();

            var files = Directory.EnumerateFiles(App.FolderPath, "*.notes.txt");

            // Iterate through disk files and populate the Note List.
            foreach (var filename in files)
            {
                notes.Add(new Note
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                });
            }

            _notesListView.ItemsSource = notes
                .OrderBy(d => d.Date)
                .ToList();
        }

        private async void OnNoteAdd_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new NotePage
            {
                BindingContext = new Note()
            });
        }

        private async void OnNoteSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new NotePage
                {
                    BindingContext = e.SelectedItem as Note
                });
            }
        }
    }
}

