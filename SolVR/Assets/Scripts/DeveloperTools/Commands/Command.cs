using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngineInternal;

namespace DeveloperTools.Commands
{
    [Serializable]
    public class Command
    {
        [SerializeField] protected string commandPattern;

        public virtual void Execute()
        {
        }

        public bool CheckMatch(string command)
        {
            var rx = new Regex(commandPattern);
            return rx.IsMatch(command);
        }
    }
}