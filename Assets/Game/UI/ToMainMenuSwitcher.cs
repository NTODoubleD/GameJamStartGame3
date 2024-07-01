using Cysharp.Threading.Tasks;
using DoubleDTeam.Containers;
using DoubleDTeam.StateMachine;
using Game.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class ToMainMenuSwitcher : MonoBehaviour
    {
        [SerializeField] private int _sceneIndex = 1;

        public async void LoadScene()
        {
            SceneManager.LoadScene(_sceneIndex, LoadSceneMode.Single);

            await UniTask.NextFrame();

            Services.ProjectContext.GetModule<StateMachine>().Enter<MainMenuState>();
        }
    }
}