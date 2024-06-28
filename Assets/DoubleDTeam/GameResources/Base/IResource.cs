using System;

namespace DoubleDTeam.GameResources.Base
{
    public interface IResource : IDisposable
    {
        public void Load();
    }
}