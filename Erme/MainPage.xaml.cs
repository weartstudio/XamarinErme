using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.m.GetListSpent();

            List<Spent> spents = await App.m.GetListSpent();
            List<DateTime> x = spents.Select(d => new DateTime(d.Date.Year, d.Date.Month, 1)).Distinct().ToList();
            //HonapValaszto.ItemsSource = x;
            foreach (var item in x)
            {
                string datumS = item.Year.ToString() + "-" + item.Month.ToString();
                HonapValaszto.Items.Add(datumS);
            }
            HonapValaszto.SelectedIndex = 1;
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
                OnAppearing();
            }
        }

        void HonapValaszto_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            var x = HonapValaszto.SelectedItem;

        }
    }
}
