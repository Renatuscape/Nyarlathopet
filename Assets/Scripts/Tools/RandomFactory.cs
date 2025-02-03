public static class RandomFactory
{
    public static string GetRandomString(int length)
    {
        // Characters that are generally safe with Unity's fallback font
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-=[]{}|;:,.<>?";
        var random = new System.Random();

        var result = new System.Text.StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            result.Append(validChars[random.Next(validChars.Length)]);
        }

        return result.ToString();
    }
}