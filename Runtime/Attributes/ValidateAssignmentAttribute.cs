using System;
using UnityEngine;

namespace UnityAutoAssign
{
    /// <summary>
    /// [NEW v3.0] Attribute to validate that components are assigned after initialization time.
    /// Useful for debugging and ensuring that everything is properly configured.
    /// </summary>
    /// <example>
    /// [AutoAssign]
    /// [ValidateAssignment(WarnIfMissing = true)]
    /// [SerializeField] private Rigidbody2D rb;
    /// </example>
    [AttributeUsage(AttributeTargets.Field)]
    public class ValidateAssignmentAttribute : PropertyAttribute
    {
        /// <summary>
        /// Show warning if assignment fails
        /// </summary>
        public bool WarnIfMissing { get; set; }

        /// <summary>
        /// Show error if assignment fails (requires being null)
        /// </summary>
        public bool ErrorIfMissing { get; set; }

        /// <summary>
        /// Time in seconds after which to validate (useful for async setup)
        /// </summary>
        public float ValidateAtSeconds { get; set; }

        public ValidateAssignmentAttribute()
        {
            WarnIfMissing = true;
            ErrorIfMissing = false;
            ValidateAtSeconds = 0f;
        }
    }
}
