using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectController : MonoBehaviour
{
    public Transform cameraT;
    public Transform objSelected;
    public Action deselectCallback = null;
    public int maxDist;
    public Button x;
    public Button copy_email;
    public String ProfessorEmail;

    private GraphicRaycaster GR;
    private EventSystem ES;
    private bool near;
    private bool _xPressed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        ProfessorEmail = null;
        x.gameObject.SetActive(false);
        copy_email.gameObject.SetActive(false);
        GR = GameObject.Find("MainCanvas").GetComponent<GraphicRaycaster>();
        ES = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        //cameraT = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(deselectCallback == null) return;
        if (!near)
        {
            if ((cameraT.position - objSelected.position).magnitude < maxDist)
            {
                near = true;
            }
            return;
        }
        if(x.gameObject.activeSelf == false) x.gameObject.SetActive(true); 
        if(copy_email.gameObject.activeSelf == false && ProfessorEmail != null) copy_email.gameObject.SetActive(true);

        var r = new List<RaycastResult>();
        if (Input.touchCount > 0)
        {
            var p = new PointerEventData(ES);
            p.position = Input.GetTouch(0).position;
            GR.Raycast(p, r);
        }

        foreach (var item in r)
        {
            if (item.gameObject.name == "StopSelectionButton")
            {
                _xPressed = true;
            }
        }

        if (_xPressed || (cameraT.position - objSelected.position).magnitude > maxDist)
        {
            ProfessorEmail = null;
            deselectCallback?.Invoke();
            deselectCallback = null;
            //x.gameObject.SetActive(false); 
            near = false;
            _xPressed = false;
        }
    }
}
