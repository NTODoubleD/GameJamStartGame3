using Cysharp.Threading.Tasks;
using DoubleDTeam.Containers;
using DoubleDTeam.StateMachine;
using Game.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private int _nextSceneIndex = 2;

        private StateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = Services.ProjectContext.GetModule<StateMachine>();
        }

        public async void StartGame()
        {
            SceneManager.LoadScene(_nextSceneIndex, LoadSceneMode.Single);

            await UniTask.NextFrame();

            _stateMachine.Enter<MainGameState>();
        }

        public void CloseGame()
        {
            Application.Quit();
        }
    }
}