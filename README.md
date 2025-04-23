# 📄 XML to JSON Processor API (.NET 8)

Esta API recebe um arquivo XML, converte para JSON, armazena o XML no Azure Blob Storage e grava os dados no banco de dados.

---

## 🚀 Endpoints

### 📤 POST `/api/xml/upload`

Recebe um arquivo XML, converte para JSON e salva no Azure e no banco de dados.

**Request:**
- **Content-Type:** `multipart/form-data`
- **Body:**
  - `file`: Arquivo `.xml`

**Exemplo cURL:**
```bash
curl -X POST https://localhost:5001/api/xml/upload \
  -H "accept: application/json" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@data.xml"
```

Resposta de sucesso:
{
  "message": "Processed successfully",
  "xmlOriginId": 1
}

Erros:

400 Bad Request – Arquivo inválido ou ausente.

500 Internal Server Error – Erro durante o processamento.

## 🧩 Modelos de Dados

### 🔹 `JsonData`

```csharp
public class JsonData
{
    public int Id { get; set; }

    public string Key { get; set; }

    public string Value { get; set; }

    public int XmlOriginId { get; set; }

    public XmlOrigin XmlOrigin { get; set; }
}
```
### 🔹 `XmlOrigin`

```csharp
public class XmlOrigin
{
    public int Id { get; set; }

    public string FileName { get; set; }

    public string BlobUrl { get; set; }

    public ICollection<JsonData> JsonData { get; set; }
}
```

🧱 Arquitetura
XmlController: Apenas repassa chamadas para a Service.

XmlProcessingService: Faz parsing do XML, salva no banco.

XmlStorageService: Salva o XML no Azure.

ExceptionMiddleware: Captura exceções e padroniza as respostas.

⚙️ Configuração
No appsettings.json:
```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=XmlToJsonDb;Trusted_Connection=True;"
},
"AzureStorage": {
  "ConnectionString": "<sua-string-de-conexão>",
  "ContainerName": "xml-files"
}
```
🧪 Testes
Use ferramentas como Postman ou Insomnia.
Certifique-se de subir um .xml bem formatado.
