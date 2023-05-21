using StatsEnums.DamageTypes;
using Structs;
using UnityEngine;


namespace ConfigClasses
{
    /** ������������ ���� ���������������� �������, ����������� � ����������.
    *   ���������������� ������, ����������� � ������������� ������ TowerConfig
    */
    namespace ConfigBuildings
    {
        /** ������������ �����
        *  ������������ �����, ���������� ��� �������������� ��� ������ ��������
        */
        public abstract class TowerConfig : BuildingConfig
        {
            [Header("������ ��������������")]
            [SerializeField]
            protected DamageType m_damageType = DamageType.Physical; /**< enum variable. ��� �����. �� ������� DamageType.Physical*/
            [SerializeField]
            [Tooltip("����")]
            private MinMax _damage; /**< Damage variable. ��������� � ������������ �� ������ */
            [SerializeField]
            [Tooltip("����� �� ��������� �����")]
            private float _attackRatePerSecond; /**< float variable. ����� �� ��������� ������� */

            public virtual DamageType GetDamageType() => m_damageType;

            /**
            * ������, ������������ ������ ��������� Damage _damage
            */
            public MinMax damage => _damage;
            /**
             * ������, ������������ float ���������� ������� �� ������� _fireRatePerSecond
             */
            public float fireRatePerSecond => _attackRatePerSecond;
        }
    }
}