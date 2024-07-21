namespace WorkoutTracker.Models
{
    public class WorkoutProgram
    {
        public int ProgramId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // List of list
        public List<Workout> Workouts { get; set; }
        
    }

    public class Workout
    {
        public int WorkoutId { get; set; }
        public string Name { get; set; }
        // List of exercises
        public List<string> Exercises { get; set; }
 
    }
}

