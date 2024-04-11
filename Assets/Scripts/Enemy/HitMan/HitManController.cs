using StatePattern.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enemy.HitMan
{
    public class HitManController : EnemyController
    {
        private HitManStateMachine stateMachine;

        public HitManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(StatePattern.StateMachine.States.IDLE);
        }

        private void CreateStateMachine() => stateMachine = new HitManStateMachine(this);

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();
        }
    }
}
