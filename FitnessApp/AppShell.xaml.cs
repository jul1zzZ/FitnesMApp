using FitnessApp.Screens;
namespace FitnessApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Регистрация маршрута для страницы деталей тренировки
            Routing.RegisterRoute("WorkoutDetailPage", typeof(WorkoutDetailPage));
            Routing.RegisterRoute("ProgressTabbedPage", typeof(Screens.ProgressTabbedPage));

        }
    }
}
