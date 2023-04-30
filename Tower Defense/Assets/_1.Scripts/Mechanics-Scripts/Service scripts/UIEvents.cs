using UnityEngine;
using UnityEngine.Events;

namespace GlobalUIEvents
{
    public class UIEvents : MonoBehaviour
    {
        //������� ��������� �������
        public static UnityEvent<short> onEmptyPositionSelected = new UnityEvent<short>();
        public static UnityEvent<short> onOccupiedPositionSelect = new UnityEvent<short>();
        public static UnityEvent onDeselectPosition = new UnityEvent();

        //������� ������� ������
        public static UnityEvent onButtonClick = new UnityEvent();

        //������� ��������� �������
        public static UnityEvent<int> onBalanceChange = new UnityEvent<int>();


        public static void SendSelectedEmptyBlock(short pointIndex) => onEmptyPositionSelected.Invoke(pointIndex);

        public static void SendDeselectBlock() => onDeselectPosition.Invoke();

        public static void SendSelectedOccupiedBlock(short pointIndex) => onOccupiedPositionSelect.Invoke(pointIndex);

        public static void SendButtonClick() => onButtonClick.Invoke();

        public static void SendBalanceChange(int newBalance)
        {
            onBalanceChange.Invoke(newBalance);
        } 
    }
}
