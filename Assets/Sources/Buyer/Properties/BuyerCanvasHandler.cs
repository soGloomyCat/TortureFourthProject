using UnityEngine;

public class BuyerCanvasHandler : MonoBehaviour
{
    private const float Offset = 1.5f;

    [SerializeField] private BuyerCanvas _buyerCanvas;

    public bool IsCreated { get; private set; }

    private void Awake()
    {
        IsCreated = false;
    }

    public void ActivateCanvas(Transform buyer, int woolCount)
    {
        IsCreated = true;
        _buyerCanvas = Instantiate(_buyerCanvas, buyer);
        _buyerCanvas.gameObject.SetActive(false);
        _buyerCanvas.transform.position = new Vector3(buyer.position.x, buyer.position.y + Offset, buyer.position.z);
        _buyerCanvas.ActivateCanvas(woolCount);
    }

    public void DisableCanvas()
    {
        _buyerCanvas.DisableCanvas();
    }

    public void UpdateInfo(int woolCount)
    {
        _buyerCanvas.UpdateInfo(woolCount);
    }
}
