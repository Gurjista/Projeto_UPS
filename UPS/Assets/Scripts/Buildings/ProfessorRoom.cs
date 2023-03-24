using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorRoom : Room
{
    [Header("informações professor")]
    [SerializeField] private string _professorName;
    [SerializeField] private string _professorEmail;
    [SerializeField] private string _description;

    public override BuildType BuildType => BuildType.Sala_de_professor;

    // Update is called once per frame
    public override void Start() {
        base.Start();
        _searchField = GameObject.Find("Search Field");
        _projectField = GameObject.Find("Canvas");
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;
        _previousMaterial = _upperConstruction.material;
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public override void HighlightConstruction()
    {
        base.HighlightConstruction();
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        _searchField.SetActive(false);
        _projectField.SetActive(false);
        _upperConstruction.material = transparentMaterial;
        // _objMaterial = _meshRenderer.material;
        // _meshRenderer.material = highlightMaterial;
        _selectController = GameObject.Find("SelectedController").GetComponent<SelectController>();
        _selectController.objSelected = transform;
        _selectController.deselectCallback = Deselect;
        _selectController.ProfessorEmail = _professorEmail;
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
