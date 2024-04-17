
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;


namespace API.Middlewares
{
    public class EncryptionMiddleware
    {
        private readonly RequestDelegate _next;
        public EncryptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        // Whenever we call any action method then call this before call the action method
        public async Task Invoke(HttpContext httpContext)
        {
            string requestBody = await ReadRequestBodyAsStringAsync(httpContext);
            Console.WriteLine(requestBody);
            string res = DecryptString(requestBody);
            Console.WriteLine(res);
            // httpContext.Request.Body = DecryptStream(httpContext.Request.Body);
            // if (httpContext.Request.QueryString.HasValue)
            // {
            //     string decryptedString = DecryptString(httpContext.Request.QueryString.Value.Substring(1));
            //     httpContext.Request.QueryString = new QueryString($"?{decryptedString}");
            // }
            httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(res));
            await _next(httpContext);
        }

        public async Task<string> ReadRequestBodyAsStringAsync(HttpContext context)
        {
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
            {
                string requestBody = await reader.ReadToEndAsync();
                return requestBody;
            }
        }

        // This are main functions that we decrypt the payload and  parameter which we pass from the angular service.

        public async Task<Stream> DecryptStreamAsync(Stream cipherStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                try
                {
                    Aes aes = GetEncryptionAlgorithm();  // Ensure proper key/IV retrieval

                    // Base64 Decoding (if applicable)
                    if (cipherStream.CanSeek && cipherStream.Length > 0) // Check if seeking supported
                    {
                        cipherStream.Position = 0; // Rewind if seekable
                        string base64EncodedString = new StreamReader(cipherStream, Encoding.UTF8).ReadToEnd();
                        cipherStream = new MemoryStream(Convert.FromBase64String(base64EncodedString)); // Replace stream with decoded bytes
                    }

                    FromBase64Transform base64Transform = new FromBase64Transform(FromBase64TransformMode.IgnoreWhiteSpaces);
                    CryptoStream base64DecodedStream = new CryptoStream(cipherStream, base64Transform, CryptoStreamMode.Read);
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    CryptoStream decryptedStream = new CryptoStream(base64DecodedStream, decryptor, CryptoStreamMode.Read);

                    await decryptedStream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;
                    return memoryStream;
                }
                catch (Exception ex)
                {
                    // Handle decryption exceptions gracefully (e.g., log error, return error response)
                    throw new Exception($"Decryption failed: {ex.Message}");  // Provide more informative message
                }
            }
        }
        private Stream DecryptStream(Stream cipherStream)
        {
            Aes aes = GetEncryptionAlgorithm();
            FromBase64Transform base64Transform = new FromBase64Transform(FromBase64TransformMode.IgnoreWhiteSpaces);
            CryptoStream base64DecodedStream = new CryptoStream(cipherStream, base64Transform, CryptoStreamMode.Read);
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            CryptoStream decryptedStream = new CryptoStream(base64DecodedStream, decryptor, CryptoStreamMode.Read);
            return decryptedStream;
        }
        private static string DecryptString(string cipherText)
        {
            Aes aes = GetEncryptionAlgorithm();
            byte[] buffer = Convert.FromBase64String(cipherText);
            MemoryStream memoryStream = new MemoryStream(buffer);
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            StreamReader streamReader = new StreamReader(cryptoStream);
            return streamReader.ReadToEnd();
        }
        private static Aes GetEncryptionAlgorithm()
        {
            Aes aes = Aes.Create();
            var secret_key = Encoding.UTF8.GetBytes("1203199320052021");
            var initialization_vector = Encoding.UTF8.GetBytes("1203199320052021");
            aes.Key = secret_key;
            aes.IV = initialization_vector;
            return aes;
        }
    }
}