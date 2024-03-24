using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pokusaj.Data;
using pokusaj.Models;
using pokusaj.ViewModels;

namespace pokusaj.Controllers
{
    public class InstructionController : Controller
    {
        private readonly StudentContext context;
        private readonly IConfiguration configuration;
        public InstructionController(StudentContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        /*
        [Authorize]
        [HttpPost("/instructions")]

        public async Task<IActionResult> ScheduleInstructions([FromBody] InstructionsSchedule instr)
        {
            InstructionsDate ins = new InstructionsDate();
            ins.status = "zahtjev";
            ins.dateTime = instr.DateTime;
            ins.professorID = instr.Id;
            ins.studentID = 
        }*/
        public IActionResult Index()
        {
            return View();
        }
    }
}
