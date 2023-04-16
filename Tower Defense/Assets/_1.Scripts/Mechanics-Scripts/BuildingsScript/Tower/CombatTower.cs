using UnityEngine;
using Buildings.TowerStates;

namespace Buildings
{
    /** ������������� �������� ������ Building.
     * ������ ����� �������� ����������� ��� ������ �����.
     */
    public class CombatTower : Building
    {
        private void Start()
        {
            if (enemyDetector)
                enemyDetector.Init(this);

            Init();

            if (towerStates.Length != 0)
            {
                for (int i = 0; i < towerStates.Length; i++)
                    towerStates[i].Init(this);
            }

            ChangeState<TowerChill>();
        }

        private void Update()
        {
            currentState.UpdateRun();
        }
    }
}

