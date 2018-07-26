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
	public partial class ScoresGridPage : ContentPage
	{
        double questions;
        double scoreFloat;
        Color labelColor;
        Color textColor;
        Button submitUpdate = new Button
        {
            Text = "Update",
            TextColor = Color.White,
            BackgroundColor = Color.FromHex("#466365"),
            VerticalOptions = LayoutOptions.FillAndExpand,
            HorizontalOptions = LayoutOptions.End
        };
        public ScoresGridPage ()
		{
			InitializeComponent ();
            stackLayout0.BackgroundColor = Color.FromHex("#EFF4FC");
            viewGrid.BackgroundColor = Color.FromHex("#466365");
            viewGrid.IsEnabled = false;
            viewGrid.Clicked += SeeScoresGrid;
            submitUpdate.IsEnabled = false;
            submitUpdate.Clicked += UpdateGrid;
        }
        void UpdateTextChanged(object sender, TextChangedEventArgs args)
        {
            Entry entry = (Entry)sender;
            if(entry.Text.Length > 0)
            {
                if(Double.TryParse(entry.Text, out double result))
                {
                    questions = double.Parse(entry.Text);
                    submitUpdate.IsEnabled = true;
                }
                else
                {
                    submitUpdate.IsEnabled = false;
                }
            }
        }
        void UpdateGrid(object sender, EventArgs args)
        {
            SeeScoresGrid(sender, args);
        }
        void OnEntryNumChanged(object sender, TextChangedEventArgs args)
        {
            Entry entry = (Entry)sender;
            if (double.TryParse(entry.Text, out double result))
            {
                if (entry.Text.Length > 0)
                {
                    questions = double.Parse(QuestionNumberTotal.Text);
                    viewGrid.IsEnabled = true;
                }
            }
            else { viewGrid.IsEnabled = false; }
        }
        public void SeeScoresGrid(object sender, EventArgs args)
        {
            if (questions <= 2000)
            {
                StackLayout totalStack = new StackLayout
                {
                    Padding = 0,
                    Spacing = 0,
                    Margin = 0,
                    BackgroundColor = Color.FromHex("#EFF4FC")
                };
                StackLayout scoreStack = new StackLayout();
                scoreStack.BackgroundColor = Color.FromHex("#EFF4FC");
                StackLayout headerStack = new StackLayout();
                headerStack.BackgroundColor = Color.FromHex("#81A7EB");
                scoreStack.Spacing = 0;
                headerStack.Orientation = StackOrientation.Horizontal;
                Label updateQuestions = new Label
                {
                    Text = "Update Questions",
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = 20,
                    TextColor = Color.White,
                    BackgroundColor = Color.Transparent
                };
                Entry updateEntry = new Entry
                {
                    Placeholder = "Points",
                    HorizontalOptions = LayoutOptions.End,
                    HorizontalTextAlignment = TextAlignment.Center,
                    TextColor = Color.Black,
                    BackgroundColor = Color.White,
                    Keyboard = Keyboard.Numeric
                };
                updateEntry.TextChanged += UpdateTextChanged;
                Frame updateFrame = new Frame
                {
                    Margin = 0,
                    Padding = 0,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.FromHex("#5386E4"),
                    Content = new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Margin = 0,
                        Padding = 10,
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            updateQuestions,
                            updateEntry,
                            submitUpdate
                        }
                    }
                };
                Label headerLeft = new Label
                {
                    Text = "Total Correct",
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HeightRequest = 60,
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    TextColor = Color.White
                };
                Label headerRight = new Label
                {
                    Text = "       Score       ",
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HeightRequest = 60,
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    TextColor = Color.White
                };
                headerStack.Children.Add(headerLeft);
                headerStack.Children.Add(headerRight);
                scoreStack.Children.Add(updateFrame);
                scoreStack.Children.Add(headerStack);
                scoreStack.Padding = 0;
                for (int i = 0; i < questions; i++)
                {
                    if (i % 2 == 0)
                    {
                        labelColor = Color.FromHex("#466365");
                        textColor = Color.White;
                    }
                    else
                    {
                        labelColor = Color.FromHex("#99C24D");
                        textColor = Color.Black;
                    }
                    scoreFloat = ((questions - i) / questions);
                    scoreFloat *= 100.0;
                    scoreFloat = Math.Round(scoreFloat, 2);
                    StackLayout subStack = new StackLayout();
                    subStack.Orientation = StackOrientation.Horizontal;
                    subStack.BackgroundColor = labelColor;
                    subStack.Spacing = 0;
                    subStack.Padding = new Thickness(80, 0, 80, 0);
                    Label total = new Label
                    {
                        Text = (questions - i).ToString(),
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Center,
                        HeightRequest = 40,
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        TextColor = textColor
                    };
                    Label score = new Label
                    {
                        Text = scoreFloat.ToString() + "%",
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        HorizontalTextAlignment = TextAlignment.End,
                        VerticalTextAlignment = TextAlignment.Center,
                        HeightRequest = 40,
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        TextColor = textColor
                    };
                    subStack.Children.Add(total);
                    subStack.Children.Add(score);
                    scoreStack.Children.Add(subStack);
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
                    Content = scoreStack,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                totalStack.Children.Add(scroll);
                totalStack.Children.Add(adStack);
                Content = totalStack;
            }
            else
            {
                DisplayAlert("Too Many Questions!", "Grids with over 2,000 questions are a bit too hard to load, and even harder to grade!", "OK");
            }
        }

    }
}