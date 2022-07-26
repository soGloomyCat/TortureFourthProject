using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionCanvas : MonoBehaviour
{
    private const string AnimationTrigger = "IsOver";

    [SerializeField] private Animator _frameAnimator;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private TMP_Text _missionTargetText;
    [SerializeField] private TMP_Text _progressText;
    [SerializeField] private Image _objectIcon;
    [SerializeField] private Image _completeIcon;

    private Coroutine _coroutine;

    public void ActivateMission(string missionTargetText, int currentCount, int needCount, Sprite targetIcon)
    {
        _progressBar.value = 0;
        _progressBar.maxValue = needCount;
        _missionTargetText.text = missionTargetText;
        _progressText.text = $"{currentCount} / {needCount}";
        _objectIcon.gameObject.SetActive(true);
        _objectIcon.sprite = targetIcon;
        _completeIcon.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void UpdateInfo(int currentCount, int needCount)
    {
        _progressBar.value = currentCount;
        _progressText.text = $"{currentCount} / {needCount}";
    }

    public void Complete()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(CloseFrame());
    }

    private IEnumerator CloseFrame()
    {
        WaitForSeconds waiter = new WaitForSeconds(0.5f);
        _objectIcon.gameObject.SetActive(false);
        _completeIcon.gameObject.SetActive(true);
        _frameAnimator.SetTrigger(AnimationTrigger);
        yield return waiter;
        Destroy(gameObject);
    }
}
