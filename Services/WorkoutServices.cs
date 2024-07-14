using Google.Cloud.Firestore;
using WorkoutTracker.Models;
using System.Threading.Tasks;

namespace WorkoutTracker.Services
{
    public class WorkoutService
    {
        private readonly FirestoreDb _firestoreDb;

        public WorkoutService(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public async Task AddWorkoutProgram(WorkoutProgram program)
        {
            var collection = _firestoreDb.Collection("workoutPrograms");
            await collection.AddAsync(program);
        }

        // Implement other CRUD methods as needed
    }
}
