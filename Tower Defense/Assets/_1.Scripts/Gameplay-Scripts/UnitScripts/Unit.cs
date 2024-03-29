using ConfigClasses.UnitConfigs;

using DebugScripts.GizmosDebug;

using System.Collections.Generic;
using Units.UnitStates;
using UnityEngine;

using StatsEnums.DamageRistances;

/**������������ ���, ���������� ������ ���������� ������� ������ */
namespace Units
{

    /** ����������� �����, ������������ ������� ������ ��� ���� ������ */
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Unit : Entity, IDamageable
    {

        [Header("�������������� �����")]
        [SerializeField] protected UnitConfig _unitCharacteristics /**< UnitConfig variable. ���������, �������� �������� ���-�� ���������. */;

        [Header("��������� �����")]
        [SerializeField] protected UnitState[] unitStates; /**< UnitState[] variable. ������ ��������� ��������. */
        protected UnitState currentState /**< UnitState variable. ������� ��������� */;

        private Queue<Transform> _pathPoints;

        public UnitConfig unitCharacteristics => _unitCharacteristics;
        public Queue<Transform> pathPoints => _pathPoints;

        protected float _currentHealthPoint;

        private new void Awake()
        {
            base.Awake();
            for(int i = 0; i < unitStates.Length; i++)
            {
                unitStates[i].Init(this);
            }

            ChangeState<UnitChill>();

            _currentHealthPoint = unitCharacteristics.healthPoints;
        }

        private void FixedUpdate()
        {
            if (currentState)
                currentState.FixedRun();
        }

        public void MoveTo(Transform targetTransform)
        {
            _pathPoints.Enqueue(targetTransform);
            if (currentState is UnitChill)
                ChangeState<UnitWalk>();
        }

        public void MoveTo(Queue<Transform> targetTransforms)
        {
            _pathPoints = new Queue<Transform>(targetTransforms);

            if (currentState is UnitChill)
                ChangeState<UnitWalk>();
        }

        /** ���������� ��������� IStateChange.
         * ����� ����� �������� ���������.
        */
        public override void ChangeState<T>()
        {
            UnitState newState = FindState<T>();
            if (newState)
            {
                if (currentState) currentState.StateStop();

                currentState = newState;
                currentState.StateStart();
            }

            Debug.Log($"new state {currentState} is set.");
        }

        private UnitState FindState<T>() where T : State
        {
            UnitState findResult = null;

            for (int i = 0; i < unitStates.Length; i++)
                if (unitStates[i] is T)
                    findResult = unitStates[i];

            return findResult;
        }


        private void OnDrawGizmosSelected()
        {
            if (_pathPoints == null || _pathPoints.Count <= 0) return;
            Transform[] path = pathPoints.ToArray();

            for (int i = 0; i < path.Length - 1; i++)
                GizmosOnPlaying.DrawLine(path[i].position, path[i + 1].position, Color.red);
        }

        public void GetPhysicalDamage(float damage)
        {
            _currentHealthPoint -= damage - damage * DamageResistances.GetDamageResistance(unitCharacteristics.physicalDamageResistance);
            CheckHealth();
        }

        public void GetExplosiveDamage(float damage)
        {
            _currentHealthPoint -= damage - damage * DamageResistances.GetDamageResistance(unitCharacteristics.areaDamageResistance);
            CheckHealth();
        }

        public void GetEnergyDamage(float damage)
        {
            _currentHealthPoint -= damage - damage * DamageResistances.GetDamageResistance(unitCharacteristics.energyDamageResistance);
            CheckHealth();
        }

        protected abstract void CheckHealth();
    }

    
}