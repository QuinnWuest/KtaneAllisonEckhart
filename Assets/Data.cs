using System;
using System.Collections.Generic;
using System.Linq;

public static class Data
{
    public static Dictionary<string, ModuleInfo> data = new ModuleInfo[]
    {
        new ModuleInfoAll("Accumulation", "accumulation"),
        new ModuleInfoAll("Algebra", "algebra"),
        new ModuleInfoAll("Benedict Cumberbatch", "benedictCumberbatch"),
        new ModuleInfoAll("Bitmaps", "BitmapsModule"),
        new ModuleInfoAll("Bitwise Operations", "BitOps"),
        new ModuleInfoAll("Blind Maze", "BlindMaze"),
        new ModuleInfoAll("Blockbusters", "blockbusters"),
        new ModuleInfoAll("Broken Buttons", "BrokenButtonsModule"),
        new ModuleInfoAll("Calculus", "calcModule"),
        new ModuleInfoAll("Challenge & Contact", "challengeAndContact"),
        new ModuleInfoAll("Cheap Checkout", "CheapCheckoutModule"),
        new ModuleInfoAll("Combination Lock", "combinationLock"),
        new ModuleInfoAll("Connection Check", "graphModule"),
        new ModuleInfoAll("Curriculum", "curriculum"),
        new ModuleInfoAll("Double Expert", "doubleExpert"),
        new ModuleInfoAll("Dragon Energy", "dragonEnergy"),
        new ModuleInfoAll("Equations X", "equationsXModule"),
        new ModuleInfoAll("Fast Math", "fastMath"),
        new ModuleInfoAll("FizzBuzz", "fizzBuzzModule"),
        new ModuleInfoAll("Flags", "FlagsModule"),
        new ModuleInfoAll("Gridlock", "GridlockModule"),
        new ModuleInfoAll("Ice Cream", "iceCreamModule"),
        new ModuleInfoAll("Identity Parade", "identityParade"),
        new ModuleInfoAll("LEGOs", "LEGOModule"),
        new ModuleInfoAll("Logic", "Logic"),
        new ModuleInfoAll("Lunchtime", "lunchtime"),
        new ModuleInfoAll("Mashematics", "mashematics"),
        new ModuleInfoAll("Matchematics", "matchematics"),
        new ModuleInfoAll("Maze Scrambler", "MazeScrambler"),
        new ModuleInfoAll("Modulus Manipulation", "modulusManipulation"),
        new ModuleInfoAll("Morse War", "MorseWar"),
        new ModuleInfoAll("Murder", "murder"),
        new ModuleInfoAll("Neutralization", "neutralization"),
        new ModuleInfoAll("Nonogram", "NonogramModule"),
        new ModuleInfoAll("Plumbing", "MazeV2"),
        new ModuleInfoAll("Retirement", "retirement"),
        new ModuleInfoAll("Skewed Slots", "SkewedSlotsModule"),
        new ModuleInfoAll("Sonic the Hedgehog", "sonic"),
        new ModuleInfoAll("Subscribe to Pewdiepie", "subscribeToPewdiepie"),
        new ModuleInfoAll("Symbolic Password", "symbolicPasswordModule"),
        new ModuleInfoAll("The Colored Maze", "coloredMaze"),
        new ModuleInfoAll("The Dealmaker", "thedealmaker"),
        new ModuleInfoAll("The Festive Jukebox", "festiveJukebox"),
        new ModuleInfoAll("The Jukebox", "jukebox"),
        new ModuleInfoAll("The Rule", "theRule"),
        new ModuleInfoAll("The Swan", "theSwan"),
        new ModuleInfoAll("Toon Enough", "toonEnough"),
        new ModuleInfoAll("Two Bits", "TwoBits"),
    }
    .ToDictionary(x => x.displayName);
}