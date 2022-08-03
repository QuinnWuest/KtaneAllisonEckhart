using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using Rnd = UnityEngine.Random;
using KModkit;

public partial class AllisonEckhartScript : MonoBehaviour
{
    public KMBombModule Module;
    public KMBombInfo BombInfo;
    public KMAudio Audio;

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;

    private void Start()
    {
        _moduleId = _moduleIdCounter++;
        if (transform.parent != null && !Application.isEditor)
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                var gameObject = transform.parent.GetChild(i).gameObject;
                var comp = gameObject.GetComponent<KMBombModule>();
                ProcessModule(comp);
            }
        }
    }

    private void ProcessModule(KMBombModule module)
    {
        var moduleType = module.ModuleType;
        var name = module.ModuleDisplayName;
    }
}
