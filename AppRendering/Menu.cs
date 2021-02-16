using System;
using System.Collections.Generic;

namespace SharpLabProject.ConsoleRendering
{
    public abstract class Menu
    {
        public Dictionary<ConsoleKey, Action> Actions { get; } = new Dictionary<ConsoleKey, Action>();

        protected Menu() { }

        public virtual string Render()
        {
            return null;
        }

        protected void RequestRender()
        {
            Rendering.Rerender();
        }
    }
}