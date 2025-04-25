using System.ComponentModel.DataAnnotations;

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
        [Display( Name = "Nintendo Entertainment System")]
        NES,
        [Display( Name = "Super Nintendo Entertainment System")]
        SNES,
        [Display( Name = "Nintendo 64")]
        N64,
        GameCube,
        GameBoy,
        [Display( Name = "GameBoy Color")]
        GameBoyColor,
        [Display( Name = "GameBoy Advance")]
        GameBoyAdvance,
        VirtualBoy,
        Wii,
        WiiU,
        [Display( Name = "Nintendo DS")]
        DS,
        [Display( Name = "Nintendo 3DS")]
        DS3,
        [Display( Name = "Sega Master System")]
        SegaMasterSystem,
        [Display( Name = "Sega Mega Drive")]
        SegaMegaDrive,
        [Display( Name = "Sega Saturn")]
        SegaSaturn,
        [Display( Name = "Sega Dreamcast")]
        SegaDreamcast,
        [Display( Name = "Sega GameGear")]
        SegaGameGear,
        [Display( Name = "Sega Genesis")]
        SegaGenesis,
        [Display( Name = "Playstation 1")]
        PS1,
        [Display( Name = "Playstation 2")]
        PS2,
        [Display( Name = "Playstation 3")]
        PS3,
        [Display( Name = "Playstation Portable")]
        PSP,
        [Display( Name = "Playstation Vita")]
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