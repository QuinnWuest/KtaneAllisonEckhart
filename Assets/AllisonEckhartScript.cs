using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using Rnd = UnityEngine.Random;
using KModkit;

public class AllisonEckhartScript : MonoBehaviour
{
    public KMBombModule Module;
    public KMBombInfo BombInfo;
    public KMAudio Audio;

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;
    private static bool alreadyRan = false;

    private void Start()
    {
        _moduleId = _moduleIdCounter++;

        if (alreadyRan)
            return;
        if (transform.parent != null && !Application.isEditor)
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                var gameObject = transform.parent.GetChild(i).gameObject;
                var comp = gameObject.GetComponent<KMBombModule>();
                ProcessModule(comp);
            }
        }
        alreadyRan = true;
    }
    private void OnDestroy() {
        alreadyRan = false;
    }

    private void ProcessModule(KMBombModule module)
    {
        var moduleType = module.ModuleType;
        var name = module.ModuleDisplayName;
        
        if (!Data.data.ContainsKey(name))
        {
            LogHidden("Module name {0} not found in data dictionary.", name);
            return;
        }
        ModuleInfo info = Data.data[name];


        TextMesh[] usedMeshes = info.GetTextMeshes(module);
        foreach (TextMesh t in usedMeshes)
            t.text = "Allison Eckhart";
    }

    private void Log(string msg, params object[] args)
    {
        Debug.LogFormat("[Allison Eckhart #{0}] {1}", msg, string.Format(msg, args));
    }
    private void LogHidden(string msg, params object[] args)
    {
        Debug.LogFormat("<Allison Eckhart #{0}> {1}", msg, string.Format(msg, args));
    }
}
