using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EZGrader
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTestPage : ContentPage
    {
        GradedTest currentTest;
        List<Entry> entryList = new List<Entry>();
        List<Label> gradeOutputList = new List<Label>();
        Color labelColor;
        Color textColor;
        Color numColor;
        Button saveEdit = new Button
        {
            Text = "Save Changes",
            HorizontalOptions = LayoutOptions.FillAndExpand,
            Margin = new Thickness(20, 20, 20, 16),
            BackgroundColor = Color.FromHex("#99C24D"),
            HeightRequest = 60,
            TextColor = Color.Black
        };
        public EditTestPage(GradedTest passedTest)
        {
            InitializeComponent();
            currentTest = passedTest;
            ViewTest();
        }

        public void ViewTest()
        {
            saveEdit.IsEnabled = false;
            saveEdit.Clicked += SaveEdit;
            entryList.Clear();
            gradeOutputList.Clear();
            StackLayout editStack = new StackLayout
            {
                BackgroundColor = Color.FromHex("#EFF4FC"),
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = 0,
                Spacing = 8
            };
            Label headLabel = new Label
            {
                Text = "Editing: " + currentTest.testNamed,
                BackgroundColor = Color.FromHex("#5386E4"),
                TextColor = Color.White,
                FontSize = 24,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 0)
            };
            editStack.Children.Add(headLabel);
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
                    Text = (i + 1).ToString(),
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
                    BackgroundColor = Color.Transparent,
                    TextColor = textColor,
                    Margin = new Thickness(10, 10, 0, 10)
                };
                Entry score = new Entry
                {
                    Text = currentTest.rawScores[i].ToString(),
                    HorizontalOptions = LayoutOptions.End,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Keyboard = Keyboard.Numeric,
                    BackgroundColor = Color.FromHex("#EFF4FC"),
                    TextColor = Color.Black,
                    WidthRequest = 40,
                    HeightRequest = 40,
                    FontSize = 12,
                    Margin = 0,
                };
                entryList.Add(score);
                Label totalPoints = new Label
                {
                    Text = " / " + currentTest.totalQuestions.ToString(),
                    HorizontalOptions = LayoutOptions.End,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalOptions = LayoutOptions.Center,
                    VerticalTextAlignment = TextAlignment.End,
                    TextColor = textColor
                };
                Label grade = new Label
                {
                    Text = currentTest.scoredScoreList[i].ToString(),
                    HorizontalOptions = LayoutOptions.End,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.End,
                    FontSize = 20,
                    TextColor = textColor,
                    WidthRequest = 100,
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
                        Spacing = 0,
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
                editStack.Children.Add(studentFrame);
            }
            editStack.Children.Add(saveEdit);
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
                Content = editStack,
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
                    scoreFloat /= currentTest.totalQuestions;
                    scoreFloat *= 100;
                    double roundedScore = Math.Round(scoreFloat, 2);
                    gradeOutputList[i].Text = roundedScore.ToString();
                    currentTest.rawScores[i] = Double.Parse(entryList[i].Text);
                    currentTest.scoredScoreList[i] = roundedScore;
                }
                else
                {
                    gradeOutputList[i].Text = "Grade";
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
                saveEdit.IsEnabled = true;
            }
            else
            {
                saveEdit.IsEnabled = false;
            }
        }
        async void SaveEdit(object sender, EventArgs args)
        {
            var editToSubmit = JsonConvert.SerializeObject(currentTest);
            Application.Current.Properties[currentTest.testNamed + currentTest.testClass] = editToSubmit;
            await Application.Current.SavePropertiesAsync();
            if (Navigation.NavigationStack[Navigation.NavigationStack.Count - 2].GetType() == typeof(DisplayTest))
            {
                DisplayTest displayPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2] as DisplayTest;
                displayPage.ViewTest();
                await Navigation.PopAsync();
            }
            else
            {
                await Navigation.PopToRootAsync();
            }
        }

    }
}