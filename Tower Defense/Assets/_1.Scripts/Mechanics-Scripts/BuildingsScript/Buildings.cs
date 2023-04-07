using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Buildings : MonoBehaviour, IStateChange, IInteractable
{
    [Header("����������")]
    [SerializeField] protected EnemyDetector _enemyDetector;
    [SerializeField] protected CombatRadiusVisualizer _combatRadiusVisualizer;

    protected Rigidbody2D _rigidbody2D;
    protected SpriteRenderer _spriteRenderer;
    

    [Header("�������������� ���������")]
    [SerializeField] protected BuildingsConfig _buildingCharacteristic;

    [Header("�������������� ��������� �� ������ ������")]
    private int _currentLevelIndex = 0;
    [SerializeField] protected BuildingsConfig[] _towerLevels;

    [Header("��������� ���������")]
    [SerializeField] protected TowerState[] towerStates;
    protected TowerState currentState;

    public EnemyDetector enemyDetector => _enemyDetector;
    public BuildingsConfig buildingsConfig => _buildingCharacteristic;


    protected void Init()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _rigidbody2D.bodyType = RigidbodyType2D.Static;

        SortLevels();
        SetNewCharacteristics();
    }

    public BuildingsConfig GetNextLevel()
    {
        int newLevel = _currentLevelIndex + 1;
        if(newLevel < _towerLevels.Length)
            return _towerLevels[newLevel];

        return null;
    }
    public void SetNextLevel()
    {
        int newLevel = Mathf.Clamp(_currentLevelIndex+1, 0, _towerLevels.Length - 1);

        if ((newLevel > _currentLevelIndex) && _towerLevels[_currentLevelIndex] )
        {
            _currentLevelIndex = newLevel;
            _buildingCharacteristic = _towerLevels[_currentLevelIndex];
            Debug.Log(string.Format("Set new Level: {0}", _towerLevels[_currentLevelIndex]));
            SetNewCharacteristics();
        }
    }

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



    //���������� ��������� IStateChange
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

    //��������� ������
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

        for(int i = 0; i < towerStates.Length; i++)
            if (towerStates[i] is T)
                findResult = towerStates[i];

        return findResult;
    }

    public void OnSelected()
    {
        if(_combatRadiusVisualizer)
            _combatRadiusVisualizer.ActiveLine(true);
    }

    public void OnDeselected()
    {
        if(_combatRadiusVisualizer)
            _combatRadiusVisualizer.ActiveLine(false);
    }
}
