using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Erme
{
    public partial class BudgetPage : ContentPage
    {
        public BudgetPage()
        {
            InitializeComponent();
            Title = "Költségkeretek";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            budgetView.ItemsSource = await App.m.GetListBudget();
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

        async void Add_Clicked(System.Object sender, System.EventArgs e)
        {
            await (App.Current as App).NavPage.Navigation.PushAsync(new BudgetAddPage());
        }
    }
}
