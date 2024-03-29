using UnityEngine;
using StatsEnums.DamageTypes;

namespace ConfigClasses
{
    namespace ConfigBuildings
    {
        [CreateAssetMenu(fileName = "EnergyTower", order = 1, menuName = "Gameplay/Towers/New EnergyTower")]
        public class EnergyTower : TowerConfig
        {
            public override DamageType GetDamageType() => DamageType.Energy;
        }
    }
}

