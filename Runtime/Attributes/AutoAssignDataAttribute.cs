using System;
using UnityEngine;

namespace UnityAutoAssign
{
    /// <summary>
    /// [NEW v3.0] Attribute to auto-assign data (ScriptableObjects, resources, etc.)
    /// Searches in Resources folders and allows references to assets.
    /// </summary>
    /// <example>
    /// [AutoAssignData(path: "Cards/FireCard")]
    /// [SerializeField] private Card fireCard;
    /// 
    /// [AutoAssignData(searchInAssets: true)]
    /// [SerializeField] private AudioClip[] allAudioClips;
    /// </example>
    [AttributeUsage(AttributeTargets.Field)]
    public class AutoAssignDataAttribute : PropertyAttribute
    {
        /// <summary>
        /// Path in Resources folder to load the asset (without extension)
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Search for the asset in the entire Assets folder (editor only)
        /// </summary>
        public bool SearchInAssets { get; set; }

        /// <summary>
        /// If true, loads all instances found in an array
        /// </summary>
        public bool LoadAllInstances { get; set; }

        public AutoAssignDataAttribute(string path = "")
        {
            ResourcePath = path;
            SearchInAssets = false;
            LoadAllInstances = false;
        }
    }
}
