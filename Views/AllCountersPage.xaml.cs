using Counter.Models;
using System.Threading.Tasks;

namespace Counter.Views
{
    public partial class AllCountersPage : ContentPage
    {
        public AllCountersPage()
        {
            InitializeComponent();

            BindingContext = new Models.AllCounters();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ((Models.AllCounters)BindingContext).LoadCounters();
        }

        private async void countersCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0)
            {
                var counter = (Models.Counter)e.CurrentSelection[0];

                await Shell.Current.GoToAsync($"{nameof(CounterPage)}?{nameof(CounterPage.ItemId)}={counter.Filename}");

                countersCollection.SelectedItem = null;
            }
        }

        private async void Add_Clicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CounterPage));
        }
    }
}