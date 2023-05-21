using UnityEngine;

namespace Buildings
{
    /** ������������ ���, ���������� ������, ����������� ��������� �����. */
    namespace TowerStates
    {
        /** ������������ ����� ��� ���� ��������� �����. */
        public abstract class TowerState : State
        {
            protected Building parentTower; /**< CombatTower variable. ������������ �����, � ������� ��������� ���������. */

            /** ����� ������������� ���������.
             * @param parentTower. ������������ �����.
             */
            public virtual void Init(Building parentTower)
            {
                if (!this.parentTower)
                    this.parentTower = parentTower;
            }

            /**����� ������ ���������.
             * ����� ������ ���������� ������ ���, ����� ��������� ���������� �����������.
             */
            public abstract override void StateStart();
            /**����� ��������� ���������.
             * ����� ������ ���������� ������ ���, ����� ��������� ���������������.
             */
            public abstract override void StateStop();

            /**����� ����� ���������.
             * �������� ������ ������������ ��� ��� ���� �����.
             */
            public abstract override void ChangeState<T>();
        }
    }
}

