using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Erme
{
    public partial class NewSpentPage : ContentPage
    {
        public NewSpentPage()
        {
            InitializeComponent();
            Title = "Új költés";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var result = await App.m.GetListCat();
            foreach (var item in result)
            {
                categorySelect.Items.Add(item.Title);
            }
        }


        async void AddBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            string t = "";
            if (!string.IsNullOrWhiteSpace(titleEntry.Text))
            {
                t = titleEntry.Text;
            }
            else
            {
                await DisplayAlert("Név", "Kérlek add meg hogy mire költöttél", "Ok");
                return;
            }

            int a = 0;
            if (!string.IsNullOrWhiteSpace(amountEntry.Text))
            {
                a = Int32.Parse(amountEntry.Text);
            }
            else
            {
                await DisplayAlert("Összeg", "Kérlek adj meg összeget", "Ok");
                return;
            }

            DateTime d = dateSelect.Date; // ennek van alap értéke, tehát nem kell csekkolni

            string c = "";
            if (categorySelect.SelectedIndex != -1)
            {
                c = categorySelect.SelectedItem.ToString();
            }
            else
            {
                await DisplayAlert("Kategória", "Kérlek válassz kategóriát", "Ok");
                return;
            }

            await App.m.AddSpent(t,a,d,c);
            await (App.Current as App).NavPage.Navigation.PushAsync(new MainPage());
        }

    }
}
