using Google.Cloud.Firestore;
using System.Collections.Generic;

namespace WorkoutTracker.Models
{

    [FirestoreData]
    public class WorkoutProgram
    {
        [FirestoreProperty]
        public int ProgramId { get; set; }

        [FirestoreProperty]
        public int UserId { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string Description { get; set; }

        [FirestoreProperty]
        public List<Workout> Workouts { get; set; }
    }

    [FirestoreData]
    public class Workout
    {
        [FirestoreProperty]
        public int WorkoutId { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public List<string> Exercises { get; set; }
    }
}

