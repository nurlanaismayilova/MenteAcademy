namespace sitee.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Profession? Profession { get; set; }
        public int ProfessionId { get; set; }
    }
}
