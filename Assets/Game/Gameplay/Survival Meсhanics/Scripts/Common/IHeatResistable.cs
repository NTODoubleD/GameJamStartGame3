using System;

namespace Game.Gameplay.SurvivalMechanics
{
    public interface IHeatResistable
    {
        float HeatResistance { get; set; }
        event Action<float> HeatResistanceChanged;
    }
}