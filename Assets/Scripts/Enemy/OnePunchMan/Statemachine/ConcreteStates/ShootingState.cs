﻿using StatePattern.Enemy;
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
        private PlayerController target;
        private float shootTimer;

        public ShootingState(OnePunchManStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            SetTarget();
            shootTimer = 0;
        }

        public void Update()
        {
            Quaternion desiredRotation = CalculateRotationTowardsPlayer();
            Owner.SetRotation(RotateTowards(desiredRotation));

            if (IsRotationComplete(desiredRotation))
            {
                shootTimer -= Time.deltaTime;
                if (shootTimer <= 0)
                {
                    ResetTimer();
                    Owner.Shoot();
                }
            }
        }

        public void OnStateExit() => target = null;

        private void SetTarget() => target = GameService.Instance.PlayerService.GetPlayer();

        private Quaternion CalculateRotationTowardsPlayer()
        {
            Vector3 directionToPlayer = target.Position - Owner.Position;
            directionToPlayer.y = 0f;
            return Quaternion.LookRotation(directionToPlayer, Vector3.up);
        }

        private Quaternion RotateTowards(Quaternion desiredRotation) => Quaternion.LerpUnclamped(Owner.Rotation, desiredRotation, Owner.Data.RotationSpeed / 30 * Time.deltaTime);

        private bool IsRotationComplete(Quaternion desiredRotation) => Quaternion.Angle(Owner.Rotation, desiredRotation) < Owner.Data.RotationThreshold;

        private void ResetTimer() => shootTimer = Owner.Data.RateOfFire;
    }
}
