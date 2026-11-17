using Counter.Models;
using System.Threading.Tasks;
using System.IO;

namespace Counter.Views;
[QueryProperty(nameof(ItemId), nameof(ItemId))]

public partial class CounterPage : ContentPage
{

    public string ItemId
    {
        set { LoadCounter(value); }
    }
    public CounterPage()
    {
        InitializeComponent();
        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.counters.txt";

        LoadCounter(Path.Combine(appDataPath, randomFileName));
    }

    private void LoadCounter(string fileName)
    {
        Models.Counter counterModel = new Models.Counter();
        counterModel.Filename = fileName;
        counterModel.Name = "";
        counterModel.Value = 0;

        if (File.Exists(fileName))
        {
            string[] data = File.ReadAllLines(fileName);

            counterModel.Name = data[0];
            counterModel.Value = int.Parse(data[1]);
        }

        BindingContext = counterModel;
    }

    private void PlusButton_Clicked(object sender, EventArgs e)
    {
        if(BindingContext is Models.Counter counter)
        {
            counter.Value++;
            CounterValueLabel.Text = counter.Value.ToString();
        }
    }

    private void MinusButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Counter counter)
        {
            counter.Value--;
            CounterValueLabel.Text = counter.Value.ToString();
        }
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {

        if (BindingContext is Models.Counter counter)
        {
            if (File.Exists(counter.Filename))
            {
                File.Delete(counter.Filename);
            }
        }
        await Shell.Current.GoToAsync("..");
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Counter counter)
        {
            if(string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                await DisplayAlert("Error", "Enter counter name", "Ok");
                return;
            }

            counter.Name = NameEntry.Text;

            File.WriteAllLines(counter.Filename, new string[]
            {
                counter.Name, 
                counter.Value.ToString()
            });
        }

        await Shell.Current.GoToAsync("..");

    }


}
