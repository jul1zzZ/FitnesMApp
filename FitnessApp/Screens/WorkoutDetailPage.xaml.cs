using FitnessApp.Models;


namespace FitnessApp.Screens;

public partial class WorkoutDetailPage : ContentPage
{
    public Workout Workout { get; set; }

    public List<Exercise> Exercises { get; set; } = new();
    public WorkoutDetailPage(Workout workout)
	{
		InitializeComponent();
        Workout = workout;
        BindingContext = this;
        LoadExercises();
        LoadExerciseTypes();
    }

    private async void LoadExercises()
    {
        Exercises = await App.Database.GetExercisesByWorkoutIdAsync(Workout.Id);
        ExerciseList.ItemsSource = Exercises;
    }

    private async void OnAddExerciseClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
            !int.TryParse(RepsEntry.Text, out int reps) ||
            !int.TryParse(SetsEntry.Text, out int sets) ||
            !double.TryParse(WeightEntry.Text, out double weight))
        {
            await DisplayAlert("Ошибка", "Пожалуйста, введите корректные значения.", "ОК");
            return;
        }



        var newExercise = new Exercise
        {
            WorkoutId = Workout.Id,
            Name = NameEntry.Text,
            Reps = reps,
            Sets = sets,
            Weight = weight
        };

        var selectedType = TypePicker.SelectedItem as ExerciseType;
        if (selectedType == null)
        {
            await DisplayAlert("Ошибка", "Выберите тип упражнения", "ОК");
            return;
        }

        newExercise.TypeId = selectedType.Id;

        await App.Database.SaveExerciseAsync(newExercise);
        LoadExercises();

        // Очистка полей
        NameEntry.Text = RepsEntry.Text = SetsEntry.Text = WeightEntry.Text = string.Empty;
    }

    private async void LoadExerciseTypes()
    {
        var types = await App.Database.GetExerciseTypesAsync();
        TypePicker.ItemsSource = types;
        TypePicker.ItemDisplayBinding = new Binding("Name");
    }
}