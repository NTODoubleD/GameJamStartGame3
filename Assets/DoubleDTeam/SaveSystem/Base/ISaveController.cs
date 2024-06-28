using DoubleDTeam.Containers.Base;

namespace DoubleDTeam.SaveSystem.Base
{
    public interface ISaveController : IModule
    {
        public void Register(string key, ISaveObject saveObject);

        public void Remove(string key);

        public void Save(string key);

        public void SaveAll();

        public void Load(string key);

        public void LoadAll();

        public bool ContainSave(string key);
    }
}