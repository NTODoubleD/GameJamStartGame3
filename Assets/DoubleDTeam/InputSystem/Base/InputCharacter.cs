using System;

namespace DoubleDTeam.InputSystem.Base
{
    public class InputCharacter
    {
        public event Action Started;
        public event Action Performed;
        public event Action Canceled;

        public void CallStart() => Started?.Invoke();
        public void CallPerform() => Performed?.Invoke();
        public void CallCancel() => Canceled?.Invoke();
    }
}