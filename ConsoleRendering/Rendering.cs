using System;
using System.Threading;

namespace ConsoleTextRPG.ConsoleRendering
{
    public static class Rendering
    {
        private static bool _waitingForInput = true;
        private static Menu _currentMenu;

        private static bool _rendered = false;
        private static bool _lockRendering = false;

        public static void BeginRenderLoop()
        {
            Console.CursorVisible = false;

            ThreadStart threadStart = new ThreadStart(RenderLoop);
            Thread childThread = new Thread(threadStart);
            childThread.Start();
        }

        private static void RenderLoop()
        {
            while (true)
            {
                if (_currentMenu == null || _lockRendering)
                {
                    Thread.Sleep(50);
                    continue;
                }
                if (!_rendered)
                {
                    string toRender = _currentMenu.Render();

                    toRender = toRender.Replace("\n", "                \n");

                    //Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.Write(toRender);

                    _rendered = true;
                }

                if (_waitingForInput)
                {
                    if (!Console.KeyAvailable) Thread.Sleep(50);
                    else
                    {
                        ConsoleKeyInfo cki = Console.ReadKey(true);

                        if (_currentMenu.Actions.ContainsKey(cki.Key))
                        {
                            _currentMenu.Actions[cki.Key]();

                            Rerender();
                        }
                    }
                }
                else
                {
                    // Animation? Or some stuff...
                }
            }
        }

        public static void SetActiveMenu(Menu menu)
        {
            _currentMenu = menu;

            Console.Clear();

            Rerender();
        }

        public static void Rerender(bool clear = false)
        {
            if (clear) Console.Clear();

            _rendered = false;
        }

        public static void LockInput()
        {
            _waitingForInput = false;
        }

        public static void UnlockInput()
        {
            _waitingForInput = true;
        }

        public static void LockRendering()
        {
            _lockRendering = true;
        }

        public static void UnlockRendering()
        {
            _lockRendering = false;
        }
    }
}