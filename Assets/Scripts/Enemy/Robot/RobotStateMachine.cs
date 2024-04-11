using Assets.Scripts.Enemy.States;
using StatePattern.Enemy;
using StatePattern.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enemy.Robot
{
    public class RobotStateMachine : GenericStateMachine<RobotController>
    {
        public RobotStateMachine(RobotController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(StatePattern.StateMachine.States.IDLE, new IdleState<RobotController>(this));
            States.Add(StatePattern.StateMachine.States.PATROLLING, new PatrollingState<RobotController>(this));
            States.Add(StatePattern.StateMachine.States.CHASING, new ChasingState<RobotController>(this));
            States.Add(StatePattern.StateMachine.States.SHOOTING, new ShootingState<RobotController>(this));

            States.Add(StatePattern.StateMachine.States.TELEPORTING, new TeleportingState<RobotController>(this));
            States.Add(StatePattern.StateMachine.States.CLONING, new CloningState<RobotController>(this));
        }
    }
}
