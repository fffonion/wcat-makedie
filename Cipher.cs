using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

public class Cipher
{
    public static string DEFAULT_IV_128;
    public static string DEFAULT_NETWORKHASH;
    private const string KEY_FACTORY_ALGORITHM = "RSA";
    private const string PUBLIC_KEY_STRING = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAytwW/1jarl9pOzhWLYn4rLO5TQAY3FLj/smhQRqyQTU2Kn8+Xqg6d5+392dlFziZUFhBivmAJKuf+i5nyE3bxfsgXwN5auea1sanDO0AsKT62Cqb9aUzYObOhkcObK2QHsW2WP7e9pQvrg+J+DVvr+JXwH60xsjFSSte5gq+zCKtlhceSEZ6OTghufGgNGtASODKsVtiu/iFvCkH6bVJ3JYXsfSsy7Oj1/Ov7cbrCgUM49Qhv0r68DvpAiBSSr715gkWcZcWoJ54ZKWWsaBvrIA2Y+qHP8FwqPX8m5Uh0dbh8fznXprjdbtQUPPPR6z6TIc+ESI4K/D/AyJigtsuOwIDAQAB";
    private const string SIGNATURE_ALGORITHM = "Sha256WithRSA";

    static Cipher()
    {
        DEFAULT_IV_128 = "=q$f]p&(K.3_#hHk";
        DEFAULT_NETWORKHASH = "Y.u=M,N-!8Jd2`RXE)k!]y<w2TFg-[4Z";
    }

    private static bool CompareBytearrays(byte[] a, byte[] b)
    {
        if (a.Length != b.Length)
        {
            return false;
        }
        int index = 0;
        foreach (byte num2 in a)
        {
            if (num2 != b[index])
            {
                return false;
            }
            index++;
        }
        return true;
    }

    private static RSACryptoServiceProvider DecodeX509PublicKey(byte[] x509key)
    {
        RSACryptoServiceProvider provider2;
        byte[] b = new byte[] { 0x30, 13, 6, 9, 0x2a, 0x86, 0x48, 0x86, 0xf7, 13, 1, 1, 1, 5, 0, 0 };
        byte[] buffer2 = new byte[15];
        MemoryStream input = new MemoryStream(x509key);
        BinaryReader reader = new BinaryReader(input);
        ushort num2 = 0;
        try
        {
            switch (reader.ReadUInt16())
            {
                case 0x8130:
                    reader.ReadByte();
                    break;

                case 0x8230:
                    reader.ReadInt16();
                    break;

                default:
                    return null;
            }
            if (CompareBytearrays(reader.ReadBytes(15), b))
            {
                switch (reader.ReadUInt16())
                {
                    case 0x8103:
                        reader.ReadByte();
                        goto Label_00CC;

                    case 0x8203:
                        reader.ReadInt16();
                        goto Label_00CC;
                }
            }
            return null;
        Label_00CC:
            if (reader.ReadByte() == 0)
            {
                switch (reader.ReadUInt16())
                {
                    case 0x8130:
                        reader.ReadByte();
                        goto Label_0123;

                    case 0x8230:
                        reader.ReadInt16();
                        goto Label_0123;
                }
            }
            return null;
        Label_0123:
            num2 = reader.ReadUInt16();
            byte num3 = 0;
            byte num4 = 0;
            switch (num2)
            {
                case 0x8102:
                    num3 = reader.ReadByte();
                    break;

                case 0x8202:
                    num4 = reader.ReadByte();
                    num3 = reader.ReadByte();
                    break;

                default:
                    return null;
            }
            byte[] buffer1 = new byte[4];
            buffer1[0] = num3;
            buffer1[1] = num4;
            byte[] buffer3 = buffer1;
            int count = BitConverter.ToInt32(buffer3, 0);
            byte num6 = reader.ReadByte();
            reader.BaseStream.Seek(-1L, SeekOrigin.Current);
            if (num6 == 0)
            {
                reader.ReadByte();
                count--;
            }
            byte[] buffer4 = reader.ReadBytes(count);
            if (reader.ReadByte() != 2)
            {
                return null;
            }
            int num7 = reader.ReadByte();
            byte[] buffer5 = reader.ReadBytes(num7);
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            RSAParameters parameters = new RSAParameters {
                Modulus = buffer4,
                Exponent = buffer5
            };
            provider.ImportParameters(parameters);
            provider2 = provider;
        }
        catch (Exception)
        {
            provider2 = null;
        }
        finally
        {
            reader.Close();
        }
        return provider2;
    }

    public static string DecryptRJ128(string prm_key, string prm_iv, string prm_text_to_decrypt)
    {
        byte[] bytes = DecryptRJ128Byte(prm_key, prm_iv, prm_text_to_decrypt);
        if (bytes == null)
        {
            return null;
        }
        return Encoding.UTF8.GetString(bytes);
    }

    public static byte[] DecryptRJ128Byte(string prm_key, string prm_iv, string prm_text_to_decrypt)
    {
        string s = prm_text_to_decrypt;
        RijndaelManaged managed = new RijndaelManaged {
            Padding = PaddingMode.PKCS7,
            Mode = CipherMode.CBC,
            KeySize = 0x100,
            BlockSize = 0x80
        };
        byte[] bytes = Encoding.UTF8.GetBytes(prm_key);
        byte[] rgbIV = Encoding.UTF8.GetBytes(prm_iv);
        ICryptoTransform transform = managed.CreateDecryptor(bytes, rgbIV);
        byte[] buffer = Convert.FromBase64String(s);
        byte[] buffer4 = new byte[buffer.Length];
        MemoryStream stream = new MemoryStream(buffer);
        int newSize = new CryptoStream(stream, transform, CryptoStreamMode.Read).Read(buffer4, 0, buffer4.Length);
        Array.Resize<byte>(ref buffer4, newSize);
        return buffer4;
    }

    public static string EncryptRJ128(string prm_key, string prm_iv, string prm_text_to_encrypt)
    {
        return EncryptRJ128Byte(prm_key, prm_iv, Encoding.UTF8.GetBytes(prm_text_to_encrypt));
    }

    public static string EncryptRJ128Byte(string prm_key, string prm_iv, byte[] toEncrypt)
    {
        RijndaelManaged managed = new RijndaelManaged {
            Padding = PaddingMode.PKCS7,
            Mode = CipherMode.CBC,
            KeySize = 0x100,
            BlockSize = 0x80
        };
        byte[] bytes = Encoding.UTF8.GetBytes(prm_key);
        byte[] rgbIV = Encoding.UTF8.GetBytes(prm_iv);
        ICryptoTransform transform = managed.CreateEncryptor(bytes, rgbIV);
        MemoryStream stream = new MemoryStream();
        CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
        stream2.Write(toEncrypt, 0, toEncrypt.Length);
        stream2.FlushFinalBlock();
        return Convert.ToBase64String(stream.ToArray());
    }

    public static byte[] SHA256Hash(string text)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        byte[] buffer2 = null;
        try
        {
            buffer2 = SHA256.Create().ComputeHash(bytes);
        }
        catch (Exception)
        {
            Console.WriteLine("SHA256.ComputHash failed");
            return null;
        }
        return buffer2;
    }

    public static bool verify(string signedData, string base64Signature)
    {
        byte[] rgbSignature = Convert.FromBase64String(base64Signature);
        RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(DecodeX509PublicKey(Convert.FromBase64String("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAytwW/1jarl9pOzhWLYn4rLO5TQAY3FLj/smhQRqyQTU2Kn8+Xqg6d5+392dlFziZUFhBivmAJKuf+i5nyE3bxfsgXwN5auea1sanDO0AsKT62Cqb9aUzYObOhkcObK2QHsW2WP7e9pQvrg+J+DVvr+JXwH60xsjFSSte5gq+zCKtlhceSEZ6OTghufGgNGtASODKsVtiu/iFvCkH6bVJ3JYXsfSsy7Oj1/Ov7cbrCgUM49Qhv0r68DvpAiBSSr715gkWcZcWoJ54ZKWWsaBvrIA2Y+qHP8FwqPX8m5Uh0dbh8fznXprjdbtQUPPPR6z6TIc+ESI4K/D/AyJigtsuOwIDAQAB")));
        deformatter.SetHashAlgorithm("SHA256");
        byte[] rgbHash = SHA256Hash(signedData);
        bool flag = false;
        if (deformatter.VerifySignature(rgbHash, rgbSignature))
        {
            flag = true;
        }
        return flag;
    }
}

