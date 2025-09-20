using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MiniFitnessTracker
{
    // ===== Part 1: Models =====
    public class User
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
            Console.WriteLine($"Age : {Age}");
            Console.WriteLine($"Weight: {Weight} Kg");
            Console.WriteLine($"Height: {Height} cm");
            Console.WriteLine($"Number of recorded Exercises: {WorkoutEntries.Count}");
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
            Console.WriteLine("Record of Exercise");
            Console.WriteLine($"Date: {Date.ToShortDateString()}");
            Console.WriteLine($"Exercise: {Exercise.Name}");
            Console.WriteLine($"Type: {Exercise.Type}");
            Console.WriteLine($"Duration: {DurationMinutes} Minute");
            Console.WriteLine($"Calories: {Calories}");
        }
    }

    // ===== Part 2: Progress + Engine =====
    public class ProgressTracker
    {
        public static void DisplayWeeklySummary(List<WorkoutEntry> entries)
        {
            if (entries == null || entries.Count == 0)
            {
                Console.WriteLine("No workout data available.");
                return;
            }

            DateTime today = DateTime.Today;
            DateTime weekStart = today.AddDays(-6);

            Console.WriteLine("\n--- Weekly Summary (Last 7 Days) ---");

            for (DateTime day = weekStart; day <= today; day = day.AddDays(1))
            {
                double totalMinutes = 0;
                double totalCalories = 0;

                foreach (var entry in entries)
                {
                    if (entry.Date.Date == day.Date)
                    {
                        totalMinutes += entry.DurationMinutes;
                        totalCalories += entry.Calories;
                    }
                }

                Console.WriteLine($"{day.ToShortDateString()}: {totalMinutes} min, {totalCalories} kcal");
            }
        }

        public static void DisplayAllTimeStats(List<WorkoutEntry> entries)
        {
            if (entries == null || entries.Count == 0)
            {
                Console.WriteLine("No workout data available.");
                return;
            }

            double totalMinutes = 0;
            double totalCalories = 0;

            foreach (var entry in entries)
            {
                totalMinutes += entry.DurationMinutes;
                totalCalories += entry.Calories;
            }

            Console.WriteLine("\n--- All Time Stats ---");
            Console.WriteLine("Total workouts: " + entries.Count);
            Console.WriteLine("Total minutes: " + totalMinutes);
            Console.WriteLine("Total calories: " + totalCalories);
        }
    }

    public class FitnessAppEngine
    {
        private User currentUser;
        private List<Exercise> exercises = new List<Exercise>();

        public FitnessAppEngine()
        {
            exercises.Add(new Exercise("Running", "Cardio", 10));
            exercises.Add(new Exercise("Cycling", "Cardio", 8));
            exercises.Add(new Exercise("Push-ups", "Strength", 7));
        }

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\n=== Mini Fitness Tracker ===");
                Console.WriteLine("1. Create Profile");
                Console.WriteLine("2. Log Workout");
                Console.WriteLine("3. Show Progress");
                Console.WriteLine("4. Save Data");
                Console.WriteLine("5. Load Data");
                Console.WriteLine("0. Exit");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();

                if (choice == "1") CreateProfile();
                else if (choice == "2") LogWorkout();
                else if (choice == "3") ShowProgress();
                else if (choice == "4") DataStorage.SaveUser(currentUser);
                else if (choice == "5") currentUser = DataStorage.LoadUser();
                else if (choice == "0") exit = true;
                else Console.WriteLine("Invalid option, try again.");

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        private void CreateProfile()
        {
            Console.WriteLine("Please enter your profile details.");
            
            // Validate Age
            int age;
            while (true)
            {
                Console.Write("Enter your age (1-120): ");
                if (int.TryParse(Console.ReadLine(), out age) && age >= 1 && age <= 120)
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please enter a valid age between 1 and 120.");
            }

            // Validate Weight
            double weight;
            while (true)
            {
                Console.Write("Enter your weight in kilograms (20-300 kg): ");
                if (double.TryParse(Console.ReadLine(), out weight) && weight >= 20 && weight <= 300)
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please enter a valid weight between 20 and 300 kg.");
            }

            // Validate Height
            double height;
            while (true)
            {
                Console.Write("Enter your height in centimeters (50-250 cm): ");
                if (double.TryParse(Console.ReadLine(), out height) && height >= 50 && height <= 250)
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please enter a valid height between 50 and 250 cm.");
            }

            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            currentUser = new User(name, age, weight, height);
            Console.WriteLine("Profile created successfully!");
        }

        private void LogWorkout()
        {
            if (currentUser == null)
            {
                Console.WriteLine("Please create a profile first.");
                return;
            }

            Console.WriteLine("Choose exercise:");
            for (int i = 0; i < exercises.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + exercises[i].Name + " (" + exercises[i].Type + ")");
            }

            Console.Write("Enter exercise number: ");
            int exIndex = int.Parse(Console.ReadLine()) - 1;

            if (exIndex < 0 || exIndex >= exercises.Count)
            {
                Console.WriteLine("Invalid exercise choice.");
                return;
            }

            Exercise chosen = exercises[exIndex];

            Console.Write("Enter duration (minutes): ");
            int duration = int.Parse(Console.ReadLine());

            WorkoutEntry entry = new WorkoutEntry(DateTime.Today, chosen, duration);
            currentUser.AddWorkoutEntry(entry);

            Console.WriteLine("Workout saved!");
            Console.WriteLine("Calories burned: " + entry.Calories);
        }

        private void ShowProgress()
        {
            if (currentUser == null || currentUser.WorkoutEntries.Count == 0)
            {
                Console.WriteLine("No data yet.");
                return;
            }

            Console.WriteLine("\n1. Weekly Summary");
            Console.WriteLine("2. All Time Stats");
            Console.Write("Enter choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
                ProgressTracker.DisplayWeeklySummary(currentUser.WorkoutEntries);
            else if (choice == "2")
                ProgressTracker.DisplayAllTimeStats(currentUser.WorkoutEntries);
            else
                Console.WriteLine("Invalid option.");
        }
    }

    // ===== Part 3: Data Storage =====
    public static class DataStorage
    {
        private static string filePath = "data.json";

        public static void SaveUser(User user)
        {
            string json = JsonSerializer.Serialize(user, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
            Console.WriteLine("Data saved successfully!");
        }

        public static User LoadUser()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("⚠ No saved data found, starting with empty profile.");
                    return null;
                }

                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<User>(json) ?? null;
            }
            catch
            {
                Console.WriteLine("⚠ Error loading data, starting with empty profile.");
                return null;
            }
        }
    }

    // ===== Program Entry =====
    class Program
    {
        static void Main(string[] args)
        {
            FitnessAppEngine engine = new FitnessAppEngine();
            engine.Run();
        }
    }
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
