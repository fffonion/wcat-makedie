using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

public class CryptoPrefs
{
    private static string sIV = "4rZymEMfa/PpeJ89qY4gyA==";
    private static string sKEY = "ZTdkNTNmNDE2NTM3MWM0NDFhNTEzNzU1";

    public enum Key
    {
        Language,
        Sound,
        Test,
        Test_PlayerParam,
        Test_Environment,
        Test_UID,
        Account,
        Config,
        CommonSaveData,
        DevelopSaveData,
        InGame
    }

    public static string Decrypt(string encString)
    {
        string s = encString;
        RijndaelManaged managed = new RijndaelManaged {
            Padding = PaddingMode.Zeros,
            Mode = CipherMode.CBC,
            KeySize = 0x80,
            BlockSize = 0x80
        };
        byte[] bytes = Encoding.UTF8.GetBytes(sKEY);
        byte[] rgbIV = Convert.FromBase64String(sIV);
        ICryptoTransform transform = managed.CreateDecryptor(bytes, rgbIV);
        byte[] buffer = Convert.FromBase64String(s);
        byte[] buffer4 = new byte[buffer.Length];
        MemoryStream stream = new MemoryStream(buffer);
        new CryptoStream(stream, transform, CryptoStreamMode.Read).Read(buffer4, 0, buffer4.Length);
        return Encoding.UTF8.GetString(buffer4).TrimEnd(new char[1]);
    }

    /*public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(GetHash(key));
    }*/

    public static string Encrypt(string rawString)
    {
        string s = rawString;
        RijndaelManaged managed = new RijndaelManaged {
            Padding = PaddingMode.Zeros,
            Mode = CipherMode.CBC,
            KeySize = 0x80,
            BlockSize = 0x80
        };
        byte[] bytes = Encoding.UTF8.GetBytes(sKEY);
        byte[] rgbIV = Convert.FromBase64String(sIV);
        ICryptoTransform transform = managed.CreateEncryptor(bytes, rgbIV);
        MemoryStream stream = new MemoryStream();
        CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
        byte[] buffer = Encoding.UTF8.GetBytes(s);
        stream2.Write(buffer, 0, buffer.Length);
        stream2.FlushFinalBlock();
        return Convert.ToBase64String(stream.ToArray());
    }

    /*public static float GetFloat(string key, [Optional, DefaultParameterValue(0f)] float defaultValue)
    {
        string s = GetString(key, defaultValue.ToString());
        float result = defaultValue;
        float.TryParse(s, out result);
        return result;
    }
    */
    private static string GetHash(string key)
    {
        byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(key));
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < buffer.Length; i++)
        {
            builder.Append(buffer[i].ToString("x2"));
        }
        return builder.ToString();
    }
    /*
    public static int GetInt(string key, [Optional, DefaultParameterValue(0)] int defaultValue)
    {
        string s = GetString(key, defaultValue.ToString());
        int result = defaultValue;
        int.TryParse(s, out result);
        return result;
    }

    public static string GetString(string key, [Optional, DefaultParameterValue("")] string defaultValue)
    {
        string str = defaultValue;
        string str2 = PlayerPrefs.GetString(GetHash(key), defaultValue.ToString());
        if (!str.Equals(str2))
        {
            str = Decrypt(str2);
        }
        return str;
    }

    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(GetHash(key));
    }

    public static void Save()
    {
        PlayerPrefs.Save();
    }

    public static void SetFloat(string key, float val)
    {
        PlayerPrefs.SetString(GetHash(key), Encrypt(val.ToString()));
    }

    public static void SetInt(string key, int val)
    {
        PlayerPrefs.SetString(GetHash(key), Encrypt(val.ToString()));
    }

    public static void SetString(string key, string val)
    {
        PlayerPrefs.SetString(GetHash(key), Encrypt(val));
    }*/
}

