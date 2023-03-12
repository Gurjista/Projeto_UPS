using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : Construction
{
    [Header("objeto a ficar transparente")]
    [SerializeField] private GameObject _upperConstruction;
    [SerializeField] private Material _upperTranparentMaterial;

    private MeshRenderer _meshRenderer;
    
    public override void Start() {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
