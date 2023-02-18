using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Construction : MonoBehaviour
{
    #region Variáveis de informação da construção
        
        [Header("Informações gerais")]
        [SerializeField] private string _name;
        [SerializeField] private List<string> _nicknames;
        [Header("Informações do objeto")]
        [SerializeField] private Mesh _buildingModel;
        [SerializeField] private Transform _contructionLocation;

        
        //Public Properties (para acesso das variaveis privadas)
        public string Name => _name;
        public List<string> Nicknames => _nicknames;
        public abstract BuildType BuildType { get; }

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
        Debug.Log("position: " + transform.position + '\n' + "Nome: " + Name);
    }
}

public enum BuildType
{
    Bloco,
    Predio,
    Sala_de_aula,
    Laboratorio,
    Sala_de_professor,
    Sala_de_projeto,
}
