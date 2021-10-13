namespace MyUtility
{
    public static class Guid
    {
        public static string Get()
        {
            return System.Guid.NewGuid().ToString("N");
        }
    }
}
