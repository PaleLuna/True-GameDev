using System.Collections.Generic;

using ConfigClasses;
using ConfigClasses.UnitConfigs;

using ModuleClass;

using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

/**������������ ���, ���������� ������ ���������� ������� ������ */
namespace Units
{

    /** ����������� �����, ������������ ������� ������ ��� ���� ������ */
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Unit : MonoBehaviour, IModuleHub, IInteractable, IStateChange
    {
        //�������
        [HideInInspector] public UnityEvent onSelect = new UnityEvent();
        [HideInInspector] public UnityEvent onDeselect = new UnityEvent();

        [Header("����������")]
        protected Rigidbody2D _rigidbody2D /**< Rigidbody2D variable. ���������, ���������� �� ���������� ���������. */;
        protected SpriteRenderer _spriteRenderer /**< SpriteRenderer variable. ���������, ���������� �� ����������� ������� �������. */;

        [Header("������")]
        private LinkedList<Module> modules = new LinkedList<Module>();

        [Header("�������������� �����")]
        [SerializeField] protected UnitConfig _unitCharacteristics /**< UnitConfig variable. ���������, �������� �������� ���-�� ���������. */;

        [Header("��������� �����")]
        [SerializeField] protected State[] unitStates; /**< TowerState[] variable. ������ ��������� ��������. */
        protected State currentState /**< TowerState variable. ������� ��������� */;

        [Header("�����")]
        private bool _isSelect = false;


        public UnitConfig unitCharacteristics => _unitCharacteristics;

        public bool isSelect { get => this._isSelect; }

        /**
        * ����� ������������� �������. ���������� �� ���������� ��������� � ������ Start().
        */
        protected void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _rigidbody2D.bodyType = RigidbodyType2D.Static;

            if (unitStates.Length != 0)
            {
            }
        }

        private void Update()
        {
            if (currentState)
                currentState.UpdateRun();
        }

        public void MoveTo(Vector2 movementVector)
        {

        }

        /** ���������� ��������� IStateChange.
         * ����� ����� �������� ���������.
        */
        public void ChangeState<T>() where T : State
        {
            State newState = FindState<T>();
            if (newState)
            {
                if (currentState) currentState.StateStop();

                currentState = newState;
                currentState.StateStart();
            }

            Debug.Log($"new state {currentState} is set.");
        }

        private State FindState<T>() where T : State
        {
            State findResult = null;

            for (int i = 0; i < unitStates.Length; i++)
                if (unitStates[i] is T)
                    findResult = unitStates[i];

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

        public void AddModule(Module module)
        {
            _ = modules.AddLast(module);
        }

        public void RemoveModule(Module module)
        {
            _ = modules.Remove(module);
        }
    }
}
