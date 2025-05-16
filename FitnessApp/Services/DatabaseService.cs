using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using FitnessApp.Models;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Workout>().Wait();
            _database.CreateTableAsync<Exercise>().Wait();
            _database.CreateTableAsync<ExerciseType>().Wait();

        }

        // ====== Workouts ======
        public Task<List<Workout>> GetWorkoutsAsync() =>
            _database.Table<Workout>().ToListAsync();

        public Task<int> SaveWorkoutAsync(Workout workout) =>
            workout.Id != 0 ? _database.UpdateAsync(workout) : _database.InsertAsync(workout);

        public Task<int> DeleteWorkoutAsync(Workout workout) =>
            _database.DeleteAsync(workout);

        // ====== Exercises ======
        public Task<List<Exercise>> GetExercisesByWorkoutIdAsync(int workoutId) =>
            _database.Table<Exercise>().Where(e => e.WorkoutId == workoutId).ToListAsync();

        public Task<int> SaveExerciseAsync(Exercise exercise) =>
            exercise.Id != 0 ? _database.UpdateAsync(exercise) : _database.InsertAsync(exercise);

        public Task<int> DeleteExerciseAsync(Exercise exercise) =>
            _database.DeleteAsync(exercise);

        public Task<int> SaveExerciseTypeAsync(ExerciseType type) =>
            _database.InsertAsync(type);
        public Task<Workout> GetWorkoutByIdAsync(int id) =>
            _database.Table<Workout>().Where(w => w.Id == id).FirstOrDefaultAsync();
        public Task<List<Exercise>> GetAllExercisesAsync() =>
            _database.Table<Exercise>().ToListAsync();

        public Task<List<ExerciseType>> GetExerciseTypesAsync() =>
            _database.Table<ExerciseType>().ToListAsync();


    }
}
