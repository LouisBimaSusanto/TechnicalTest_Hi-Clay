using UnityEngine;

namespace Game.Combat
{
    public class ShootCoolDown
    {
        private float fireRate;
        private float nextShootTime;

        public ShootCoolDown(float fireRate)
        {
            this.fireRate = fireRate;
        }

        public void SetFireRate(float newFireRate)
        {
            fireRate = newFireRate;
        }

        public bool CanShoot()
        {
            if (Time.time < nextShootTime) return false;
            nextShootTime = Time.time + fireRate;
            return true;
        }
    }
}