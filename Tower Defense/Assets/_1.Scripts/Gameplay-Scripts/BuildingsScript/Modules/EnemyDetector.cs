using ConfigClasses;
using ModuleClass;
using System.Collections.Generic;
using Unit.EnemyScrips;
using UnityEngine;
using UnityEngine.Events;

namespace Buildings
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class EnemyDetector : Module
    {
        public UnityEvent onEnemyCrossesArea = new UnityEvent();

        private CircleCollider2D _circleCollider2d;
        private Building _parentBuilding;

        private LinkedList<Enemy> _enemyList;

        private bool isActive = false;

        private void Awake()
        {
            base.Init();

            _enemyList = new LinkedList<Enemy>();
            _circleCollider2d = GetComponent<CircleCollider2D>();
            _circleCollider2d.isTrigger = true;

            SetParent();
        }

        private void SetParent()
        {
            _parentBuilding = ClassConverter<Building>.Convert(m_moduleParent);
        }

        private void OnEnable()
        {
            isActive = true;
            CheckCollidersInZone();
            _parentBuilding.AddModule(this);
        }

        private void OnDisable()
        {
            isActive = false;
            _enemyList.Clear();
            _parentBuilding.RemoveModule(this);
        }

        public Enemy GetNextEnemy() => (_enemyList.Count <= 0) ? null : _enemyList.First.Value;
        public bool IsHeadLink(Enemy enemy) => (_enemyList.Count <= 0) ? false : _enemyList.First.Value == enemy;

        //������������ ����������� � �������
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!isActive) return;
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                Debug.Log("hit " + collision.gameObject);

                _ = _enemyList.AddLast(enemy);
                onEnemyCrossesArea.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!isActive) return;
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            Debug.Log("out");
            if (enemy)
                _ = _enemyList.Remove(enemy);
        }

        private void CheckCollidersInZone()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _circleCollider2d.radius);

            foreach (Collider2D collider in colliders)
            {
                OnTriggerEnter2D(collider);
            }
        }

        public override void UpdateData(EntityConfig data)
        {
            if (data == null || data.combatRadius < 0) return;

            _circleCollider2d.radius = data.combatRadius;
        }
    }
}
