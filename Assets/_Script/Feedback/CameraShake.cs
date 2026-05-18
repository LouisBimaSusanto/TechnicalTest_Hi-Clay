using Unity.Cinemachine;
using UnityEngine;

namespace Game.CameraSystem
{
    public class CameraShake : MonoBehaviour
    {
        public static CameraShake Instance;

        [SerializeField] private CinemachineImpulseSource impulseSource;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void ShakeCamera(float force)
        {
            impulseSource.GenerateImpulse(force);
        }
    }
}
