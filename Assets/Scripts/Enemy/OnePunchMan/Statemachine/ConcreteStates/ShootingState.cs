using StatePattern.Enemy;
using StatePattern.Enemy.Bullet;
using StatePattern.Main;
using StatePattern.Player;
using StatePattern.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy.OnePunchMan.Statemachine.ConcreteStates
{
    public class ShootingState : IState
    {
        public OnePunchManController Owner { get; set; }
        private OnePunchManStateMachine stateMachine;
        private Quaternion desiredRotation;
        private PlayerController target;
        private float shootTimer;

        public ShootingState(OnePunchManStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            shootTimer = Owner.enemyScriptableObject.RateOfFire;
            target = Owner.target;
        }

        public void OnStateExit()
        {
            shootTimer = 0;
        }

        public void Update()
        {
            desiredRotation = CalculateRotationTowardsPlayer();
            SetRotation(RotateTowards(desiredRotation));

            if (IsFacingPlayer(desiredRotation))
            {
                shootTimer -= Time.deltaTime;
                if (shootTimer <= 0)
                {
                    shootTimer = Owner.enemyScriptableObject.RateOfFire;
                    Shoot();
                }
            }
        }

        public void Shoot()
        {
            Owner.enemyView.PlayShootingEffect();
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.ENEMY_SHOOT);
            BulletController bullet = new BulletController(Owner.enemyView.transform, Owner.enemyScriptableObject.BulletData);
        }

        private Quaternion CalculateRotationTowardsPlayer()
        {
            Vector3 directionToPlayer = target.Position - Owner.Position;
            directionToPlayer.y = 0f;
            return Quaternion.LookRotation(directionToPlayer, Vector3.up);
        }

        private Quaternion RotateTowards(Quaternion desiredRotation) => Quaternion.LerpUnclamped(Owner.Rotation, desiredRotation, Owner.enemyScriptableObject.RotationSpeed / 30 * Time.deltaTime);

        public void SetRotation(Quaternion desiredRotation) => Owner.enemyView.transform.rotation = desiredRotation;

        private bool IsFacingPlayer(Quaternion desiredRotation) => Quaternion.Angle(Owner.Rotation, desiredRotation) < Owner.Data.RotationThreshold;

    }
}
