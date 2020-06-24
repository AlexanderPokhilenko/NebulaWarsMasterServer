namespace DataLayer.Tables
{
    public class WarshipDbDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int WarshipTypeId { get; set; }
        public int PowerLevel { get; set; }
        public int PowerPoints { get; set; }
        public int WarshipRating { get; set; }
        
        public AccountDbDto Account { get; set; }
        public WarshipType WarshipType { get; set; }
    }
}