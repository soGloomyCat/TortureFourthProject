using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    [SerializeField] private Sheep _sheep;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private SheepOvergrowthHandler _handler;

    public void Spawn()
    {
        Sheep tempSheep = Instantiate(_sheep, transform);
        tempSheep.Inizialize(_targetPosition.position);
        _handler.AddNewSheep(tempSheep);
    }
}
