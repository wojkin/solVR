using System.Collections.Generic;
using UnityEngine;

namespace VisualCoding.Blocks.Connectors
{
    /// <summary>
    /// A class representing an in-connector of a block.
    /// </summary>
    public class InConnector : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>The block to which the in-connector belongs.</summary>
        [SerializeField] private Block block;

        #endregion

        #region Variables

        /// <summary>Delegate for a connector destroyed even handler.</summary>
        public delegate void ConnectorDestroyedHandler();

        /// <summary>Event raised when the in connector is destroyed.</summary>
        private event ConnectorDestroyedHandler ConnectorDestroyed;

        /// <summary>Dictionary storing the connected <see cref="OutConnector"/>s as keys and their connector destroyed
        /// event handlers as values.</summary>
        private Dictionary<OutConnector, ConnectorDestroyedHandler> _connected;

        /// <summary>Property for accessing the block.</summary>
        public Block Block => block;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initializes variables.
        /// </summary>
        private void Awake()
        {
            _connected = new Dictionary<OutConnector, ConnectorDestroyedHandler>();
        }

        /// <summary>
        /// Invokes the connector destroyed event, notifying <see cref="OutConnector"/>s connected to this connector.
        /// </summary>
        private void OnDestroy()
        {
            ConnectorDestroyed?.Invoke();
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Method for connecting an <see cref="OutConnector"/>.
        /// </summary>
        /// <param name="connector"><see cref="OutConnector"/> to be connected.</param>
        /// <param name="destroyHandler">Handler for the <see cref="ConnectorDestroyed"/> event.</param>
        public void Connect(OutConnector connector, ConnectorDestroyedHandler destroyHandler)
        {
            _connected.Add(connector, destroyHandler);
            ConnectorDestroyed += destroyHandler;
        }

        /// <summary>
        /// Method for disconnecting an <see cref="OutConnector"/>.
        /// </summary>
        /// <param name="connector"><see cref="OutConnector"/> to be disconnected.</param>
        public void Disconnect(OutConnector connector)
        {
            ConnectorDestroyed -= _connected[connector];
            _connected.Remove(connector);
        }

        #endregion
    }
}