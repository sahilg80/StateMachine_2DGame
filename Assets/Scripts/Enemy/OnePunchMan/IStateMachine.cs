using StatePattern.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enemy.OnePunchMan
{
    public interface IStateMachine
    {
        public void ChangeState(States newState);
    }
}
