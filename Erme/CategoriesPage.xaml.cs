using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Erme
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public CategoriesPage()
        {
            InitializeComponent();
            Title = "Kategóriák";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            MyListView.ItemsSource = await App.m.GetListCat();
        }

        async void CatAddBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            string s = CatTitle.Text; 
            CatTitle.Text = "";
            if (!string.IsNullOrWhiteSpace(s))
            {
                await App.m.AddCategory(s);
            }
            else
            {
                await DisplayAlert("Kategória", "Kérlek adj meg egy nevet a kategória létrhozásához", "Ok");
                return;
            }
            OnAppearing();
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

    }
}
