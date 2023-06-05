using UnityEngine;
using GlobalUIEvents;

/** ������������ ���, ���������� ������-���������. */
namespace Managers
{
    /** �����-��������, ������������� �������� ������. */
    public class InteractionManager : Singleton<InteractionManager>
    {
        private Camera _mainCam; /**< Camera variable. ��������� ������. */

        [SerializeField]
        private LayerMask _listOfInteractivity; /**< LayerMask variable. ������ ������������ ����, � �������� ����� ��������������� �����. */ 

        private IInteractable _selectedObject; /**< IInteractable variable. ������� ���������� ������. */

        private int _rayDistance = 10; /**< Integer variable. ����� ������������ ����. */
        private bool _isButtonClick = false; /**< Bool variable. ����, �����������, ��� ��� ��� ���������� �� ������ */

        private void Awake()
        {
            UIEvents.onButtonClick.AddListener(IsButtonClick);
        }

        private void Start()
        {
            _mainCam = Camera.main;
        }

        /** �����, ������������� �������� ������ ������ ����*/
        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (_isButtonClick)
                    _isButtonClick = false;
                else
                    SelectObject();
            }
        }

        /** ����� ��������� ������� */
        private void SelectObject()
        {
            if (_selectedObject != null)
                _selectedObject.OnDeselected();

            RaycastHit2D hit = Physics2D.Raycast(_mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, _rayDistance, _listOfInteractivity);
            if (!hit.collider) return;

            _selectedObject = hit.collider.gameObject.GetComponent<IInteractable>();
            if (_selectedObject != null)
                _selectedObject.OnSelected();
        }

        /** ����� ��������� ����� _isButtonClick */
        private void IsButtonClick()
        {
            _isButtonClick = true;
        }
    }
}

