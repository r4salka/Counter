using Counter.Views;

namespace Counter
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CounterPage), typeof(CounterPage));
        }
    }
}
