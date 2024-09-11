namespace DoubleDCore.Identification
{
    public interface IIdentifying
    {
        public string ID { get; }
        public string GenerateIdentifier();
        public void SetIdentifier(string id);
    }
}