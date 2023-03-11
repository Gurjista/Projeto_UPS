using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ProjectPanel : MonoBehaviour
{
    [SerializeField] private string _projectRoom;
    [SerializeField] private Button _mapButton;
    // Start is called before the first frame update
    void Start()
    {
        var project = FindObjectsOfType<ProjectRoom>();
        var aux = project.First(x => x.Name == _projectRoom);
        _mapButton.onClick.AddListener(aux.HighlightConstruction);
        Debug.Log("Nome aux: " + aux.name);
    }

   
}
