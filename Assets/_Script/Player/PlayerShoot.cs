using Game.Combat;
using UnityEngine;

namespace Game.Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [Header("Shoot Settings")]
        [SerializeField] private Bullet bulletPrefab;

        [SerializeField]
        private float fireRate = 0.25f;

        private PlayerInputHandler inputHandler;
        private PlayerMovement movement;
        private PlayerReference reference;

        private Shooter shooter;
        private ShootCoolDown fireRateHandler;

        private void Awake()
        {
            inputHandler = GetComponent<PlayerInputHandler>();
            movement = GetComponent<PlayerMovement>();
            reference = GetComponent<PlayerReference>();

            shooter = new Shooter(
                bulletPrefab,
                reference.FirePoint
            );

            fireRateHandler = new ShootCoolDown(fireRate);
        }

        private void OnEnable()
        {
            inputHandler.OnShootPressed += HandleShoot;
        }

        private void OnDisable()
        {
            inputHandler.OnShootPressed -= HandleShoot;
        }

        private void HandleShoot()
        {
            if (!fireRateHandler.CanShoot())
            {
                return;
            }

            Vector2 shootDirection =
                movement.IsFacingRight
                ? Vector2.right
                : Vector2.left;

            shooter.Shoot(shootDirection);
        }

        private void OnValidate()
        {
            fireRateHandler?.SetFireRate(fireRate);
        }
    }
}