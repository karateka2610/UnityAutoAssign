using System;
using UnityEngine;

namespace UnityAutoAssign
{
    /// <summary>
    /// [NEW v3.0] Attribute for lifecycle events of AutoAssign.
    /// Allows executing methods when assignment is complete.
    /// </summary>
    /// <example>
    /// [AutoAssign]
    /// [AutoAssignCallback(OnAssignedMethodName = nameof(OnComponentAssigned))]
    /// [SerializeField] private Rigidbody2D rb;
    /// 
    /// private void OnComponentAssigned()
    /// {
    ///     Debug.Log("Rigidbody was automatically assigned!");
    /// }
    /// </example>
    [AttributeUsage(AttributeTargets.Field)]
    public class AutoAssignCallbackAttribute : PropertyAttribute
    {
        /// <summary>
        /// Name of the method to call when assignment is successful
        /// </summary>
        public string OnAssignedMethodName { get; set; }

        /// <summary>
        /// Name of the method to call when assignment fails
        /// </summary>
        public string OnAssignFailedMethodName { get; set; }

        public AutoAssignCallbackAttribute()
        {
            OnAssignedMethodName = "";
            OnAssignFailedMethodName = "";
        }
    }
}
