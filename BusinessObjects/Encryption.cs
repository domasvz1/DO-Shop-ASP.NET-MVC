using System.Text;

namespace BusinessObjects
{
    public class Encryption
    {
        public static string EncryptPassword(string password)
        {
            if (password != null)
            {
                System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] data = Encoding.ASCII.GetBytes(password);
                data = x.ComputeHash(data);
                return Encoding.ASCII.GetString(data);
            }
            return "";
        }
    }
}
