namespace pokusaj.Models
{
    public class InstructionsDate
    {
        public int Id { get; set; }
        public int professorID {  get; set; }
        public string studentID { get; set; }
        public DateTime dateTime { get; set; }
        public string status {  get; set; }
    }
}
