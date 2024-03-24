using Microsoft.AspNetCore.Mvc;
using pokusaj.ViewModels;
using pokusaj.Data;
using pokusaj.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace pokusaj.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext context;
        private readonly IConfiguration configuration;
        public StudentController(StudentContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/register/student")]
        public async Task<IActionResult> Register (StudentRegister student)
        {
            //Console.WriteLine(student.Name + " ." + student.ProfilePicture + ". " +student.Surname + " just registered :) " );
            if (ModelState.IsValid)
            {
                //check if email is already taken

                if (await this.context.Students.AnyAsync(x => (x.Email == student.Email))) 
                {
                    return BadRequest(new { success = false, message = "User with this email already exists." });
                }

                /*
                Console.WriteLine(student.ProfilePicture.ToString());
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(student.ProfilePicture.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/profilePictures", fileName);

                
                using (var stream = System.IO.File.Create(filePath))
                {
                    await student.ProfilePicture.CopyToAsync(stream);
                }
                */
                Student newStudent = new Student();
                newStudent.Name = student.Name;
                newStudent.Surname = student.Surname;
                newStudent.Email = student.Email;
                newStudent.Password = student.Password;
                newStudent.ProfilePicture = "/profilePictures/slika";

                this.context.Students.Add(newStudent);
                await this.context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "register success"
                });
            }
            return BadRequest(ModelState);
        }

        [HttpPost("/login/student")]
        public async Task<IActionResult> Login ([FromBody]StudentLogin student)
        {
            if (ModelState.IsValid)
            {
                //Console.WriteLine("pokusava: " + student.Email + " s " + student.Password);
                Student? found = null;

                found = await this.context.Students.FirstOrDefaultAsync(x => (x.Email == student.Email && x.Password == student.Password));

                if (found == null)
                {
                    return Unauthorized("No matching login information");
                }
                Console.WriteLine(student.ToString());
                var token = GenerateJwtToken(found);

                var response = new
                {
                    success = "true",
                    student = found,
                    token = token,
                    message = "login successful"
                };
                return Ok(response);

            }
            return BadRequest(ModelState);
        }
        
        [Authorize] 
        [HttpGet("/student/{email}")]
        
        public async Task<IActionResult> GetStudentsByEmail (string email)
        {
            Console.WriteLine("tu si");
            var user = await context.Students.FirstOrDefaultAsync(x => (x.Email == email));
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

        [Authorize]
        [HttpGet("/students")]
        public async Task<IActionResult> GetAllStudents()
        {
            Console.WriteLine("get all");
            var users = await this.context.Students
                .ToListAsync();

            return Ok(new { success = true, users });
        }

        private string GenerateJwtToken(Student user)
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
    }
}
