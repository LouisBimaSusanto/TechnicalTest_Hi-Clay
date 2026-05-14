using UnityEngine;

namespace Game.Combat
{
    public class Shooter
    {
        private Bullet bulletPrefab;
        private Transform firePoint;

        public Shooter(Bullet bulletPrefab, Transform firePoint)
        {
            this.bulletPrefab = bulletPrefab;
            this.firePoint = firePoint;
        }

        public void Shoot(Vector2 direction)
        {
            Bullet spawnedBullet = Object.Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.identity
                );

            spawnedBullet.Initialize(direction);
        }
    }
}

