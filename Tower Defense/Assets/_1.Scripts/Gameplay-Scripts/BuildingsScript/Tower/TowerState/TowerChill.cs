using UnityEngine;

namespace Buildings
{
    namespace TowerStates
    {
        /** ���������, ������������ ��������� ����� � ��������� �����. */
        public class TowerChill : TowerState
        {
            private EnemyDetector _enemyDetector;

            public override void Init(CombatTower parentTower)
            {
                base.Init(parentTower);
                _enemyDetector = parentTower.GetComponentInChildren<EnemyDetector>();
            }

            public override void StateStart()
            {
                _enemyDetector?.onEnemyCrossesArea.AddListener(ChangeState<TowerCombat>);
            }

            public override void StateStop()
            {
                _enemyDetector?.onEnemyCrossesArea.RemoveListener(ChangeState<TowerCombat>);
            }

            public override void ChangeState<T>()
            {
                parentTower.ChangeState<T>();
            }
        }
    }
}

