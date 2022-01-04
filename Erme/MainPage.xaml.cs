using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erme.Persistence;
using Xamarin.Forms;

namespace Erme
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Title = "Költések";
            datumok();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }

        private async void datumok()
        {
            List<string> dates = await App.sp.GetSpentDates();
            HonapValaszto.ItemsSource = dates;

            HonapValaszto.SelectedIndex = dates.Count - 1;

        }

        private async void NewSpentButton_Clicked(object sender, EventArgs e)
        {
            await (App.Current as App).NavPage.Navigation.PushAsync(new NewSpentPage());
        }

        private async void CategoriesButton_Clicked(object sender, EventArgs e)
        {
            await (App.Current as App).NavPage.Navigation.PushAsync(new CategoriesPage());
        }

        private async void BudgetButton_Clicked(object sender, EventArgs e)
        {
            await (App.Current as App).NavPage.Navigation.PushAsync(new BudgetPage());
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var content = e.Item as Spent;
            await (App.Current as App).NavPage.Navigation.PushAsync(new SpentDetailsPage(content));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        async void Delete_Clicked(System.Object sender, System.EventArgs e)
        {
            var mi = ((MenuItem)sender);
            bool answer = await DisplayAlert("Törlés", "Biztosan törölni akarod?", "Igen", "Nem");
            if (answer)
            {
                await App.m.Delete(mi.CommandParameter);
                Listing(HonapValaszto.SelectedItem.ToString());
            }
        }

        void HonapValaszto_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            string x = HonapValaszto.SelectedItem.ToString();
            Listing(x);
        }

        async void Listing(string x)
        {
            var xy = await App.sp.CollectedSpents(x);
            collectionView.ItemsSource = xy;


            int aa = 0;
            foreach (var item in xy)
            {
                aa += item.Amount;
            }
            amountAll.Text = aa.ToString("C0");
            var cultureInfo = CultureInfo.GetCultureInfo("en-US");
            amountAllUsd.Text = String.Format(cultureInfo, "{0:C0}", (aa / App.c.usdHuf));
            var cultureInfo2 = CultureInfo.GetCultureInfo("fr-FR");
            amountAllEur.Text = String.Format(cultureInfo2, "{0:C0}", (aa / App.c.eurHuf));

            // ha túllépte
            bool alertIsOn = await App.sp.Alert_if_too_much(x);
            alert.IsVisible = (alertIsOn) ? true : false;
        }

    }
}
