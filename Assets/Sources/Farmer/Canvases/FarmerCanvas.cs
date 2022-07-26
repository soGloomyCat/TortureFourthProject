using TMPro;
using UnityEngine;

public class FarmerCanvas : MonoBehaviour
{
    [SerializeField] private FarmerWallet _wallet;
    [SerializeField] private WoolCollector _collector;
    [SerializeField] private TMP_Text _balanceText;
    [SerializeField] private GameObject _woolsFrame;
    [SerializeField] private TMP_Text _woolsCountText;

    private void OnEnable()
    {
        _wallet.BalanceChanged += OnChangeBalance;
        _collector.WoolsCountChanged += OnCountChanged;
    }

    private void OnDisable()
    {
        _wallet.BalanceChanged -= OnChangeBalance;
        _collector.WoolsCountChanged -= OnCountChanged;
    }

    private void OnChangeBalance(int balance)
    {
        _balanceText.text = balance.ToString();
    }

    private void OnCountChanged(int count)
    {
        if (count > 0)
        {
            _woolsFrame.SetActive(true);
            _woolsCountText.text = count.ToString();
            return;
        }

        _woolsFrame.SetActive(false);
    }
}
