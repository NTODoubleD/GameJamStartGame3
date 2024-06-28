using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;
using DoubleDTeam.UI.Base;
using UnityEngine;

namespace DoubleDTeam.UI.Initializers
{
    public class PageRegister : InitializeObject
    {
        [SerializeField] private MonoPage[] _monoPages;

        private IUIManager uiManager;

        public override void Initialize()
        {
            uiManager = Services.ProjectContext.GetModule<IUIManager>();

            foreach (var monoPage in _monoPages)
                uiManager.RegisterPageByType(monoPage);
        }

        public override void Deinitialize()
        {
            foreach (var monoPage in _monoPages)
                uiManager.RemovePage(monoPage);
        }
    }
}