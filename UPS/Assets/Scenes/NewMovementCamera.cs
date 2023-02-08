using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovementCamera : MonoBehaviour
{

    [SerializeField] private Camera Camera;
    [SerializeField] private Transform Camera_support;

    protected Plane Plane;
    private Vector2 StartTouchPos0;
    private Vector2 EndTouchPos0;
    private Vector2 StartTouchPos1;
    private Vector2 EndTouchPos1;

    private void Awake()
    {
        if (Camera == null)
            Camera = Camera.main;
    }

    private void Update()
    {

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

            //mover cam a metade do ray
            Camera.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);

            if (pos2b != pos2)
                Camera.transform.RotateAround(pos1, Plane.normal, Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, Plane.normal));
        }

        //Angle
        if (Input.touchCount >= 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if(touch0.phase == TouchPhase.Began)
            {
                StartTouchPos0 = touch0.position;
            }

            else if(touch0.phase == TouchPhase.Moved || touch0.phase == TouchPhase.Ended)
            {
                EndTouchPos0 = touch0.position;
            }

            if (touch1.phase == TouchPhase.Began)
            {
                StartTouchPos1 = touch1.position;
            }

            else if (touch1.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Ended)
            {
                EndTouchPos1 = touch1.position;
            }

            
            // Dedos indo para cima, camera para baixo
            if(EndTouchPos0.y > StartTouchPos0.y && EndTouchPos1.y > StartTouchPos1.y)
            {
                if (Camera.transform.rotation.x > -20)
                {
                    Camera_support.transform.Rotate(new Vector3(-1, 0, 0));// Muda o suporte da camera em vez da camera em si
                }
            }

            // Dedos indo para baixo, camera para cima
            if (EndTouchPos0.y < StartTouchPos0.y && EndTouchPos1.y < StartTouchPos1.y)
            {

                if (Camera.transform.rotation.y < 20)
                {
                    Camera_support.transform.Rotate(new Vector3(1, 0, 0));// Muda o suporte da camera em vez da camera em si
                }
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }
}