using FitnessApp.Services;


namespace FitnessApp
{
    public partial class App : Application
    {
        private static DatabaseService _database;

        public static DatabaseService Database =>
      _database ??= new DatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "fitness.db3"));

        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}