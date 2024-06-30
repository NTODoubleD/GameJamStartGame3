using DoubleDTeam.Containers;
using DoubleDTeam.TimeTools;
using DoubleDTeam.UI.Base;
using Game.Gameplay.Scripts;
using Game.Monologue;
using Game.UI;
using Game.UI.Pages;
using UnityEngine;

public class TEST : MonoBehaviour
{
    [SerializeField] private MonologueGroupInfo _monologueGroupInfo;

    private void Start()
    {
        var deerFab = Services.SceneContext.GetModule<DeerFabric>();
        
        var timer = new Timer(this, TimeBindingType.ScaledTime);

        var uiManager = Services.ProjectContext.GetModule<IUIManager>();

        timer.Start(3f, () => uiManager.OpenPage<MessagePage, MonologueGroupInfo>(_monologueGroupInfo));
    }
}