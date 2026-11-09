using Counter.Models;
using Counter.Storage;

namespace Counter.Views
{
    public partial class CounterPage : ContentPage
    {
        public CounterPage()
        {
            InitializeComponent();
        }

        private async void Add_Clicked(object? sender, EventArgs e)
        {
            string name = NameEntry.Text;

            if (string.IsNullOrWhiteSpace(name))
            {
                await DisplayAlert("Warning", "Enter a counter name", "OK");
                return;
            }

            CounterItem newCounter = new CounterItem();
            newCounter.Name = name;
            newCounter.Value = 0;

            CounterStorage.counters.Add(newCounter);
            CounterStorage.SaveCounters();

            await Shell.Current.GoToAsync("..");
        }
    }
}