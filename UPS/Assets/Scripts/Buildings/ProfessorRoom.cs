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
        _selectController.ProfessorEmail = _professorEmail;
    }
}
