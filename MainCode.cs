using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1_EX1
{
    internal class User
    {
   
      
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; } 
        public double Height { get; set; } 
        public List<WorkoutEntry> WorkoutEntries { get; set; }

      
        public User(string name, int age, double weight, double height)
        {
            Name = name;
            Age = age;
            Weight = weight;
            Height = height;
            WorkoutEntries = new List<WorkoutEntry>();
            
        }

        
        public void UpdateProfile(string name, int age, double weight, double height)
        {
            Name = name;
            Age = age;
            Weight = weight;
            Height = height;
            Console.WriteLine("Updated Successfully");
        }

        public void DisplayProfile()
        {
            
            Console.WriteLine($"Name : {Name}");
            Console.WriteLine($" Age : {Age} ");
            Console.WriteLine($"Weight: {Weight} Kg");
            Console.WriteLine($"Height: {Height} cm");
            Console.WriteLine($" Number of recorded Exercises: {WorkoutEntries.Count}");
            
        }

        
        public void AddWorkoutEntry(WorkoutEntry entry)
        {
            WorkoutEntries.Add(entry);
        }
    }

   
    public class Exercise
    {
        
        public string Name { get; set; }
        public string Type { get; set; } 
        public double CaloriesPerMinute { get; set; }

        
        public Exercise(string name, string type, double caloriesPerMinute)
        {
            Name = name;
            Type = type;
            CaloriesPerMinute = caloriesPerMinute;
        }

      
        public double CaloriesForDuration(int durationMinutes)
        {
            return CaloriesPerMinute * durationMinutes;
        }
    }

    
    public class WorkoutEntry
    {
       
        public DateTime Date { get; set; }
        public Exercise Exercise { get; set; }
        public int DurationMinutes { get; set; }
        public double Calories { get; private set; }

      
        public WorkoutEntry(DateTime date, Exercise exercise, int durationMinutes)
        {
            Date = date;
            Exercise = exercise;
            DurationMinutes = durationMinutes;
            Calories = exercise.CaloriesForDuration(durationMinutes);
        }

       
        public void DisplayEntry()
        {
            Console.WriteLine("recored of Exercise ");
            Console.WriteLine($"Date: {Date.ToShortDateString()}");
            Console.WriteLine($"Exercise: {Exercise.Name}");
            Console.WriteLine($"Type: {Exercise.Type}");
            Console.WriteLine($"DurationMinutes: {DurationMinutes} Minute");
            Console.WriteLine($"Calories: {Calories} ");
           
        }
    }
