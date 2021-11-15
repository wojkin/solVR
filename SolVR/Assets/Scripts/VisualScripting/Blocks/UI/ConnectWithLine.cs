using UnityEngine;

namespace VisualCoding.Blocks.UI
{
    /// <summary>
    /// Component for connecting two objects with a line.
    /// </summary>
    /// <remarks>The <see cref="LineRenderer"/> should have only two positions configured.</remarks>
    [RequireComponent(typeof(LineRenderer))]
    public class ConnectWithLine : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Target of the connection.</summary>
        [SerializeField] private Transform target;

        #endregion

        #region Variables

        /// <summary>Line renderer responsible for drawing the line.</summary>
        private LineRenderer _line;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initializes variables.
        /// </summary>
        private void Start()
        {
            _line = GetComponent<LineRenderer>();
        }

        /// <summary>
        /// Sets the line start and end positions so the it connects the target with the current position.
        /// </summary>
        private void Update()
        {
            _line.SetPosition(0, target.position);
            _line.SetPosition(1, transform.position);
        }

        #endregion
    }
}