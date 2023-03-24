using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : Construction
{
    [SerializeField] protected Material transparentMaterial;
    //[SerializeField] private Material highlightMaterial;
    [SerializeField] protected MeshRenderer _upperConstruction;

    protected MeshRenderer _meshRenderer;
    
    protected SelectController _selectController;
    protected Material _previousMaterial;
    protected GameObject _searchField;
    protected GameObject _projectField;

    public override void Start() {
        base.Start();
        _searchField = GameObject.Find("Search Field");
        _projectField = GameObject.Find("Canvas");
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
        _searchField.SetActive(false);
        _projectField.SetActive(false);
        // _objMaterial = _meshRenderer.material;
        // _meshRenderer.material = highlightMaterial;
        _selectController = GameObject.Find("SelectedController").GetComponent<SelectController>();
        _selectController.objSelected = transform;
        _selectController.deselectCallback = Deselect;
    }

    public void Deselect()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        _upperConstruction.material = _previousMaterial;
        //_meshRenderer.material = _objMaterial;
        _searchField.SetActive(true);
        _projectField.SetActive(true);
    }
}
