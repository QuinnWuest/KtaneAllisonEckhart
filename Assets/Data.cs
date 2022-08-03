using System;
using System.Collections.Generic;
using System.Linq;

public static class Data
{
    public static Dictionary<string, ModuleInfo> data = new ModuleInfo[]
    {
        new ModuleInfoAll("Accumulation", "accumulation"),
        new ModuleInfoAll("Algebra", "algebra"),
        new ModuleInfoAll("Bitmaps", "BitmapsModule"),
        new ModuleInfoAll("Bitwise Operations", "BitOps"),
        new ModuleInfoAll("Blind Maze", "BlindMaze"),
        new ModuleInfoAll("Broken Buttons", "BrokenButtonsModule"),
        new ModuleInfoAll("Cheap Checkout", "CheapCheckoutModule"),
        new ModuleInfoAll("Combination Lock", "combinationLock"),
        new ModuleInfoAll("Connection Check", "graphModule"),
        new ModuleInfoAll("Curriculum", "curriculum"),
        new ModuleInfoAll("Fast Math", "fastMath"),
        new ModuleInfoAll("FizzBuzz", "fizzBuzzModule"),
        new ModuleInfoAll("Gridlock", "GridlockModule"),
        new ModuleInfoAll("Ice Cream", "iceCreamModule"),
        new ModuleInfoAll("Identity Parade", "identityParade"),
        new ModuleInfoAll("LEGOs", "LEGOModule"),
        new ModuleInfoAll("Logic", "Logic"),
        new ModuleInfoAll("Mashematics", "mashematics"),
        new ModuleInfoAll("Maze Scrambler", "MazeScrambler"),
        new ModuleInfoAll("Morse War", "MorseWar"),
        new ModuleInfoAll("Murder", "murder"),
        new ModuleInfoAll("Neutralization", "neutralization"),
        new ModuleInfoAll("Nonogram", "NonogramModule"),
        new ModuleInfoAll("Plumbing", "MazeV2"),
        new ModuleInfoAll("Skewed Slots", "SkewedSlotsModule"),
        new ModuleInfoAll("Symbolic Password", "symbolicPasswordModule"),
        new ModuleInfoAll("The Swan", "theSwan"),
        new ModuleInfoAll("Two Bits", "TwoBits"),
    }
    .ToDictionary(x => x.displayName);
}