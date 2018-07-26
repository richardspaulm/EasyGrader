using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EZGrader
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EnterTestScores : ContentPage
	{
        GradedTest testToSubmit;
        EnteredClass classToScore;
        string testToGrade;
        double testQuestions;
        Color labelColor;
        Color textColor;
        Color numColor;
        List<Entry> entryList = new List<Entry>();
        List<Label> gradeOutputList = new List<Label>();
        Button submitTest = new Button
        {
            Text = "Submit",
            HorizontalOptions = LayoutOptions.FillAndExpand,
            Margin = new Thickness(20, 20, 20, 250),
            BackgroundColor = Color.FromHex("#99C24D"),
            HeightRequest = 60,
            TextColor = Color.Black
        };
        List<Double> rawScoreList = new List<Double>();

        public EnterTestScores (EnteredClass passedClass, double ques, string newTestName)
		{
			InitializeComponent ();
            classToScore = passedClass;
            testToGrade = newTestName;
            testQuestions = ques;
            submitTest.Clicked += OnSubmitClicked;
            submitTest.IsEnabled = false;
            IDictionary<string, object> properties = Application.Current.Properties;
            int labelCount = 0;

            StackLayout classStack = new StackLayout
            {
                BackgroundColor = Color.FromHex("#EFF4FC"),
                Spacing = 8
            };
            Label headLabel = new Label
            {
                Text = "Test: " + testToGrade,
                BackgroundColor = Color.FromHex("#5386E4"),
                TextColor = Color.White,
                FontSize = 30,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                HeightRequest = 60,
                VerticalTextAlignment = TextAlignment.Center
            };
            Label subLabel = new Label
            {
                Text = "Class: " + passedClass.classNamed,
                BackgroundColor = Color.FromHex("#81A7EB"),
                TextColor = Color.Black,
                FontSize = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HeightRequest = 40,
                Margin = new Thickness(0, 0, 0, 20)
            };
            StackLayout headStack = new StackLayout
            {
                Spacing = 0,
                Children = { headLabel, subLabel }
            };
            classStack.Children.Add(headStack);
            foreach (String s in passedClass.studentList)
            {
                if (labelCount % 2 == 0)
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
                labelCount++;
                Label studentNum = new Label
                {
                    Text = labelCount.ToString(),
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    WidthRequest = 15,
                    BackgroundColor = numColor,
                    TextColor = textColor
                };
                Label student = new Label
                {
                    Text = s,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = 15,
                    BackgroundColor = Color.Transparent,
                    TextColor = textColor
                };
                Entry score = new Entry
                {
                    Placeholder = "Score",
                    HorizontalOptions = LayoutOptions.End,
                    HorizontalTextAlignment = TextAlignment.Center,
                    Keyboard = Keyboard.Numeric,
                    TextColor = textColor
                };
                entryList.Add(score);
                Label totalPoints = new Label
                {
                    Text = " / " + testQuestions.ToString(),
                    HorizontalOptions = LayoutOptions.End,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalOptions = LayoutOptions.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = textColor
                };
                Label grade = new Label
                {
                    Text = "Grade",
                    HorizontalOptions = LayoutOptions.End,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.End,
                    WidthRequest = 80,
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    TextColor = textColor,
                    Margin = new Thickness(0, 0, 10, 0)
                };
                gradeOutputList.Add(grade);

                Frame studentFrame = new Frame
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = 0,
                    Margin = new Thickness(10, 0, 10, 0),
                    BackgroundColor = labelColor,
                    Content = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            studentNum,
                            student,
                            score,
                            totalPoints,
                            grade
                        }
                    }
                };
                score.TextChanged += ScoreChanged;
                classStack.Children.Add(studentFrame);
            }

            classStack.Children.Add(submitTest);
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
        void ScoreChanged(object sender, TextChangedEventArgs args)
        {
            int falseCount = 0;
            for (int i = 0; i < entryList.Count; ++i)
            {
                if (Double.TryParse(entryList[i].Text, out double result) && entryList[i].Text != null)
                {
                    double scoreFloat = Double.Parse(entryList[i].Text);
                    scoreFloat /= testQuestions;
                    scoreFloat *= 100;
                    double roundedScore = Math.Round(scoreFloat, 2);
                    gradeOutputList[i].Text = roundedScore.ToString();
                }
                else
                {
                    gradeOutputList[i].Text = 0.ToString();
                }
            }
            foreach (Entry e in entryList)
            {
                if (e.Text == null) { ++falseCount; }
                if (e.Text != null)
                {
                    if (e.Text.Length > 0) { }
                    else { ++falseCount; }
                }
            }
            if (falseCount == 0)
            {
                submitTest.IsEnabled = true;
            }
            else
            {
                submitTest.IsEnabled = false;
            }
        }
        async void OnSubmitClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            Task<bool> task = DisplayAlert("Submit Test?", "This will save your test to your phone! You can always edit the scores later.",
            "Submit", "Not yet");
            bool result = await task;
            if (result) { SubmitTest(button, args); }
            else {  }
        }
        async void SubmitTest(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            IDictionary<string, object> properties = Application.Current.Properties;
            List<Double> scoreList = new List<Double>();
            StackLayout testReview = new StackLayout
            {
                BackgroundColor = Color.FromHex("#EFF4FC")
            };
            foreach (Entry e in entryList)
            {
                rawScoreList.Add(Double.Parse(e.Text));
                double scoreFloat = Double.Parse(e.Text);
                scoreFloat /= testQuestions;
                scoreFloat *= 100;
                double roundedScore = Math.Round(scoreFloat, 2);
                scoreList.Add(roundedScore);
            }
            var test = new GradedTest
            {
                testNamed = testToGrade,
                testClass = classToScore.classNamed,
                scoredNameList = classToScore.studentList,
                scoredScoreList = scoreList,
                totalQuestions = testQuestions,
                rawScores = rawScoreList
            };
            testToSubmit = test;
            var testJson = JsonConvert.SerializeObject(testToSubmit);
            Application.Current.Properties.Add(testToSubmit.testNamed + testToSubmit.testClass, testJson);
            await Application.Current.SavePropertiesAsync();
            await Navigation.PopToRootAsync();
        }

    }
}