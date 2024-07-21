using Google.Cloud.Firestore;
using WorkoutTracker.Models;
using System.Threading.Tasks;


// Explin what the following code does

// The WorkoutService class is a service class that provides methods to interact with the Firestore database. 
// The class has a constructor that takes a FirestoreDb object as a parameter. 
// The AddWorkoutProgram method adds a new workout program to the Firestore database. 
// The method takes a WorkoutProgram object as a parameter and adds it to the "workoutPrograms" collection in the Firestore database. 
// The method is asynchronous and returns a Task object.
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
