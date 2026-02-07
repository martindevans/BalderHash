namespace BalderHash.Extensions
{
    public static class UInt16Extensions
    {
        public static string BalderHash(this ushort number)
        {
            return new BalderHash16(number).ToString();
        }
    }
}
