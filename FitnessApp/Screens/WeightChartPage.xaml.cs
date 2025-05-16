using FitnessApp.Models;
using Microcharts;
using SkiaSharp;

namespace FitnessApp.Screens;

public partial class WeightChartPage : ContentPage
{
    private List<ExerciseType> _types;

    public WeightChartPage()
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

        var allExercises = await App.Database.GetAllExercisesAsync();
        var filtered = allExercises
            .Where(e => e.TypeId == selectedType.Id)
            .OrderBy(e => e.Id)
            .ToList();

        if (filtered.Count == 0)
        {
            Chart.Chart = null;
            StatsLabel.Text = "Нет данных";
            return;
        }

        var workouts = await App.Database.GetWorkoutsAsync();
        var workoutDict = workouts.ToDictionary(w => w.Id, w => w.Date);

        var entries = filtered.Select(e => new ChartEntry((float)e.Weight)
        {
            Label = workoutDict.TryGetValue(e.WorkoutId, out var date) ? date.ToShortDateString() : "",
            ValueLabel = e.Weight.ToString("0.0"),
            Color = SKColor.Parse("#00bfff")
        }).ToList();

        Chart.Chart = new LineChart { Entries = entries };

        // 📊 Статистика
        var max = filtered.Max(e => e.Weight);
        var avg = filtered.Average(e => e.Weight);
        var delta = filtered.Last().Weight - filtered.First().Weight;

        StatsLabel.Text = $"Макс: {max} кг | Среднее: {avg:0.0} кг | Прирост: {delta:+0.0;-0.0} кг";
    }
}