namespace Game.Gameplay.SurvivalMechanics
{
    public interface IRealtimeSurvivalMechanic
    {
        void Enable();
        void Disable();
        void Pause();
        void Unpause();
    }
}