using FitnessApp.Models;
using FitnessApp.Screens;
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Collections.Generic;

namespace FitnessApp
{
    public partial class MainPage : ContentPage
    {
        public List<Workout> Workouts { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();
            LoadWorkouts();
            BindingContext = this;  // Для привязки данных
        }

        private async void LoadWorkouts()
        {
            Workouts = await App.Database.GetWorkoutsAsync();
            WorkoutList.ItemsSource = Workouts;

            // Слушаем изменение выбора элемента в списке
            WorkoutList.SelectionChanged += async (s, e) =>
            {
                if (e.CurrentSelection.FirstOrDefault() is Workout selected)
                {
                    // Переход к подробной информации о тренировке
                    await Shell.Current.GoToAsync($"//WorkoutDetailPage?workoutId={selected.Id}");
                    WorkoutList.SelectedItem = null;
                }
            };
        }

        // Обработчик для добавления тренировки
        private async void OnAddWorkoutClicked(object sender, EventArgs e)
        {
            var workout = new Workout
            {
                Name = "Утренняя пробежка",
                Type = "Кардио",
                Date = DateTime.Now
            };
            await App.Database.SaveWorkoutAsync(workout);
            LoadWorkouts();  // Перезагружаем список тренировок
        }

        private async void OnProgressPageClicked(object sender, EventArgs e)
        {
            // Переход к странице с прогрессом
            await Shell.Current.GoToAsync("ProgressTabbedPage");
        }
    }
}
