using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Dropdown
{
    /// <summary>
    /// Class handling dropdown that corresponds to an enum.
    /// </summary>
    /// <typeparam name="TEnum">An enum that is a base for a dropdown.</typeparam>
    [RequireComponent(typeof(TMP_Dropdown))]
    public abstract class EnumDropdown<TEnum> : MonoBehaviour where TEnum : Enum
    {
        protected TMP_Dropdown dropdown; // a dropdown that will be based on enum values
    
        [Tooltip("Event that invokes on value changed in dropdown.")]
        [SerializeField] protected UnityEvent<TEnum> onDropdownValueChanged;

        /// <summary>
        /// Initialize dropdown options.
        /// </summary>
        public void Awake()
        {
            dropdown = GetComponent<TMP_Dropdown>();
            PopulateDropdown();
            HandleDropdownChangedValue(dropdown.value); // initial selected dropdown value
        }

        /// <summary>
        /// Subscribes to event. 
        /// </summary>
        private void OnEnable()
        {
            dropdown.onValueChanged.AddListener(HandleDropdownChangedValue);
        }

        /// <summary>
        /// Populates dropdown with names of the enum values.
        /// </summary>
        protected virtual void PopulateDropdown()
        {
            var enumNames = Enum.GetNames(typeof(TEnum));
            dropdown.AddOptions(new List<string>(enumNames));
        }

        /// <summary>
        /// Handles changed value in dropdown.
        /// </summary>
        /// <param name="index">An index of selected value in dropdown.</param>
        private void HandleDropdownChangedValue(int index)
        {
            var enumValue = (TEnum)Enum.GetValues(typeof(TEnum)).GetValue(index);
            onDropdownValueChanged.Invoke(enumValue);
        }

        /// <summary>
        /// Unsubscribes from event. 
        /// </summary>
        public void OnDisable()
        {
            dropdown.onValueChanged.RemoveListener(HandleDropdownChangedValue);
        }
    }
}
