using UnityEngine;

using StatsEnums;
using DamageTypes;
using Structs;

abstract public class BuildingsConfig : ScriptableObject
{
    [Header("�������� ��������")]
    [SerializeField][Tooltip("������� ���������")] private Levels _towerLevel;
    [SerializeField][Tooltip("������ ���������")] private Sprite _towerSprite;

    [Header("������������� ��������")]
    [SerializeField][Tooltip("���������")] private int _levelCost;
    [SerializeField][Tooltip("����� �������� ��� �������")] private int _sellCost;

    [Header("����� ��������������")]
    [SerializeField][Tooltip("���� ���������, ��������� ����������")] private Damage _damage;
    [Space]
    [SerializeField][Tooltip("����� �� ��������� �������")] private float _fireRatePerSecond;
    [SerializeField][Tooltip("������ �������� ���������")] private float _combatRadius;

    protected DamageType m_damageType = DamageType.Physical;

    public Levels towerLevel => _towerLevel;
    public int levelCost => _levelCost;
    public int sellCost => _sellCost;
    public Sprite towerSprite => _towerSprite;

    public virtual DamageType GetDamageType() => m_damageType;

    public Damage damage => _damage;
    public float fireRatePerSecond => _fireRatePerSecond;
    public float combatRadius => _combatRadius;
}