using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cmCamera;
    [SerializeField] private Camera camera;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float zoomSens;
    [SerializeField] private float rotateSens;
    [SerializeField] private Vector3 maxDistance;
    [SerializeField] private Vector3 minDistance;
    [SerializeField] private Transform _startingPoint;
    
    [Header("O plano que contem o mapa")]
    public Transform mainPlane;
    private bool _updateTouch = true;
    private Touch[] _lastT;
    private Vector3 _center;
    private Vector3 _movePos;
    private float _rotAngle;
    private GraphicRaycaster GR;
    private EventSystem ES;

    public float selectDist;
    public CinemachineVirtualCamera CmCamera => cmCamera;
    private void Awake()
    {
        //transform.LookAt(cmCamera.Follow);
        _lastT = new Touch[5];
        GR = GameObject.Find("MainCanvas").GetComponent<GraphicRaycaster>();
        ES = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    void Update()
    {
        var t = Input.touches;

        foreach (var touch in t)
        {
            _updateTouch = true;
            if (_lastT.Any(x => x.Equals(touch)))
                _updateTouch = false;
        }

        t.CopyTo(_lastT, 0);
        if (Input.touchCount >= 1)
        {
            if (_updateTouch)
            {
                var p = new PointerEventData(ES);
                p.position = Input.GetTouch(0).position;
                var r = new List<RaycastResult>();
                GR.Raycast(p, r);
                if(r.Count > 0) return;
                _updateTouch = false;
                if (Input.touchCount == 1)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        var tPos = Input.GetTouch(0).position;
                        var lastPos = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;

                        var r1 = camera.ScreenPointToRay(lastPos);
                        var r2 = camera.ScreenPointToRay(tPos);
                        Physics.Raycast(r1, out var h1, 1000, groundLayer);
                        Physics.Raycast(r2, out var h2, 1000, groundLayer);
                        var dist = h2.point - h1.point;
                        _center = h2.point;

                        cmCamera.Follow.Translate(-dist, Space.World);
                    }


                }

                if (Input.touchCount == 2)
                {
                    var tPos0 = Input.GetTouch(0).position;
                    var tPos1 = Input.GetTouch(1).position;
                    var lastPos0 = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;
                    var lastPos1 = Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition;

                    Vector3 movement = Vector3.zero;
                    if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
                    {
                        //Finger 1
                        var r01 = camera.ScreenPointToRay(lastPos0);
                        Physics.Raycast(r01, out var h01, 1000, groundLayer);
                        var r02 = camera.ScreenPointToRay(tPos0);
                        Physics.Raycast(r02, out var h02, 1000, groundLayer);
                        Debug.DrawRay(transform.position, r01.direction * 1000, Color.green);
                        var dist0 = h02.point - h01.point;

                        //Finger 2
                        var r11 = camera.ScreenPointToRay(lastPos1);
                        Physics.Raycast(r11, out var h11, 1000, groundLayer);
                        var r12 = camera.ScreenPointToRay(tPos1);
                        Physics.Raycast(r12, out var h12, 1000, groundLayer);
                        Debug.DrawRay(transform.position, r11.direction * 1000, Color.green);
                        var dist1 = h12.point - h11.point;

                        var lastCenter = Vector3.Lerp(h01.point, h11.point, 0.5f);
                        _center = Vector3.Lerp(h02.point, h12.point, 0.5f);
                        Debug.DrawLine(transform.position, _center, Color.black);

                        var centerDir = _center - cmCamera.transform.position;
                        var lastCenterDir = lastCenter - cmCamera.transform.position;

                        //Zoom
                        var zoom = (h12.point - h02.point).magnitude - (h11.point - h01.point).magnitude;
                        movement += centerDir.normalized * (zoom * zoomSens);

                        //Moving with two fingers
                        //movement += lastCenter - center;


                        //Rotating
                        var angle = Vector3.SignedAngle(h01.point - _center, h02.point - _center, Vector3.up) +
                                    Vector3.SignedAngle(h11.point - _center, h12.point - _center, Vector3.up);
                        var dir = mainPlane.position - _center;
                        var rot = Quaternion.AngleAxis(angle, Vector3.up) * dir;
                        rot -= dir;
                        _rotAngle += angle;
                        //movement += rot;


                        cmCamera.Follow.Translate(movement, Space.World);
                    }
                }
            }
        }
        if (_rotAngle != 0)
        {
            var r = Mathf.Lerp(0, _rotAngle, rotateSens * Time.deltaTime);
            mainPlane.transform.RotateAround(_center, Vector3.up, r);
            _rotAngle -= r;
        }

        cmCamera.Follow.position = new Vector3(Mathf.Clamp(cmCamera.Follow.position.x, -minDistance.x, maxDistance.x), 
            Mathf.Clamp(cmCamera.Follow.position.y, minDistance.y, maxDistance.y),
            Mathf.Clamp(cmCamera.Follow.position.z, -minDistance.z, maxDistance.z));
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "limit"){
            var newPos = _startingPoint.transform.position;
            newPos -= this.transform.forward.normalized * this.selectDist;
        
            this.CmCamera.Follow.position = newPos;
            Debug.Log("check");
        }
    }
}
