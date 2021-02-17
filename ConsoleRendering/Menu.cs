using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleTextRPG.ConsoleRendering
{
    public abstract class Menu
    {
        private Menu _parent;

        protected Action OnReturnControl { get; set; }

        public Dictionary<ConsoleKey, Action> Actions { get; } = new Dictionary<ConsoleKey, Action>();

        protected Menu() { }

        protected Menu(Menu parent)
        {
            _parent = parent;
        }

        public virtual string Render()
        {
            return null;
        }

        protected void RequestRender()
        {
            Rendering.Rerender();
        }

        protected void ReturnControl()
        {
            if (_parent == null) return;

            _parent.OnReturnControl?.Invoke();

            Rendering.SetActiveMenu(_parent);
        }

        protected void Delay(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}