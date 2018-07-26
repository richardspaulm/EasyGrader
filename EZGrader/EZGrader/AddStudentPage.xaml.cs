using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EZGrader;

namespace EZGrader
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddStudentPage : ContentPage
	{
        int viewLabelCount;
        EnteredClass classToEdit;
        Button commitStudentButton = new Button
        {
            Text = "Add Student",
            HorizontalOptions = LayoutOptions.FillAndExpand,
            Margin = new Thickness(4, 4, 4, 20),
            BackgroundColor = Color.FromHex("#99C24D"),
            TextColor = Color.Black
        };
        List<String> newStudentList = new List<String>();
        Color opposingColor;
        Color textColor;
        Color frameColor;
        String studentToAdd;
        public AddStudentPage (EnteredClass classToView)
		{
			InitializeComponent ();
            classToEdit = classToView;
            viewLabelCount = 0;
            IDictionary<string, object> properties = Application.Current.Properties;
            StackLayout classStack = new StackLayout
            {
                BackgroundColor = Color.FromHex("#EFF4FC"),
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 8
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
            Entry newStudent = new Entry
            {
                Placeholder = "New Student",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = 0
            };
            newStudent.TextChanged += NewStudentEntryChanged;

            commitStudentButton.IsEnabled = false;
            commitStudentButton.Clicked += CommitStudent;
            classStack.Children.Add(headLabel);
            classStack.Children.Add(newStudent);
            classStack.Children.Add(commitStudentButton);
            foreach (String s in classToEdit.studentList)
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
                    Margin = 10,
                    BackgroundColor = Color.Transparent,
                    TextColor = textColor,
                };
                Frame studentFrame = new Frame
                {

                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = 0,
                    Margin = new Thickness(10, 0, 10, 0),
                    BackgroundColor = frameColor,
                    Content = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            studentNum,
                            student
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
            StackLayout superStack = new StackLayout
            {
                Padding = 0,
                Spacing = 0,
                Margin = 0,
                Children = { scroll, adStack }
            };
            Content = superStack;
        }

        void NewStudentEntryChanged(object sender, TextChangedEventArgs args)
        {
            Entry entry = (Entry)sender;
            if (entry.Text != null)
            {
                if (entry.Text.Length > 0)
                {
                    studentToAdd = entry.Text;
                    commitStudentButton.IsEnabled = true;
                }

            }
            else
            {
                commitStudentButton.IsEnabled = false;
            }
        }
        async void CommitStudent(object sender, EventArgs args)
        {
            commitStudentButton.IsEnabled = false;
            IDictionary<string, object> properties = Application.Current.Properties;
            if (!newStudentList.Contains(studentToAdd))
            {
                classToEdit.studentList.Add(studentToAdd);
                classToEdit.studentList.Sort();
                var editToSubmit = JsonConvert.SerializeObject(classToEdit);
                Application.Current.Properties[classToEdit.classNamed] = editToSubmit;
                await Application.Current.SavePropertiesAsync();
            }
            DisplayClass displayPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2] as DisplayClass;
            displayPage.ViewClass();
            await Navigation.PopAsync();
        }
    }
}