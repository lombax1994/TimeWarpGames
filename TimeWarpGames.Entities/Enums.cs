namespace TimeWarpGames.Entities
{
    public enum State
    {
        Nieuw,
        Goed,
        Gebruikt,
        Slecht,
        Refurbished
    }

    public enum Platform
    {
        NES,
        SNES,
        N64,
        GameCube,
        GameBoy,
        GameBoyColor,
        GameBoyAdvance,
        VirtualBoy,
        Wii,
        WiiU,
        DS,
        DS3,
        SegaMasterSystem,
        SegaMegaDrive,
        SegaSaturn,
        SegaDreamcast,
        SegaGameGear,
        SegaGenesis,
        PS1,
        PS2,
        PS3,
        PSP,
        PSVita,
        Xbox,
        Xbox360,
        Atari2600,
        Atari5200
    }

    public enum Genre
    {
        Action,
        Adventure,
        RPG,
        Simulation,
        Strategy,
        Sports,
        Racing,
        Puzzle,
        Fighting,
        Shooter,
        Platformer,
        Stealth,
        Horror
    }

    public enum AccessoryType
    {
        Controller,
        OtherInputDevice,
        MemoryCard,
        Cable,
        Adapter,
        Extension,
        Decoration
    }
}