using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Construction : MonoBehaviour
{
    #region Variáveis de informação da construção
        
        [Header("Informações gerais")]
        [SerializeField] private string _name;
        [SerializeField] private List<string> _nicknames;

        [Header("Highlight na busca")]
        [SerializeField] private GameObject _highlightCube;

        protected CameraController _camera;
        
        //informações do objeto
        private Transform _contructionLocation;

        
        //Public Properties (para acesso das variaveis privadas)
        public string Name => _name;
        public List<string> Nicknames => _nicknames;
        public abstract BuildType BuildType { get; }

        #endregion
    // Start is called before the first frame update
    public virtual void Start()
    {
        _contructionLocation = gameObject.transform; 
        GetConstructionList();
    }


    public virtual void GetConstructionList(){}

    public virtual void HighlightConstruction()
    {
        if (_camera == null) _camera = FindObjectOfType<CameraController>();
        var newPos = transform.position;
        newPos -= _camera.transform.forward.normalized * _camera.selectDist;
        
        _camera.CmCamera.Follow.position = newPos;
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
    Banheiro,
    Sala_de_administracao,
    Sala_de_Manutencao
}
