namespace BalderHash.Extensions
{
    public static class UInt64Extensions
    {
        public static string BalderHash(this ulong number)
        {
            return new BalderHash64(number).ToString();
        }
    }
}
