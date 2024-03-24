using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using pokusaj.Data;
using pokusaj.Models;
using pokusaj.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace pokusaj.Controllers
{
    public class ProfessorController : Controller
    {

        private readonly StudentContext context;
        private readonly IConfiguration configuration;
        public ProfessorController(StudentContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        [HttpPost("/register/professor")]
        public async Task<IActionResult> Register(ProfessorRegister prof)
        {
            //Console.WriteLine(student.Name + " ." + student.ProfilePicture + ". " +student.Surname + " just registered :) " );
            if (ModelState.IsValid)
            {
                //check if email is already taken

                if (await this.context.Professors.AnyAsync(x => (x.Email == prof.Email)))
                {
                    return BadRequest(new { success = false, message = "User with this email already exists." });
                }

                
                Professor newProf = new Professor();
                newProf.Name = prof.Name;
                newProf.Surname = prof.Surname;
                newProf.Email = prof.Email;
                newProf.Password = prof.Password;
                newProf.ProfilePicture = "/profilePictures/slika"; //TODO
                newProf.InstructionsCount = prof.Subjects[0].Split(",").Length;
                newProf.Subjects = prof.Subjects[0];

                this.context.Professors.Add(newProf);
                await this.context.SaveChangesAsync();

                
                return Ok(new
                {
                    success = true,
                    message = "register success"
                });
            }
            return BadRequest(ModelState);
        }

        [HttpPost("/login/professor")]
        public async Task<IActionResult> Login([FromBody] StudentLogin student)
        {
            if (ModelState.IsValid)
            {
                //Console.WriteLine("pokusava: " + student.Email + " s " + student.Password);
                Professor? found = null;

                found = await this.context.Professors.FirstOrDefaultAsync(x => (x.Email == student.Email && x.Password == student.Password));

                if (found == null)
                {
                    return Unauthorized("No matching login information");
                }
                Console.WriteLine(student.ToString());
                var token = GenerateJwtToken(found);

                var response = new
                {
                    success = "true",
                    professor = found,
                    token = token,
                    message = "login successful"
                };
                return Ok(response);

            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpGet("/professors")]
        public async Task<IActionResult> GetAllProfessors()
        {
            //Console.WriteLine("get all");
            var users = await this.context.Professors
                .ToListAsync();

            return Ok(new { success = true, users });
        }

        [Authorize]
        [HttpGet("/professor/{email}")]

        public async Task<IActionResult> GetProfessorsByEmail(string email)
        {
            //Console.WriteLine("tu si");
            var user = await context.Professors.FirstOrDefaultAsync(x => (x.Email == email));
            Console.WriteLine(user.ToString());
            if (user == null) return NotFound(new { success = false, message = "User not found." });

            var response = new
            {
                success = true,
                student = user,
                message = "successfully found student"
            };

            return Ok(response);
        }

        private string GenerateJwtToken(Professor user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.ID.ToString()),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
