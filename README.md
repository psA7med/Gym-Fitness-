# Mini Fitness Tracker - Part 2

## 📌 Description
This repository contains **Part 2: Progress + Engine Logic** of the Mini Fitness Tracker project.

- **ProgressTracker.cs**
  - `DisplayWeeklySummary()`: Shows total minutes and calories burned for the last 7 days.
  - `DisplayAllTimeStats()`: Shows total workouts, minutes, and calories burned overall.

- **FitnessAppEngine.cs**
  - Main menu with options to:
    1. Create Profile
    2. Log Workout
    3. Show Progress (Weekly or All Time)
    0. Exit
  - Handles user input and connects the Models (User, Exercise, WorkoutEntry) with ProgressTracker.

## 👨‍💻 Responsibility
This part was developed as **Part 2** of a group project by 3 members.  
It focuses on:
- Building the logic to connect User, Exercise, and WorkoutEntry classes.  
- Providing features to record workouts and show progress.  

## 🚀 How to Run
> Note: This part depends on **Part 1 (Models)** to work correctly.  
Make sure you have `User.cs`, `Exercise.cs`, and `WorkoutEntry.cs` available in your project.

1. Clone this repo:
   ```bash
   git clone https://github.com/YourUsername/FitnessTracker-Logic.git
   ```
2. Add the Models from Part 1 to the same project.
3. Run the project in Visual Studio or using:
   ```bash
   dotnet run
   ```

## 📂 Project Structure
```
├── ProgressTracker.cs
├── FitnessAppEngine.cs
├── README.md
```

---
✅ Part 2 Completed: Progress + Engine Logic
