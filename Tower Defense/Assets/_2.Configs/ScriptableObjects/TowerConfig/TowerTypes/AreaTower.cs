using UnityEngine;
using TowerStats;

[CreateAssetMenu(fileName = "AreaTower", order = 2, menuName = "Gameplay/Towers/New AreaTower")]
public class AreaTower : BuildingsConfig
{
    [SerializeField] private float _splashRadius;

    public float splashRadius => _splashRadius;
    public override DamageType GetDamageType() => DamageType.Area;
}