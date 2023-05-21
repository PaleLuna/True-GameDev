using UnityEngine;

namespace ConfigClasses
{
    /** ������������ ���� ���������������� �������.
    *  ������������ ��� ��� �������-��������. 
    *  � ����� ������������ ��� ��������� ������, ������������� �� ConfigBuilding.
    */
    namespace ConfigBuildings
    {
        public abstract class BuildingConfig : EntityConfig
        {
            [Header("������������� ��������")]
            [SerializeField]
            [Tooltip("���������")]
            private int _levelCost; /**< integer variable. ��������� �������� ��� �������*/
            [SerializeField]
            [Tooltip("����� �������� ��� �������")]
            private int _sellCost; /**< integer variable. ����� �������� ��� �������*/

            /**
            * ������, ������������ int ���������� ��������� _levelCost
            */
            public int levelCost => _levelCost;

            /**
            * ������, ������������ int ���������� ���� ������� _sellCost
            */
            public int sellCost => _sellCost;
        }
    }
}