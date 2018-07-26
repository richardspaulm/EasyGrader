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
	public partial class DisplayTest : ContentPage
	{
        GradedTest currentTest;

        Button editButton = new Button
        {
            Text = "Edit Test",
            HorizontalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor = Color.FromHex("#99C24D"),
            TextColor = Color.Black
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
        Color labelColor;
        Color textColor;
        Color numColor;
        //Main Function
        public DisplayTest (GradedTest testToView)
		{
			InitializeComponent ();
            currentTest = testToView;
            deleteButton.Clicked += OnDeleteClicked;
            ViewTest();       
        }
        void GoToTest(object sender, EventArgs args)
        {
            ViewTest();
        }
        public void ViewTest()
        {
            StackLayout headStack = new StackLayout
            {
                Padding = 0,
                Spacing = 0
            };
            StackLayout mainStack = new StackLayout
            {
                BackgroundColor = Color.FromHex("#EFF4FC"),
                Spacing = 8
            };
            StackLayout buttonStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = 0,
                Spacing = 0,
                Margin = new Thickness(10, 0, 10, 10)
            };
            //Header Information
            Label testNameLabel = new Label
            {
                Text = currentTest.testNamed,
                BackgroundColor = Color.FromHex("#5386E4"),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = 40,
                HeightRequest = 55,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.White
            };
            Label classNameLabel = new Label
            {
                Text = "Class: " + currentTest.testClass,
                BackgroundColor = Color.FromHex("#81A7EB"),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = 20,
                HeightRequest = 35,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black
            };
            headStack.Children.Add(testNameLabel);
            headStack.Children.Add(classNameLabel);
            mainStack.Children.Add(headStack);

            //Button Stack
            editButton.Clicked += EditTest;
            buttonStack.Children.Add(editButton);
            buttonStack.Children.Add(deleteButton);
            mainStack.Children.Add(buttonStack);

            //Score Frames
            for (int i = 0; i < currentTest.scoredNameList.Count; ++i)
            {
                if (i % 2 == 0)
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
                Label studentNum = new Label
                {
                    Text = (i+1).ToString(),
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    WidthRequest = 20,
                    BackgroundColor = numColor,
                    TextColor = textColor
                };
                Label student = new Label
                {
                    Text = currentTest.scoredNameList[i],
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = 15,
                    Margin = new Thickness(10, 10, 0, 10),
                    BackgroundColor = Color.Transparent,
                    TextColor = textColor
                };
                Label score = new Label
                {
                    Text = currentTest.rawScores[i].ToString() + "/" + currentTest.totalQuestions.ToString(),
                    HorizontalOptions = LayoutOptions.End,
                    Margin = new Thickness(10, 0, 10, 0),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = textColor
                };
                Label grade = new Label
                {
                    Text = currentTest.scoredScoreList[i].ToString() + "%",
                    HorizontalOptions = LayoutOptions.End,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.End,
                    WidthRequest = 120,
                    FontSize = 20,
                    TextColor = textColor,
                    Margin = new Thickness(0, 0, 10, 0)

                };
                Frame scoreFrame = new Frame
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = 0,
                    Margin = new Thickness(10, 0, 10, 0), 
                    BackgroundColor = labelColor,
                    Content = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Spacing = 0,
                        Children =
                        {
                            studentNum,
                            student,
                            score,
                            grade
                        }
                    }
                };
                mainStack.Children.Add(scoreFrame);
            }
            //Final Content Scrollview
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
                Content = mainStack,
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
        async void EditTest(object sender, EventArgs args)
        {
            editButton.IsEnabled = false;
            await Navigation.PushAsync(new EditTestPage(currentTest));
            editButton.IsEnabled = true;   

        }



        async void OnDeleteClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            Task<bool> task = DisplayAlert("Delete Test?", "Remove this test from your phone?",
            "Goodbye", "Please no");
            bool result = await task;
            if (result) { DeleteTest(button, args); }
            else { ViewTest(); }
        }
        async void DeleteTest(object sender, EventArgs args)
        {
            Application.Current.Properties.Remove(currentTest.testNamed+currentTest.testClass);
            await Application.Current.SavePropertiesAsync();
            await Navigation.PopToRootAsync();
        }
    }
}