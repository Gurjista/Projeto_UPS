using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Search
{
    public class ResultsController : MonoBehaviour
    {
        [SerializeField] private GameObject resultPrefab;
        [SerializeField] private GameObject contentArea;
        [SerializeField] private GameObject scrollView;
        public List<GameObject> _allResultsReferences = new List<GameObject>();
        //private GameObject[] _allResultsReferences;

        private void Awake()
        {
            var constructions = FindObjectsOfType<Construction>();
            //_allResultsReferences = new GameObject[constructions.Length];
            int i = 0;
            foreach (var construction in constructions)
            {
                var aux = Instantiate<GameObject>(resultPrefab, contentArea.transform);

                aux.gameObject.name = construction.Name;
                aux.GetComponentInChildren<TextMeshProUGUI>().text = construction.Name + " - (" + construction.BuildType + ")";
                aux.GetComponent<Button>().onClick.AddListener(construction.HighlightConstruction);
                aux.GetComponent<Button>().onClick.AddListener(CloseScroll);

                
                
                _allResultsReferences.Add(aux);
                //_allResultsReferences[i] = aux;
                i++;

                if (construction.Nicknames != null)
                {
                    foreach (string nickname in construction.Nicknames)
                    {
                        var auxNicknames = Instantiate<GameObject>(resultPrefab, contentArea.transform);
                        auxNicknames.gameObject.name = nickname;
                        auxNicknames.GetComponentInChildren<TextMeshProUGUI>().text = nickname + " - " + construction.Name + " - (" + construction.BuildType + ")";
                        auxNicknames.GetComponent<Button>().onClick.AddListener(construction.HighlightConstruction);
                        auxNicknames.GetComponent<Button>().onClick.AddListener(CloseScroll);
                        _allResultsReferences.Add(auxNicknames);
                    }
                }

                

            }
        }


        #region Callbacks
        
        
        public void OnTypedCharacter(string value)
        {
            scrollView.SetActive(value.Length > 0);
            if(value.Length == 0) return;

            var search = _allResultsReferences.Where(x => x.name.ToLower().Contains(value.ToLower()));
            //StartsWith(value, true, CultureInfo.CurrentCulture));
            int num = 0;
            foreach (var result in _allResultsReferences)
            {
                if (search.Contains(result))
                {
                    result.gameObject.SetActive(true);
                    num++;
                }
                else
                {
                    result.gameObject.SetActive(false);
                }
            }
            
            contentArea.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 150 * num);
        }

        public void OnSelectSearch(string value)
        {
            if(value.Length > 0)
                scrollView.SetActive(true);
        }

        public void OnDeselectSearch(string value)
        {
            if (value.Length <= 0)
                scrollView.SetActive(false);
            
        }

        private void CloseScroll()
        {
            scrollView.SetActive(false);
        }
        #endregion
        
    }
}