using System;
using UnityEngine;

namespace UnityAutoAssign
{
    /// <summary>
    /// [NEW v3.0] Attribute to auto-assign UI system components.
    /// Simplifies obtaining Canvas, LayoutGroup, etc.
    /// </summary>
    /// <example>
    /// [AutoAssignUI(UIComponentType.Canvas)]
    /// [SerializeField] private Canvas mainCanvas;
    /// 
    /// [AutoAssignUI(UIComponentType.Button, searchInChildren: true)]
    /// [SerializeField] private Button submitButton;
    /// </example>
    [AttributeUsage(AttributeTargets.Field)]
    public class AutoAssignUIAttribute : PropertyAttribute
    {
        /// <summary>
        /// Type of UI component to search for
        /// </summary>
        public UIComponentType ComponentType { get; }

        /// <summary>
        /// Search for it in the children of the GameObject
        /// </summary>
        public bool SearchInChildren { get; set; }

        /// <summary>
        /// If true, searches for the first component of this type with a specific name
        /// </summary>
        public string ComponentName { get; set; }

        public AutoAssignUIAttribute(UIComponentType type = UIComponentType.Canvas)
        {
            ComponentType = type;
            SearchInChildren = false;
            ComponentName = "";
        }
    }

    /// <summary>
    /// Commonly used UI component types
    /// </summary>
    public enum UIComponentType
    {
        Canvas,
        CanvasGroup,
        GraphicRaycaster,
        LayoutGroup,
        ContentSizeFitter,
        Button,
        Slider,
        Scrollbar,
        InputField,
        Text,
        Image,
        RawImage,
        Dropdown,
        Toggle,
        ToggleGroup,
        ScrollRect,
        Custom
    }
}
