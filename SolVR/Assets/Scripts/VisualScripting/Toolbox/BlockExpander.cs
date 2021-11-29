using System.Linq;
using UnityEngine;
using VisualScripting.Blocks;

namespace VisualScripting.Toolbox
{
    public class BlockExpander : MonoBehaviour
    {
        #region Variables

        /// <summary>Multiplier for leering block's scale.</summary>
        private const float LerpFactor = 20f;

        /// <summary>If the difference between current and target scale is below this threshold, the scale will be
        /// instantly set to one.</summary>
        private const float LerpThreshold = 0.001f;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Smoothly expands a block until it reaches a scale of one.
        /// </summary>
        void Update()
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, LerpFactor * Time.deltaTime);

            if (1f - transform.localScale.x < LerpThreshold)
            {
                transform.localScale = Vector3.one;

                // destroy this component or the gameobject it's attached to if it's not a block
                if (GetComponent<Block>() != null)
                {
                    Destroy(this); // destroy this component if the object it's attached to is a block
                }
                else
                {
                    // if it's not a block parent all children of this gameobject to it's parent and destroys itself
                    var children = transform.Cast<Transform>().ToList();

                    foreach (var child in children)
                        child.SetParent(transform.parent);

                    Destroy(gameObject);
                }
            }
        }

        #endregion
    }
}