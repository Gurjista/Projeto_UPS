using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Construction : MonoBehaviour
{
    #region Variáveis de informação da construção
        
        [Header("Informações gerais")]
        [SerializeField] private string _name;
        [SerializeField] private List<string> _nicknames;
        [Header("Infotmações do objeto")]
        [SerializeField] private Mesh _buildingModel;
        [SerializeField] private Transform _contructionLocation;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _contructionLocation = gameObject.transform; 
        if (_buildingModel != null)
        {
            GetComponent<MeshFilter>().mesh = _buildingModel;    
        }

        GetConstructionList();
    }


    public virtual void GetConstructionList(){}

    public void HighlightConstruction(){
        // Highlight Construction when is researched
    }
    }
