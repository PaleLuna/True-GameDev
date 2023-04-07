using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    [Header("����������")]
    [SerializeField] private WalletScript _walletScript;

    [Header("�������� ������")]
    [SerializeField] private int _startBalance;

    private void Start()
    {
        _walletScript.SetCurrentBalace(_startBalance);
    }
}
