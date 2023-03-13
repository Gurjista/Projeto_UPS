using System;
using System.Linq;
using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Search
{
    //Script para controlar a search engine, sera utilizado um Singleton pro enquanto. 
    public class SearchEngine : MonoBehaviour
    {
        #region Fields and Properties
        
        #endregion
        
        
        private void Awake()
        {
            var allBuildings = FindObjectsOfType<Construction>();
            
        }


        #region Callbacks
        // public void OnTypedCharacter(string value)
        // {
        //     
        // }
        #endregion
        
    }
}