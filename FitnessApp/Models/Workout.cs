using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace FitnessApp.Models
{
    public class Workout
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Type { get; set; } 
    }
}
