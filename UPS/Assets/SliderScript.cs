using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{

    [SerializeField] private Slider _slider;
    [SerializeField] private Camera Camera;

    private float Pos_init;
    private float Pos_Set;
    private float rotation;

    // Start is called before the first frame update
    void Start()
    {
        
        _slider.onValueChanged.AddListener((x) =>
        {
            RotationCamera(x);

        });
    }
    // fazer com que os valores do slider va de 0 a 1, 
    private void RotationCamera(float x)
    {
        Pos_init = Camera.transform.localEulerAngles.x;

        Pos_Set = x;

        rotation = Pos_Set - Pos_init;

        Camera.transform.Rotate(rotation, 0, 0);

       
    }
}