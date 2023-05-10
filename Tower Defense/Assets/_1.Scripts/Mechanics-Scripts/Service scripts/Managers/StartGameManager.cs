using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    /** ��������, ���������� � ����������� ��� �������� � ������ */
    public class StartGameManager : Singleton<StartGameManager>
    {
        [Header("����������")]

        [Header("�������� ������")]
        [SerializeField] private int _startBalance; /**< Integer variable. ��������� ������ �� ������ */

        private void Start()
        {
            WalletScript.instance.SetCurrentBalace(_startBalance);
        }
    }
}

