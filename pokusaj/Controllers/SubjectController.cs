using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pokusaj.Data;
using pokusaj.Models;
using pokusaj.ViewModels;

namespace pokusaj.Controllers
{
    public class SubjectController : Controller
    {
        private readonly StudentContext context;
        private readonly IConfiguration configuration;
        public SubjectController(StudentContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        [Authorize]
        [HttpPost("/subject")]
        public async Task<IActionResult> AddSubject ([FromBody]SubjectAdding subject)
        {
            if (ModelState.IsValid)
            {
                if (await this.context.Subjects.AnyAsync(x => (x.Url == subject.Url)))
                {
                    return BadRequest(new { success = false, message = "Subject with this Url already exists." });
                }
                Subject newSubject = new Subject();
                newSubject.Url = subject.Url;
                newSubject.Title = subject.Title;
                newSubject.Description = subject.Description;

                this.context.Subjects.Add(newSubject);
                await this.context.SaveChangesAsync();

                var response = new
                {
                    success = "true",
                    message = "subject successfully created"
                };
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpGet("/subjects")]
        public async Task<IActionResult> GetAllSubjects ()
        {
            var subjects = await this.context.Subjects.ToListAsync();
            var response = new
            {
                success = "true",
                subjects = subjects
            };
            return Ok(response);
        }

        [Authorize]
        [HttpGet("/subject/{url}")]
        public async Task<IActionResult> GetSubjectByUrl(string url)
        {
            var found = await this.context.Subjects.FirstOrDefaultAsync(x => (x.Url == url));
            //Console.WriteLine("nasao " + found.ToString());
            var response = new
            {
                success = "true",
                subject = found
            };
            return Ok(response);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
