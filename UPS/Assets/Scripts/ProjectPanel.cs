using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProjectPanel : MonoBehaviour
{
    [SerializeField] private string _projectRoom;
    [SerializeField] private GameObject _mapButton;
    // Start is called before the first frame update
    void Start()
    {
        var project = FindObjectOfType<ProjectRoom>();
        var aux = project.First(x => x.name == _projectRoom);
        aux.GetComponent<Button>().onClick.AddListener(project.HighlightConstruction);
    }

   
}
