public static class Enums
{
    public enum TerrainType
    {
        NULL,
        DESOLATE,
        MEADOW,
        SWAMP,
        FOREST,
        ROCK,
        WATER,
        BORDER
    }

    public enum RuleLogic
    {
        AND,
        OR
    }
    public enum RuleKind
    {
        MAX_ALLOWED,
        MIN_ALLOWED,
        EXACTLY_ALLOWED,
        ONLY_ALLOWED
    }

    public enum CreatureType
    {
        NULL,
        BIRD,
        HAWK,
        RABBIT,
        BEAR,
        SMALLFISH,
        BIGFISH,
        DEER,
        WILDBOARD,
        WOLF,
        RACCOON,
        TRASH,
        HUMAN,
        FIRE,
        HERON
    }

    public enum CardType
    {
        TILE,
        TOKEN,
        EVENT
    }

    public enum HexDirection
    {
        NORTH,
        NORTH_EAST,
        SOUTH_EAST,
        SOUTH,
        SOUTH_WEST,
        NORTH_WEST,
        NULL
    }
}
