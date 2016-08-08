using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebulous
{
    public enum ControlState
    {
              NONE
            , TITLE
            , GAME
    }

    public static class GameState
    {
        private static bool terminateProgram = false;

        private static ControlState controlState = ControlState.NONE;
        private static ControlState pendingControlState = ControlState.TITLE;

        public static void NewGame()
        {

        }


        public static ControlState ControlState
        {
            get
            {
                return controlState;
            }

            set
            {
                controlState = value;
            }
        }

        public static ControlState PendingControlState
        {
            get
            {
                return pendingControlState;
            }

            set
            {
                pendingControlState = value;
            }
        }
        public static bool TerminateProgram
        {
            get
            {
                return terminateProgram;
            }

            set
            {
                terminateProgram = value;
            }
        }

    }
}
