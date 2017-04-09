using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Ranaitfleur.Infrastructure.SagePayApi.Models;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace Ranaitfleur.Infrastructure.SagePayApi
{
    public class Cryptography : ICryptography
    {
        private readonly char _cryptPrefix = '@';
        private readonly IConfigurationRoot _configuration;

        public Cryptography(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public string EncryptModel(SagePayCryptModel model)
        {
            string cryptString = SerializeToQueryString(model);

            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            using (var aes = GetAesAlgorithm())
            {
                string key = _configuration["SagePay:EncryptionKey"];
                string vector = _configuration["SagePay:EncryptionVector"];
                var encryptor = aes.CreateEncryptor(encoding.GetBytes(key), encoding.GetBytes(vector));

                byte[] encrypted;
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(cryptString);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }

                var encryptedValue = BitConverter.ToString(encrypted).Replace("-", string.Empty);
                return _cryptPrefix + encryptedValue.ToUpperInvariant();
            }
        }

        public SagePayResponseModel DecryptModel(string crypt)
        {
            crypt = crypt.ToUpper().TrimStart(_cryptPrefix);
            var cryptBytes = Encoding.UTF8.GetBytes(crypt);

            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            using (var aes = GetAesAlgorithm())
            {
                var key = _configuration["SagePay:EncryptionKey"];
                var vector = _configuration["SagePay:EncryptionVector"];
                var decryptor = aes.CreateDecryptor(encoding.GetBytes(key), encoding.GetBytes(vector));

                string decryptedValue;
                using (var msDecrypt = new MemoryStream(cryptBytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            decryptedValue = srDecrypt.ReadToEnd();
                        }
                    }
                }

                var response = DeserializeFromQueryString<SagePayResponseModel>(decryptedValue);
                return response;
            }
        }

        private Aes GetAesAlgorithm()
        {
            var aes = Aes.Create();
            aes.BlockSize = int.Parse(_configuration["SagePay:EncryptionBlock"]);
            aes.KeySize = int.Parse(_configuration["SagePay:EncryptionBlock"]);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            return aes;
        }

        private string SerializeToQueryString(object request)
        {
            var properties = from p in request.GetType().GetProperties()
                             where p.GetValue(request) != null
                             select p.Name + "=" + (p.PropertyType.GetTypeInfo().IsEnum ? (int)p.GetValue(request) : p.GetValue(request));

            return string.Join("&", properties.ToArray());
        }

        private T DeserializeFromQueryString<T>(string response) where T : new()
        {
            var dict = QueryHelpers.ParseNullableQuery(response);

            var obj = new T();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var name = GetPropertyName(property);
                if (dict.ContainsKey(name) == false)
                    continue;

                var valueAsString = dict[name];
                var value = Parse(property.PropertyType, valueAsString);

                if (value == null)
                    continue;

                property.SetValue(obj, value);
            }
            return obj;
        }

        private string GetPropertyName(PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<DataMemberAttribute>(false);
            return attribute?.Name ?? property.Name;
        }

        private object Parse(Type dataType, string value)
        {
            TypeConverter obj = TypeDescriptor.GetConverter(dataType);
            return obj.ConvertFromString(null, CultureInfo.InvariantCulture, value);
        }
    }
}
