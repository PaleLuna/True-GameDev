using UnityEngine;

namespace Buildings
{
    namespace TowerStates
    {
        /** ���������, ������������ ��������� ����� � ��������� �����. */
        public class TowerChill : TowerState
        {
            public override void StateStart()
            {
            }

            public override void StateStop()
            {
            }

            public override void ChangeState<T>()
            {
                parentTower.ChangeState<T>();
            }
        }
    }
}

