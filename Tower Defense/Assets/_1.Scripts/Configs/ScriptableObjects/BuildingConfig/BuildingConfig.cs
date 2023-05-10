using StatsEnums;
using UnityEngine;

/** ������������ ���� ���������������� �������.
 *  ������������ ��� ��� �������-��������. 
 *  � ����� ������������ ��� ��������� ������, ������������� �� ScriptableObject.
 */
namespace ConfigClasses
{
    namespace ConfigBuildings
    {
        public class BuildingConfig : ScriptableObject
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
        }
    }
}