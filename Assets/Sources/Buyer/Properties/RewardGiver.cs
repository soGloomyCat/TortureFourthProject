using System.Collections;
using UnityEngine;

public class RewardGiver : MonoBehaviour
{
    [SerializeField] private Money _rewardPrefab;
    [SerializeField] private Transform _spawnPoint;

    private Coroutine _coroutine;
    private bool _isStarted;

    public Transform SpawnPoint => _spawnPoint;
    public bool IsStarted => _isStarted;

    public void PrepairRewardIssue(int rewardCount, Transform rewardPoint, Transform walletPoint)
    {
        _isStarted = true;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(CompleteService(rewardCount, rewardPoint, walletPoint));
    }

    private IEnumerator CompleteService(int rewardCount, Transform rewardPoint, Transform walletPoint)
    {
        WaitForSeconds waiter = new WaitForSeconds(0.02f);
        int spawnedMoneyCount = 0;

        while (spawnedMoneyCount <= rewardCount)
        {
            Money tempReward = Instantiate(_rewardPrefab);
            tempReward.transform.position = _spawnPoint.position;
            tempReward.PrepairMove(rewardPoint, walletPoint);
            spawnedMoneyCount++;
            yield return waiter;
        }
    }
}
