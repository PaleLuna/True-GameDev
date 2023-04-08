using UnityEngine;

using StatsEnums;
using DamageTypes;
using Structs;

/** ������������ ���� ���������������� �������.
 *  ������������ ��� ��� �������-��������. 
 *  � ����� ������������ ��� ��������� ������, ������������� �� ScriptableObject
 */
namespace ConfigClasses
{
    /** ������������ ���� ���������������� �������, ����������� � ����������.
    *   ���������������� ������, ����������� � ������������� ������ BuildingsConfig
    */
    namespace BuildingConfig 
    {
        /** ������������ �����
        *  ������������ �����, ���������� ��� �������������� ��� ��������
        */
        abstract public class BuildingsConfig : ScriptableObject
        {
            [Header("�������� ��������")]
            [SerializeField]
            [Tooltip("������� ���������")]
            private Levels _towerLevel; /**< enum variable. �������, � �������� ��������� ���-�� */
            [SerializeField]
            [Tooltip("������ ���������")]
            private Sprite _towerSprite; /**< Sprite variable. ������������ ������ ��������� */

            [Header("������������� ��������")]
            [SerializeField]
            [Tooltip("���������")]
            private int _levelCost; /**< integer variable. ��������� �������� ��� �������*/
            [SerializeField]
            [Tooltip("����� �������� ��� �������")]
            private int _sellCost; /**< integer variable. ����� �������� ��� �������*/

            [Header("����� ��������������")]
            [SerializeField]
            [Tooltip("���� ���������, ��������� ����������")]
            private Damage _damage; /**< Damage variable. ��������� � ������������ �� ������ */
            [Space]
            [SerializeField]
            [Tooltip("����� �� ��������� �������")]
            private float _fireRatePerSecond; /**< float variable. ����� �� ��������� ������� */
            [SerializeField]
            [Tooltip("������ �������� ���������")]
            private float _combatRadius; /**< float variable. ������ �������� ����� */

            protected DamageType m_damageType = DamageType.Physical; /**< enum variable. ��� �����. �� ������� DamageType.Physical*/

            /**
             * ������, ������������ ������ ������������ Levels _towerLevel
             */
            public Levels towerLevel => _towerLevel;

            /**
             * ������, ������������ int ���������� ��������� _levelCost
             */
            public int levelCost => _levelCost;

            /**
             * ������, ������������ int ���������� ���� ������� _sellCost
             */
            public int sellCost => _sellCost;

            /**
             * ������, ������������ Sprite ���������� _towerSprite
             */
            public Sprite towerSprite => _towerSprite;

            /**
             * ������, ������������ ������ ��������� Damage _damage
             */
            public Damage damage => _damage;
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
