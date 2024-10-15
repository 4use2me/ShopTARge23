namespace ShopTARge23.Models.Spaceships
{
    public class SpaceshipImageViewModel
    {
        public Guid ImageId { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string Image { get; set; }
        public Guid? SpaceshipId { get; set; }
    }
}
