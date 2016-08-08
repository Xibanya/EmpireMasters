using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Nebulous
{
    public static class Input
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        private static KeyboardState newKeyState;
        private static KeyboardState oldKeyState;

        private static MouseState newMouseClickState;
        private static MouseState oldMouseClickState;

        private static MouseState newMouseState;
        private static MouseState oldMouseState;

        public static void Initialize()
        {
            oldKeyState = newKeyState = Keyboard.GetState();
            oldMouseClickState = newMouseClickState = Mouse.GetState();
            oldMouseState = newMouseState = Mouse.GetState();
        }

        public static void Update()
        {
            if (!HasFocus()) return;

            oldKeyState = newKeyState;
            newKeyState = Keyboard.GetState();

            oldMouseClickState = newMouseClickState;
            newMouseClickState = Mouse.GetState();

            newMouseState = Mouse.GetState();
            oldMouseState = Mouse.GetState();
        }

        private static bool HasFocus()
        {
            var activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return false;       // No window is currently activated
            }

            var procId = Process.GetCurrentProcess().Id;
            int activeProcId;
            GetWindowThreadProcessId(activatedHandle, out activeProcId);

            return activeProcId == procId;
        }

        public static bool KeyPressed(Keys key)
        {
            return (newKeyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key));
        }

        public static bool KeyDown(Keys key)
        {
            return newKeyState.IsKeyDown(key);
        }

        public static bool LeftMouseClicked
        {
            get
            {
                return (newMouseClickState.LeftButton == ButtonState.Pressed && oldMouseClickState.LeftButton == ButtonState.Released);
            }
        }

        public static bool RightMouseClicked
        {
            get
            {
                return (newMouseClickState.RightButton == ButtonState.Pressed && oldMouseClickState.RightButton == ButtonState.Released);
            }
        }

        public static bool LeftMouseReleased
        {
            get
            {
                return (newMouseClickState.LeftButton == ButtonState.Released && oldMouseClickState.LeftButton == ButtonState.Pressed);
            }
        }

        public static bool RightMouseReleased
        {
            get
            {
                return (newMouseClickState.RightButton == ButtonState.Released && oldMouseClickState.RightButton == ButtonState.Pressed);
            }
        }

        public static bool LeftMouseDown
        {
            get
            {
                return (newMouseClickState.LeftButton == ButtonState.Pressed && oldMouseClickState.LeftButton == ButtonState.Pressed);
            }
        }

        public static bool RightMouseDown
        {
            get
            {
                return (newMouseClickState.RightButton == ButtonState.Pressed && oldMouseClickState.RightButton == ButtonState.Pressed);
            }
        }

        public static bool MouseWheelDown
        {
            get
            {
                return (newMouseState.ScrollWheelValue < oldMouseState.ScrollWheelValue);
            }
        }

        public static bool MouseWheelUp
        {
            get
            {
                return (newMouseState.ScrollWheelValue > oldMouseState.ScrollWheelValue);
            }
        }

        public static int MouseAbsoluteX
        {
            get
            {
                return newMouseState.X;
            }
        }

        public static int MouseAbsoluteY
        {
            get
            {
                return newMouseState.Y;
            }
        }
    }
}
