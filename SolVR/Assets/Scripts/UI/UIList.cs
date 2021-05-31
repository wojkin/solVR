using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class UIList : MonoBehaviour
    {
        [SerializeField]
        private GameObject template;
        [SerializeField]
        private List<ScriptableObject> list;
        
        void InitializeListElements()
        {
            foreach (var data in list)
            {
                var listEntry = Instantiate(template, gameObject.transform);
                listEntry.SetActive(true);
                var listElement = listEntry.GetComponent<IUIListElement>();
                listElement.Populate(data);
            }
        }

        private void Awake()
        {
            InitializeListElements();
        }
    }
}
