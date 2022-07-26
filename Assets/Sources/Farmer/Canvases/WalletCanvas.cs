using System.Collections;
using TMPro;
using UnityEngine;

public class WalletCanvas : MonoBehaviour
{
    private const string AnimationTrigger = "IsOver";

    [SerializeField] private FarmerWallet _wallet;
    [SerializeField] private GameObject _rewardFrame;
    [SerializeField] private Animator _rewardAnimator;
    [SerializeField] private TMP_Text _rewardText;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _wallet.NeedShowReward += ActivateInfo;
    }

    private void OnDisable()
    {
        _wallet.NeedShowReward -= ActivateInfo;
    }

    private void ActivateInfo(int reward)
    {
        _rewardText.text = $"+{reward}";

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ShowPickText());
    }

    private IEnumerator ShowPickText()
    {
        WaitForSeconds waiter = new WaitForSeconds(0.4f);
        _rewardFrame.gameObject.SetActive(true);
        yield return waiter;
        _rewardAnimator.SetTrigger(AnimationTrigger);
        yield return waiter;
        _rewardFrame.gameObject.SetActive(false);
    }
}
