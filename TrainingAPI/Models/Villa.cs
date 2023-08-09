namespace TrainingAPI.Models.DTO
{
    public class Villa
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Details { get; set; } = null!;
        public double Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Amenity { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
