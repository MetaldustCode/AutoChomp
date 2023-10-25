namespace AutoChomp
{
    internal enum GhostState
    {
        Alive,
        Afraid,
        Dead,
        Invisible
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
}