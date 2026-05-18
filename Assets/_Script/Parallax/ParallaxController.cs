using UnityEngine;

namespace Game.CameraSystem
{
    public class ParallaxController : MonoBehaviour
    {
        [SerializeField] private ParallaxLayer[] layers;

        private void Awake()
        {
            if (layers == null || layers.Length == 0)
            {
                layers = GetComponentsInChildren<ParallaxLayer>();
            }
        }
    }

}
