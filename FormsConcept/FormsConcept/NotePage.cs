using System;
using System.IO;
using FormsConcept.Model;
using Xamarin.Forms;

namespace FormsConcept
{
    
    public class NotePage : ContentPage
    {
        private Editor _noteEditor;

        public NotePage()
        {
            

            Title = "Note";

            Label noteLabel = new Label
            {
                Text = "Note",
                HorizontalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold,
            };

            _noteEditor = new Editor();
            _noteEditor.Placeholder = "Enter Note Text";
            _noteEditor.HeightRequest = 100;
            _noteEditor.SetBinding(Editor.TextProperty, "Text");

            Grid noteButtonGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                },
            };

            Button saveButton = new Button();
            saveButton.Text = "Save";
            saveButton.Clicked += OnSaveButton_Clicked;

            Button deleteButton = new Button();
            deleteButton.Text = "Delete";
            deleteButton.Clicked += OnDeleteButton_Clicked;

            noteButtonGrid.Children.Add(saveButton);
            noteButtonGrid.Children.Add(deleteButton, 1, 0);

            Content = new StackLayout()
            {
                Margin = new Thickness(10, 35, 10, 10),

                Children =
                {
                    noteLabel,
                    _noteEditor,
                    noteButtonGrid
                }
            };
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            if (string.IsNullOrWhiteSpace(note.Filename))
            {
                // Save.
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
                File.WriteAllText(filename, note.Text);
            }
            else
            {
                // Update.
                File.WriteAllText(note.Filename, note.Text);
            }

            await Navigation.PopAsync();
        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            if (File.Exists(note.Filename) == true)
            {
                File.Delete(note.Filename);
            }

            await Navigation.PopAsync();
        }
    }
}