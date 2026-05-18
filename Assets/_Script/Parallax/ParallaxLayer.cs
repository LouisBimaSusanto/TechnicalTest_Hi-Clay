using UnityEngine;
namespace Game.CameraSystem
{
    public class ParallaxLayer : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float parallaxMultiplier = 0.5f;
        [SerializeField] private bool infiniteScrolling = true;
        private Transform cameraTransform;
        private Vector3 lastCameraPosition;
        private float spriteWidth;
        private float cameraHalfWidth;
        private void Start()
        {
            cameraTransform = Camera.main.transform;
            lastCameraPosition = cameraTransform.position;

            cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;

            Renderer renderer = GetComponentInChildren<Renderer>();
            if (renderer != null) spriteWidth = renderer.bounds.size.x;
        }
        private void LateUpdate()
        {
            Vector3 delta = cameraTransform.position - lastCameraPosition;
            transform.position += new Vector3(
                delta.x * parallaxMultiplier,
                delta.y * parallaxMultiplier,
                0f
            );
            lastCameraPosition = cameraTransform.position;

            if (infiniteScrolling && spriteWidth > 0)
            {
                float distanceX = cameraTransform.position.x - transform.position.x;
                float threshold = cameraHalfWidth + spriteWidth * 0.5f;

                if (distanceX > threshold)
                {
                    transform.position += new Vector3(spriteWidth, 0f, 0f);
                }
                else if (distanceX < -threshold)
                {
                    transform.position -= new Vector3(spriteWidth, 0f, 0f);
                }
            }
        }
    }
}