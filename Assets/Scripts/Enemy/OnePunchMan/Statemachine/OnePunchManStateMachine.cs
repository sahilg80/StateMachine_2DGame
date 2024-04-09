using Assets.Scripts.Enemy.OnePunchMan.Statemachine.ConcreteStates;
using StatePattern.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enemy.OnePunchMan.Statemachine
{
    public class OnePunchManStateMachine
    {
        private OnePunchManController Owner;
        private IState currentState;
        protected Dictionary<OnePunchManStates, IState> States = new Dictionary<OnePunchManStates, IState>();

        public OnePunchManStateMachine(OnePunchManController Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(OnePunchManStates.IDLE, new IdleState(this));
            States.Add(OnePunchManStates.ROTATING, new RotatingState(this));
            States.Add(OnePunchManStates.SHOOTING, new ShootingState(this));
        }

        private void SetOwner()
        {
            foreach (IState state in States.Values)
            {
                state.Owner = Owner;
            }
        }

        public void Update() => currentState?.Update();

        protected void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }

        public void ChangeState(OnePunchManStates newState) => ChangeState(States[newState]);
    }

}
