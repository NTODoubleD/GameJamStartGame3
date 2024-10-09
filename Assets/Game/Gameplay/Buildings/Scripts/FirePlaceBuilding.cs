using Game.UI.Pages;

namespace Game.Gameplay.Buildings
{
    public class FirePlaceBuilding : UpgradableBuilding
    {
        public void OpenCookingPage()
        {
            UIManager.OpenPage<CookingPage>();
        }

        public override ILevelsConfig GetLevelsConfig()
        {
            throw new System.NotImplementedException();
        }
    }
}