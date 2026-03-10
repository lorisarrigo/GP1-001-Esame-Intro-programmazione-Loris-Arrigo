using UnityEngine;

public class Placement : MonoBehaviour
{
    [SerializeField] LayerMask slots;

    Ray ray; 
    RaycastHit hit;
    [SerializeFiled] Mesh line;
    private void Awake()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    private void Update()
    {
        Place();
    }
    private void Place()
    {
        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit, Mathf.Infinity, slots))
        {
            Vector3 PlacePos = new(          
                hit.point.x,
                hit.point.y + 0.1f,
                hit.point.z
                );
            Instantiate(UIManager.Instance.selectedTur, PlacePos, Quaternion.identity, hit.transform);
        }
    }

    private void OnDrawGizmos()
    { 
        Physics.Raycast(ray, out hit, Mathf.Infinity, slots);
        Gizmos.color = Color.blue;
        //Gizmos.DrawWireMesh();
    }
}
