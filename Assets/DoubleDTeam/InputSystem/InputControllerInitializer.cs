using System.Collections.Generic;
using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;
using DoubleDTeam.InputSystem.Base;
using UnityEngine;

namespace DoubleDTeam.InputSystem
{
    public class InputControllerInitializer : InitializeObject
    {
        [SerializeField] private List<InputMap> _maps;

        public override void Initialize()
        {
            var inputController = new InputController();

            Services.ProjectContext.RegisterModule(inputController);

            foreach (var inputMap in _maps)
                inputController.BindMap(inputMap);
        }

        public override void Deinitialize()
        {
            Services.ProjectContext.RemoveModule<InputController>();
        }
    }
}