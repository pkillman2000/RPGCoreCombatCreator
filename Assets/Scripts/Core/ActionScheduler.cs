using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction _currentAction;

        public void StartAction(IAction action)
        {
            if(action == _currentAction)
            {
                return;
            }

            /*
             * Call Cancel method in whatever script is currently active
             * If Fighter is currently attacking and the mouse is clicked to move,
             * Fighter.Cancel is called and _currentAction is now set to Mover.
             * Then, if we attack, Mover.Cancel is called.
            */

            if (_currentAction != null)
            {
                _currentAction.Cancel();
            }

            _currentAction = action;
        }
    }
}
