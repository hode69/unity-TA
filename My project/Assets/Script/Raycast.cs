using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;

public class Raycast : MonoBehaviour
{
    [SerializeField] public string selectableTag = "Selectable";
    [SerializeField] public string opsiselect = "Opsi";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material hijau;

    public Transform selection, cameraTarget, dump, cam2, Gyro, Option, tembak;
    private Transform _selection, _option;
    public Color warnaAwal;
    public TextMeshProUGUI txt, timeTXT;

    public GameObject CPU, FAN, RAM1,RAM2, PSU, HDD, MOBOCASE, VGA, CASE, GOParent, GrabParent;
    public GameObject KFan, KFP, K24, K12, K8, KPowSata, KSATA;
    public GameObject KFanPasang, KFPPasang, K24Pasang, K12Pasang, K8Pasang, KPowSataPasang, KSATAPasang;
    public GameObject OP1, OP2, OP3, OP4, OP5, OP6;
    public GameObject SELECT, PLACE, Prompt, Hasil, panel1, panel2, panel3, panel4, WorldUI, ButtonUI;

    Vector3 positionAwal, posDone, posCam2;
    Quaternion rotationAwal, rotation1, rotation2, rotation3;

    Transform selectedObj, pasangan, opPasang;

    public Collider CaseCollider;

    public int kesalahan;
    float currentTime = 0f;
    bool startTimer;

    void Awake()
    {
        Time.timeScale = 1f;
        posDone = dump.position;
        posCam2 = cam2.position;
        startTimer = false;
        rotation1 = Quaternion.Euler(0,0,180);
        rotation2 = Quaternion.Euler(0,180,0);
        rotation3 = Quaternion.Euler(-180,0,0);
    }

    void Update()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;

            _selection = null;
        }

        if (_option != null)
        {
            var opsiRenderer = _option.GetComponent<Image>();
            opsiRenderer.color = warnaAwal;

            _option = null;
        }
        
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1));
        RaycastHit hit;

        //objek raycast
        if (Physics.Raycast(ray,out hit, Mathf.Infinity))
        {
            selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                }
            }
            else
            {
                selection = null;
            }
            _selection = selection;
        }
        else
        {
            selection = null;
        }

        //opsi raycast
        if (Physics.Raycast(ray,out hit, Mathf.Infinity))
        {
            // Debug.Log(hit.collider.gameObject.name + " Hit Something");
            Option = hit.transform;

            if (Option.CompareTag(opsiselect))
            {
                var opsiRenderer = Option.GetComponent<Image>();
                if (opsiRenderer != null)
                {
                    opsiRenderer.color = new Color32(255,0,0,100);
                }
            }
            else
            {
                Option = null;
            }
 
            _option = Option;
        }
        else
        {
            Option = null;
        }

        //tembak raycast
        if (Physics.Raycast(ray,out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag(selectableTag))
            {
                tembak = hit.transform;
            }
            else
            {
                tembak = null;
            }
        }
        else
        {
            tembak = null;
        }
        ObjekDitangan();

        //Case Tag
        if(MOBOCASE.activeSelf && PSU.activeSelf && HDD.activeSelf)
        {
            CASE.transform.gameObject.tag = "TidakSelect";
            int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
            CaseCollider.enabled = false;
            Gyro.position = posCam2;
            WorldUI.SetActive(true);

            //kabel tag
            K12.transform.gameObject.tag = "Selectable";
            K24.transform.gameObject.tag = "Selectable";
            K8.transform.gameObject.tag = "Selectable";
            KFP.transform.gameObject.tag = "Selectable";
            KPowSata.transform.gameObject.tag = "Selectable";
            KSATA.transform.gameObject.tag = "Selectable";
        }
            
        if(startTimer == true)
        {
            currentTime += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            timeTXT.text = time.ToString(@"mm\:ss\:fff");
        }
        Summary();
    }

    public void AmbilObject()
    {      
        //mengambil 
        if (selectedObj == null && selection != null && selection.name != "CASE" && selection.name != "MOBO CASE")
        {
            startTimer = true;
            positionAwal = selection.position;
            rotationAwal = selection.gameObject.transform.rotation;
            selectedObj = selection;


            SELECT.SetActive(false);
            PLACE.SetActive(true);
        }
    }

    public void ObjekDitangan() 
    {
        if(selectedObj != null && selectedObj.name != "CASE" && selectedObj.name != "MOBO CASE") 
        {
            if(selectedObj.name == "24" || selectedObj.name == "12" || selectedObj.name == "8" || selectedObj.name == "SATAPOWER" || selectedObj.name == "KabelFAN" || selectedObj.name == "KabelFP" || selectedObj.name == "SATA")
            {
                var selectkabel = selectedObj.GetComponent<Renderer>();
                selectkabel.material = hijau;
            }
            else
            {
                //selectedObj.position = cameraTarget.position;
                selectedObj.parent = GrabParent.transform;
                selectedObj.localPosition = new Vector3(0f, 0f, 0f);
                
                if(selectedObj.name == "PSU" || selectedObj.name == "VGA")
                {
                    selectedObj.localRotation= rotation1;
                }

                if(selectedObj.name == "MOBO")
                {
                    selectedObj.localRotation= rotation2;
                }

                if(selectedObj.name == "HDD" || selectedObj.name == "CPU" || selectedObj.name == "HSF")
                {
                    selectedObj.localRotation= rotation3;
                }
                
            }
        }
        else {}
    }

    public void Lepas()
    {
        if(selectedObj != null)
        {
            var selectkabel = selectedObj.GetComponent<Renderer>();
            selectkabel.material = defaultMaterial;

            selectedObj.parent = GOParent.transform;
            selectedObj.position = positionAwal;
            selectedObj.rotation = rotationAwal;
            selectedObj = null;

            if(pasangan != null || opPasang != null)
            {
                pasangan = null;
                opPasang = null;
            }
            Switcher();
        }
        
    }

    public void Switcher()
    {
        SELECT.SetActive(true);
        PLACE.SetActive(false);
    }

    public void PasangObjek()
    {
        if(selectedObj != null && pasangan == null && tembak !=null)
        {
             pasangan = tembak;
             opPasang = tembak;
        }

        if(selectedObj != null && opPasang == null && Option != null)
        {
            pasangan = Option;
            opPasang = Option;
        }

        if(selectedObj != null && (pasangan != null || opPasang != null )) 
        {
            //CPU-MOBO
            if(selectedObj.name == "CPU" && pasangan.name == "MOBO")
            {
                selectedObj.position = posDone;
                CPU.SetActive(true);  
                selectedObj = null;      
                pasangan = null;    
                opPasang = null;
                Switcher();
                return;
            }

            //FAN-MOBO
            if(selectedObj.name == "HSF" && pasangan.name == "MOBO")
            {
                if(CPU.activeSelf)
                {
                    selectedObj.position = posDone;
                    FAN.SetActive(true);  
                    selectedObj = null;
                    pasangan = null; 
                    opPasang = null;
                    Switcher();
                    return;
                }
            }

            //RAM1-MOBO
            if(selectedObj.name == "RAM1" && pasangan.name == "MOBO")
            {
                selectedObj.position = posDone;
                RAM1.SetActive(true);
                selectedObj = null;
                pasangan = null; 
                opPasang = null;
                Switcher();
                return;
            }

            //RAM2-MOBO
            if(selectedObj.name == "RAM2" && pasangan.name == "MOBO")
            {
                selectedObj.position = posDone;
                RAM2.SetActive(true);
                selectedObj = null;
                pasangan = null; 
                opPasang = null;
                Switcher();
                return;
            }

            //PSU-CASE
            if(selectedObj.name == "PSU" && pasangan.name == "CASE")
            {
                selectedObj.position = posDone;
                PSU.SetActive(true);
                selectedObj = null;
                pasangan = null; 
                opPasang = null;
                Switcher();
                return;
            }

            //HDD-CASE
            if(selectedObj.name == "HDD" && pasangan.name == "CASE")
            {
                selectedObj.position = posDone;
                HDD.SetActive(true);
                selectedObj = null;
                pasangan = null; 
                opPasang = null;
                Switcher();
                return;
            }

            //MOBO-CASE
            if(selectedObj.name == "MOBO" && pasangan.name == "CASE")
            {
                if(RAM1.activeSelf && RAM2.activeSelf && CPU.activeSelf && FAN.activeSelf)
                {
                    selectedObj.position = posDone;
                    MOBOCASE.SetActive(true);
                    selectedObj = null;
                    pasangan = null; 
                    opPasang = null;
                    Switcher();
                    return;
                }
            }

            //VGA-MOBO
            if(selectedObj.name == "VGA" && pasangan.name == "MOBO CASE")
            {
                selectedObj.position = posDone;
                VGA.SetActive(true);
                MOBOCASE.transform.gameObject.tag = "TidakSelect";
                OP5.SetActive(true);
                selectedObj = null;
                pasangan = null; 
                opPasang = null;
                Switcher();
                return;
            }

            //KabelFP-MOBO
            if(selectedObj.name == "KabelFP" && opPasang.name == "opsi6")
            {
                KFP.SetActive(false);
                KFPPasang.SetActive(true);
                OP6.SetActive(false);
                selectedObj = null;
                opPasang = null;
                pasangan = null; 
                Switcher();
                return;
            }

            //Kabel24-MOBO
            if(selectedObj.name == "24" && opPasang.name == "opsi2")
            {
                K24.SetActive(false);
                K24Pasang.SetActive(true);
                OP2.SetActive(false);
                selectedObj = null;
                opPasang = null;
                pasangan = null; 
                Switcher();
                return;
            }

            //Kabel8-MOBO
            if(selectedObj.name == "8" && opPasang.name == "opsi1")
            {
                K8.SetActive(false);
                K8Pasang.SetActive(true);
                OP1.SetActive(false);
                selectedObj = null;
                opPasang = null;
                pasangan = null; 
                Switcher();
                return;
            }

            //Kabel12-VGA
            if(selectedObj.name == "12" && opPasang.name == "opsi5")
            {
                K12.SetActive(false);
                K12Pasang.SetActive(true);
                OP5.SetActive(false);
                selectedObj = null;
                opPasang = null;
                pasangan = null; 
                Switcher();
                return;
            }

            //KabelPowSata-HDD
            if(selectedObj.name == "SATAPOWER" && opPasang.name == "opsi3")
            {
                KPowSata.SetActive(false);
                KPowSataPasang.SetActive(true);
                OP3.SetActive(false);
                selectedObj = null;
                opPasang = null;
                pasangan = null; 
                Switcher();
                return;
            }

            //KabelSATA-HDD
            if(selectedObj.name == "SATA" && (opPasang.name == "opsi4" || opPasang.name == "opsi3"))
            {
                KSATA.SetActive(false);
                KSATAPasang.SetActive(true);
                OP4.SetActive(false);
                selectedObj = null;
                opPasang = null;
                pasangan = null;
                Switcher();
                return;
            }

            Debug.Log("salah");
            StartCoroutine ("PromptSalah");
            kesalahan++;
            opPasang = null;
            pasangan = null;
        }
    }

    IEnumerator PromptSalah()
    {
            Prompt.SetActive(true);
            yield return new WaitForSeconds (2f);
            Prompt.SetActive(false);
    }

    //summarry
    public void Summary()
    {
        if(K12Pasang.activeSelf && K24Pasang.activeSelf && K8Pasang.activeSelf && KFPPasang.activeSelf && KSATAPasang.activeSelf && KPowSataPasang.activeSelf)
        {
            Time.timeScale = 0f;
            ButtonUI.SetActive(false);
            Hasil.SetActive(true);
            txt.text = "Total Waktu : " + timeTXT.text +"\nTotal Kesalahan : " + kesalahan;
        }
    }

    public void selesaiSimulasi()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Ulang()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

}
