using UnityEngine;

namespace VisualScripting.Blocks.Connectors
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

        /// <summary>Delegate for a connector destroyed handler.</summary>
        public delegate void ConnectorDestroyedHandler();

        /// <summary>Instance of the <see cref="ConnectorDestroyedHandler"/> delegate.</summary>
        private ConnectorDestroyedHandler _connectorDestroyed;

        /// <summary>Connected <see cref="OutConnector"/>.</summary>
        private OutConnector _connected;

        /// <summary>Property for accessing the block.</summary>
        public Block Block => block;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Invokes the connector destroyed handler of the connected <see cref="OutConnector"/>.
        /// </summary>
        private void OnDestroy()
        {
            _connectorDestroyed?.Invoke();
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Method for connecting an <see cref="OutConnector"/>.
        /// </summary>
        /// <param name="connector"><see cref="OutConnector"/> to be connected.</param>
        /// <param name="destroyHandler">Handler for the <see cref="_connectorDestroyed"/> event.</param>
        public void Connect(OutConnector connector, ConnectorDestroyedHandler destroyHandler)
        {
            // if a connector was already connected, disconnect it
            if (_connected != null)
                _connected.Disconnect();

            _connected = connector;
            _connectorDestroyed = destroyHandler;
        }

        /// <summary>
        /// Method for disconnecting an <see cref="OutConnector"/>.
        /// </summary>
        public void Disconnect()
        {
            _connectorDestroyed = null;
            _connected = null;
        }

        #endregion
    }
}