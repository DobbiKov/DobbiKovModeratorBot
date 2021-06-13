using System;
using System.Collections.Generic;
using System.Text;

namespace DobbiKovModeratorBot.Domain.Enums
{
    public static class KeyboardIdenifiers
    {
        private static string[] ids = new string[] { "keyboard_kick_command_yes", "keyboard_kick_command_no" };

        public static string GetIdentifier(KeyboardIdentifier id)
        {
            return ids[(int)id];
        }
    }
    public enum KeyboardIdentifier : uint
    {
        banCommandYes,
        banCommandNo
    }
}
