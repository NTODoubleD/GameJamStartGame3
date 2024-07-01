using DoubleDTeam.Containers;
using DoubleDTeam.UI;
using DoubleDTeam.UI.Base;

namespace Game.UI.Pages
{
    public class MainMenuPage : MonoPage, IUIPage
    {
        public void Open()
        {
        }

        public void OpenTutorial()
        {
            Services.ProjectContext.GetModule<IUIManager>().OpenPage<TutorialPage>();
        }
    }
}