using System;

namespace DoubleDTeam.InputSystem.Base
{
    public class PayloadedInputCharacter<TValueType>
    {
        public event Action<TValueType> Started;
        public event Action<TValueType> Performed;
        public event Action<TValueType> Canceled;

        public void CallStart(TValueType context) => Started?.Invoke(context);
        public void CallPerform(TValueType context) => Performed?.Invoke(context);
        public void CallCancel(TValueType context) => Canceled?.Invoke(context);
    }
}