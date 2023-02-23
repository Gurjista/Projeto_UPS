using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class NewMovementCamera : MonoBehaviour
{

    [SerializeField] public Transform Pos_obj;
    [SerializeField] private Camera Camera;
    [SerializeField] CinemachineVirtualCamera vcam;
    [SerializeField] private Transform Camera_support;
    [SerializeField] private float Min_zoom;
    [SerializeField] private float Max_zoom;
    [SerializeField] private float GO_Altura;
    [SerializeField] private float Go_offset;
    [SerializeField] private float _GOspeed;
    [SerializeField] private int Min_x;
    [SerializeField] private int Max_x;
    [SerializeField] private int Min_z;
    [SerializeField] private int Max_z;


    protected Plane Plane;


    private void Awake()
    {
        if (Camera == null)
            Camera = Camera.main;
        
    }

    private void Update()
    {

        
        // Delimitar a translacao
        if (Camera.transform.position.x > Max_x) Camera.transform.position = new Vector3(Max_x, Camera.transform.position.y, Camera.transform.position.z);
        if (Camera.transform.position.x < Min_x) Camera.transform.position = new Vector3(Min_x, Camera.transform.position.y, Camera.transform.position.z);
        if (Camera.transform.position.z > Max_z) Camera.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, Max_z);
        if (Camera.transform.position.z < Min_z) Camera.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, Min_z);

        //Update Plane
        if (Input.touchCount >= 1)
            Plane.SetNormalAndPosition(transform.up, transform.position);

        var Delta1 = Vector3.zero;
        var Delta2 = Vector3.zero;

        //Move
        if (Input.touchCount >= 1)
        {
            Delta1 = PlanePositionDelta(Input.GetTouch(0));
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                Camera.transform.Translate(Delta1, Space.World);
        }

        //Pinch
        if (Input.touchCount >= 2)
        {
            var pos1 = PlanePosition(Input.GetTouch(0).position);
            var pos2 = PlanePosition(Input.GetTouch(1).position);
            var pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
            var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

            //calc zoom
            var zoom = Vector3.Distance(pos1, pos2) /
                       Vector3.Distance(pos1b, pos2b);

            //caso de quinas
            if (zoom == 0 || zoom > 10)
                return;

            //mover cam a metade do ray, caso onde esteja entre o max e min
            if (Camera.transform.position.y > Min_zoom && Camera.transform.position.y < Max_zoom)
            {
                Camera.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);
            }

            // Caso a camera esteja abaixo do zoom minimo, permitindo so afastar o zoom
            if(Camera.transform.position.y < Min_zoom && zoom < 1)
            {
                Camera.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);
            }
            // Caso a camera esteja acmida do zoom max, permitindo so aprox o zoom
            if (Camera.transform.position.y > Max_zoom && zoom > 1)
            {
                Camera.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);
            }

            if (pos2b != pos2)
            {
                 Camera_support.transform.RotateAround(pos1, Plane.normal, Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, Plane.normal));
            }
        }
    }




    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //nao esta se movendo
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        //delta
        var rayBefore = Camera.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = Camera.ScreenPointToRay(touch.position);
        if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        //nao esta no plano
        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //posicao
        var rayNow = Camera.ScreenPointToRay(screenPos);
        if (Plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }

}