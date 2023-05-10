using UnityEngine;
using Unit.EnemyScrips;

namespace Buildings
{
    namespace TowerStates
    {
        /** ���������, ������������ ��������� ����� � ��������� ���. */
        public class TowerCombat : TowerState
        {

            private EnemyDetector _enemyDetector;
            private Enemy _currentEnemy /**< Enemy variable. ����������, ��������� ������� ���� ��� �����. */;

            public override void Init(CombatTower parentTower)
            {
                base.Init(parentTower);

                _enemyDetector = parentTower.GetComponentInChildren<EnemyDetector>();
            }

            /**����� ������ ���������. 
             * ����� ����� ������� �����, ��������� � ���� ��������.
             */
            public override void StateStart()
            {
                _currentEnemy = _enemyDetector.GetNextEnemy();
                Debug.Log(_currentEnemy);
            }

            /**����� ��������� ���������. 
             * ����� ���������� �������� ���������� _currentEnemy.
             */
            public override void StateStop()
            {
                _currentEnemy = null;
            }

            /**����� ���������� � �������� �������.
             * ����� ������������ �������� � ����� ����.
             * � ������ ���������� �����, ������ ��������� �� TowerChill.
             */
            public override void UpdateRun()
            {
                if (_currentEnemy && _enemyDetector.IsHeadLink(_currentEnemy))
                    Debug.Log((_currentEnemy.transform.position - transform.position).normalized);
                else
                    ChangeState<TowerChill>();
            }

            public override void ChangeState<T>()
            {
                parentTower.ChangeState<T>();
            }
        }
    }
}
