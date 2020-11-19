using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public class Input
    {
        public const string HORIZONTAL = "Horizontal";
        public const string VERTICAL   = "Vertical";
    }

    public class Layer
    {
        public const int LEVEL_BOUNDS = 8;
        public const int NPC          = 10;
    }

    public class Amount
    {
        public const int OF_OPTION_WINDOWS = 4;
    }

    public class ConnectionPoint
    {
        public enum Type { In, Out }
    }

    public class Menu
    {
        public const float BAR_HEIGHT    = 20f;
        public const float LETTER_LENGTH = 8.75f;
    }
}
