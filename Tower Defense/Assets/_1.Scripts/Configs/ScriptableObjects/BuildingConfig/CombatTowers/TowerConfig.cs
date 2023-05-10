using UnityEngine;

using DamageTypes;
using Structs;


namespace ConfigClasses
{
    /** ������������ ���� ���������������� �������, ����������� � ����������.
    *   ���������������� ������, ����������� � ������������� ������ TowerConfig
    */
    namespace ConfigBuildings
    {
        /** ������������ �����
        *  ������������ �����, ���������� ��� �������������� ��� ��������
        */
        abstract public class TowerConfig : BuildingConfig
        {
            [Header("����� ��������������")]
            [SerializeField]
            [Tooltip("���� ���������, ��������� ����������")]
            private MinMax _damage; /**< Damage variable. ��������� � ������������ �� ������ */
            [Space]
            [SerializeField]
            [Tooltip("����� �� ��������� �������")]
            private float _fireRatePerSecond; /**< float variable. ����� �� ��������� ������� */
            [SerializeField]
            [Tooltip("������ �������� ���������")]
            private float _combatRadius; /**< float variable. ������ �������� ����� */

            protected DamageType m_damageType = DamageType.Physical; /**< enum variable. ��� �����. �� ������� DamageType.Physical*/

            /**
             * ������, ������������ ������ ��������� Damage _damage
             */
            public MinMax damage => _damage;
            /**
             * ������, ������������ float ���������� ������� �� ������� _fireRatePerSecond
             */
            public float fireRatePerSecond => _fireRatePerSecond;
            /**
             * ������, ������������ float ���������� ������� �������� _combatRadius
             */
            public float combatRadius => _combatRadius;


            /**
             * �����, ������������ ������ ������������ DamageType m_damageType
             */
            public virtual DamageType GetDamageType() => m_damageType;
        }
    }
}