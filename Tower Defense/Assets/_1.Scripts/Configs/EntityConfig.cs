using StatsEnums;
using UnityEngine;

/** ������������ ���� ���������������� �������.
 *  ������������ ��� ��� �������-��������. 
 *  � ����� ������������ ��� ��������� ������, ������������� �� EntityConfig.
 */
namespace ConfigClasses
{
    public abstract class EntityConfig : ScriptableObject
    {
        [Header("�������������� ��������������")]
        [SerializeField]
        [Tooltip("�������")]
        private Levels _towerLevel; /**< enum variable. �������, � �������� ��������� ���-�� */
        [SerializeField]
        [Tooltip("������")]
        private Sprite _towerSprite; /**< Sprite variable. ������������ ������ ��������� */
        [Space]
        [Tooltip("������ ��������")]
        [SerializeField]
        private float _activeRadius; /**< float variable. ������ �������� ����� */

        /**
         * ������, ������������ float ���������� ������� �������� _combatRadius
         */
        public float combatRadius => _activeRadius;

        /**
        * ������, ������������ ������ ������������ Levels _towerLevel
        */
        public Levels towerLevel => _towerLevel;
        /**
        * ������, ������������ Sprite ���������� _towerSprite
        */
        public Sprite towerSprite => _towerSprite;
    }
}
