using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private Transform _selection;

    void Update()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }
        
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1));

        RaycastHit hit;
        if (Physics.Raycast(ray,out hit, Mathf.Infinity))
        {
            Debug.Log(hit.collider.gameObject.name + " Hit Something");
            var selection = hit.transform;

            if (selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                }
            }

            _selection = selection;
        }
    }
}
