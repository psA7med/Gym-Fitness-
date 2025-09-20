using System;
using System.Collections.Generic;

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
            Console.WriteLine("\n=== Mini Fitness Tracker ===");
            Console.WriteLine("1. Create Profile");
            Console.WriteLine("2. Log Workout");
            Console.WriteLine("3. Show Progress");
            Console.WriteLine("0. Exit");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();

            if (choice == "1") CreateProfile();
            else if (choice == "2") LogWorkout();
            else if (choice == "3") ShowProgress();
            else if (choice == "0") exit = true;
            else Console.WriteLine("Invalid option, try again.");
        }
    }

    private void CreateProfile()
    {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();

        Console.Write("Enter age: ");
        int age = int.Parse(Console.ReadLine());

        Console.Write("Enter weight (kg): ");
        double weight = double.Parse(Console.ReadLine());

        Console.Write("Enter height (cm): ");
        double height = double.Parse(Console.ReadLine());

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
