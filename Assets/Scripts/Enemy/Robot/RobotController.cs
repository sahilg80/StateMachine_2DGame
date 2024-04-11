using StatePattern.Enemy;
using StatePattern.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enemy.Robot
{
    public class RobotController : EnemyController
    {
        private RobotStateMachine stateMachine;
        public int CloneCountLeft { get; private set; }

        public RobotController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            SetCloneCount(enemyScriptableObject.CloneCount);
            enemyView.SetController(this);
            ChangeColor(EnemyColorType.Default);
            CreateStateMachine();
            stateMachine.ChangeState(StatePattern.StateMachine.States.IDLE);
        }

        public void SetCloneCount(int cloneCountToSet) => CloneCountLeft = cloneCountToSet;

        private void CreateStateMachine() => stateMachine = new RobotStateMachine(this);

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();
        }

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            stateMachine.ChangeState(StatePattern.StateMachine.States.CHASING);
        }

        public override void PlayerExitedRange() => stateMachine.ChangeState(StatePattern.StateMachine.States.IDLE);

        public override void Die()
        {
            if (CloneCountLeft > 0)
                stateMachine.ChangeState(StatePattern.StateMachine.States.CLONING);
            base.Die();
        }

        public void Teleport() => stateMachine.ChangeState(StatePattern.StateMachine.States.TELEPORTING);

        public void SetDefaultColor(EnemyColorType colorType) => enemyView.SetDefaultColor(colorType);

        public void ChangeColor(EnemyColorType colorType) => enemyView.ChangeColor(colorType);
    }
}
