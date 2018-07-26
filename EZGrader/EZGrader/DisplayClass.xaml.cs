using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EZGrader
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DisplayClass : ContentPage
    {
        int viewLabelCount;
        Color textColor;
        Color frameColor;
        Color opposingColor;
        List<Button> deleteButtonList = new List<Button>();
        Button addStudentButton = new Button
        {
            Text = "Add Student",
            HorizontalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor = Color.FromHex("#99C24D"),
            TextColor = Color.Black,
            Margin = new Thickness(10, 0, 10, 0)
        };
        Button editClassButton = new Button
        {
            Text = "Edit Class",
            HorizontalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor = Color.FromHex("#2E2D4D"),
            TextColor = Color.White
        };
        Button deleteButton = new Button
        {
            Text = "Delete",
            HorizontalOptions = LayoutOptions.End,
            FontSize = 12,
            BackgroundColor = Color.FromHex("#FF3E41"),
            Margin = 0,
            WidthRequest = 80,
            TextColor = Color.Black
        };
        EnteredClass classToEdit;
		public DisplayClass (EnteredClass passedClass)
		{
			InitializeComponent();
            classToEdit = passedClass;
            addStudentButton.Clicked += AddStudentMode;
            editClassButton.Clicked += EditClass;

            deleteButton.Clicked += OnDeleteClassClicked;
            ViewClass();
        }
        async void EditClass(object sender, EventArgs args)
        {
                await Navigation.PushAsync(new EditClassPage(classToEdit));
        }
        async void OnDeleteClassClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            Task<bool> task = DisplayAlert("Delete Class?", "This will remove this class from your list, but test associated with it will remain",
"Delete", "Cancel");
            bool result = await task;
            if (result)
            {
                Application.Current.Properties.Remove(classToEdit.classNamed);
                await Application.Current.SavePropertiesAsync();
                await Navigation.PopToRootAsync();
            }
            else { ViewClass(); }
        }

        async void AddStudentMode(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AddStudentPage(classToEdit));
        }

        public void ViewClass()
        {
            viewLabelCount = 0;
            deleteButtonList.Clear();

            StackLayout classStack = new StackLayout
            {
                BackgroundColor = Color.FromHex("#EFF4FC"),
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 8
            };
            StackLayout headButtons = new StackLayout
            {
                Spacing = 0,
                Margin = 10
            };
            Label headLabel = new Label
            {
                Text = classToEdit.classNamed,
                BackgroundColor = Color.FromHex("#5386E4"),
                TextColor = Color.White,
                FontSize = 30,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            headButtons.Orientation = StackOrientation.Horizontal;
            headButtons.Children.Add(editClassButton);
            headButtons.Children.Add(deleteButton);
            classStack.Children.Add(headLabel);
            classStack.Children.Add(headButtons);
            classStack.Children.Add(addStudentButton);

            foreach (string s in classToEdit.studentList)
            {
                viewLabelCount++;
                if (viewLabelCount % 2 == 0)
                {
                    frameColor = Color.FromHex("#677F81");
                    opposingColor = Color.FromHex("#466365");
                    textColor = Color.White;
                }
                else
                {
                    frameColor = Color.FromHex("#31A0B7");
                    opposingColor = Color.FromHex("#048BA8");
                    textColor = Color.Black;
                }
                Label studentNum = new Label
                {
                    Text = viewLabelCount.ToString(),
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    WidthRequest = 20,
                    BackgroundColor = opposingColor,
                    TextColor = textColor
                };
                Label student = new Label
                {
                    Text = s,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = 15,
                    BackgroundColor = Color.Transparent,
                    TextColor = textColor
                };
                Button deleteStudent = new Button
                {
                    Text = "Delete",
                    HorizontalOptions = LayoutOptions.End,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 10,
                    BackgroundColor = Color.FromHex("#FF3E41"),
                    TextColor = Color.Black
                };
                deleteButtonList.Add(deleteStudent);
                deleteStudent.Clicked += OnDeleteStudentClicked;
                Frame studentFrame = new Frame
                {

                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = 0,
                    BackgroundColor = frameColor,
                    Margin = new Thickness(10, 0, 10, 0),
                    Content = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            studentNum,
                            student,
                            deleteStudent
                        }
                    }
                };
                classStack.Children.Add(studentFrame);
            }

            AdMobView adMob = new AdMobView
            {
                AdUnitId = "ca-app-pub-7713536655172041/3111807400"
            };
            StackLayout adStack = new StackLayout
            {
                Margin = 0,
                VerticalOptions = LayoutOptions.End,
                Children = { adMob }
            };
            ScrollView scroll = new ScrollView
            {
                Content = classStack,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            StackLayout superStack2 = new StackLayout
            {
                Padding = 0,
                Spacing = 0,
                Margin = 0,
                Children = { scroll, adStack }
            };
            Content = superStack2;
        }
        async void OnDeleteStudentClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            Task<bool> task = DisplayAlert("Delete Student?", "This will remove this student from your class list, but not your heart",
            "Delete", "Cancel");
            bool result = await task;
            if (result) { DeleteStudent(button, args); }
            else { ViewClass(); }
        }
        async void DeleteStudent(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            int buttonIndex = deleteButtonList.IndexOf(button);
            classToEdit.studentList.RemoveAt(buttonIndex);
            var editToSubmit = JsonConvert.SerializeObject(classToEdit);
            Application.Current.Properties[classToEdit.classNamed] = editToSubmit;
            await Application.Current.SavePropertiesAsync();
            ViewClass();
        }
    }

}