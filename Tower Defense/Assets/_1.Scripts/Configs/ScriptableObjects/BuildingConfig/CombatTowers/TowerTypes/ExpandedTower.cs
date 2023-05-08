using UnityEngine;
using DamageTypes;

namespace ConfigClasses 
{
    namespace TowerConfig
    {
        [CreateAssetMenu(fileName = "ExpandedTower", order = 3, menuName = "Gameplay/Towers/New ExpandedTower")]
        public class ExpandedTower : TowerConfig
        {
            [Header("��� �����")]
            [SerializeField] private DamageType _thisTowerDamageType;

            private void OnValidate() => m_damageType = _thisTowerDamageType;
        }
    }
}
