using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.ServicesContracts;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.WebUtilities;
using Azure.Core;
using System.Text.RegularExpressions;

namespace SmartMetric.WebAPI.Filters.AutorizationFilter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class TokenAuthorizationFilter : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            byte[] encbuff = Encoding.UTF8.GetBytes("Pedro.Maia" + "§" + DateTime.Now.Ticks + "§" + "508268800");

            string base64UrlEncoded = WebEncoders.Base64UrlEncode(Encrypt(encbuff));

            //string strEndereco = "https://smartstep.pt" + "/Default.aspx?token=" + convertedString;

            //// Verificar o token recebido da aplicação principal (SmartTime)
            //var token = context.HttpContext.Request.Query["token"].ToString();

            string token = base64UrlEncoded;

            if (!string.IsNullOrEmpty(token))
            {
                string decryptedToken = DecryptToken(token);

                string[] strDados = decryptedToken.Split('§');
                DateTime dtDataHoraToken = new DateTime(Convert.ToInt64(strDados[1]));

                bool bTokenValido = DateTime.Now.AddMinutes(-10) <= dtDataHoraToken && DateTime.Now.AddMinutes(10) >= dtDataHoraToken;

                if (!bTokenValido || strDados.Length < 3)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                context.HttpContext.Items["IsUtilizador"] = !IsEmail(strDados[0]);
                context.HttpContext.Items["Identifier"] = strDados[0];
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private string DecryptToken(string encodedToken)
        {
            // Decodificar a string Base64Url
            byte[] encryptedBytes = WebEncoders.Base64UrlDecode(encodedToken);

            // Chame o método Decrypt para descriptografar os bytes
            byte[] decryptedBytes = Decrypt(encryptedBytes);

            // Converter os bytes descriptografados de volta para a string original
            string originalString = Encoding.UTF8.GetString(decryptedBytes);

            return originalString;
        }

        private byte[] Decrypt(byte[] input)
        {
            using (Aes aes = Aes.Create())
            {
                PasswordDeriveBytes pdb = new PasswordDeriveBytes("Smart12qazxswSt3p", new byte[] { 0x16, 0x29, 0x81, 0x91 });

                aes.Key = pdb.GetBytes(aes.KeySize / 8);
                aes.IV = pdb.GetBytes(aes.BlockSize / 8);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(input, 0, input.Length);
                    }

                    return ms.ToArray();
                }
            }
        }

        private bool IsEmail(string input)
        {
            // Utilizando uma expressão regular simples para verificar se a string se parece com um email
            // Esta é uma verificação básica e pode não cobrir todos os casos
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(input, emailPattern);
        }


        private byte[] Encrypt(byte[] input)
        {
            PasswordDeriveBytes pdb =
              new PasswordDeriveBytes("Smart12qazxswSt3p",
              new byte[] { 0x16, 0x29, 0x81, 0x91 });
            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
    }

}
