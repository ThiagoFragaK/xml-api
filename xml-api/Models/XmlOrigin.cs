namespace xml_api.Models
{
    public class XmlOrigin
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string BlobUrl { get; set; }

        public ICollection<JsonData> JsonData { get; set; }
    }
}
