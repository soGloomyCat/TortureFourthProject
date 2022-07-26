using UnityEngine;

public class SheepMaterialChanger : MonoBehaviour
{
    [SerializeField] private MeshRenderer _currentMaterial;
    [SerializeField] private Material _material;

    private Material _startMaterial;

    private void Awake() => _startMaterial = _currentMaterial.material;

    public void ChangeMaterial() => _currentMaterial.material = _material;

    public void ResumeMaterial() => _currentMaterial.material = _startMaterial;
}
