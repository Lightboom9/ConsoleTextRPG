using System;
using System.Threading;

namespace SharpLabProject.AppRendering
{
    public static class Rendering
    {
        private static bool _waitingForInput = true;
        private static Menu _currentMenu;

        private static bool _rendered = false;

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
                if (_currentMenu == null)
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

        public static void SetMenu(Menu menu)
        {
            _currentMenu = menu;

            Rerender();
        }

        public static void Rerender()
        {
            _rendered = false;
        }
    }
}