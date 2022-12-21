using UnityEngine;
using TMPro;

public class Raycast : MonoBehaviour
{
    [SerializeField] public string selectableTag = "Selectable";
    [SerializeField] public string UnTag = "TidakSelect";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material hijau;

    public Transform selection, cameraTarget, Dump, cam2, Gyro;
    private Transform _selection;
    public TextMeshProUGUI txt; 

    public GameObject CPU, FAN, RAM1,RAM2, PSU, HDD, MOBOCASE, MOBOCLONE, VGA, CASE, CASECLONE;

    public GameObject KFan, KFP, K24, K12, K8, KPowSata, KSATA;

    public GameObject KFanPasang, KFPPasang, K24Pasang, K12Pasang, K8Pasang, KPowSataPasang, KSATAPasang;

    Vector3 positionAwal, posDone, posCam2;

    Transform selectedObj;

    public Collider CaseCollider;

    void Start()
    {
        // txt = GetComponent<TextMeshProUGUI>();
        posDone = Dump.position;
        posCam2 = cam2.position;
    }

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
            // Debug.Log(hit.collider.gameObject.name + " Hit Something");
            selection = hit.transform;

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
        else
        {
            selection = null;
        }

        ObjekDitangan();

        Debug.Log(MOBOCASE.activeSelf);
        Debug.Log(PSU.activeSelf);
        Debug.Log(HDD.activeSelf);
        if(MOBOCASE.activeSelf && PSU.activeSelf && HDD.activeSelf)
        {
            CASE.transform.gameObject.tag = "TidakSelect";
            int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
            CaseCollider.enabled = false;
            Gyro.position = posCam2;
        }
    }

    public void AmbilObject()
    {       
        if (selectedObj == null && selection != null)
        {
            positionAwal = selection.position;
            selectedObj = selection;
        }
    }

    public void ObjekDitangan() 
    {
        if(selectedObj != null && selectedObj.name != "CASE" && selectedObj.name != "MOBO CASE") 
        {
            if(selectedObj.name == "24" || selectedObj.name == "12" || selectedObj.name == "8" || selectedObj.name == "SATAPOWER" || selectedObj.name == "KabelFAN" || selectedObj.name == "KabelFP")
            {
                var selectkabel = selectedObj.GetComponent<Renderer>();
                selectkabel.material = hijau;
            }
            else
            {
                selectedObj.position = cameraTarget.position;
            }
            
            // txt.text = "Pasang";
        }
        else {
            // txt.text = "Ambil";
        }
    }

    public void Lepas()
    {
        var selectkabel = selectedObj.GetComponent<Renderer>();
        selectedObj.position = positionAwal;
        selectkabel.material = defaultMaterial;
        selectedObj = null;
        
    }

    public void PasangObjek()
    {
        if(selectedObj != null && selection != null) 
        {
            //CPU-MOBO
            if(selectedObj.name == "CPU" && selection.name == "MOBO")
            {
                selectedObj.position = posDone;
                CPU.SetActive(true);  
                selectedObj = null;          
            }
            else
            {
                Debug.Log("salah");
            }

            //FAN-MOBO
            if(selectedObj.name == "HSF" && selection.name == "MOBO")
            {
                if(CPU.activeSelf)
                {
                    selectedObj.position = posDone;
                    FAN.SetActive(true);  
                    selectedObj = null;
                }
                else
                {
                    Debug.Log("salah");
                }
            }
            else
            {
                Debug.Log("salah");
            }

            //RAM1-MOBO
            if(selectedObj.name == "RAM1" && selection.name == "MOBO")
            {
                selectedObj.position = posDone;
                RAM1.SetActive(true);
                selectedObj = null;
            }
            else
            {

            }

            //RAM2-MOBO
            if(selectedObj.name == "RAM2" && selection.name == "MOBO")
            {
                selectedObj.position = posDone;
                RAM2.SetActive(true);
                selectedObj = null;
            }
            else
            {
                
            }

            //PSU-CASE
            if(selectedObj.name == "PSU" && selection.name == "CASE")
            {
                selectedObj.position = posDone;
                PSU.SetActive(true);
                selectedObj = null;
            }
            else
            {
                
            }

            //HDD-CASE
            if(selectedObj.name == "HDD" && selection.name == "CASE")
            {
                selectedObj.position = posDone;
                HDD.SetActive(true);
                selectedObj = null;
            }
            else
            {
                
            }

            //MOBO-CASE
            if(selectedObj.name == "MOBO" && selection.name == "CASE")
            {
                if(RAM1.activeSelf && RAM2.activeSelf && CPU.activeSelf && FAN.activeSelf)
                {
                    selectedObj.position = posDone;
                    MOBOCASE.SetActive(true);
                    selectedObj = null;
                }
            }
            else
            {
                
            }

            //VGA-MOBO
            if(selectedObj.name == "VGA" && selection.name == "MOBO CASE")
            {
                selectedObj.position = posDone;
                VGA.SetActive(true);
                MOBOCASE.transform.gameObject.tag = "TidakSelect";
                selectedObj = null;
            }
            else
            {
                
            }

            //KabelFAN-MOBO
            if(selectedObj.name == "KabelFAN" && selection.name == "")
            {

            }
            else
            {
                
            }

            //KabelFP-MOBO
            if(selectedObj.name == "" && selection.name == "")
            {

            }
            else
            {
                
            }

            //Kabel24-MOBO
            if(selectedObj.name == "" && selection.name == "")
            {

            }
            else
            {
                
            }

            //Kabel8-MOBO
            if(selectedObj.name == "" && selection.name == "")
            {

            }
            else
            {
                
            }

            //Kabel12-VGA
            if(selectedObj.name == "" && selection.name == "")
            {

            }
            else
            {
                
            }

            //KabelPowSata-HDD
            if(selectedObj.name == "" && selection.name == "")
            {

            }
            else
            {
                
            }

            //KabelSATA-HDD
            if(selectedObj.name == "" && selection.name == "")
            {

            }
            else
            {
                
            }
        }
    }

    
}
