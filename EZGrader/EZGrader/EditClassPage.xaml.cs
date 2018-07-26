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
	public partial class EditClassPage : ContentPage
	{

        Button submit = new Button
        {
            Text = "Submit",
            BackgroundColor = Color.FromHex("#99C24D"),
            HorizontalOptions = LayoutOptions.FillAndExpand,
            TextColor = Color.Black
        };
        List<Button> deleteButtonList = new List<Button>();
        List<Entry> studentEditList = new List<Entry>();
        int viewLabelCount;
        int falseCount;
        EnteredClass classToEdit;
        Color opposingColor;
        Color textColor;
        Color frameColor;
        public EditClassPage (EnteredClass classToView)
		{
			InitializeComponent ();
            classToEdit = classToView;
            deleteButtonList.Clear();
            studentEditList.Clear();
            submit.IsEnabled = false;
            submit.Clicked += SubmitEdit;
            submit.IsEnabled = false;
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
                Text = "Editing - " + classToEdit.classNamed,
                BackgroundColor = Color.FromHex("#5386E4"),
                TextColor = Color.White,
                FontSize = 24,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            classStack.Children.Add(headLabel);

            foreach (String s in classToEdit.studentList)
            {
                viewLabelCount++;
                if (viewLabelCount % 2 == 0)
                {
                    frameColor = Color.FromHex("#31A0B7");
                    textColor = Color.Black;
                    opposingColor = Color.FromHex("#048BA8");

                }
                else
                {
                    frameColor = Color.FromHex("#677F81");
                    textColor = Color.White;
                    opposingColor = Color.FromHex("#466365");
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
                Entry student = new Entry
                {
                    Text = s,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Start,
                    FontSize = 20,
                    BackgroundColor = Color.Transparent,
                    TextColor = textColor
                };
                student.TextChanged += StudentNameEdited;
                studentEditList.Add(student);

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

            classStack.Children.Add(submit);
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
        async void SubmitEdit(object sender, EventArgs args)
        {
            classToEdit.studentList.Sort();
            var editToSubmit = JsonConvert.SerializeObject(classToEdit);
            Application.Current.Properties[classToEdit.classNamed] = editToSubmit;
            await Application.Current.SavePropertiesAsync();
            DisplayClass displayPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2] as DisplayClass;
            displayPage.ViewClass();
            await Navigation.PopAsync();
        }

        void StudentNameEdited(object sender, TextChangedEventArgs args)
        {

            falseCount = 0;
            Entry entry = (Entry)sender;
            for (int i = 0; i < studentEditList.Count; ++i)
            {
                if (studentEditList[i].Text != null)
                {
                    classToEdit.studentList[i] = studentEditList[i].Text;
                    if (classToEdit.studentList[i].Length == 0) { ++falseCount; }
                }

                if (falseCount == 0)
                {
                    submit.IsEnabled = true;
                }
                else
                {
                    submit.IsEnabled = false;
                }
            }
        }
        async void OnDeleteStudentClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            Task<bool> task = DisplayAlert("Delete Student?", "This will remove this student from your class list, but not your heart",
            "Delete", "Cancel");
            bool result = await task;
            if (result) { DeleteStudent(button, args); }
            else {  }
        }
        async void DeleteStudent(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            int buttonIndex = deleteButtonList.IndexOf(button);
            classToEdit.studentList.RemoveAt(buttonIndex);
            var editToSubmit = JsonConvert.SerializeObject(classToEdit);
            Application.Current.Properties[classToEdit.classNamed] = editToSubmit;
            await Application.Current.SavePropertiesAsync();
            await Navigation.PopAsync();
        }



    }
}