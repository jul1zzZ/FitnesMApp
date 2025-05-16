using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace FitnessApp.Models
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int WorkoutId { get; set; }  // Внешний ключ к тренировке

        public string Name { get; set; }

        public int Reps { get; set; }      // Повторы

        public int Sets { get; set; }      // Подходы

        public double Weight { get; set; } // Вес, если применимо
        public int TypeId { get; set; } // Внешний ключ на ExerciseType

    }
}
