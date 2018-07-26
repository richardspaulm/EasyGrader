using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EZGrader
{
	public partial class MainPage : ContentPage
	{

        public MainPage()
		{
            InitializeComponent();

            //Navigation
            scoresGrid.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new ScoresGridPage());
            };
            newTestButton.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new Page1());
            };
            EnterClass.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new ClassEntry());
            };
            myClassesButton.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new ClassPage());
            };
            previousTests.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new TestView());
            };
            //Button Colors
            scoresGrid.BackgroundColor = Color.FromHex("#466365");
            gridFrame.BackgroundColor = Color.FromHex("#677F81");

            newTestButton.BackgroundColor = Color.FromHex("#048BA8");
            newTestFrame.BackgroundColor = Color.FromHex("#31A0B7");

            EnterClass.BackgroundColor = Color.FromHex("#2E4057");
            newClassFrame.BackgroundColor = Color.FromHex("#546275");

            previousTests.BackgroundColor = Color.FromHex("#99C24D");
            previousTestsFrame.BackgroundColor = Color.FromHex("#ABCD6D");

            myClassesButton.BackgroundColor = Color.FromHex("#2E2D4D");
            myClassesFrame.BackgroundColor = Color.FromHex("#54536D");
        }

    }
}
