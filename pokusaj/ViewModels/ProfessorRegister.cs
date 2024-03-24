using System.ComponentModel.DataAnnotations.Schema;

namespace pokusaj.ViewModels
{
    public class ProfessorRegister
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Surname { get; set; }
        public string? Password { get; set; }
        public string? ProfilePicture { get; set; }
        public int? InstructionsCount { get; set; }
        public string[]? Subjects { get; set; }
    }
}
