using UnityEngine;

namespace VisualCoding.Toolbox
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
                Destroy(this); // destroy this component as it's no longer needed
            }
        }

        #endregion
    }
}