﻿namespace pokusaj.ViewModels
{
    public class StudentRegister
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Password { get; set; }
        public IFormFile? ProfilePicture { get; set; }

    }
}
