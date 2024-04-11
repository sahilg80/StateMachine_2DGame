using Assets.Scripts.Enemy.Robot;
using StatePattern.Enemy;
using StatePattern.Main;
using StatePattern.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enemy.States
{
    public class CloningState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public CloningState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            CreateAClone();
            CreateAClone();
        }

        public void Update() { }

        public void OnStateExit() { }

        private void CreateAClone()
        {
            RobotController clonedRobot = GameService.Instance.EnemyService.CreateEnemy(Owner.Data) as RobotController;
            clonedRobot.SetCloneCount((Owner as RobotController).CloneCountLeft - 1);
            clonedRobot.Teleport();
            clonedRobot.SetDefaultColor(EnemyColorType.Clone);
            clonedRobot.ChangeColor(EnemyColorType.Clone);
            GameService.Instance.EnemyService.AddEnemy(clonedRobot);
        }
    }
}
