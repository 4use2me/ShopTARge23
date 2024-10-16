namespace ShopTARge23.Models.Kindergartens
{
    public class ImageViewModel
    {
        public Guid ImageId { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string Image { get; set; }
        public Guid? KindergartenId { get; set; }
    }
}
