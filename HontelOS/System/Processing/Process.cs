/*
* PROJECT:          HontelOS
* CONTENT:          Process class
* PROGRAMMERS:      Jort van Dalen
*/

using Cosmos.System.Graphics;
using HontelOS.System.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HontelOS.System.Processing
{
    public class Process
    {
        public string Name;
        public string[] Arguments;
        public ProcessType Type;
        public int PID;

        public List<Action> OnKill = new();

        public void Init(string name, ProcessType processType, string[] arguments)
        {
            Name = name;
            Type = processType;
            Arguments = arguments;
            Kernel.Processes.Add(this);
        }

        public void Kill()
        {
            foreach(var a in OnKill) a.Invoke();
            Kernel.Processes.Remove(this);
        }

        public virtual void Update() { }

        public Process GetProcess() => this;
    }

    public enum ProcessType
    {
        Driver
    }
}
