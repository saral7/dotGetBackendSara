using System.ComponentModel.DataAnnotations;

namespace pokusaj.ViewModels
{
    public class InstructionsSchedule
    {
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int Id { get; set; }
    }
}
