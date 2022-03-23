using WelcomeToHogwarts.Persistance.Entities;

namespace WelcomeToHogwarts.Persistance.DataTransferObjects
{
    public class BasePotionDto
    {
        public string MakerName { get; set; }
        public string PotionName { get; set; }
    }
    public class CreatePotionDto : BasePotionDto
    {
        public List<Guid> IngredientsId { get; set; }
    }

    public class CreatePotionStatusDto
    {
        public Potion Potion { get; set; }
        public Status Status { get; set; }
        public string StatusMessage { get; set; }
    }
    public class GetPotionsStatusDto
    {
        public List<Potion> Potions { get; set; }
        public Status Status { get; set; }
        public string StatusMessage { get; set; }
    }
}
