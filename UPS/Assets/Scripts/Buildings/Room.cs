using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : Construction
{
    [SerializeField] private Material transparentMaterial;
    //[SerializeField] private Material highlightMaterial;
    [SerializeField] private MeshRenderer _upperConstruction;

    private MeshRenderer _meshRenderer;
    
    private SelectController _selectController;
    private Material _previousMaterial;

    public override void Start() {
        base.Start();
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;
        
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void HighlightConstruction()
    {
        base.HighlightConstruction();
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        _previousMaterial = _upperConstruction.material;
        _upperConstruction.material = transparentMaterial;
        // _objMaterial = _meshRenderer.material;
        // _meshRenderer.material = highlightMaterial;
        _selectController = GameObject.Find("SelectedController").GetComponent<SelectController>();
        _selectController.objSelected = transform;
        _selectController.deselectCallback = Deselect;
    }

    private void Deselect()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        _upperConstruction.material = _previousMaterial;
        //_meshRenderer.material = _objMaterial;
    }
}
