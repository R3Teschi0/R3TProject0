using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private Vector3 lastPos;

    [SerializeField] private LayerMask mask;

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.nearClipPlane;

        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100, mask))
        {
            lastPos = hit.point;
        }

        return lastPos;
            
    }
}
