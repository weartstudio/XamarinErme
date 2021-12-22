using System;
using System.Collections.Generic;
using Erme.Persistence;
using Xamarin.Forms;

namespace Erme
{
    public partial class SpentDetailsPage : ContentPage
    {
        private Spent _x { get; set; }

        public SpentDetailsPage(Spent x)
        {
            InitializeComponent();

            Title = "Költés részletei";
            _x = x;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _title.Text = _x.Title;
            _amount.Text = _x.Amount.ToString() + " Ft";
            _date.Text = _x.Date.Date.ToShortDateString();

            var cat = await App.m.GetSpecificCategoryById(_x.CategoryId);
            _category.Text = cat.Title;
        }
    }
}
