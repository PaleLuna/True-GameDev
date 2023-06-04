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
            Time.timeScale = 1.0F;

            WalletScript.instance.SetCurrentBalace(_startBalance);
        }
    }
}

