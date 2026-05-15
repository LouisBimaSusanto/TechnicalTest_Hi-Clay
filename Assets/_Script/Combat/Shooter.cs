using UnityEngine;

namespace Game.Combat
{
    public class Shooter
    {
        private Bullet bulletPrefab;
        private Transform firePoint;
        private BulletOwner owner;

        public Shooter(Bullet bulletPrefab, Transform firePoint, BulletOwner owner)
        {
            this.bulletPrefab = bulletPrefab;
            this.firePoint = firePoint;
            this.owner = owner;
        }

        public void Shoot(Vector2 direction)
        {
            Bullet spawnedBullet = Object.Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.identity
                );

            spawnedBullet.Initialize(direction, owner);
        }
    }
}

