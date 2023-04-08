using UnityEngine;

namespace Buildings
{
    namespace TowerStates
    {
        /** ���������, ������������ ��������� ����� � ��������� ���. */
        public class TowerCombat : TowerState
        {
            [Header("�������")]
            [SerializeField] GameObject bullet /**< GameObject variable. ������ ����. */;

            private Enemy _currentEnemy /**< Enemy variable. ����������, ��������� ������� ���� ��� �����. */;

            /**����� ������ ���������. 
             * ����� ����� ������� �����, ��������� � ���� ��������.
             */
            public override void StateStart()
            {
                _currentEnemy = parentTower.enemyDetector.GetNextEnemy();
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
                if (_currentEnemy && parentTower.enemyDetector.IsHeadLink(_currentEnemy))
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
