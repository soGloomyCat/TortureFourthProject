using TMPro;
using UnityEngine;

public class SheepSpawnerCanvas : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private SheepSpawnerHandler _handler;
    [SerializeField] private TMP_Text _costText;

    private void OnEnable() => _handler.CostChanged += ChangeCost;

    private void OnDisable() => _handler.CostChanged -= ChangeCost;

    public void ChangeCost(int cost) => _costText.text = cost.ToString();
}
