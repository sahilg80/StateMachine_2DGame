using StatePattern.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enemy.OnePunchMan.Statemachine
{
    public interface IState
    {
        // Reference to the owner (e.g., the enemy character) that holds this state.
        public OnePunchManController Owner { get; set; }

        // Called when the owner enters this state.
        public void OnStateEnter();

        // Called during each game update cycle to update the state's behavior.
        public void Update();

        // Called when the owner exits this state.
        public void OnStateExit();
    }
}
