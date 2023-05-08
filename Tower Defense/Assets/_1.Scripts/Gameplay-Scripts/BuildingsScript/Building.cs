using Buildings.TowerStates;
using ConfigClasses.TowerConfig;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public abstract class Building :
        MonoBehaviour, IStateChange, IInteractable, IModuleHub
    {
        //�������
        [HideInInspector] public UnityEvent onSelect = new UnityEvent();
        [HideInInspector] public UnityEvent onDeselect = new UnityEvent();

        [Header("����������")]
        protected Rigidbody2D _rigidbody2D /**< Rigidbody2D variable. ���������, ���������� �� ���������� ���������. */;
        protected SpriteRenderer _spriteRenderer /**< SpriteRenderer variable. ���������, ���������� �� ����������� ������� �������. */;

        [Header("������")]
        private LinkedList<IModule> modules = new LinkedList<IModule>();

        [Header("�������������� ���������")]
        protected TowerConfig _buildingCharacteristic /**< BuildingsConfig variable. ���������, �������� �������� ���-�� ���������. */;

        [Header("��������� ���������")]
        [SerializeField] protected TowerState[] towerStates; /**< TowerState[] variable. ������ ��������� ��������. */
        protected TowerState currentState /**< TowerState variable. ������� ��������� */;

        [Header("�����")]
        private bool _isSelect = false;


        public TowerConfig buildingsConfig => _buildingCharacteristic;

        public bool isSelect { get => this._isSelect; }

        /**
        * ����� ������������� �������. ���������� �� ���������� ��������� � ������ Start().
        */
        protected void Init()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _rigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        /**
         * �����, ����������� �������������� ���������. C������ ����� SetNewLevel().
         * @see SetNewLevel()
        */
        public void SetNewCharacteristics(TowerConfig buildingsConfig)
        {
            this._buildingCharacteristic = buildingsConfig;

            if (_spriteRenderer)
                _spriteRenderer.sprite = _buildingCharacteristic.towerSprite;
            else
                Debug.LogError("SpriteRenderer �� ����������!");

            foreach (IModule item in modules)
            {
                item.SetSpecifications(buildingsConfig);
            }
        }

        /** ���������� ��������� IStateChange.
         * ����� ����� �������� ���������.
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

        private TowerState FindState<T>() where T : State
        {
            TowerState findResult = null;

            for (int i = 0; i < towerStates.Length; i++)
                if (towerStates[i] is T)
                    findResult = towerStates[i];

            return findResult;
        }

        /** ���������� ��������� IInteractable.
         * �����, ������������� ��� ��������� �������� �������.
        */
        public void OnSelected()
        {
            _isSelect = true;
            onSelect.Invoke();
        }
        /** ���������� ��������� IInteractable.
        * �����, ������������� ��� ������ ��������� �������� �������.
        */
        public void OnDeselected()
        {
            _isSelect = false;
            onDeselect.Invoke();
        }

        public void AddModule(IModule module)
        {
            _ = modules.AddLast(module);
        }

        public void RemoveModule(IModule module)
        {
            _ = modules.Remove(module);
        }
    }   
}