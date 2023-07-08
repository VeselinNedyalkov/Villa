using TrainingAPI.Models.DTO;

namespace TrainingAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villasList = new List<VillaDTO>
            {
                new VillaDTO {Id = 1, Name = "Pool View", Sqft = 100 , Occupancy = 8},
                new VillaDTO {Id = 2, Name = "Forest view" , Sqft = 60 , Occupancy = 6}
            };
    }
}
