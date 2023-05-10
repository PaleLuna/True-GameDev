using UnityEngine;

namespace ConfigClasses
{
    namespace ConfigBuildings
    {
        [CreateAssetMenu(fileName = "Barracs", order = 4, menuName = "Gameplay/Towers/New Barracks")]
        public class Barracks : BuildingConfig
        {
            [Header("�������������� ������")]
            [SerializeField] [Tooltip("���������� ������ ������������")] private byte _unitCount;
            [SerializeField] [Tooltip("����� �� �������������� ������ ����� � ��������")] private uint _respawnTime;
        }
    }
}

