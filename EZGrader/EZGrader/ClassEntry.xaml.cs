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
    public partial class ClassEntry : ContentPage
    {
        List<String> names = new List<String>();
        List<EntryExt> entryList = new List<EntryExt>();
        Button submit = new Button
        {
            Text = "Submit",
            BackgroundColor = Color.FromHex("#048BA8"),
            HorizontalOptions = LayoutOptions.FillAndExpand,
            Margin = new Thickness(20, 20, 20, 16),
            HeightRequest = 60,
            TextColor = Color.Black
        };
        int falseCount;
        int focusIndex;
        Color labelColor;
        Color textColor;
        bool classExists;
        Color numColor;
        ReturnType returnType;
        public ClassEntry()
        {
            InitializeComponent();
            classExists = false;
            submit.Clicked += OnSubmitClicked;
            submit.IsEnabled = false;
            xamlStack.BackgroundColor = Color.FromHex("#EFF4FC");
            createNewClassButton.BackgroundColor = Color.FromHex("#2E4057");
            createNewClassButton.TextColor = Color.White;

        }
        async void OnSubmitClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            Task<bool> task = DisplayAlert("Add Class?", "You can also edit your class from the 'My Classes' Page",
            "Submit", "Not yet");
            bool result = await task;
            if (result) { StoreAndReturn(button, args); }
            else { }
        }
        async void StoreAndReturn(object sender, EventArgs args)
        {
            names.Sort();
            var newClass = new EnteredClass
            {
                classNamed = className.Text,
                studentList = names
        };
            var classJson = JsonConvert.SerializeObject(newClass);
            Application.Current.Properties.Add(newClass.classNamed, classJson);
            await Application.Current.SavePropertiesAsync();
            await Navigation.PopToRootAsync();
        }
        void OnEntryNumChanged(object sender, TextChangedEventArgs args)
        {
            classExists = false;
            Entry entry = (Entry)sender;
            if (Double.TryParse(studentNum.Text, out double result) && className.Text != null)
            {
                createNewClassButton.IsEnabled = true;
            }
            else { createNewClassButton.IsEnabled = false; }
        }
        public void CheckDictionary(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            IDictionary<string, object> properties = Application.Current.Properties;
            foreach(String s in properties.Keys)
            {
                if(s == className.Text)
                {
                    classExists = true;
                }
            }
            if (classExists)
            {
                DisplayAlert("Class Already Exists", "You already have a class with that Name! You can delete or edit that class, or" +
                             " you can rename this one", "Return");
            }
            else
            {
                GenerateStudentLabels();
            }
        }
        public void GenerateStudentLabels()
        {
            StackLayout classInfoStack = new StackLayout { BackgroundColor = Color.FromHex("#EFF4FC"), Spacing = 8 };
            StackLayout classHeader = new StackLayout();
            classHeader.Spacing = 0;

            Label headLabel = new Label
            {
                Text = className.Text,
                BackgroundColor = Color.FromHex("#5386E4"),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = 40,
                HeightRequest = 55,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.White
            };
            Label studentNumLabel = new Label
            {
                Text = "Number of Students: " + studentNum.Text,
                BackgroundColor = Color.FromHex("#81A7EB"),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = 20,
                HeightRequest = 35,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                Margin = new Thickness(0, 0, 0, 10)
            };

            classHeader.Children.Add(headLabel);
            classHeader.Children.Add(studentNumLabel);
            classInfoStack.Children.Add(classHeader);
            if (int.TryParse(studentNum.Text, out int result))
            {
                for (int i = 0; i < int.Parse(studentNum.Text); i++)
                {
                    if(i == (int.Parse(studentNum.Text) - 1))
                    {
                        returnType = ReturnType.Done;
                    }
                    else
                    {
                        returnType = ReturnType.Next;
                    }
                    if(i % 2 == 0)
                    {
                        labelColor = Color.FromHex("#ABCD6D");
                        numColor = Color.FromHex("#99C24D");
                        textColor = Color.Black;
                    }
                    else
                    {
                        labelColor = Color.FromHex("#54536D");
                        numColor = Color.FromHex("#2E2D4D");
                        textColor = Color.White;
                    }

                    Label nameNum = new Label
                    {
                        Text = (i + 1).ToString(),
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = numColor,
                        TextColor = textColor,
                        WidthRequest = 25,
                        FontSize = 15,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center
                    };
                    EntryExt name = new EntryExt
                    {
                        Placeholder = "Last name, First name",
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HorizontalTextAlignment = TextAlignment.Start,
                        FontSize = 15,
                        BackgroundColor = labelColor,
                        TextColor = textColor,
                        Margin = 0,
                        ReturnType = returnType
                    };
                    Frame nameFrame = new Frame
                    {
                        Padding = 0,
                        Margin = new Thickness(10, 0, 10, 0),
                        Content = new StackLayout
                        {
                            Padding = 0,
                            Spacing = 0,
                            Orientation = StackOrientation.Horizontal,
                            Children = { nameNum, name }
                        }
                    };
                    name.Focused += FocusUp;
                    name.Completed += GoNextName;
                    entryList.Add(name);
                    name.TextChanged += OnStudentNameChanged;
                    classInfoStack.Children.Add(nameFrame);
                    names.Add(name.Text);
                }

                classInfoStack.Children.Add(submit);
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
                    Content = classInfoStack,
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
            else
            {
                DisplayAlert("Uh oh", "This app only supports whole students", "Return");
            }
        }
        void FocusUp(object sender, EventArgs args)
        {
            for(int i = 0; i < names.Count; ++i)
            {
                if (entryList[i].IsFocused)
                {
                    focusIndex = i;
                }
            }
        }
        void OnStudentNameChanged(object sender, TextChangedEventArgs args)
        {
            falseCount = 0;
            EntryExt entry = (EntryExt)sender;
            for (int i = 0; i < names.Count; ++i)
            {
                if (entryList[i].Text != null)
                    {
                        names[i] = entryList[i].Text;
                }
                else if(entryList[i].Text == null || entryList[i].Text.Length == 0)
                {
                    ++falseCount;
                }
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
        void GoNextName(object sender, EventArgs args)
        {
            EntryExt entry = (EntryExt)sender;
            if (focusIndex < (entryList.Count - 1))
            {
                entryList[focusIndex + 1].Focus();
            }
            else { }
        }
	}
}