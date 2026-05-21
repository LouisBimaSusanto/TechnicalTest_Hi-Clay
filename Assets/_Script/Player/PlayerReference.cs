using UnityEngine;

namespace Game.Player
{
    public class PlayerReference : MonoBehaviour
    {
        [Header("Core Component")]
        public Rigidbody2D Rigidbody2D;
        public SpriteRenderer SpriteRenderer;

        [Header("Combat")]
        public Transform FirePoint;
        public Transform MeleePoint;
    }
}
