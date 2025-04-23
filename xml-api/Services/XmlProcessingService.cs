using Newtonsoft.Json;
using System.Xml.Linq;
using xml_api.Data;
using xml_api.Models;

namespace xml_api.Services
{
    public class XmlProcessingService
    {
        private readonly AppDbContext _context;
        private readonly XmlStorageService _xmlStorageService;

        public XmlProcessingService(AppDbContext context, XmlStorageService xmlStorageService)
        {
            _context = context;
            _xmlStorageService = xmlStorageService;
        }

        public async Task<int> ProcessXmlFileAsync(Stream fileStream, string fileName)
        {
            var blobUrl = await _xmlStorageService.UploadToAzureStorageAsync(fileStream, fileName);

            fileStream.Position = 0; // reset stream
            var jsonData = ConvertXmlToJson(fileStream);

            var xmlOrigin = new XmlOrigin { FileName = fileName, BlobUrl = blobUrl };
            _context.XmlOrigins.Add(xmlOrigin);
            await _context.SaveChangesAsync();

            var jsonContents = jsonData.Select(kvp => new JsonData
            {
                Key = kvp.Key,
                Value = kvp.Value,
                XmlOriginId = xmlOrigin.Id
            });

            _context.JsonDatas.AddRange(jsonContents);
            await _context.SaveChangesAsync();

            return xmlOrigin.Id;
        }

        private Dictionary<string, string> ConvertXmlToJson(Stream fileStream)
        {
            using var reader = new StreamReader(fileStream);
            var xml = XDocument.Parse(reader.ReadToEnd());
            var json = JsonConvert.SerializeXNode(xml, Formatting.None, omitRootObject: true);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}
