using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Erme
{
    public partial class BudgetAddPage : ContentPage
    {
        public BudgetAddPage()
        {
            InitializeComponent();
            Title = "Keret hozzáadása";
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            int a = 0;
            if (!string.IsNullOrWhiteSpace(mennyi.Text))
            {
                a = Int32.Parse(mennyi.Text);
            }
            else
            {
                await DisplayAlert("Összeg", "Kérlek adj meg összeget", "Ok");
                return;
            }

            int sz = 0;
            if (szazalek.Value > 0)
            {
                sz = (int)szazalek.Value;
            }
            else
            {
                await DisplayAlert("Százalék", "Kérlek adj meg százalékot", "Ok");
                return;
            }

            DateTime d = datePicker.Date;

            await App.m.AddBudget(a, sz, d);
            await (App.Current as App).NavPage.Navigation.PushAsync(new BudgetPage());


        }

        void szazalek_BindingContextChanged(System.Object sender, System.EventArgs e)
        {
            stepperLabel.Text = szazalek.Value.ToString();
        }
    }
}
