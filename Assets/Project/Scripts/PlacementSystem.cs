using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] GameObject cellIndicator;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Grid grid;

    private const float FIX_HEIGHT = 0.3f;

    private void Update()
    {
        Vector3 MousePos = inputManager.GetSelectedMapPosition();
        Debug.Log(MousePos);

        Vector3Int cellPosition = grid.WorldToCell(MousePos);
        Vector3 pos = grid.GetCellCenterWorld(cellPosition);
        pos.y = FIX_HEIGHT;
        cellIndicator.transform.position = pos;
    }
}
