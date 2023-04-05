using UnityEngine;

[CreateAssetMenu(fileName = "Barracs", order = 4, menuName = "Gameplay/Towers/New Barracks")]
public class Barracks : BuildingsConfig
{
    [Header("�������������� ������")]
    [SerializeField][Tooltip("���������� ������ ������������")] private byte _unitCount;
    [SerializeField][Tooltip("����� �� �������������� ������ ����� � ��������")] private uint _respawnTime;
}