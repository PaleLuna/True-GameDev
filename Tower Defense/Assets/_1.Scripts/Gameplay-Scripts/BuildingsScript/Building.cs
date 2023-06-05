using Buildings.TowerStates;
using ConfigClasses.ConfigBuildings;
using ModuleClass;
using UnityEngine;

/** ������������ ���� �������, ��� ��������� � ����������.
 *  � ����� ������������ ��� ��������� ������, ������������� �� Building, � ����� ������, ����������� ��������� ��������.
 */
namespace Buildings
{
    /** ������������ ����� ��� ���� ��������.
    * ����� ���������� ������� ����� ����� � �������, ������� �������� ������ ��� ���� ��������.
    */

    public class Building : Entity
    {
        [Header("�������������� ���������")]
        protected BuildingConfig _buildingCharacteristic /**< BuildingsConfig variable. ���������, �������� �������� ���-�� ���������. */;

        [Header("��������� ���������")]
        [SerializeField] protected TowerState[] towerStates; /**< TowerState[] variable. ������ ��������� ��������. */
        protected TowerState currentState /**< TowerState variable. ������� ��������� */;

        public BuildingConfig buildingsConfig => _buildingCharacteristic;

        private new void Awake()
        {
            base.Awake();

            _rigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        private void Start()
        {
            foreach(TowerState i in towerStates)
            {
                i.Init(this);
            }
            ChangeState<TowerChill>();
        }

        private void Update()
        {
            if (currentState)
                currentState.UpdateRun();
        }

        /**
         * �����, ����������� �������������� ���������. C������ ����� SetNewLevel().
         * @see SetNewLevel()
        */
        public void SetNewCharacteristics(BuildingConfig buildingsConfig)
        {
            _buildingCharacteristic = buildingsConfig;

            if (_spriteRenderer)
                _spriteRenderer.sprite = _buildingCharacteristic.sprite;
            else
                Debug.LogError("SpriteRenderer �� ����������!");

            foreach (Module item in modules)
            {
                item.UpdateData(buildingsConfig);
            }
        }

        public void UpdateCharacteristics()
        {
            if (_spriteRenderer)
                _spriteRenderer.sprite = _buildingCharacteristic.sprite;
            else
                Debug.LogError("SpriteRenderer �� ����������!");

            foreach (Module item in modules)
            {
                item.UpdateData(buildingsConfig);
            }
        }

        /** ���������� ��������� IStateChange.
         * ����� ����� �������� ���������.
        */
        public override void ChangeState<T>()
        {
            TowerState newState = FindState<T>();
            if (newState)
            {
                if (currentState) currentState.StateStop();

                currentState = newState;
                currentState.StateStart();
            }

            Debug.Log($"new state {currentState} is set.");
        }


        private TowerState FindState<T>() where T : State
        {
            TowerState findResult = null;

            for (int i = 0; i < towerStates.Length; i++)
                if (towerStates[i] is T)
                    findResult = towerStates[i];

            return findResult;
        }

        
    }
}