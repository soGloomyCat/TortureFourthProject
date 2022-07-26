using TMPro;
using UnityEngine;

public class BuyerCanvas : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMP_Text _needCountText;

    public void ActivateCanvas(int woolCount)
    {
        _needCountText.text = woolCount.ToString();
        _canvas.gameObject.SetActive(true);
    }

    public void DisableCanvas()
    {
        _canvas.gameObject.SetActive(false);

    }

    public void UpdateInfo(int woolCount)
    {
        _needCountText.text = woolCount.ToString();
    }
}
