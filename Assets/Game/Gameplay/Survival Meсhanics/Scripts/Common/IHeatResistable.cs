using System;

namespace Game.Gameplay.SurvivalMechanics
{
    public interface IHeatResistable
    {
        int HeatResistance { get; set; }
        event Action<int> HeatResistanceChanged;
    }
}