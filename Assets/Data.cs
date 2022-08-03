using System;
using System.Collections.Generic;
using System.Linq;

public static class Data
{

    public static Dictionary<string, ModuleInfo> data = new ModuleInfo[]
    {
        new ModuleInfoAll("The Simpleton", "TheSimpletonScript"),


    }.ToDictionary(x => x.displayName);



}