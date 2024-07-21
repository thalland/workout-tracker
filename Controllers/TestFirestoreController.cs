using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;
using System.Threading.Tasks;

namespace WorkoutTracker.Controllers
{
    [ApiController]
    [Route("workouttracker/v1/[controller]")]
    public class TestFirestoreController : ControllerBase
    {
        private readonly FirestoreDb _firestoreDb;

        public TestFirestoreController(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        [HttpGet]
        [Route("testconnection")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                // Attempt to retrieve a collection
                var collection = _firestoreDb.Collection("testCollection");
                var snapshot = await collection.GetSnapshotAsync();

                // Check if the collection has any documents
                if (snapshot.Documents.Count > 0)
                {
                    return Ok("Firestore connection is successful!");
                }
                else
                {
                    return Ok("Firestore connection is successful, but the collection is empty.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error connecting to Firestore: {ex.Message}");
            }
        }
    }
}
