using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Middlewares
{
    public class DecryptionMiddleware
    {
        private readonly RequestDelegate _next;
        public static string key = "mySecretKeyHere"; //Same as in Angular
        public DecryptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            string requestBody = await ReadRequestBodyAsStringAsync(httpContext);
            Console.WriteLine(requestBody);
            string res = DecryptString(requestBody);
            Console.WriteLine(res);
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
            var secret_key = Encoding.UTF8.GetBytes("mySecretSalt");
            var initialization_vector = Encoding.UTF8.GetBytes("mySecretSaltIV");
            aes.Key = secret_key;
            aes.IV = initialization_vector;
            return aes;
        }
    }
}