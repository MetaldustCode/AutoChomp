using SharpDX.DirectInput;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsInput
    {
        internal Direction GetDirection()
        {
            Direction direction2 = GetMovementKeyboard();
            if (direction2 != Direction.None)
                return direction2;

            return Direction.None;
        }

        public Direction GetMovementKeyboard()
        {
            Direction Direction = Direction.None;

            var keyboardState = new KeyboardState();
            try
            {
                clsSharpDX.keyboard.GetCurrentState(ref keyboardState);
            }
            catch (Exception)
            {
                return Direction.None;
            }

            if (keyboardState != null)
            {
                if (keyboardState.PressedKeys.Count > 0)
                {
                    List<Key> lstKeys = keyboardState.PressedKeys;

                    for (int i = 0; i < lstKeys.Count; i++)
                    {
                        if (lstKeys[i] == Key.Up) return Direction.Up;
                        if (lstKeys[i] == Key.Down) return Direction.Down;
                        if (lstKeys[i] == Key.Right) return Direction.Right;
                        if (lstKeys[i] == Key.Left) return Direction.Left;
                    }
                }
            }
            return Direction;
        }
    }
}