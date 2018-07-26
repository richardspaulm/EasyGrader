using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EZGrader
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClassPage : ContentPage
	{
        public EnteredClass classToEdit;
        List<String> currentClassList = new List<String>();
        List<String> dictionaryKeys = new List<String>();
        List<EnteredClass> classList = new List<EnteredClass>();
        EnteredClass iteratedClass;
        int count;
        Color buttonColor;
        Color buttonTextColor;
        Color frameColor;
        public ClassPage ()
		{
			InitializeComponent ();
            count = 0;

            //No Classes Logged Screen

            StackLayout noClassesStack = new StackLayout
            {
                Padding = 0,
                Spacing = 10,
                BackgroundColor = Color.FromHex("#EFF4FC"),
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            Label oopsLabel = new Label
            {
                Text = "oops!",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#1152C9"),
                HeightRequest = 40,
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };

            Label gridLabel = new Label
            {
                Text = "This page is for viewing and " +
                       "editing your previously added" +
                       " classes, you can add your first" +
                       " class here!",
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.End,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(60, 0, 60, 0),
                HeightRequest = 120
            };
            Button enterClass = new Button
            {
                Text = "Enter Class",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(60, 0, 60, 0),
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                BackgroundColor = Color.FromHex("#2E4057"),
                HeightRequest = 80
            };
            AdMobView adLabel = new AdMobView
            {
                AdUnitId = "ca-app-pub-7713536655172041/3111807400",
                VerticalOptions = LayoutOptions.End
            };
            StackLayout adStack = new StackLayout
            {
                Margin = 0
            };
            StackLayout superStack = new StackLayout
            {
                Margin = 0,
                Spacing = 0
            };
            adStack.Children.Add(adLabel);
            superStack.Children.Add(noClassesStack);
            superStack.Children.Add(adStack);
            enterClass.Clicked += EnterClass;
            noClassesStack.Children.Add(oopsLabel);
            noClassesStack.Children.Add(gridLabel);
            noClassesStack.Children.Add(enterClass);
            IDictionary<string, object> properties = Application.Current.Properties;

            foreach(KeyValuePair<String, object> item in properties)
            {
                dictionaryKeys.Add(item.Key);
            }
            StackLayout mainStack = new StackLayout
            {
                Spacing = 0,
                Margin = 0,
                Padding = 0,
                BackgroundColor = Color.FromHex("EFF4FC")
            };
            for (int i = 0; i < properties.Count; ++i)
            {
                if (properties[dictionaryKeys[i]].ToString().Contains("classNamed"))
                {
                    ++count;
                    iteratedClass = JsonConvert.DeserializeObject<EnteredClass>(properties[dictionaryKeys[i]].ToString());
                    classList.Add(iteratedClass);
                    if (count % 2 == 0)
                    {
                        buttonColor = Color.FromHex("#466365");
                        buttonTextColor = Color.Black;
                        frameColor = Color.FromHex("#677F81");
                    }
                    else
                    {
                        buttonColor = Color.FromHex("#048BA8");
                        buttonTextColor = Color.White;
                        frameColor = Color.FromHex("#31A0B7");
                    }
                    Label className = new Label
                    {
                        Text = iteratedClass.classNamed,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        FontSize = 20,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Center,
                        Margin = new Thickness(4, 8, 4, 4)
                    };
                    Label classStudentNum = new Label
                    {
                        Text = "Number of Students: " + iteratedClass.studentList.Count.ToString(),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        FontSize = 18,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Center,
                        Margin = new Thickness(4, 4, 4, 8)
                    };
                    StackLayout classInfoStack = new StackLayout
                    {
                        Padding = 0,
                        Spacing = 0,
                        Margin = 0,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
                            className,
                            classStudentNum
                        }
                    };
                        Button classes = new Button
                        {
                            Text = "View " + iteratedClass.classNamed,
                            HorizontalOptions = LayoutOptions.End,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            FontSize = 12,
                            WidthRequest = 150,
                            BackgroundColor = buttonColor,
                            TextColor = buttonTextColor,
                            Margin = 0
                        };
                    Frame classFrame = new Frame
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Start,
                        Padding = 0,
                        Margin = 0,
                        BackgroundColor = frameColor,
                        Content = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Padding = 0,
                            Spacing = 0,
                            Margin = 0,
                            Children =
                            {
                                classInfoStack,
                                classes
                            }
                        }
                    };

                        classes.Clicked += ClassSelection;
                        mainStack.Children.Add(classFrame);                   
                }
            }
            if (count == 0)
            {
                Content = superStack;
            }
            else
            {
                AdMobView adMob = new AdMobView
                {
                    AdUnitId = "ca-app-pub-7713536655172041/3111807400"
                };
                StackLayout adStack2 = new StackLayout
                {
                    Margin = 0,
                    VerticalOptions = LayoutOptions.End,
                    Children = { adMob }
                };
                ScrollView scroll = new ScrollView
                {
                    Content = mainStack,
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
        }
        async void ClassSelection(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            string slicedButtonText = button.Text.Substring(5, (button.Text.Length - 5));
            IDictionary<string, object> properties = Application.Current.Properties;
            foreach (EnteredClass c in classList)
            {
                if (slicedButtonText == c.classNamed)
                {
                    classToEdit = c;
                }
            }
            await Navigation.PushAsync(new DisplayClass(classToEdit));
        }
        async void EnterClass(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ClassEntry());
        }
    }
}