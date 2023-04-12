using UnityEngine;

namespace Managers
{
    /** ��������, ���������� � ����������� ��� �������� � ������ */
    public class StartGameManager : MonoBehaviour
    {
        [Header("����������")]
        [SerializeField] private WalletScript _walletScript; /**< WalletScript variable. ������, ���������� �������� ������ ������. */

        [Header("�������� ������")]
        [SerializeField] private int _startBalance; /**< Integer variable. ��������� ������ �� ������ */

        private void Start()
        {
            _walletScript.SetCurrentBalace(_startBalance);
        }
    }
}

