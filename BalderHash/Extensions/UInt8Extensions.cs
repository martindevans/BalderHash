namespace BalderHash.Extensions
{
    public static class UInt8Extensions
    {
        public static string BalderHash(this byte number)
        {
            return new BalderHash8(number).ToString();
        }
    }
}
