using Microcharts;
using SkiaSharp;
using FitnessApp.Models;

namespace FitnessApp.Screens;

public partial class ProgressPage : ContentPage
{
    private List<ExerciseType> _types; // 🔧 Добавлено это поле

    public ProgressPage()
	{
		InitializeComponent();
        LoadExerciseTypes();


    }

    private async void LoadExerciseTypes()
    {
        _types = await App.Database.GetExerciseTypesAsync();
        TypePicker.ItemsSource = _types;
        TypePicker.ItemDisplayBinding = new Binding("Name");
    }

    private async void OnTypeSelected(object sender, EventArgs e)
    {
        if (TypePicker.SelectedItem is not ExerciseType selectedType)
            return;

        var allExercises = await App.Database.GetAllExercisesAsync(); // добавим метод ниже
        var filtered = allExercises
            .Where(e => e.TypeId == selectedType.Id)
            .ToList();

        var workouts = await App.Database.GetWorkoutsAsync();
        var workoutDict = workouts.ToDictionary(w => w.Id, w => w.Date);

        var entries = filtered.Select(e => new ChartEntry((float)e.Weight)
        {
            Label = workoutDict.TryGetValue(e.WorkoutId, out var date) ? date.ToShortDateString() : "—",
            ValueLabel = e.Weight.ToString("0.0"),
            Color = SKColor.Parse("#00bfff")
        }).ToList();

        WeightChart.Chart = new LineChart { Entries = entries };
    }

}