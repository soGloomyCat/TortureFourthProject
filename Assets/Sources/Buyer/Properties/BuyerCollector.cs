using UnityEngine;

public class BuyerCollector : MonoBehaviour
{
    private const int TriggerValue = 1;

    [SerializeField] private BuyerCanvasHandler _canvasHandler;
    [SerializeField] private Transform _pool;

    private Wool _lastWool;
    private int _remainWoolCount;
    private bool _isCollected;

    public int Cost => _lastWool.Cost;
    public int RemainWoolCount => _remainWoolCount;
    public bool IsCollected => _isCollected;

    public bool TryAccept(Wool wool)
    {
        if (wool == null)
            return false;

        return true;
    }

    public void Accept(Wool wool)
    {
        if (wool == null)
            return;

        _remainWoolCount--;
        wool.transform.parent = _pool;

        if (_pool.childCount > TriggerValue)
            wool.Cut(wool.transform, _lastWool.RackPoint);
        else
            wool.Cut(wool.transform, _pool);

        _lastWool = wool;
        _canvasHandler.UpdateInfo(_remainWoolCount);

        if (_remainWoolCount == 0)
            _isCollected = true;
    }

    public void ActivateCanvas(int remainWoolCount)
    {
        if (_canvasHandler.IsCreated == false)
        {
            _canvasHandler.ActivateCanvas(transform, remainWoolCount);
            _remainWoolCount = remainWoolCount;
            _isCollected = false;
        }
    }

    public void DeactivateCanvas()
    {
        _canvasHandler.DisableCanvas();
    }
}
