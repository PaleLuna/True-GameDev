using UnityEngine;
using ConfigClasses.BuildingConfig;
using Buildings.TowerStates;

/** ������������ ���� �������, ��� ��������� � ����������.
 *  � ����� ������������ ��� ��������� ������, ������������� �� Building, � ����� ������, ����������� ��������� ��������.
 */
namespace Buildings
{
    /** ������������ ����� ��� ���� ��������.
    * ����� ���������� ������� ����� ����� � �������, ������� �������� ������ ��� ���� ��������.
    */
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Building : MonoBehaviour, IStateChange, IInteractable
    {
        [Header("����������")]
        [SerializeField] protected EnemyDetector _enemyDetector; /**< EnemyDetector variable. ���������, ���������� �� ����������� ������*/
        [SerializeField] protected CombatRadiusVisualizer _combatRadiusVisualizer /**< CombatRadiusVisualizer variable. ���������, ���������� �� ������������ �������*/;

        protected Rigidbody2D _rigidbody2D /**< Rigidbody2D variable. ���������, ���������� �� ���������� ��������� */;
        protected SpriteRenderer _spriteRenderer /**< SpriteRenderer variable. ���������, ���������� �� ����������� ������� �������*/;


        [Header("�������������� ���������")]
        [SerializeField] protected BuildingsConfig _buildingCharacteristic /**< BuildingsConfig variable. ���������, �������� �������� ���-�� ���������*/;

        [Header("�������������� ��������� �� ������ ������")]
        private int _currentLevelIndex = 0 /** < integer variable. ������ �������� ������ ��������� */;
        [SerializeField] protected BuildingsConfig[] _towerLevels /** < integer[] variable. ������ ������� ��������*/;

        [Header("��������� ���������")]
        [SerializeField] protected TowerState[] towerStates; /** < TowerState[] variable. ������ ��������� �������� */
        protected TowerState currentState /** < TowerState variable. ������� ���������*/;

        public EnemyDetector enemyDetector => _enemyDetector;
        public BuildingsConfig buildingsConfig => _buildingCharacteristic;

        /**
         * ����� ������������� �������. ���������� �� ���������� ��������� � ������ Start()
        */
        protected void Init()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _rigidbody2D.bodyType = RigidbodyType2D.Static;

            SortLevels();
            SetNewCharacteristics();
        }

        /**
         * ����� ��������� ���������� ���������� ������ ���������
        */
        public BuildingsConfig GetNextLevel()
        {
            int newLevel = _currentLevelIndex + 1;
            if (newLevel < _towerLevels.Length)
                return _towerLevels[newLevel];

            return null;
        }
        /**
         * ����� ��������� ������������� �����, ����������� ���������� � ������
        */
        public void SetNextLevel()
        {
            int newLevel = Mathf.Clamp(_currentLevelIndex + 1, 0, _towerLevels.Length - 1);

            if ((newLevel > _currentLevelIndex) && _towerLevels[_currentLevelIndex])
            {
                _currentLevelIndex = newLevel;
                _buildingCharacteristic = _towerLevels[_currentLevelIndex];
                Debug.Log(string.Format("Set new Level: {0}", _towerLevels[_currentLevelIndex]));
                SetNewCharacteristics();
            }
        }
        /**
         * ����� ���������� ������� �������������
        */
        private void SetNewCharacteristics()
        {
            if (_enemyDetector)
                _enemyDetector.combatRadius = _buildingCharacteristic.combatRadius;
            else
                Debug.LogError("EnemyDetector �� ����������!");

            if (_spriteRenderer)
                _spriteRenderer.sprite = _buildingCharacteristic.towerSprite;
            else
                Debug.LogError("SpriteRenderer �� ����������!");

            if (_combatRadiusVisualizer)
                _combatRadiusVisualizer.SetLine(_buildingCharacteristic.combatRadius);
            else
                Debug.LogError("CombatRadiusVisualizer �� ����������!");
        }


        /** ���������� ��������� IStateChange
         * ����� ����� �������� ���������
        */
        public void ChangeState<T>() where T : State
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


        private void SortLevels()
        {
            for (int i = 1; i < _towerLevels.Length; i++)
            {
                BuildingsConfig cacheConfig = _towerLevels[i];

                int j = i - 1;
                for (; j >= 0 && _towerLevels[j].towerLevel > cacheConfig.towerLevel; j--)
                    _towerLevels[j + 1] = _towerLevels[j];

                _towerLevels[j + 1] = cacheConfig;
            }

            Debug.Log("Levels was sorted");
        }

        private TowerState FindState<T>() where T : State
        {
            TowerState findResult = null;

            for (int i = 0; i < towerStates.Length; i++)
                if (towerStates[i] is T)
                    findResult = towerStates[i];

            return findResult;
        }

        /** ���������� ��������� IInteractable
         * �����, ������������� ��� ��������� �������� �������
        */
        public void OnSelected()
        {
            if (_combatRadiusVisualizer)
                _combatRadiusVisualizer.ActiveLine(true);
        }
        /** ���������� ��������� IInteractable
        * �����, ������������� ��� ������ ��������� �������� �������
        */
        public void OnDeselected()
        {
            if (_combatRadiusVisualizer)
                _combatRadiusVisualizer.ActiveLine(false);
        }
    }
}

