using System.Collections;
using TMPro;
using UnityEngine;

public class CollectorCanvas : MonoBehaviour
{
    private const float CountTextOffset = 1.5f;
    private const float OverloadTextOffset = 0.5f;
    private const string AnimationTrigger = "IsOver";

    [SerializeField] private WoolCollector _collector;
    [SerializeField] private TMP_Text _pickCountText;
    [SerializeField] private Animator _pickTextAnimator;
    [SerializeField] private TMP_Text _overloadText;

    private bool _isOverload;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _isOverload = false;
        _collector.WoolPicked += ActivatePickText;
        _collector.OverloadReached += ActivateOverloadText;
        _collector.OverloadRemoved += DeactivateOverloadText;
    }

    private void OnDisable()
    {
        _collector.WoolPicked -= ActivatePickText;
        _collector.OverloadReached -= ActivateOverloadText;
        _collector.OverloadRemoved -= DeactivateOverloadText;
    }

    private void Update()
    {
        if (_isOverload)
            ActivateOverloadText();
    }

    private void ActivatePickText()
    {
        if (_coroutine != null)
        {
            _pickCountText.gameObject.SetActive(false);
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ShowPickText());
    }

    private void ActivateOverloadText()
    {
        _isOverload = true;
        transform.position = new Vector3(_collector.Position.position.x, _collector.Position.position.y + OverloadTextOffset, _collector.Position.position.z);
        _overloadText.gameObject.SetActive(true);
    }

    private void DeactivateOverloadText()
    {
        _overloadText.gameObject.SetActive(false);
        _isOverload = false;
    }

    private IEnumerator ShowPickText()
    {
        WaitForSeconds waiter = new WaitForSeconds(0.5f);
        transform.position = new Vector3(_collector.transform.position.x, _collector.transform.position.y + CountTextOffset, _collector.transform.position.z);
        _pickCountText.gameObject.SetActive(true);
        yield return waiter;
        _pickTextAnimator.SetTrigger(AnimationTrigger);
        yield return waiter;
        _pickCountText.gameObject.SetActive(false);
    }
}
