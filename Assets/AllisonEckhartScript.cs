using KModkit;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllisonEckhartScript : MonoBehaviour
{
    public KMBombModule Module;
    public KMBombInfo BombInfo;
    public KMAudio Audio;

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;
    private static bool alreadyRan = false;
    private static List<KMBombModule> _foundMods = new List<KMBombModule>();

    private static readonly string[] _prompts = new string[]
    {

    };

    private void Start()
    {
        _moduleId = _moduleIdCounter++;
        Generate();
    }

    private void Generate()
    {
        if (alreadyRan)
            return;
        string sn = BombInfo.GetSerialNumber();
        KMBombModule[] mods = FindObjectsOfType<KMBombModule>().Where(x => x.GetComponent<KMBombInfo>() != null && x.GetComponent<KMBombInfo>().GetSerialNumber() == sn).ToArray();
        for (int i = 0; i < mods.Length; i++)
        {
            var name = mods[i].ModuleDisplayName;
            if (!Data.data.ContainsKey(name))
                Debug.LogFormat("<Allison Eckhart #{0}> Module name {1} not found in data dictionary.", _moduleId, name);
            else
            {
                _foundMods.Add(mods[i]);
                ProcessModule(mods[i]);
            }
        }
        Debug.LogFormat("[Allison Eckhart #{0}] Found {1} mods.", _moduleId, _foundMods.Count);
        alreadyRan = true;
    }

    private void OnDestroy()
    {
        alreadyRan = false;
        _foundMods = new List<KMBombModule>();
    }

    private void ProcessModule(KMBombModule module)
    {
        ModuleInfo info = Data.data[module.ModuleDisplayName];
        TextMesh[] usedMeshes = info.GetTextMeshes(module);
        switch (module.ModuleDisplayName)
        {
            case "Accumulation": SetText(usedMeshes[11], "ALLISON", 0.5f, 0.5f); SetText(usedMeshes[12], "ECKHART", 0.5f, 0.5f); break;
            case "Algebra": SetText(usedMeshes[13], "ALLISON", 0.7f, 0.7f); SetText(usedMeshes[12], "ECKHART", 0.7f, 0.7f); break;
            case "Benedict Cumberbatch": SetText(usedMeshes[26], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Bitwise Operations": SetText(usedMeshes[9], "ALLISON\nECKHART", 0.6f, 0.4f); break;
            case "Bitmaps": SetText(usedMeshes[0], "ALLI", 0.5f, 0.5f); SetText(usedMeshes[1], "SON", 0.5f, 0.5f); SetText(usedMeshes[2], "ECK", 0.5f, 0.5f); SetText(usedMeshes[3], "HART", 0.5f, 0.5f); break;
            case "Blind Maze": SetText(usedMeshes[4], "ALLISON\nECKHART", 0.7f, 0.8f); break;
            case "Blockbusters": SetText(usedMeshes[0], "ALLISON ECKHART", 0.6f, 0.6f); SetText(usedMeshes[1], "ALLISON ECKHART", 0.6f, 0.6f); break;
            case "Broken Buttons": SetText(usedMeshes[1], "ALLISON", 0.7f, 0.7f); SetText(usedMeshes[0], "ECKHART", 0.7f, 0.7f); break;
            case "Calculus": SetText(usedMeshes[2], "ALLISON\nECKHART", 1f, 1f); break;
            case "Challenge & Contact": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Cheap Checkout": SetText(usedMeshes[4], "ALLISON", 0.6f, 0.6f); SetText(usedMeshes[13], "ECKHART", 0.6f, 0.6f); break;
            case "Combination Lock": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.5f, 0.4f); break;
            case "Connection Check": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.4f, 0.6f); break;
            case "Curriculum": SetText(usedMeshes[2], "ALLISON ECKHART", 0.5f, 0.5f); break;
            case "Double Expert": SetText(usedMeshes[0], "ALLISON", 0.6f, 0.6f); SetText(usedMeshes[1], "ECKHART", 0.6f, 0.6f); break;
            case "Dragon Energy": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.8f, 0.6f); break;
            case "Equations X": SetText(usedMeshes[14], "ALLISON\nECKHART", 0.6f, 0.5f); break;
            case "Fast Math": SetText(usedMeshes[12], "ALLISON\nECKHART", 0.4f, 0.5f); break;
            case "FizzBuzz": SetText(usedMeshes[3], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Flags": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Gridlock": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Ice Cream": SetText(usedMeshes[4], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Identity Parade": SetText(usedMeshes[8], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "LEGOs": SetText(usedMeshes[3], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Logic": SetText(usedMeshes[16], "ALLISON\nECKHART", 0.6f, 0.5f); break;
            case "Lunchtime": SetText(usedMeshes[1], "ALLISON ECKHART", 0.7f, 0.7f); break;
            case "Mashematics": SetText(usedMeshes[7], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Matchematics": SetText(usedMeshes[3], "ALLISON", 0.9f, 0.9f); SetText(usedMeshes[4], "ECKHART", 0.9f, 0.9f); break;
            case "Maze Scrambler": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.9f); break;
            case "Modulus Manipulation": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Morse War": SetText(usedMeshes[1], "ALLISON", 0.9f, 0.9f); SetText(usedMeshes[0], "ECKHART", 0.9f, 0.9f); break;
            case "Murder": SetText(usedMeshes[9], "ALLISON ECKHART", 0.5f, 0.5f); break;
            case "Neutralization": SetText(usedMeshes[10], "ALLISON ECKHART", 0.6f, 0.6f); break;
            case "Nonogram": SetText(usedMeshes[0], "ALLISON ECKHART", 0.5f, 0.5f); break;
            case "Plumbing": SetText(usedMeshes[0], "ALLISON ECKHART", 0.4f, 0.6f); break;
            case "Retirement": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Skewed Slots": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Sonic the Hedgehog": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Subscribe to Pewdiepie": SetText(usedMeshes[2], "ALLISON", 0.9f, 0.9f); SetText(usedMeshes[5], "ECKHART", 0.9f, 0.9f); break;
            case "Symbolic Password": SetText(usedMeshes[0], "ALLISON ECKHART", 0.5f, 0.5f); break;
            case "The Colored Maze": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.6f); break;
            case "The Dealmaker": SetText(usedMeshes[1], "ALLISON", 0.9f, 0.9f); SetText(usedMeshes[2], "ECKHART", 0.9f, 0.9f); break;
            case "The Festive Jukebox": SetText(usedMeshes[3], "ALLISON\nECKHART", 1.1f, 1.1f); break;
            case "The Jukebox": SetText(usedMeshes[3], "ALLISON\nECKHART", .9f, .9f); break;
            case "The Rule": SetText(usedMeshes[1], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "The Swan": SetText(usedMeshes[12], "ALLISON\nECKHART", 0.6f, 0.7f); break;
            case "Toon Enough": SetText(usedMeshes[0], "ALLISON", 0.7f, 0.7f); SetText(usedMeshes[1], "ECKHART", 0.7f, 0.7f); break;
            case "Two Bits": SetText(usedMeshes[11], "ALLISON", 0.7f, 0.7f); SetText(usedMeshes[12], "ECKHART", 0.7f, 0.7f); break;
            default: for (int i = 0; i < usedMeshes.Length; i++) SetText(usedMeshes[i], i.ToString(), 0.5f, 0.5f); break;
        }
    }

    private void SetText(TextMesh tMesh, string text, float scaleX, float scaleY)
    {
        tMesh.text = text;
        tMesh.gameObject.transform.localScale = new Vector3(tMesh.gameObject.transform.localScale.x * scaleX, tMesh.gameObject.transform.localScale.y * scaleY, tMesh.gameObject.transform.localScale.z);
    }
}
