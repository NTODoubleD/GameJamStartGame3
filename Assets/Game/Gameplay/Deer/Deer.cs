using DoubleDTeam.Containers;
using DoubleDTeam.UI.Base;
using Game.UI.Pages;
using UnityEngine;

namespace Game.Gameplay
{
    public class Deer : MonoBehaviour
    {
        public DeerInfo DeerInfo => GetDeerInfo();
        private IUIManager _uiManager;

        private void Awake()
        {
            _uiManager = Services.ProjectContext.GetModule<IUIManager>();
        }

        private DeerInfo GetDeerInfo()
        {
            return new DeerInfo()
            {
                Name = "Max",
                Age = 2,
                HungerDegree = 0.5f,
                Gender = GenderType.Male,
                Status = DeerStatus.Standard,
                WorldPosition = transform.position
            };
        }

        public void OpenInfoPage()
        {
            _uiManager.OpenPage<DeerInfoPage, DeerInfo>(DeerInfo);
        }
    }
}