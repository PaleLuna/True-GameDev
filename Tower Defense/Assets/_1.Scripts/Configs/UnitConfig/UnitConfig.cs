using StatsEnums.DamageTypes;
using StatsEnums.DamageRistances;
using Structs;
using UnityEngine;

namespace ConfigClasses
{
    namespace UnitConfigs
    {
        [CreateAssetMenu(fileName = "TypicalUnit", menuName = "Gameplay/Units/New Unit")]
        public class UnitConfig : EntityConfig
        {
            [Header("��������� ����������")]
            [SerializeField] private int _healthPoints;

            [Header("��������� ��������������")]
            [SerializeField] private int _velocity;

            [Header("�������� ��������������")]
            [SerializeField] private DamageResistance _physicalDamageResistance;
            [SerializeField] private DamageResistance _energyDamageResistance;
            [SerializeField] private DamageResistance _areaDamageResistance;
            [SerializeField] private DamageResistance _amyDamageResistance;

            [Header("������ ��������������")]
            [SerializeField]
            protected DamageType m_damageType = DamageType.Physical; /**< enum variable. ��� �����. �� ������� DamageType.Physical*/
            [SerializeField]
            [Tooltip("����")]
            private MinMax _damage; /**< Damage variable. ��������� � ������������ �� ������ */
            [SerializeField]
            [Tooltip("����� �� ��������� �����")]
            private float _attackRatePerSecond; /**< float variable. ����� �� ��������� ������� */

            [Header("������������� ��������")]
            [SerializeField] private int _rewardForMurder;
            [SerializeField] private int _damageToBase;

            public int healthPoints => _healthPoints;
            public int velocity => _velocity;

            public DamageResistance physicalDamageResistance => _physicalDamageResistance;
            public DamageResistance energyDamageResistance => _energyDamageResistance;
            public DamageResistance areaDamageResistance => _areaDamageResistance;
            public DamageResistance amyDamageResistance => _amyDamageResistance;

            /**
            * ������, ������������ ������ ��������� DamageType m_damageType
            */
            public DamageType GetDamageType() => m_damageType;
            /**
            * ������, ������������ ������ ��������� MinMax _damage
            */
            public MinMax damage => _damage;
            /**
             * ������, ������������ float ���������� ������� �� ������� _fireRatePerSecond
             */
            public float fireRatePerSecond => _attackRatePerSecond;

            public int rewardForMurder => _rewardForMurder;
            public int damageToBase => _damageToBase;
        }
    }
}