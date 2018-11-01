
using System.Security.Cryptography;
using System.Text;
public static class HashUtil
{
    public static string Get(string text)
    {
        return Get(Encoding.UTF8.GetBytes(text));
    }
    /// <summary>
    /// 计算哈希值字符串
    /// </summary>
    public static string Get(byte[] buffer)
    {
        if (buffer == null || buffer.Length < 1)
            return "";

        HashAlgorithm hash = HashAlgorithm.Create();
        byte[] hashBuffer = hash.ComputeHash(buffer);
        StringBuilder sb = new StringBuilder();
        foreach (var b in hashBuffer)
            sb.Append(b.ToString("x2"));
        return sb.ToString();
    }
}
