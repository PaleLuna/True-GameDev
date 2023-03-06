using System.ComponentModel;
using UnityEngine;

namespace TowerStats
{
    public enum Levels:byte
    {
        First,
        Second,
        Third,
        Special
    }
    public enum DamageType:byte
    {
        Physical,
        Energy,
        Area,
        AMY
    }

    abstract public class TowerStatsInfo : ScriptableObject
    {
        

        [Header("�������� ��������")]
        [SerializeField][Tooltip("������� �����")] protected Levels m_towerLevel;
        [SerializeField][Tooltip("���������")] protected ushort m_levelCost;
        [SerializeField][Tooltip("������ �����")] protected Sprite m_towerSprite;

        [Header("����� ��������������")]
        [SerializeField][Tooltip("���� ����� �� �������")] protected uint m_damage;
        [SerializeField][Tooltip("���������������� � ��������/�������")] protected uint m_fireRatePerSecond;
        [SerializeField][Tooltip("������ �������� �����")] protected float m_combatRadius;

        protected DamageType m_damageType = DamageType.Physical;

        public Levels towerLevel => m_towerLevel;
        public uint levelCost => m_levelCost;
        public Sprite towerSprite => m_towerSprite;

        public virtual DamageType GetDamageType() => m_damageType;

        public uint damage => m_damage;
        public uint fireRatePerSecond => m_fireRatePerSecond;
        public float combatRadius => m_combatRadius;
    }

}
