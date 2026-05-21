using UnityEngine;

namespace Game.CameraSystem 
{
    public class CameraTargetFollow : MonoBehaviour
    {
        [SerializeField] private Transform player;

        private Vector3 initalPosition;

        private void Start()
        {
            initalPosition = transform.position;
        }

        private void LateUpdate()
        {
            transform.position = new Vector3(
                player.position.x,
                player.position.y,
                player.position.z
                );
        }
    }

}
