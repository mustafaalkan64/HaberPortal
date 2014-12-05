namespace Best.Core.Domain.DbEntities
{
    public class GaleryImage:BaseEntity
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }
        public string ImgUrlOriginal { get; set; }
        public string ImgUrlBig { get; set; }
        public string ImgUrlMiddle { get; set; }
        public string ImgUrlSmall { get; set; }
        public int GaleryId { get; set; }

        public virtual Galery Galery { get; set; }
    }
}
