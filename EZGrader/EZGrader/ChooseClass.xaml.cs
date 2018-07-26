using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EZGrader;

namespace EZGrader
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChooseClass : ContentPage
	{
        int count;
        string testToGrade;
        EnteredClass classToScore;
        Color buttonTextColor;
        Color frameColor;
        bool testExists;
        List<String> studentList = new List<String>();
        List<String> dictionaryKeys = new List<String>();
        double testQuestions;
        EnteredClass iteratedClass;

        GradedTest testToSubmit;
        Button home = new Button
        {
            Text = "Save Test",
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.End,
            BackgroundColor = Color.FromHex("#1152C9"),
            TextColor = Color.White,
            HeightRequest = 50
        };
        StackLayout mainStack = new StackLayout
        {
            Padding = 0,
            Spacing = 0,
            Margin = 0,
            BackgroundColor = Color.FromHex("#EFF4FC")
        };
        Color buttonColor;
        List<String> classKeysList = new List<string>();

        public ChooseClass (double ques, string testName)
		{
			InitializeComponent();
            testExists = false;
            testToGrade = testName;
            testQuestions = ques;
            count = 0;
            IDictionary<string, object> properties = Application.Current.Properties;


            foreach (KeyValuePair<String, object> item in properties)
            {
                dictionaryKeys.Add(item.Key);
            }
            for(int i = 0; i < properties.Count; ++i)
            {

                if (properties[dictionaryKeys[i]].ToString().Contains("classNamed"))
                {
                    ++count;
                    iteratedClass = JsonConvert.DeserializeObject<EnteredClass>(properties[dictionaryKeys[i]].ToString());
                    classKeysList.Add(iteratedClass.classNamed);
                    if (count % 2 == 0)
                    {
                        buttonColor = Color.FromHex("#2E2D4D");
                        buttonTextColor = Color.White;
                        frameColor = Color.FromHex("#54536D");
                    }
                    else
                    {
                        buttonColor = Color.FromHex("#99C24D");
                        buttonTextColor = Color.Black;
                        frameColor = Color.FromHex("#ABCD6D");
                    }

                    Button classes = new Button
                    {
                        Text = iteratedClass.classNamed,
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        FontSize = 20,
                        HeightRequest = 90,
                        WidthRequest = 160,
                        BackgroundColor = buttonColor,
                        TextColor = buttonTextColor,
                        Margin = 0
                    };
                    Label studentLabel = new Label
                    {
                        Text = "Students: " + iteratedClass.studentList.Count.ToString(),
                        HorizontalOptions = LayoutOptions.Start,
                        HorizontalTextAlignment = TextAlignment.Start,
                        FontSize = 18,
                        VerticalTextAlignment = TextAlignment.Center
                    };
                    Frame buttonFrame = new Frame
                    {
                        Margin = 0,
                        Padding = 0,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = frameColor,
                        Content = new StackLayout
                        {
                            Margin = 0,
                            Padding = 0,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                classes,studentLabel
                            }
                        }
                    };
                    
                    classes.Clicked += CheckDictionary;
                    mainStack.Children.Add(buttonFrame);
                }
                
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

            ScrollView scroll= new ScrollView
                {
                    Content = mainStack,
                    Padding = 0,
                    Margin = 0,
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
        private void CheckDictionary(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            IDictionary<string, object> properties = Application.Current.Properties;
            foreach (String s in classKeysList)
            {
                if (button.Text == s)
                {
                    var classJson = JsonConvert.DeserializeObject<EnteredClass>(properties[s].ToString());
                    classToScore = classJson;
                }
            }
            if (properties.Keys.Contains(testToGrade + classToScore.classNamed))
            {
                testExists = true;
            }
            else
            {
                testExists = false;
            }
            if (testExists)
            {
                DisplayAlert("Test Already Exists", "You already have a Test with that name for this class!! Make sure you chose the right class" +
                             " or edit your test name", "Return");
            }
            else
            {
                EnterScores(sender, args);
            }
        }
        async void EnterScores(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new EnterTestScores(classToScore, testQuestions, testToGrade));
        }
        async void ReturnHome(object sender, EventArgs args)
        {
            var testJson = JsonConvert.SerializeObject(testToSubmit);
            Application.Current.Properties.Add(testToSubmit.testNamed+testToSubmit.testClass, testJson);
            await Application.Current.SavePropertiesAsync();
            await Navigation.PopToRootAsync();
        }
    }
	
}