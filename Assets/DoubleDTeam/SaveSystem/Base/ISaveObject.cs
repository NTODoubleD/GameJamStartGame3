namespace DoubleDTeam.SaveSystem.Base
{
    public interface ISaveObject
    {
        public string GetData();

        public void OnLoad(string data);
    }
}