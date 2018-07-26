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
	public partial class Page1 : ContentPage
	{
        double questions;
        string testToGrade;
        public Page1()
        {
            InitializeComponent();
            stackLayout.BackgroundColor = Color.FromHex("#EFF4FC");
            enterTest.BackgroundColor = Color.FromHex("#2E4057");
            enterTest.IsEnabled = false;
            enterTest.Clicked += ChooseClassPage;
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
                Text = "This page is for saving test" +
                       " scores for each student in your" +
                       " class. In order to use it, you" +
                       " must first enter your class here!",
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            Button newClass = new Button
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
            Label gridLabel = new Label
            {
                Text = "If you instead would like to" + 
                       " see a grid with all the possible" +
                       " scores for a set number of points," +
                       " go here!",
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Start,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))

            };
            Button newGrid = new Button
            {
                Text = "Scores Grid",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(60, 0, 60, 0),
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                BackgroundColor = Color.FromHex("#466365"),
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
                Spacing = 0,
                Margin = 0
            };
            adStack.Children.Add(adLabel);
            superStack.Children.Add(noClasesMainStack);
            superStack.Children.Add(adStack);
            newGrid.Clicked += GoToGrid;
            newClass.Clicked += AddAClass;
            noClassesStack.Children.Add(oopsLabel);
            noClassesStack.Children.Add(errorLabel);
            noClassesStack.Children.Add(newClass);
            goToGridStack.Children.Add(gridLabel);
            goToGridStack.Children.Add(newGrid);
            noClasesMainStack.Children.Add(noClassesStack);
            noClasesMainStack.Children.Add(goToGridStack);
            IDictionary<string, object> properties = Application.Current.Properties;
            if(properties.Count == 0) { Content = superStack; }
        }

        async void GoToGrid(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ScoresGridPage());
        }
        async void ChooseClassPage(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ChooseClass(questions, testToGrade));
        }
        async void AddAClass(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ClassEntry());
        }
        void OnEntryNumChanged(object sender, TextChangedEventArgs args)
        {
            Entry entry = (Entry)sender;
            if (entry.Text.Length > 0)
            {
                if (double.TryParse(QuestionNumberTotal.Text, out double result))
                {
                    questions = double.Parse(QuestionNumberTotal.Text);
                }
            }
            else
            {
                questions = 0;
            }
            if (testName.Text != null && QuestionNumberTotal.Text != null)
            {
                if (entry.Text.Length > 0 && testName.Text.Length > 0)
                {
                    enterTest.IsEnabled = true;
                }
                else { enterTest.IsEnabled = false; }
            }
        }
        void EntryTextChanged(object sender, TextChangedEventArgs args)
        {
            Entry entry = (Entry)sender;
            testToGrade = entry.Text;
            if (testName.Text != null && QuestionNumberTotal.Text != null)
            {
                if (testName.Text.Length > 0 && QuestionNumberTotal.Text.Length > 0)
                {
                    enterTest.IsEnabled = true;
                }
                else { enterTest.IsEnabled = false; }
            }

        }


    }
}
