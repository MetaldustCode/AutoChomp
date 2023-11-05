namespace AutoChomp
{
    internal enum GhostState
    {
        Alive,
        Afraid,
        Dead
    }

    internal enum HouseState
    {
        ReturnHouse,
        LeaveHouse,
        OutHouse,
        InHouse
    }

    internal enum StartLocation
    {
        Right,
        Left,
        Middle,
        Outside
    }

    internal enum PacmanState
    {
        Close,
        Mid,
        Open,
        Dead
    }

    internal enum Squiggle
    {
        Standard,
        Alternate
    }

    internal enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right,
        Freeze
    }

    internal enum GhostColor
    {
        Default,
        White
    }

    internal enum GameMode
    {
        MainGame,
        ReloadGame,
        PacmanDeath
    }

    internal enum InputMode
    {
        None,
        Keyboard,
        Random,
        Gluttany,
        AStar
    }
}