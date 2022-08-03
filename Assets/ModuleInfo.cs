using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ModuleInfo {

    public string displayName;
    public string scriptName;
    public ModuleInfo(string displayName, string scriptName)
    {
        this.displayName = displayName;
        this.scriptName = scriptName;
    }
    public abstract TextMesh[] GetTextMeshes(MonoBehaviour module);
}

public class ModuleInfoInclude : ModuleInfo
{
    private string[] _includes;
    public ModuleInfoInclude(string displayName, string scriptName, string[] includes) : base(displayName, scriptName)
    { _includes = includes; }
    public override TextMesh[] GetTextMeshes(MonoBehaviour module)
    {
        return module.GetComponentsInChildren<TextMesh>().Where(t => _includes.Contains(t.name)).ToArray();
    }
}

public class ModuleInfoExcept : ModuleInfo
{
    private string[] _except;
    public ModuleInfoExcept(string displayName, string scriptName, string[] except) : base(displayName, scriptName)
    { _except = except; }
    public override TextMesh[] GetTextMeshes(MonoBehaviour module)
    {
        return module.GetComponentsInChildren<TextMesh>().Where(t => !_except.Contains(t.name)).ToArray();
    }
}
public class ModuleInfoAll : ModuleInfo
{
    public ModuleInfoAll(string displayName, string scriptName) : base(displayName, scriptName) { }
    public override TextMesh[] GetTextMeshes(MonoBehaviour module)
    {
        return module.GetComponentsInChildren<TextMesh>();
    }
}



