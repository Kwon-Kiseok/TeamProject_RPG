using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HOGUS.Scripts.DP;

namespace HOGUS.Scripts.DP
{
    public class Invoker : MonoBehaviour
    {
        // Temp example commands dictionary
        private Dictionary<string, Command> commands = new();

        public void ExecuteCommand(Command command)
        {
            command.Execute();
            commands.Add("Example Key", command);
        }
    }
}
