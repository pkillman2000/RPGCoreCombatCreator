using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Core
{
    public interface IAction
    {
        // This will call the Cancel method in whatever script is passed in
        void Cancel();
    }
}
