using System;
using System.Security.Cryptography;
using System.Text;

public class CryptoHelper
{
    public CryptoHelper()
    {

    }

    public string HashWithSHA512(string plainText)
    {
        byte[] data = Encoding.UTF8.GetBytes(plainText);
        byte[] result;
        SHA512 shaM = new SHA512Managed();
        result = shaM.ComputeHash(data);

        return Convert.ToBase64String(result);
    }
}