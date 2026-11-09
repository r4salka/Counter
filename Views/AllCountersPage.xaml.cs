using Counter.Models;
using Counter.Storage;

namespace Counter.Views
{
    public partial class AllCountersPage : ContentPage
    {
        public AllCountersPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CountersLayout.Children.Clear();

            foreach (CounterItem counter in CounterStorage.counters)
            {
                CreateCounter(counter);
            }
        }

        private async void Add_Clicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CounterPage));
        }

        private void CreateCounter(CounterItem counter)
        {
            Label nameLabel = new Label();
            nameLabel.Text = counter.Name;
            nameLabel.Style = (Style)CountersLayout.Resources["NameStyle"];

            Label valueLabel = new Label();
            valueLabel.Text = counter.Value.ToString();
            valueLabel.Style = (Style)CountersLayout.Resources["ValueStyle"];

            Button plusButton = new Button();
            plusButton.Text = "+";

            Button minusButton = new Button();
            minusButton.Text = "-";

            plusButton.Clicked += (sender, e) =>
            {
                counter.Value++;
                valueLabel.Text = counter.Value.ToString();
                CounterStorage.SaveCounters();
            };

            minusButton.Clicked += (sender, e) =>
            {
                counter.Value--;
                valueLabel.Text = counter.Value.ToString();
                CounterStorage.SaveCounters();
            };

            HorizontalStackLayout buttons = new HorizontalStackLayout();
            buttons.Style = (Style)CountersLayout.Resources["ButtonsStyle"];
            buttons.Children.Add(plusButton);
            buttons.Children.Add(minusButton);

            Button deleteButton = new Button();
            deleteButton.Text = "Delete";

            VerticalStackLayout newCounter = new VerticalStackLayout();
            newCounter.Style = (Style)CountersLayout.Resources["CounterStyle"];
            newCounter.Children.Add(nameLabel);
            newCounter.Children.Add(valueLabel);
            newCounter.Children.Add(buttons);
            newCounter.Children.Add(deleteButton);

            deleteButton.Clicked += (sender, e) =>
            {
                CounterStorage.counters.Remove(counter);
                CountersLayout.Children.Remove(newCounter);
                CounterStorage.SaveCounters();
            };

            CountersLayout.Children.Add(newCounter);
        }
    }
}