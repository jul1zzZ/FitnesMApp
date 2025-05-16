using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Models
{
    public class ExerciseType
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } // Например: "Приседания", "Тяга", "Прыжки"
    }
}
