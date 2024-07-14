using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Models;
using WorkoutTracker.Services;
using System.Threading.Tasks;

namespace WorkoutTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutController : ControllerBase
    {
        private readonly WorkoutService _workoutService;

        public WorkoutController(WorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkoutProgram([FromBody] WorkoutProgram program)
        {
            await _workoutService.AddWorkoutProgram(program);
            return Ok(program);
        }

        // Implement other CRUD endpoints as needed
        // hello world2
    }
}
