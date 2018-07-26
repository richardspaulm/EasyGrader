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
	public partial class TestView : ContentPage
    {
        List<GradedTest> testList = new List<GradedTest>();
        GradedTest testSelected;
        List<String> dictionaryKeys = new List<String>();
        int count;
        Color buttonColor;
        Color textColor;
        Color frameColor;
        GradedTest iteratedTest;
		public TestView ()
		{
			InitializeComponent ();
            count = 0;
            //No Classes Logged Screen
            StackLayout noClasesMainStack = new StackLayout
            {
                Padding = 0,
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            StackLayout noClassesStack = new StackLayout
            {
                Padding = 0,
                Spacing = 10,
                BackgroundColor = Color.FromHex("#EFF4FC"),
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            StackLayout goToGridStack = new StackLayout
            {
                Padding = 0,
                Spacing = 10,
                BackgroundColor = Color.FromHex("#FFDFA5"),
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
            Label errorLabel = new Label
            {
                Text = "This page is for viewing tests" +
                       " that you have already graded." +
                       " If you have already entered your class" +
                       " and want to add a new test, click here!",
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            Button newTest = new Button
            {
                Text = "New Test",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(60, 0, 60, 0),
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                BackgroundColor = Color.FromHex("#048BA8"),
                HeightRequest = 80
            };
            Label gridLabel = new Label
            {
                Text = "Otherwise, if you have not yet " +
                       "added your class list, you can" +
                       " hit this button to do so",
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Start,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))

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
            StackLayout superStack = new StackLayout
            {
                Spacing = 0
            };
            StackLayout adStack = new StackLayout
            {
                Margin = 0,
                VerticalOptions = LayoutOptions.End
            };
            adStack.Children.Add(adLabel);
            superStack.Children.Add(noClasesMainStack);
            superStack.Children.Add(adStack);
            enterClass.Clicked += EnterClass;
            newTest.Clicked += AddATest;
            noClassesStack.Children.Add(oopsLabel);
            noClassesStack.Children.Add(errorLabel);
            noClassesStack.Children.Add(newTest);
            goToGridStack.Children.Add(gridLabel);
            goToGridStack.Children.Add(enterClass);
            noClasesMainStack.Children.Add(noClassesStack);
            noClasesMainStack.Children.Add(goToGridStack);
            IDictionary<string, object> properties = Application.Current.Properties;
            foreach (KeyValuePair<String, object> item in properties)
            {
                dictionaryKeys.Add(item.Key);
            }
            StackLayout mainStack = new StackLayout
            {
                Padding = 0,
                Spacing = 0,
                Margin = 0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex("#EFF4FC")
            };
            for (int i = 0; i < properties.Count; ++i)
            {               
                if (properties[dictionaryKeys[i]].ToString().Contains("scoredNameList"))
                {
                    if (count % 2 == 0)
                    {
                        buttonColor = Color.FromHex("#2E4057");
                        frameColor = Color.FromHex("#546275");
                        textColor = Color.White;
                    }
                    else
                    {
                        buttonColor = Color.FromHex("#99C24D");
                        frameColor = Color.FromHex("#ABCD6D");
                        textColor = Color.Black;
                    }
                    iteratedTest = JsonConvert.DeserializeObject<GradedTest>(properties[dictionaryKeys[i]].ToString());
                    testList.Add(iteratedTest);                    
                    ++count;
                    Label testNameLabel = new Label
                    {
                        Text = "Test: " + iteratedTest.testNamed,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        FontSize = 20,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Center,
                        Margin = new Thickness(4, 8, 4, 4)
                    };
                    Label testClassLabel = new Label
                    {
                        Text = "Class: " + iteratedTest.testClass,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        FontSize = 18,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Center,
                        Margin = new Thickness(4, 4, 4, 8)
                    };
                    StackLayout testInfoStack = new StackLayout
                    {
                        Padding = 0,
                        Spacing = 0,
                        Margin = 0,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
                            testNameLabel,
                            testClassLabel
                        }
                    };
                    Button testButton = new Button
                    {
                        Text = "View: " + iteratedTest.testNamed,
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        FontSize = 16,
                        BackgroundColor = buttonColor,
                        TextColor = textColor,
                        Margin = 0,
                        WidthRequest = 180
                    };
                    Frame testFrame = new Frame
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
                                testInfoStack,
                                testButton
                            }
                        }
                    };
                    testButton.Clicked += TestSelected;
                    mainStack.Children.Add(testFrame);
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
        async void EnterClass(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ClassEntry());
        }
        async void AddATest(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Page1());
        }

        async void TestSelected(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            IDictionary<string, object> properties = Application.Current.Properties;
            foreach(GradedTest t in testList)
            {
                if (button.Text == "View: " + t.testNamed)
                {
                    testSelected = t;
                }
            }
            await Navigation.PushAsync(new DisplayTest(testSelected));
        }
	}
}