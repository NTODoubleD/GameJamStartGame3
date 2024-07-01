using Cysharp.Threading.Tasks;
using DoubleDTeam.Containers;
using DoubleDTeam.StateMachine;
using Game.States;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainGameState : MonoBehaviour
{
    [SerializeField] private int _nextScene = 3;

    public async void Next()
    {
        SceneManager.LoadScene(_nextScene, LoadSceneMode.Single);

        await UniTask.NextFrame();

        Services.ProjectContext.GetModule<StateMachine>().Enter<MainGameState>();
    }
}