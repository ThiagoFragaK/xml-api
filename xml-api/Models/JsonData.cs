namespace xml_api.Models
{
    public class JsonData
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public int XmlOriginId { get; set; }
        public XmlOrigin XmlOrigin { get; set; }
    }
}
