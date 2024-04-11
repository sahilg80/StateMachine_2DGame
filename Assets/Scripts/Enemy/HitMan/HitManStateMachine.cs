using Assets.Scripts.Enemy.States;
using StatePattern.Enemy;
using StatePattern.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enemy.HitMan
{
    public class HitManStateMachine : GenericStateMachine<HitManController>
    {
        public HitManStateMachine(HitManController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(StatePattern.StateMachine.States.IDLE, new IdleState<HitManController>(this));
            States.Add(StatePattern.StateMachine.States.PATROLLING, new PatrollingState<HitManController>(this));
            States.Add(StatePattern.StateMachine.States.CHASING, new ChasingState<HitManController>(this));
            States.Add(StatePattern.StateMachine.States.SHOOTING, new ShootingState<HitManController>(this));
            States.Add(StatePattern.StateMachine.States.TELEPORTING, new TeleportingState<HitManController>(this));
        }
    }
}
