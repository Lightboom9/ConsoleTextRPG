using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleTextRPG.ConsoleRendering
{
    public abstract class Menu
    {
        private Menu _parent;

        protected Action OnReturnControl { get; set; }

        private string _lastReturnControlMessage = null;
        protected string GetLastReturnControlMessage()
        {
            string msg = _lastReturnControlMessage;
            _lastReturnControlMessage = null;

            return msg;
        }

        public Dictionary<ConsoleKey, Action> Actions { get; } = new Dictionary<ConsoleKey, Action>();

        protected Menu() { }

        protected Menu(Menu parent)
        {
            _parent = parent;
        }

        public bool FullRerenderRequested { get; protected set; }

        public void FullRerenderRequestComplete()
        {
            FullRerenderRequested = false;
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

            //Console.WriteLine("\nReturning from " + this.GetType() + " to " + _parent.GetType());
            //Console.ReadKey(true);

            Rendering.SetActiveMenu(_parent);

            _parent.OnReturnControl?.Invoke();
        }
        protected void ReturnControl(string msg)
        {
            if (_parent == null) return;

            _parent._lastReturnControlMessage = msg;
            
            ReturnControl();
        }

        protected void HandleControl(Menu child)
        {
            child._parent = this;

            Rendering.SetActiveMenu(child);
        }

        protected void Delay(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}