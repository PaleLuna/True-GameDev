using UnityEngine;

namespace Units
{
    /** ������������ ���, ���������� ������ ���������� ����������� ��������� ������ */
    namespace EnemyScrips
    {
        public class Enemy : Unit
        {
            protected override void CheckHealth()
            {
                if (_currentHealthPoint <= 0)
                {
                    GlobalEvents.SendEnemyDeath(this);
                    Destroy(this.gameObject);
                }
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.CompareTag("Base")) 
                {
                    GlobalEvents.SendEnemyOnBase(unitCharacteristics.damageToBase);
                }
            }
        }
    }
}
