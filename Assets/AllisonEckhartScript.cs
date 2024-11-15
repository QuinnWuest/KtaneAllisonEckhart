using KModkit;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class AllisonEckhartScript : MonoBehaviour
{
    public KMBombModule Module;
    public KMBombInfo Bomb;
    public KMAudio Audio;

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;
    private static bool alreadyRan = false;
    private static List<KMBombModule> _foundMods = new List<KMBombModule>();
    private bool debugMode = true;
    private int solution;

    private void Start()
    {
        _moduleId = _moduleIdCounter++;
        GenerateAllisonEckhart();
        //GeneratePrompt();
    }

    public class EWNode {
        public string Text;
        public int? Value;
        public EWNode[] Children;

        public EWNode(string text, int? value, EWNode[] children) {
            Text = text ?? "";
            Value = value;
            Children = children ?? new EWNode[] { };
        }
    }

    private void GeneratePrompt(int count) 
    {
        int limit = count;

        string[] starts = "INPUT,COMPUTE,CALCULATE,PUNCH IN,TYPE IN,DETERMINE,EVALUATE,QUANTIFY".Split(',');
        int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 30, 40, 50, 60, 70, 80, 90 };
        string[] numberWords = "ZERO,ONE,TWO,THREE,FOUR,FIVE,SIX,SEVEN,EIGHT,NINE,TEN,ELEVEN,TWELVE,THIRTEEN,FOURTEEN,FIFTEEN,SIXTEEN,SEVENTEEN,EIGHTEEN,NINETEEN,TWENTY,THIRTY,FORTY,FIFTY,SIXTY,SEVENTY,EIGHTY,NINETY".Split(',');
    
        string promptText = starts.PickRandom();
        List<int> promptValues = new List<int> {  };

        //using this edgework for testing, before the edgework detection gets added: 
        //1 mod (Allison Eckhart); 2in2 [par ser] [ser] [] #=J05ZD3

        int mods = 1; //Bomb.GetModuleIDs().Count();
        int distMods = 1; //DistinctCount(Bomb.GetModuleIDs());
        int uniqMods = 1; //UniqueCount(Bomb.GetModuleIDs());
        int nonNeedys = 1; //Bomb.GetSolvableModuleIDs().Count();
        int needys = 0; //modules - nonNeedyModules;

        int bats = 2; //Bomb.GetBatteryCount();
        int holders = 2; //Bomb.GetBatteryHolderCount();
        int aaBats = 0; //Bomb.GetBatteryCount(KMBombInfoExtensions.KnownBatteryType.AA);
        int dBats = 2; //Bomb.GetBatteryCount(KMBombInfoExtensions.KnownBatteryType.D);

        int inds = 0; //Bomb.GetIndicators().Count();
        int lits = 0; //Bomb.GetOnIndicators().Count();
        int unlits = 0; //Bomb.GetOffIndicators().Count();
        int vowelInds = 0; //Bomb.GetIndicators().Select(i => i.Intersect("AEIOU").Any());
        int vowelLits = 0; //Bomb.GetOnIndicators().Select(i => i.Intersect("AEIOU").Any());
        int vowelUnlits = 0; //Bomb.GetOffIndicators().Select(i => i.Intersect("AEIOU").Any());
        int indLetterSum = 0;
        int litLetterSum = 0;
        int unlitLetterSum = 0;
        int distIndLetterSum = 0;
        int distLitLetterSum = 0;
        int distUnlitLetterSum = 0;
        int uniqIndLetterSum = 0;
        int uniqLitLetterSum = 0;
        int uniqUnlitLetterSum = 0;
        int indVowelSum = 0;
        int litVowelSum = 0;
        int unlitVowelSum = 0;
        int indConsonantSum = 0;
        int litConsonantSum = 0;
        int unlitConsonantSum = 0;

        int ports = 3;
        int distPorts = 2;
        int uniqPorts = 1;
        int plates = 3;
        int emptyPlates = 1;
        int nonEmptyPlates = 2;
        int dvis = 0;
        int parallels = 1;
        int ps2s = 0;
        int rj45s = 0;
        int serials = 2;
        int rcas = 0;

        int firstSnDigit = 0;
        int secondSnDigit = 5;
        int? thirdSnDigit = 3;
        int lastSnDigit = 3;
        int firstSnLetter = 10;
        int secondSnLetter = 26;
        int? thirdSnLetter = 4;
        int lastSnLetter = 4;
        int? firstSnChar = null;
        int? secondSnChar = 0;
        int thirdSnChar = 5;
        int lastSnChar = 3;
        int snLetters = 3;
        int snDigits = 3;
        int snVowels = 0;
        int snConsonants = 3;
        int snEvens = 1;
        int snOdds = 2;
        int snDigitSum = 8;
        int snDistDigitSum = 8;
        int snUniqDigitSum = 8;
        int snEvenSum = 0;
        int snOddSum = 8;
        int snLetterSum = 40;
        int snDistLetterSum = 40;
        int snUniqLetterSum = 40;
        int snVowelSum = 0;
        int snConsonantSum = 40;

        EWNode aaa = new EWNode ("[[[AA] BATTERY] COUNT]", aaBats, null);
        EWNode aab = new EWNode ("[[[D] BATTERY] COUNT]", dBats, null);
        EWNode aba = new EWNode ("[[[LIT] INDICATOR] COUNT]", lits, null);
        EWNode abb = new EWNode ("[[[UNLIT] INDICATOR] COUNT]", unlits, null);
        EWNode aca = new EWNode ("[[[DISTINCT] MODULE] COUNT]", distMods, null);
        EWNode acb = new EWNode ("[[[NEEDY] MODULE] COUNT]", needys, null);
        EWNode acc = new EWNode ("[[[NON-NEEDY] MODULE] COUNT]", nonNeedys, null);
        EWNode acd = new EWNode ("[[[UNIQUE] MODULE] COUNT]", uniqMods, null);
        EWNode ada = new EWNode ("[[[DISTINCT] PORT] COUNT]", distPorts, null);
        EWNode adb = new EWNode ("[[[DVI-D] PORT] COUNT]", dvis, null);
        EWNode adc = new EWNode ("[[[PARALLEL] PORT] COUNT]", parallels, null);
        EWNode add = new EWNode ("[[[PS/2] PORT] COUNT]", ps2s, null);
        EWNode ade = new EWNode ("[[[RJ-45] PORT] COUNT]", rj45s, null);
        EWNode adf = new EWNode ("[[[SERIAL] PORT] COUNT]", serials, null);
        EWNode adg = new EWNode ("[[[STEREO RCA] PORT] COUNT]", rcas, null);
        EWNode adh = new EWNode ("[[[UNIQUE] PORT] COUNT]", uniqPorts, null);
        EWNode aea = new EWNode ("[[[EMPTY] PORT PLATE] COUNT]", emptyPlates, null);
        EWNode aeb = new EWNode ("[[[NON-EMPTY] PORT PLATE] COUNT]", nonEmptyPlates, null);
        EWNode af = new EWNode ("[[BATTERY] COUNT]", bats, null);
        EWNode ag = new EWNode ("[[BATTERY HOLDER] COUNT]", holders, null);
        EWNode ah = new EWNode ("[[INDICATOR] COUNT]", inds, null);
        EWNode ai = new EWNode ("[[MODULE] COUNT]", mods, null);
        EWNode aj = new EWNode ("[[PORT] COUNT]", ports, null);
        EWNode ak = new EWNode ("[[PORT PLATE] COUNT]", plates, null);
        EWNode alaa = new EWNode ("[[SERIAL NUMBER [[EVEN] DIGIT]] COUNT]", snEvens, null);
        EWNode alab = new EWNode ("[[SERIAL NUMBER [[ODD] DIGIT]] COUNT]", snOdds, null);
        EWNode alb = new EWNode ("[[SERIAL NUMBER [CONSONANT]] COUNT]", snConsonants, null);
        EWNode alc = new EWNode ("[[SERIAL NUMBER [DIGIT]] COUNT]", snDigits, null);
        EWNode ald = new EWNode ("[[SERIAL NUMBER [LETTER]] COUNT]", snLetters, null);
        EWNode ale = new EWNode ("[[SERIAL NUMBER [VOWEL]] COUNT]", snVowels, null);
        EWNode baa = new EWNode ("[[1ST] SERIAL NUMBER [CHARACTER]]", firstSnChar, null);
        EWNode bab = new EWNode ("[[2ND] SERIAL NUMBER [CHARACTER]]", secondSnChar, null);
        EWNode bac = new EWNode ("[[3RD] SERIAL NUMBER [CHARACTER]]", thirdSnChar, null);
        EWNode bad = new EWNode ("[[FIRST] SERIAL NUMBER [CHARACTER]]", firstSnChar, null);
        EWNode bae = new EWNode ("[[LAST] SERIAL NUMBER [CHARACTER]]", lastSnChar, null);
        EWNode baf = new EWNode ("[[SECOND] SERIAL NUMBER [CHARACTER]]", secondSnChar, null);
        EWNode bag = new EWNode ("[[THIRD] SERIAL NUMBER [CHARACTER]]", thirdSnChar, null);
        EWNode bba = new EWNode ("[[1ST] SERIAL NUMBER [DIGIT]]", firstSnDigit, null);
        EWNode bbb = new EWNode ("[[2ND] SERIAL NUMBER [DIGIT]]", secondSnDigit, null);
        EWNode bbc = new EWNode ("[[3RD] SERIAL NUMBER [DIGIT]]", thirdSnDigit, null);
        EWNode bbd = new EWNode ("[[FIRST] SERIAL NUMBER [DIGIT]]", firstSnDigit, null);
        EWNode bbe = new EWNode ("[[LAST] SERIAL NUMBER [DIGIT]]", lastSnDigit, null);
        EWNode bbf = new EWNode ("[[SECOND] SERIAL NUMBER [DIGIT]]", secondSnDigit, null);
        EWNode bbg = new EWNode ("[[THIRD] SERIAL NUMBER [DIGIT]]", thirdSnDigit, null);
        EWNode bca = new EWNode ("[[1ST] SERIAL NUMBER [LETTER IN A1Z26]]", firstSnLetter, null);
        EWNode bcb = new EWNode ("[[2ND] SERIAL NUMBER [LETTER IN A1Z26]]", secondSnLetter, null);
        EWNode bcc = new EWNode ("[[3RD] SERIAL NUMBER [LETTER IN A1Z26]]", thirdSnLetter, null);
        EWNode bcd = new EWNode ("[[FIRST] SERIAL NUMBER [LETTER IN A1Z26]]", firstSnLetter, null);
        EWNode bce = new EWNode ("[[LAST] SERIAL NUMBER [LETTER IN A1Z26]]", lastSnLetter, null);
        EWNode bcf = new EWNode ("[[SECOND] SERIAL NUMBER [LETTER IN A1Z26]]", secondSnLetter, null);
        EWNode bcg = new EWNode ("[[THIRD] SERIAL NUMBER [LETTER IN A1Z26]]", thirdSnLetter, null);
        EWNode caa = new EWNode ("[NUMBER OF [[AA] BATTERIES]]", aaBats, null);
        EWNode cab = new EWNode ("[NUMBER OF [[D] BATTERIES]]", dBats, null);
        EWNode cba = new EWNode ("[NUMBER OF [[LIT] INDICATORS]]", lits, null);
        EWNode cbb = new EWNode ("[NUMBER OF [[UNLIT] INDICATORS]]", unlits, null);
        EWNode cca = new EWNode ("[NUMBER OF [[LIT] INDICATORS CONTAINING A VOWEL]]", vowelLits, null);
        EWNode ccb = new EWNode ("[NUMBER OF [[UNLIT] INDICATORS CONTAINING A VOWEL]]", vowelUnlits, null);
        EWNode cda = new EWNode ("[NUMBER OF [[DISTINCT] MODULES]]", distMods, null);
        EWNode cdb = new EWNode ("[NUMBER OF [[NEEDY] MODULES]]", needys, null);
        EWNode cdc = new EWNode ("[NUMBER OF [[NON-NEEDY] MODULES]]", nonNeedys, null);
        EWNode cdd = new EWNode ("[NUMBER OF [[UNIQUE] MODULES]]", uniqMods, null);
        EWNode cea = new EWNode ("[NUMBER OF [[EMPTY] PORT PLATES]]", emptyPlates, null);
        EWNode ceb = new EWNode ("[NUMBER OF [[NON-EMPTY] PORT PLATES]]", nonEmptyPlates, null);
        EWNode cfa = new EWNode ("[NUMBER OF [[DISTINCT] PORTS]]", distPorts, null);
        EWNode cfb = new EWNode ("[NUMBER OF [[DVI-D] PORTS]]]", dvis, null);
        EWNode cfc = new EWNode ("[NUMBER OF [[PARALLEL] PORTS]]]", parallels, null);
        EWNode cfd = new EWNode ("[NUMBER OF [[PS/2] PORTS]]]", ps2s, null);
        EWNode cfe = new EWNode ("[NUMBER OF [[RJ-45] PORTS]]]", rj45s, null);
        EWNode cff = new EWNode ("[NUMBER OF [[SERIAL] PORTS]]]", serials, null);
        EWNode cfg = new EWNode ("[NUMBER OF [[STEREO RCA] PORTS]]]", rcas, null);
        EWNode cfh = new EWNode ("[NUMBER OF [[UNIQUE] PORTS]]", uniqPorts, null);
        EWNode cg = new EWNode ("[NUMBER OF [BATTERIES]]", bats, null);
        EWNode ch = new EWNode ("[NUMBER OF [BATTERY HOLDERS]]", holders, null);
        EWNode ci = new EWNode ("[NUMBER OF [INDICATORS]]", inds, null);
        EWNode cj = new EWNode ("[NUMBER OF [INDICATORS CONTAINING A VOWEL]]", vowelInds, null);
        EWNode ck = new EWNode ("[NUMBER OF [MODULES]]", mods, null);
        EWNode cl = new EWNode ("[NUMBER OF [PORT PLATES]]", plates, null);
        EWNode cm = new EWNode ("[NUMBER OF [PORTS]]", ports, null);
        EWNode cna = new EWNode ("[NUMBER OF [SERIAL NUMBER [CONSONANTS]]]", snConsonants, null);
        EWNode cnb = new EWNode ("[NUMBER OF [SERIAL NUMBER [DIGITS]]]", snDigits, null);
        EWNode cnc = new EWNode ("[NUMBER OF [SERIAL NUMBER [LETTERS]]]", snLetters, null);
        EWNode cnd = new EWNode ("[NUMBER OF [SERIAL NUMBER [VOWELS]]]", snVowels, null);
        EWNode da = new EWNode ("[NUMBER OF [EVEN] DIGITS FROM SERIAL NUMBER]", snEvens, null);
        EWNode db = new EWNode ("[NUMBER OF [ODD] DIGITS FROM SERIAL NUMBER]", snOdds, null);
        EWNode eaa = new EWNode ("[SUM OF [[DISTINCT] DIGITS IN SERIAL NUMBER]]", snDistDigitSum, null);
        EWNode eab = new EWNode ("[SUM OF [[UNIQUE] DIGITS IN SERIAL NUMBER]]", snUniqDigitSum, null);
        EWNode ebaaaaa = new EWNode ("[SUM OF [[[[DISTINCT] LETTERS] IN A1Z26] FROM [[LIT] INDICATORS]]]", distLitLetterSum, null);
        EWNode ebaaaab = new EWNode ("[SUM OF [[[[UNIQUE] LETTERS] IN A1Z26] FROM [[LIT] INDICATORS]]]", uniqLitLetterSum, null);
        EWNode ebaaab = new EWNode ("[SUM OF [[[CONSONANTS] IN A1Z26] FROM [[LIT] INDICATORS]]]", litConsonantSum, null);
        EWNode ebaaac = new EWNode ("[SUM OF [[[VOWELS] IN A1Z26] FROM [[LIT] INDICATORS]]]", litVowelSum, null);
        EWNode ebaab = new EWNode ("[SUM OF [[LETTERS IN A1Z26] FROM [[LIT] INDICATORS]]]", litLetterSum, null);
        EWNode ebabaaa = new EWNode ("[SUM OF [[[[DISTINCT] LETTERS] IN A1Z26] FROM [[UNLIT] INDICATORS]]]", distUnlitLetterSum, null);
        EWNode ebabaab = new EWNode ("[SUM OF [[[[UNIQUE] LETTERS] IN A1Z26] FROM [[UNLIT] INDICATORS]]]", uniqUnlitLetterSum, null);
        EWNode ebabab = new EWNode ("[SUM OF [[[CONSONANTS] IN A1Z26] FROM [[UNLIT] INDICATORS]]]", unlitConsonantSum, null);
        EWNode ebabac = new EWNode ("[SUM OF [[[VOWELS] IN A1Z26] FROM [[UNLIT] INDICATORS]]]", unlitVowelSum, null);
        EWNode ebabb = new EWNode ("[SUM OF [[LETTERS IN A1Z26] FROM [[UNLIT] INDICATORS]]]", unlitLetterSum, null);
        EWNode ebbaaa = new EWNode ("[SUM OF [[[[DISTINCT] LETTERS] IN A1Z26] FROM [INDICATORS]]]", distIndLetterSum, null);
        EWNode ebbaab = new EWNode ("[SUM OF [[[[UNIQUE] LETTERS] IN A1Z26] FROM [INDICATORS]]]", uniqIndLetterSum, null);
        EWNode ebbab = new EWNode ("[SUM OF [[[CONSONANTS] IN A1Z26] FROM [INDICATORS]]]", indConsonantSum, null);
        EWNode ebbac = new EWNode ("[SUM OF [[[VOWELS] IN A1Z26] FROM [INDICATORS]]]", indVowelSum, null);
        EWNode ebbb = new EWNode ("[SUM OF [[LETTERS IN A1Z26] FROM [INDICATORS]]]", indLetterSum, null);
        EWNode ebcaa = new EWNode ("[SUM OF [[[EVEN] DIGITS] FROM [SERIAL NUMBER]]]", snEvenSum, null);
        EWNode ebcab = new EWNode ("[SUM OF [[[ODD] DIGITS] FROM [SERIAL NUMBER]]]", snOddSum, null);
        EWNode ebcbaa = new EWNode ("[SUM OF [[[[DISTINCT] LETTERS] IN A1Z26] FROM [SERIAL NUMBER]]]", snDistLetterSum, null);
        EWNode ebcbab = new EWNode ("[SUM OF [[[[UNIQUE] LETTERS] IN A1Z26] FROM [SERIAL NUMBER]]]", snUniqLetterSum, null);
        EWNode ebcbb = new EWNode ("[SUM OF [[[CONSONANTS] IN A1Z26] FROM [SERIAL NUMBER]]]", snConsonantSum, null);
        EWNode ebcbc = new EWNode ("[SUM OF [[[LETTERS] IN A1Z26] FROM [SERIAL NUMBER]]]", snLetterSum, null);
        EWNode ebcbd = new EWNode ("[SUM OF [[[VOWELS] IN A1Z26] FROM [SERIAL NUMBER]]]", snVowelSum, null);
        EWNode ec = new EWNode ("[SUM OF [DIGITS IN SERIAL NUMBER]]", snDigitSum, null);
        EWNode eda = new EWNode ("[SUM OF [SERIAL NUMBER [CONSONANTS IN A1Z26]]]", snConsonantSum, null);
        EWNode edb = new EWNode ("[SUM OF [SERIAL NUMBER [DIGITS]]]", snDigitSum, null);
        EWNode edc = new EWNode ("[SUM OF [SERIAL NUMBER [LETTERS IN A1Z26]]]", snLetterSum, null);
        EWNode edd = new EWNode ("[SUM OF [SERIAL NUMBER [VOWELS IN A1Z26]]]", snVowelSum, null);
        EWNode ebaaaa = new EWNode (null, -1, new EWNode[] { ebaaaaa, ebaaaab });
        EWNode ebabaa = new EWNode (null, -1, new EWNode[] { ebabaaa, ebabaab });
        EWNode ebaaa = new EWNode (null, -1, new EWNode[] { ebaaaa, ebaaab, ebaaac });
        EWNode ebaba = new EWNode (null, -1, new EWNode[] { ebabaa, ebabab, ebabac });
        EWNode ebbaa = new EWNode (null, -1, new EWNode[] { ebbaaa, ebbaab });
        EWNode ebcba = new EWNode (null, -1, new EWNode[] { ebcbaa, ebcbab });
        EWNode ebaa = new EWNode (null, -1, new EWNode[] { ebaaa, ebaab });
        EWNode ebab = new EWNode (null, -1, new EWNode[] { ebaba, ebabb });
        EWNode ebba = new EWNode (null, -1, new EWNode[] { ebbaa, ebbab, ebbac });
        EWNode ebca = new EWNode (null, -1, new EWNode[] { ebcaa, ebcab });
        EWNode ebcb = new EWNode (null, -1, new EWNode[] { ebcba, ebcbb, ebcbc, ebcbd });
        EWNode ala = new EWNode (null, -1, new EWNode[] { alaa, alab });
        EWNode eba = new EWNode (null, -1, new EWNode[] { ebaa, ebab });
        EWNode ebb = new EWNode (null, -1, new EWNode[] { ebba, ebbb });
        EWNode ebc = new EWNode (null, -1, new EWNode[] { ebca, ebcb });
        EWNode aa = new EWNode (null, -1, new EWNode[] { aaa, aab });
        EWNode ab = new EWNode (null, -1, new EWNode[] { aba, abb });
        EWNode ac = new EWNode (null, -1, new EWNode[] { aca, acb, acc, acd });
        EWNode ad = new EWNode (null, -1, new EWNode[] { ada, adb, adc, add, ade, adf, adg, adh });
        EWNode ae = new EWNode (null, -1, new EWNode[] { aea, aeb });
        EWNode al = new EWNode (null, -1, new EWNode[] { ala, alb, alc, ald, ale });
        EWNode ba = new EWNode (null, -1, new EWNode[] { baa, bab, bac, bad, bae, baf, bag });
        EWNode bb = new EWNode (null, -1, new EWNode[] { bba, bbb, bbc, bbd, bbe, bbf, bbg });
        EWNode bc = new EWNode (null, -1, new EWNode[] { bca, bcb, bcc, bcd, bce, bcf, bcg });
        EWNode ca = new EWNode (null, -1, new EWNode[] { caa, cab });
        EWNode cb = new EWNode (null, -1, new EWNode[] { cba, cbb });
        EWNode cc = new EWNode (null, -1, new EWNode[] { cca, ccb });
        EWNode cd = new EWNode (null, -1, new EWNode[] { cda, cdb, cdc, cdd });
        EWNode ce = new EWNode (null, -1, new EWNode[] { cea, ceb });
        EWNode cf = new EWNode (null, -1, new EWNode[] { cfa, cfb, cfc, cfd, cfe, cff, cfg, cfh });
        EWNode cn = new EWNode (null, -1, new EWNode[] { cna, cnb, cnc, cnd });
        EWNode ea = new EWNode (null, -1, new EWNode[] { eaa, eab });
        EWNode eb = new EWNode (null, -1, new EWNode[] { eba, ebb, ebc });
        EWNode ed = new EWNode (null, -1, new EWNode[] { eda, edb, edc, edd });
        EWNode a = new EWNode (null, -1, new EWNode[] { aa, ab, ac, ad, ae, af, ag, ah, ai, aj, ak, al });
        EWNode b = new EWNode (null, -1, new EWNode[] { ba, bb, bc });
        EWNode c = new EWNode (null, -1, new EWNode[] { ca, cb, cc, cd, ce, cf, cg, ch, ci, cj, ck, cl, cm, cn });
        EWNode d = new EWNode (null, -1, new EWNode[] { da, db });
        EWNode e = new EWNode (null, -1, new EWNode[] { ea, eb, ec, ed });
        EWNode x = new EWNode (null, -1, new EWNode[] { a, b, c, d, e });

        if (limit > 3) {
            promptText += GenEWText(limit - 2);
        }

        string GenEWText(int lim) {
            TryAgain:
            var currentNode = x;
            bool good = true;
            while (currentNode.Value == -1) {
                List<EWNode> paths = new List<EWNode>();
                for (int path = 0; path < currentNode.Children.Count(); path++) {
                    if (currentNode.Children[path].Value != null) {
                        paths.add(currentNode.Children[path]);
                    }
                }
                if (paths.Count() == 1) {
                    //this is fine
                } else {
                    List<int> values = new List<int>();
                    for (int pv = 0; pv < paths.Count(); pv++) {
                        if (!values.Contains(paths[pv].Value)) {
                            values.Add(paths[pv].Value);
                            if (values.Count() > 1) { break; }
                        }
                    }
                    if (values.Count() == 1) {
                        good = false;
                        break;
                    }
                }
                currentNode = paths.PickRandom();
            }
            int numberOfAllisonEckhartsInThisEdgeworkPhrase = currentNode.Text.Split("[").Count() - 1;
            if (!good || numberOfAllisonEckhartsInThisEdgeworkPhrase > lim) {
                goto TryAgain;
            }
            promptValues.Add(currentNode.Value);
            return " " + currentNode.Text;
        }
    }

    int DistinctCount(string[] arr) 
    {
        /* this shit broken :(
        List<string> things = new List<string>();

        foreach (var thing in arr) {
            if (!arr.Contains(thing))
                things.Add(thing);
        }

        return things.Count();
        */
        return -1;
    }

    int UniqueCount(string[] arr) 
    {
        /* ditto 
        List<string> things = new List<string>();
        List<int> amounts = new List<int>();

        foreach (var thing in arr) {
            if (arr.Contains(thing)) {
                amounts[things.IndexOf(thing)]++;
            } else {
                things.Add(thing);
                amounts.Add(thing);
            }
        }

        return things.Select(i => amounts[things.IndexOf(i)] == 1);
        */
        return -1;
    }

    private void GenerateAllisonEckhart()
    {
        if (alreadyRan)
            return;
        string sn = Bomb.GetSerialNumber();
        KMBombModule[] mods = FindObjectsOfType<KMBombModule>().Where(x => x.GetComponent<KMBombInfo>() != null && x.GetComponent<KMBombInfo>().GetSerialNumber() == sn).ToArray();
        List<string> names = new List<string> {  };
        for (int i = 0; i < mods.Length; i++)
        {
            string name = mods[i].ModuleDisplayName;
            if (Data.data.ContainsKey(name))
            {
                _foundMods.Add(mods[i]);
                names.Add(name);
                if (debugMode) { ProcessModule(mods[i]); }
            }
        }
        Debug.LogFormat("<Allison Eckhart #{0}> Found {1} mods: {2}", _moduleId, _foundMods.Count, names.ToArray().Join("; "));
        while (_foundMods.Count > 10) 
        {
            int modIndex = Rnd.Range(0, _foundMods.Count);
            _foundMods.RemoveAt(modIndex);
            names.RemoveAt(modIndex);
        }
        Debug.LogFormat("[Allison Eckhart #{0}] Possessing {1} mods: {2}", _moduleId, _foundMods.Count, names.ToArray().Join("; "));
        //TODO(?): If multiple Allison Eckharts are present, divy up the supported modules among the Allison Eckharts.
        GeneratePrompt(_foundMods.Count);
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
        Debug.Log(module.ModuleDisplayName);
        switch (module.ModuleDisplayName)
        {
            case "Allison Eckhart": break;
            case "0": SetText(usedMeshes[9], "ALLISON", 0.25f, 0.5f); SetText(usedMeshes[11], "ECKHART", 0.22f, 0.5f); break;
            case "3N+1": SetText(usedMeshes[2], "ALLISON", 0.666f, 1f); SetText(usedMeshes[0], "ECKHART", 0.555f, 1f); break;
            case "A Message": SetText(usedMeshes[3], "ALLISON", 0.666f, 1f); SetText(usedMeshes[4], "ECKHART", 0.666f, 1f); break;
            case "ASCII Art": SetText(usedMeshes[36], "ALLISON\nECKHART", 0.8f, 0.5f); break;
            case "ASCII Maze": SetText(usedMeshes[6], "ALLISON", 0.2f, 0.5f); SetText(usedMeshes[5], "ECKHART", 0.1f, 0.25f); break;
            case "Accelerando": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.4f, 0.5f); break;
            case "Accumulation": SetText(usedMeshes[11], "ALLISON", 0.9f, 1f); SetText(usedMeshes[12], "ECKHART", 0.9f, 1f); break;
            case "Addition": SetText(usedMeshes[10], "ALLISON", 0.35f, 1f); SetText(usedMeshes[11], "ECKHART", 0.3f, 1f); break;
            case "Adjacent Letters": SetText(usedMeshes[1], "ALLISON\nECKHART", 0.8f, 0.8f); break;
            case "Adventure Game": SetText(usedMeshes[7], "ALLISON\nECKHART", 0.428f, 0.625f); break;
            case "Affine Cycle": SetText(usedMeshes[20], "A", 1f, 1f); SetText(usedMeshes[21], "L", 1f, 1f); SetText(usedMeshes[22], "L", 1f, 1f); SetText(usedMeshes[23], "I", 1f, 1f); SetText(usedMeshes[24], "S", 1f, 1f); SetText(usedMeshes[25], "O", 1f, 1f); SetText(usedMeshes[26], "N", 1f, 1f); SetText(usedMeshes[29], "E", 1f, 1f); SetText(usedMeshes[30], "C", 1f, 1f); SetText(usedMeshes[31], "K", 1f, 1f); SetText(usedMeshes[32], "H", 1f, 1f); SetText(usedMeshes[33], "A", 1f, 1f); SetText(usedMeshes[34], "R", 1f, 1f); SetText(usedMeshes[35], "T", 1f, 1f); break;
            case "Alchemy": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Alfa-Bravo": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.54f); break;
            case "Algebra": SetText(usedMeshes[13], "ALLISON", 0.7f, 0.7f); SetText(usedMeshes[12], "ECKHART", 0.7f, 0.7f); break;
            case "Answering Can Be Fun": SetText(usedMeshes[11], "ALLISON\nECKHART", 0.6875f, 0.555f); break;
            case "Antichamber": SetText(usedMeshes[4], "ALLISON ECKHART", 1f, 1f); break;
            case "Ars Goetia Identification": SetText(usedMeshes[38], "ALLISON\nECKHART", 0.49f, 0.49f); break;
            case "Atbash Cipher": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.696f, 0.696f); break;
            case "Audio Morse": SetText(usedMeshes[1], "ALLISON ECKHART", 1f, 1f); break;
            case "Backgrounds": SetText(usedMeshes[1], "ALLISON\nECKHART", 0.8f, 1f); break;
            case "Bartending": SetText(usedMeshes[5], "ALLISON", 0.909f, 1f); SetText(usedMeshes[6], "ECKHART", 0.882f, 1f); break;
            case "Base-1": SetText(usedMeshes[9], "ALLISON\nECKHART", 0.429f, 0.5f); break;
            case "Basic Morse": SetText(usedMeshes[11], "A", 1f, 1f); SetText(usedMeshes[12], "L", 1f, 1f); SetText(usedMeshes[13], "L", 1f, 1f); SetText(usedMeshes[14], "I", 1f, 1f); SetText(usedMeshes[15], "S", 1f, 1f); SetText(usedMeshes[16], "O", 1f, 1f); SetText(usedMeshes[17], "N", 1f, 1f); SetText(usedMeshes[19], "E", 1f, 1f); SetText(usedMeshes[20], "C", 1f, 1f); SetText(usedMeshes[21], "K", 1f, 1f); SetText(usedMeshes[22], "H", 1f, 1f); SetText(usedMeshes[23], "A", 1f, 1f); SetText(usedMeshes[24], "R", 1f, 1f); SetText(usedMeshes[25], "T", 1f, 1f); break;
            case "Benedict Cumberbatch": SetText(usedMeshes[26], "ALLISON\nECKHART", 0.833f, 0.448f); break;
            case "Binary Buttons": SetText(usedMeshes[5], "ALLISON\nECKHART", 1f, 0.486f); break;
            case "Binary Cipher": SetText(usedMeshes[1], "ALLISON", 0.25f, 1f); SetText(usedMeshes[2], "ECKHART", 0.24f, 1f); break;
            case "Binary Grid": SetText(usedMeshes[25], "ALLISON", 1f, 1f); SetText(usedMeshes[26], "ECKHART", 1f, 1f); break;
            case "Binary Morse": SetText(usedMeshes[12], "ALLISON", 0.18f, 1f); SetText(usedMeshes[13], "ECKHART", 0.18f, 1f); break;
            case "Binary": SetText(usedMeshes[5], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Bitmaps": SetText(usedMeshes[0], "ALLI", 0.5f, 0.5f); SetText(usedMeshes[1], "SON", 0.5f, 0.5f); SetText(usedMeshes[2], "ECK", 0.5f, 0.5f); SetText(usedMeshes[3], "HART", 0.5f, 0.5f); break;
            case "Bitwise Operations": SetText(usedMeshes[9], "ALLISON\nECKHART", 0.6f, 0.4f); break;
            case "Blackjack": SetText(usedMeshes[0], "ALLI", 1f, 1f); SetText(usedMeshes[1], "SON", 1f, 1f); SetText(usedMeshes[2], "ECK", 1f, 1f); SetText(usedMeshes[3], "HART", 1f, 1f); break;
            case "Blank Card": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.6f, 0.4f); break;
            case "Blaseball": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 0.555f); break;
            case "Blind Maze": SetText(usedMeshes[4], "ALLISON\nECKHART", 0.7f, 0.8f); break;
            case "Blockbusters": SetText(usedMeshes[0], "ALLISON ECKHART", 0.6f, 0.6f); SetText(usedMeshes[1], "ALLISON ECKHART", 0.6f, 0.6f); break;
            case "Bomb It!": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 0.642f); break;
            case "Bone Apple Tea": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.15f, 0.4f); break;
            case "Boolean Maze": SetText(usedMeshes[5], "ALLISON", 1f, 1f); SetText(usedMeshes[6], "ECKHART", 1f, 1f); break;
            case "Boomdas": SetText(usedMeshes[9], "ALLISON\nECKHART", 0.833f, 1f); break;
            case "Boozlesnap": SetText(usedMeshes[0], "ALLISON\nECKHART", -0.2f, -0.28f); break;
            case "Bowling": SetText(usedMeshes[6], "ALLISON\nECKHART", 0.8f, 0.6f); break;
            case "Boxing": SetText(usedMeshes[1], "A LLISON\nECKHART", 1f, 0.5f); break;
            case "Breaktime": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Bridge": SetText(usedMeshes[14], "ALLISON\nECKHART", 1f, 1f); break;
            case "Broken Buttons": SetText(usedMeshes[1], "ALLISON", 0.7f, 0.7f); SetText(usedMeshes[0], "ECKHART", 0.7f, 0.7f); break;
            case "Button Order": SetText(usedMeshes[0], "ALLI5ON", 0.211f, 1f); SetText(usedMeshes[1], "ECKHART", 0.188f, 1f); break;
            case "CA-RPS": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Caesar Cycle": SetText(usedMeshes[20], "A", 1f, 1f); SetText(usedMeshes[21], "L", 1f, 1f); SetText(usedMeshes[22], "L", 1f, 1f); SetText(usedMeshes[23], "I", 1f, 1f); SetText(usedMeshes[24], "S", 1f, 1f); SetText(usedMeshes[25], "O", 1f, 1f); SetText(usedMeshes[26], "N", 1f, 1f); SetText(usedMeshes[29], "E", 1f, 1f); SetText(usedMeshes[30], "C", 1f, 1f); SetText(usedMeshes[31], "K", 1f, 1f); SetText(usedMeshes[32], "H", 1f, 1f); SetText(usedMeshes[33], "A", 1f, 1f); SetText(usedMeshes[34], "R", 1f, 1f); SetText(usedMeshes[35], "T", 1f, 1f); break;
            case "Calculus": SetText(usedMeshes[2], "ALLISON\nECKHART", 1f, 1f); break;
           
            case "Catchphrase": SetText(usedMeshes[12], "ALLISON", 0.172f, 1f); SetText(usedMeshes[11], "ECKHART", 0.15f, 1f); break;
            case "Challenge & Contact": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Chaotic Countdown": SetText(usedMeshes[14], "ALLI", 0.361f, 0.5f); SetText(usedMeshes[15], "SON", 0.361f, 0.5f); SetText(usedMeshes[16], "ECK", 0.361f, 0.5f); SetText(usedMeshes[17], "HART", 0.274f, 0.5f); break;
            case "Character Slots": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 0.375f); break;
            case "Cheap Checkout": SetText(usedMeshes[4], "ALLISON", 0.6f, 0.6f); SetText(usedMeshes[13], "ECKHART", 0.6f, 0.6f); break;
            case "Cheep Checkout": SetText(usedMeshes[3], "ALLISON", 1f, 1f); SetText(usedMeshes[4], "ECKHART", 1f, 1f); break;
            case "Chessword": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.64f, 0.4f); break;
            case "Chinese Strokes": SetText(usedMeshes[2], "ALLISON", 0.3125f, 1f); SetText(usedMeshes[3], "ECKHART", 0.25f, 1f); break;
            case "Chinese Zodiac": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 1f); break;
            case "Chord Progressions": SetText(usedMeshes[3], "ALLISON\nECKHART", 0.866f, 1f); break;
            case "Cipher Machine": SetText(usedMeshes[34], "ALLISON", 0.22f, 1f); SetText(usedMeshes[35], "ECKHART", 0.22f, 1f); break;
            case "Cistercian Numbers": SetText(usedMeshes[8], "ALLISON", 0.25f, 1f); SetText(usedMeshes[13], "ECKHART", 0.25f, 1f); break;
            case "Coffeebucks": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 1f); break;
            case "Colo(u)r Talk": SetText(usedMeshes[1], "\n\nALLISON\nECKHART", 0.7f, 0.25f); break;
            case "Color Math": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.781f, 0.45f); break;
            case "Color One Two": SetText(usedMeshes[0], "ALLISON", 0.286f, 1f); SetText(usedMeshes[1], "ECKHART", 0.286f, 1f); break;
            case "Color-Cycle Button": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 0.425f); break;
            case "Colorful Dials": SetText(usedMeshes[39], "ALLISON\nECKHART", 0.6f, 0.5f); break;
            case "Colors Maximization": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 0.6f); break;
            case "Colour Code": SetText(usedMeshes[10], "ALLISON", 0.25f, 1f); SetText(usedMeshes[11], "ECKHART", 0.325f, 1f); break;
            case "Combination Lock": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.5f, 0.4f); break;
            case "Connection Check": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.4f, 0.6f); break;
            case "Cooking": SetText(usedMeshes[8], "\nALLISON\nECKHART", 0.56f, 0.383f); break;
            case "Cosmic": SetText(usedMeshes[2], "ALLISON", 0.666f, 1f); SetText(usedMeshes[0], "ECKHART", 0.555f, 1f); break;
            case "Critters": SetText(usedMeshes[1], "ALLISON\nECKHART", 0.357f, 0.266f); break;
            case "Cruel Binary": SetText(usedMeshes[3], "ALLISON", 1f, 1f); SetText(usedMeshes[4], "ECKHART", 1f, 1f); break;
            case "Cruel Boolean Maze": SetText(usedMeshes[5], "ALLISON", 1f, 1f); SetText(usedMeshes[6], "ECKHART", 1f, 1f); break;
            case "Cruel Colour Flash": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 0.8f, 1f); break;
            case "Cruel Modulo": SetText(usedMeshes[13], "ALLISON", 0.333f, 1f); SetText(usedMeshes[14], "ECKHART", 0.333f, 1f); break;
            case "Cryptic Password": SetText(usedMeshes[6], "ALLISON\nECKHART", 0.875f, 0.475f); break;
            case "Crypto Market": SetText(usedMeshes[0], "ALLISON", 0.632f, 0.8f); SetText(usedMeshes[1], "ECKHART", 0.789f, 1f); break;
            case "Currents": SetText(usedMeshes[8], "ALLISON\nECKHART", 0.837f, 0.482f); break;
            case "Curriculum": SetText(usedMeshes[2], "ALLISON ECKHART", 0.5f, 0.5f); break;
            case "Customer Identification": SetText(usedMeshes[40], "ALLISON\nECKHART", 0.555f, 0.5f); break;
            case "Daylight Directions": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.75f, 0.5f); break;
            case "Deck Creating": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.764f, 0.5f); break;
            case "Dice Cipher": SetText(usedMeshes[0], "ALLISON", 0.7f, 1f); SetText(usedMeshes[1], "ECKHART", 0.866f, 1f); break;
            case "Dictation": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.8f, 0.4f); break;
            case "Digital Dials": SetText(usedMeshes[37], "ALLISON\nECKHART", 0.6f, 0.5f); break;
            case "Digital Grid": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[26], "ECKHART", 1f, 1f); break;
            case "Discolour Flash": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Divisible Numbers": SetText(usedMeshes[1], "ALLISON", 0.514f, 1f); SetText(usedMeshes[2], "ECKHART", 0.514f, 1f); break;
            case "Dominoes": SetText(usedMeshes[0], "ALLISON ECKHART", 0.7f, 1f); break;
            case "Double Color": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.826f, 0.6f); break;
            case "Double Digits": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.8f, 0.5f); break;
            case "Double Expert": SetText(usedMeshes[0], "ALLISON", 0.6f, 0.6f); SetText(usedMeshes[1], "ECKHART", 0.6f, 0.6f); break;
            case "Double Pitch": SetText(usedMeshes[16], "ALLISON", 0.222f, 1f); SetText(usedMeshes[18], "ECKHART", 0.167f, 1f); break;
            case "Dragon Energy": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.8f, 0.6f); break;
            case "Dual Sequences": SetText(usedMeshes[8], "ALLISON\nECKHART", 0.171f, 0.357f); break;
            case "Eight": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.48f); break;
            case "Emotiguy Identification": SetText(usedMeshes[40], "ALLISON\nECKHART", 0.557f, 0.6f); break;
            case "Encrypted Dice": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 0.5f); break;
            case "Encrypted Equations": SetText(usedMeshes[0], "ALLISON", 0.267f, 1f); SetText(usedMeshes[1], "ECKHART", 0.267f, 1f); break;
            case "Encrypted Morse": SetText(usedMeshes[0], "ALLISON", 0.2f, 0.2f); SetText(usedMeshes[1], "ECKHART", 0.2f, 0.2f); break;
            case "Encryption Lingo": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.765f, 0.667f); break;
            case "English Entries": SetText(usedMeshes[1], "ALLISON", 0.4f, 1f); SetText(usedMeshes[2], "ECKHART", 0.34f, 1f); break;
            case "Equations X": SetText(usedMeshes[14], "ALLISON\nECKHART", 0.6f, 0.5f); break;
            case "Error Codes": SetText(usedMeshes[13], "ALLISON\nECKHART", 0.8f, 1f); break;
            case "Face Recognition": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 0.488f); break;
            case "Factory Code": SetText(usedMeshes[4], "ALLISON\nECKHART", 1f, 0.5f); break;
            case "Fast Math": SetText(usedMeshes[12], "ALLISON\nECKHART", 0.4f, 0.5f); break;
            case "Faulty 14 Segment Display": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.783f, 0.901f); break;
            case "Faulty Accelerando": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.4f, 0.5f); break;
            case "Faulty Binary": SetText(usedMeshes[5], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Faulty Digital Root": SetText(usedMeshes[2], "ALLISON", 0.625f, 1f); SetText(usedMeshes[3], "ECKHART", 0.625f, 1f); break;
            case "Feature Cryptography": SetText(usedMeshes[7], "ALLISON", 0.1125f, 0.5f); SetText(usedMeshes[8], "ECKHART", 0.1125f, 0.5f); break;
            case "Fishing": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.6f, 0.5f); break;
            case "Fitting In": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.606f, 0.4f); break;
            case "FizzBuzz": SetText(usedMeshes[3], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Flags": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Flavor Text": SetText(usedMeshes[0], "ALLISON", 0.38f, 1f); SetText(usedMeshes[1], "ECKHART", 0.34f, 1f); break;
            case "Fruits": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.715f, 0.5f); break;
            case "Functions": SetText(usedMeshes[10], "ALLISON", 0.25f, 1f); SetText(usedMeshes[11], "ECKHART", 0.25f, 1f); break;
            case "Game of Life Cruel": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Game of Life Simple": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Generated Maze": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.3f, 0.5f); break;
            case "Genetic Sequence": SetText(usedMeshes[4], "ALLISON", 0.733f, 1f); SetText(usedMeshes[5], "ECKHART", 0.733f, 1f); break;
            case "Geometry Dash": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 0.9f, 1f); break;
            case "Geometry": SetText(usedMeshes[2], "ALLISON\nECKHART", 1f, 1f); break;
            case "Golf": SetText(usedMeshes[2], "ALLISON\nECKHART", 1f, 1f); break;
            case "Greek Calculus": SetText(usedMeshes[11], "ALLISON", 1f, 1f); SetText(usedMeshes[17], "ECKHART", 1f, 1f); break;
            case "Greek Letter Grid": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Grid Matching": SetText(usedMeshes[5], "ALLISON", 1f, 1f); SetText(usedMeshes[4], "ECKHART", 1f, 1f); break;
            case "Gridlock": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Grocery Store": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Hereditary Base Notation": SetText(usedMeshes[16], "ALLISON", 1f, 1f); SetText(usedMeshes[17], "ECKHART", 1f, 1f); break;
            case "Hertz": SetText(usedMeshes[5], "ALLISON", 1f, 1f); SetText(usedMeshes[6], "ECKHART", 1f, 1f); break;
            case "Hill Cycle": SetText(usedMeshes[22], "A", 1f, 1f); SetText(usedMeshes[23], "L", 1f, 1f); SetText(usedMeshes[24], "L", 1f, 1f); SetText(usedMeshes[25], "I", 1f, 1f); SetText(usedMeshes[26], "S", 1f, 1f); SetText(usedMeshes[27], "O", 1f, 1f); SetText(usedMeshes[28], "N", 1f, 1f); SetText(usedMeshes[31], "E", 1f, 1f); SetText(usedMeshes[32], "C", 1f, 1f); SetText(usedMeshes[33], "K", 1f, 1f); SetText(usedMeshes[34], "H", 1f, 1f); SetText(usedMeshes[35], "A", 1f, 1f); SetText(usedMeshes[36], "R", 1f, 1f); SetText(usedMeshes[37], "T", 1f, 1f); break;
            case "Hold On": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Homophones": SetText(usedMeshes[5], "ALLISON\nECKHART", 1f, 1f); break;
            case "Human Resources": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "I'm Not a Robot": SetText(usedMeshes[3], "ALLISON", 1f, 1f); SetText(usedMeshes[5], "ECKHART", 1f, 1f); SetText(usedMeshes[1], "ALLISON ECKHART", 1f, 1f); break;
            case "Ice Cream": SetText(usedMeshes[4], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Icon Reveal": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 1f); break;
            case "Identification Crisis": SetText(usedMeshes[37], "ALLISON\nECKHART", 1f, 1f); break;
            case "Identifying Soulless": SetText(usedMeshes[40], "ALLISON\nECKHART", 1f, 1f); break;
            case "Identity Parade": SetText(usedMeshes[8], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Increasing Indices": SetText(usedMeshes[9], "ALLISON ECKHART", 1f, 1f); break;
            case "Ingredients": SetText(usedMeshes[3], "ALLISON", 1f, 1f); SetText(usedMeshes[4], "ECKHART", 1f, 1f); break;
            case "Insanagrams": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[50], "ECKHART", 1f, 1f); break;
            case "Inside": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Iron Lung": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[4], "ECKHART", 1f, 1f); break;
            case "Jackbox.TV": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Johnson Solids": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Jumble Cycle": SetText(usedMeshes[25], "A", 1f, 1f); SetText(usedMeshes[26], "L", 1f, 1f); SetText(usedMeshes[27], "L", 1f, 1f); SetText(usedMeshes[28], "I", 1f, 1f); SetText(usedMeshes[29], "S", 1f, 1f); SetText(usedMeshes[30], "O", 1f, 1f); SetText(usedMeshes[31], "N", 1f, 1f); SetText(usedMeshes[34], "E", 1f, 1f); SetText(usedMeshes[35], "C", 1f, 1f); SetText(usedMeshes[36], "K", 1f, 1f); SetText(usedMeshes[37], "H", 1f, 1f); SetText(usedMeshes[38], "A", 1f, 1f); SetText(usedMeshes[39], "R", 1f, 1f); SetText(usedMeshes[40], "T", 1f, 1f); break;
            case "Kahoot!": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Keep Clicking": SetText(usedMeshes[3], "ALLISON ECKHART", 1f, 1f); break;
            case "Keypad Maze": SetText(usedMeshes[9], "ALLISON", 1f, 1f); SetText(usedMeshes[11], "ECKHART", 1f, 1f); break;
            case "Kyudoku": SetText(usedMeshes[36], "ALLISON ECKHART", 1f, 1f); break;
            case "LED Math": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[0], "ECKHART", 1f, 1f); break;
            case "LEGOs": SetText(usedMeshes[3], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Latin Hypercube": SetText(usedMeshes[1], "ALLI", 1f, 1f); SetText(usedMeshes[3], "SON", 1f, 1f); SetText(usedMeshes[5], "ECK", 1f, 1f); SetText(usedMeshes[7], "HART", 1f, 1f); break;
            case "Levenshtien Distance": SetText(usedMeshes[11], "ALLISON", 1f, 1f); SetText(usedMeshes[10], "ECKHART", 1f, 1f); break;
            case "Life Iteration": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Line Equations": SetText(usedMeshes[16], "ALLISON", 1f, 1f); SetText(usedMeshes[19], "ECKHART", 1f, 1f); break;
            case "Lines of Code": SetText(usedMeshes[12], "ALLISON ECKHART", 1f, 1f); break;
            case "Logic": SetText(usedMeshes[16], "ALLISON\nECKHART", 0.6f, 0.5f); break;
            case "Mahjong Quiz Easy": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Mahjong Quiz Hard": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Mahjong Quiz Scrambled": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Maintenance": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Malfunctions": SetText(usedMeshes[10], "ALLISON", 1f, 1f); SetText(usedMeshes[11], "ECKHART", 1f, 1f); break;
            case "Mashematics": SetText(usedMeshes[7], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Masked Morse": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Mastermind Cruel": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Mastermind Restricted Cruel": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Mastermind Simple": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Matchematics": SetText(usedMeshes[3], "ALLISON", 0.9f, 0.9f); SetText(usedMeshes[4], "ECKHART", 0.9f, 0.9f); break;
            case "Matchmaker": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Math 'em": SetText(usedMeshes[10], "ALLISON", 1f, 1f); SetText(usedMeshes[12], "ECKHART", 1f, 1f); break;
            case "Maze Identification": SetText(usedMeshes[4], "ALLISON\nECKHART", 1f, 1f); break;
            case "Maze Scrambler": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.9f); break;
            case "Mega Man 2": SetText(usedMeshes[10], "ALLISON ECKHART", 1f, 1f); break;
            case "Melody Memory": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Meme Review": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Meteor": SetText(usedMeshes[3], "ALLISON ECKHART", 1f, 1f); break;
            case "Meter": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Micro-Modules": SetText(usedMeshes[11], "ALLISON\nECKHART", 1f, 1f); break;
            case "Mineseeker": SetText(usedMeshes[11], "ALLISON\nECKHART", 1f, 1f); break;
            case "Minesweeper": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Mischboozl": SetText(usedMeshes[3], "ALLISON ECKHART", 1f, 1f); break;
            case "Modern Cipher": SetText(usedMeshes[2], "A", 1f, 1f); SetText(usedMeshes[3], "L", 1f, 1f); SetText(usedMeshes[4], "L", 1f, 1f); SetText(usedMeshes[5], "I", 1f, 1f); SetText(usedMeshes[6], "S", 1f, 1f); SetText(usedMeshes[7], "O", 1f, 1f); SetText(usedMeshes[8], "N", 1f, 1f); SetText(usedMeshes[9], "E", 1f, 1f); SetText(usedMeshes[10], "C", 1f, 1f); SetText(usedMeshes[11], "K", 1f, 1f); SetText(usedMeshes[12], "H", 1f, 1f); SetText(usedMeshes[13], "A", 1f, 1f); SetText(usedMeshes[14], "R", 1f, 1f); SetText(usedMeshes[15], "T", 1f, 1f); break;
            case "Modules Against Humanity": SetText(usedMeshes[4], "ALLISON", 1f, 1f); SetText(usedMeshes[5], "ECKHART", 1f, 1f); break;
            case "Modulo": SetText(usedMeshes[13], "ALLISON", 1f, 1f); SetText(usedMeshes[14], "ECKHART", 1f, 1f); break;
            case "Modulus Manipulation": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Monsplode Trading Cards": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Morse War": SetText(usedMeshes[1], "ALLISON", 0.9f, 0.9f); SetText(usedMeshes[0], "ECKHART", 0.9f, 0.9f); break;
            case "Moved": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Mssngv Wls": SetText(usedMeshes[2], "ALLISON ECKHART", 1f, 1f); break;
            case "Murder": SetText(usedMeshes[9], "ALLISON ECKHART", 0.5f, 0.5f); break;
            case "Musical Transposition": SetText(usedMeshes[2], "ALLISON ECKHART", 1f, 1f); break;
            case "My Mom": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Natures": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[0], "ECKHART", 1f, 1f); break;
            case "Negativity": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Neutralization": SetText(usedMeshes[10], "ALLISON ECKHART", 0.6f, 0.6f); break;
            case "Newline": SetText(usedMeshes[4], "ALLISON", 1f, 1f); SetText(usedMeshes[5], "ECKHART", 1f, 1f); break;
            case "Nifty Number": SetText(usedMeshes[18], "ALLISON ECKHART", 1f, 1f); break;
            case "Nonbinary Puzzle": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Nonogram": SetText(usedMeshes[0], "ALLISON ECKHART", 0.5f, 0.5f); break;
            case "Not Bitmaps": SetText(usedMeshes[0], "ALLI", 1f, 1f); SetText(usedMeshes[1], "SON", 1f, 1f); SetText(usedMeshes[2], "ECK", 1f, 1f); SetText(usedMeshes[3], "HART", 1f, 1f); break;
            case "Not Connection Check": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Not Identification": SetText(usedMeshes[40], "ALLISON ECKHART", 1f, 1f); break;
            case "Not Murder": SetText(usedMeshes[9], "ALLISON ECKHART", 1f, 1f); break;
            case "Not Number Pad": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[0], "ECKHART", 1f, 1f); break;
            case "Not Poker": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Not Symbolic Coordinates": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Not Symbolic Password": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); break;
            case "Notes": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Notre-Dame Cipher": SetText(usedMeshes[4], "ALLISON ECKHART", 1f, 1f); break;
            case "Number Sequence": SetText(usedMeshes[11], "ALLISON", 1f, 1f); SetText(usedMeshes[12], "ECKHART", 1f, 1f); break;
            case "Numbers": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "One Links to All": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Outrageous": SetText(usedMeshes[13], "ALLISON ECKHART", 1f, 1f); break;
            case "Over Kilo": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Palindromes": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Pandemonium Cipher": SetText(usedMeshes[39], "ALLISON ECKHART", 1f, 1f); break;
            case "Papa's Pizzeria": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Parliament": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Partial Derivatives": SetText(usedMeshes[12], "ALLISON", 1f, 1f); SetText(usedMeshes[13], "ECKHART", 1f, 1f); break;
            case "Pawns": SetText(usedMeshes[5], "ALLISON", 1f, 1f); SetText(usedMeshes[6], "ECKHART", 1f, 1f); break;
            case "Pickup Identification": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Pigpen Cycle": SetText(usedMeshes[21], "A", 1f, 1f); SetText(usedMeshes[22], "L", 1f, 1f); SetText(usedMeshes[23], "L", 1f, 1f); SetText(usedMeshes[24], "I", 1f, 1f); SetText(usedMeshes[25], "S", 1f, 1f); SetText(usedMeshes[26], "O", 1f, 1f); SetText(usedMeshes[27], "N", 1f, 1f); SetText(usedMeshes[30], "E", 1f, 1f); SetText(usedMeshes[31], "C", 1f, 1f); SetText(usedMeshes[32], "K", 1f, 1f); SetText(usedMeshes[33], "H", 1f, 1f); SetText(usedMeshes[34], "A", 1f, 1f); SetText(usedMeshes[35], "R", 1f, 1f); SetText(usedMeshes[36], "T", 1f, 1f); break;
            case "Pigpen Rotations": SetText(usedMeshes[27], "ALLISON", 1f, 1f); SetText(usedMeshes[26], "ECKHART", 1f, 1f); break;
            case "Pixel Number Base": SetText(usedMeshes[17], "ALLISON\nECKHART", 1f, 1f); break;
            case "Placement Roulette": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Planets": SetText(usedMeshes[11], "ALLISON\nECKHART", 1f, 1f); break;
            case "Plant Identification": SetText(usedMeshes[40], "ALLISON\nECKHART", 1f, 1f); break;
            case "Playfair Cycle": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Plumbing": SetText(usedMeshes[0], "ALLISON ECKHART", 0.4f, 0.6f); break;
            case "Poker": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Purchasing Properties": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Puzzle Identification": SetText(usedMeshes[40], "ALLISON\nECKHART", 1f, 1f); break;
            case "Quaternions": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[0], "ECKHART", 1f, 1f); break;
            case "Quaver": SetText(usedMeshes[13], "ALLISON\nECKHART", 1f, 1f); break;
            case "Quintuples": SetText(usedMeshes[30], "ALLISON\nECKHART", 1f, 1f); break;
            case "QuizBuzz": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[0], "ECKHART", 1f, 1f); break;
            case "Quote Crazy Talk End Quote": SetText(usedMeshes[2], "ALLISON\nECKHART", 1f, 1f); break;
            case "RGB Quiz": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Radiator": SetText(usedMeshes[10], "ALLISON", 1f, 1f); SetText(usedMeshes[11], "ECKHART", 1f, 1f); break;
            case "Rain Hell": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Rain": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "ReGret-B Filtering": SetText(usedMeshes[6], "A", 1f, 1f); SetText(usedMeshes[7], "L", 1f, 1f); SetText(usedMeshes[8], "L", 1f, 1f); SetText(usedMeshes[9], "I", 1f, 1f); SetText(usedMeshes[11], "S", 1f, 1f); SetText(usedMeshes[12], "O", 1f, 1f); SetText(usedMeshes[13], "N", 1f, 1f); SetText(usedMeshes[14], "E", 1f, 1f); SetText(usedMeshes[15], "C", 1f, 1f); SetText(usedMeshes[16], "K", 1f, 1f); SetText(usedMeshes[18], "H", 1f, 1f); SetText(usedMeshes[19], "A", 1f, 1f); SetText(usedMeshes[20], "R", 1f, 1f); SetText(usedMeshes[21], "T", 1f, 1f); break;
            case "ReGrettaBle Relay": SetText(usedMeshes[6], "A", 1f, 1f); SetText(usedMeshes[7], "L", 1f, 1f); SetText(usedMeshes[8], "L", 1f, 1f); SetText(usedMeshes[9], "I", 1f, 1f); SetText(usedMeshes[11], "S", 1f, 1f); SetText(usedMeshes[12], "O", 1f, 1f); SetText(usedMeshes[13], "N", 1f, 1f); SetText(usedMeshes[14], "E", 1f, 1f); SetText(usedMeshes[15], "C", 1f, 1f); SetText(usedMeshes[16], "K", 1f, 1f); SetText(usedMeshes[18], "H", 1f, 1f); SetText(usedMeshes[19], "A", 1f, 1f); SetText(usedMeshes[20], "R", 1f, 1f); SetText(usedMeshes[21], "T", 1f, 1f); break;
            case "Reading Between the Lines": SetText(usedMeshes[1], "ALLI", 1f, 1f); SetText(usedMeshes[2], "SON", 1f, 1f); SetText(usedMeshes[3], "ECK", 1f, 1f); SetText(usedMeshes[4], "HART", 1f, 1f); break;
            case "Reformed Role Reversal": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Regular Hexpressions": SetText(usedMeshes[10], "ALLISON\nECKHART", 1f, 1f); break;
            case "Resistors": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Retirement": SetText(usedMeshes[2], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Reverse Morse": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Reverse Polish Notation": SetText(usedMeshes[13], "ALLISON", 1f, 1f); SetText(usedMeshes[14], "ECKHART", 1f, 1f); break;
            case "Risky Wires": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Robit Programming": SetText(usedMeshes[5], "ALLISON\nECKHART", 1f, 1f); break;
            case "Robot Programming": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Roguelike Game": SetText(usedMeshes[6], "ALLISON\nECKHART", 1f, 1f); break;
            case "Role Reversal": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Rubik’s Clock": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "SI-HTS": SetText(usedMeshes[4], "ALLISON ECKHART", 1f, 1f); break;
            case "SQL - Basic": SetText(usedMeshes[1], "ALLISON ECKHART", 1f, 1f); break;
            case "SQL - Cruel": SetText(usedMeshes[1], "ALLISON ECKHART", 1f, 1f); break;
            case "SQL - Evil": SetText(usedMeshes[1], "ALLISON ECKHART", 1f, 1f); break;
            case "Scalar Dials": SetText(usedMeshes[37], "ALLISON ECKHART", 1f, 1f); break;
            case "Schlag den Bomb": SetText(usedMeshes[34], "ALLISON ECKHART", 1f, 1f); break;
            case "Scipting": SetText(usedMeshes[23], "ALLISON ECKHART", 1f, 1f); break;
            case "Scratch-Off": SetText(usedMeshes[0], "ALLISON ECKHART", 1f, 1f); SetText(usedMeshes[1], "ALLISON ECKHART", 1f, 1f); break;
            case "Sequences": SetText(usedMeshes[13], "ALLISON", 1f, 1f); SetText(usedMeshes[16], "ECKHART", 1f, 1f); break;
            case "Shapes and Bombs": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Shell Game": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Shifting Maze": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 1f); break;
            case "Shufflewords": SetText(usedMeshes[25], "ALLISON\nECKHART", 1f, 1f); break;
            case "Shut-the-Box": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Silly Slots": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Silo Autorization": SetText(usedMeshes[34], "ALLISON", 1f, 1f); SetText(usedMeshes[35], "ECKHART", 1f, 1f); break;
            case "Simon Selects": SetText(usedMeshes[8], "ALLISON\nECKHART", 1f, 1f); break;
            case "Simpleton't": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Sink": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Skewed Slots": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "Skyrim": SetText(usedMeshes[7], "ALLISON", 1f, 1f); SetText(usedMeshes[6], "ECKHART", 1f, 1f); break;
            case "Snack Attack": SetText(usedMeshes[3], "ALLISON\nECKHART", 1f, 1f); break;
            case "Sonic the Hedgehog": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.6f, 0.6f); break;
            case "Sorry Sliders": SetText(usedMeshes[0], "A\nE", 1f, 1f); SetText(usedMeshes[2], "L\nC", 1f, 1f); SetText(usedMeshes[4], "L\nK", 1f, 1f); SetText(usedMeshes[6], "I\nH", 1f, 1f); SetText(usedMeshes[5], "S\nA", 1f, 1f); SetText(usedMeshes[3], "O\nR", 1f, 1f); SetText(usedMeshes[1], "N\nT", 1f, 1f); break;
            case "Spilling Paint": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "SpriteClub Betting Simulation": SetText(usedMeshes[11], "ALLISON", 1f, 1f); SetText(usedMeshes[17], "ECKHART", 1f, 1f); break;
            case "Standard Button Masher": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Starmap Reconstruction": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "State of Aggregation": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Subscribe to Pewdiepie": SetText(usedMeshes[2], "ALLISON", 0.9f, 0.9f); SetText(usedMeshes[5], "ECKHART", 0.9f, 0.9f); break;
            case "Subways": SetText(usedMeshes[4], "ALLISON\nECKHART", 1f, 1f); break;
            case "Switching Maze": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 1f); break;
            case "Symbolic Coordinates": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Symbolic Password": SetText(usedMeshes[0], "ALLISON ECKHART", 0.5f, 0.5f); break;
            case "Taco Tuesday": SetText(usedMeshes[11], "ALLISON\nECKHART", 1f, 1f); break;
            case "Ternary Converter": SetText(usedMeshes[8], "ALLISON", 1f, 1f); SetText(usedMeshes[9], "ECKHART", 1f, 1f); break;
            case "Ternary Tiles": SetText(usedMeshes[12], "ALLISON\nECKHART", 1f, 1f); break;
            case "Tesseractivity": SetText(usedMeshes[1], "ALLI", 1f, 1f); SetText(usedMeshes[3], "SON", 1f, 1f); SetText(usedMeshes[5], "ECK", 1f, 1f); SetText(usedMeshes[7], "HART", 1f, 1f); break;
            case "Tetramorse": SetText(usedMeshes[4], "ALLISON\nECKHART", 1f, 1f); break;
            case "The Amber Button": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "The Black Button": SetText(usedMeshes[2], "ALLISON\nECKHART", 1f, 1f); break;
            case "The Black Page": SetText(usedMeshes[3], "ALLISON", 1f, 1f); SetText(usedMeshes[4], "ECKHART", 1f, 1f); break;
            case "The Calculator": SetText(usedMeshes[22], "ALLISON\nECKHART", 1f, 1f); break;
            case "The Code": SetText(usedMeshes[11], "ALLISON", 1f, 1f); SetText(usedMeshes[12], "ECKHART", 1f, 1f); break;
            case "The Colored Maze": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.6f); break;
            case "The Dealmaker": SetText(usedMeshes[1], "ALLISON", 0.9f, 0.9f); SetText(usedMeshes[2], "ECKHART", 0.9f, 0.9f); break;
            case "The Deck of Many Things": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "The Door": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "The Exploding Pen": SetText(usedMeshes[3], "ALLISON\nECKHART", 1f, 1f); break;
            case "The Festive Jukebox": SetText(usedMeshes[3], "ALLISON\nECKHART", 1.1f, 1.1f); break;
            case "The Funny Number": SetText(usedMeshes[11], "ALLISON", 1f, 1f); SetText(usedMeshes[12], "ECKHART", 1f, 1f); break;
            case "The Furloid Jukebox": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "The Jukebox": SetText(usedMeshes[3], "ALLISON\nECKHART", .9f, .9f); break;
            case "The Legendere Symbol": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "The Modkit": SetText(usedMeshes[8], "ALLISON ECKHART", 1f, 1f); SetText(usedMeshes[9], "♀", 1f, 1f); break; //This was Tas' idea I'm nowhere near this clever --Blan
            case "The Number Cipher": SetText(usedMeshes[15], "ALLISON", 1f, 1f); SetText(usedMeshes[16], "ECKHART", 1f, 1f); break;
            case "The Number": SetText(usedMeshes[11], "ALLISON", 1f, 1f); SetText(usedMeshes[12], "ECKHART", 1f, 1f); break;
            case "The Rule": SetText(usedMeshes[1], "ALLISON\nECKHART", 0.5f, 0.5f); break;
            case "The Stock Market": SetText(usedMeshes[17], "ALLISON\nECKHART", 1f, 1f); break;
            case "The Tile Maze": SetText(usedMeshes[53], "ALLISON", 1f, 1f); SetText(usedMeshes[54], "ECKHART", 1f, 1f); break;
            case "The cRule": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Timezone": SetText(usedMeshes[13], "ALLISON\nECKHART", 1f, 1f); break;
            case "Toon Enough": SetText(usedMeshes[0], "ALLISON", 0.7f, 0.7f); SetText(usedMeshes[1], "ECKHART", 0.7f, 0.7f); break;
            case "Topsy Turvy": SetText(usedMeshes[2], "ALLISON\nECKHART", 1f, 1f); break;
            case "Totally Accurate Minecraft Simulator": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Touch Transmission": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Towers": SetText(usedMeshes[21], "ALLISON", 1f, 1f); SetText(usedMeshes[20], "ECKHART", 1f, 1f); break;
            case "Training Text": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Transmitted Morse": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Tribal Council": SetText(usedMeshes[6], "ALLISON\nECKHART", 1f, 1f); break;
            case "Triple Term": SetText(usedMeshes[5], "ALLISON\nECKHART", 1f, 1f); break;
            case "Truchet Tiles": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Turtle Robot": SetText(usedMeshes[2], "ALLISON\nECKHART", 1f, 1f); break;
            case "Two Bits": SetText(usedMeshes[11], "ALLISON", 0.7f, 0.7f); SetText(usedMeshes[12], "ECKHART", 0.7f, 0.7f); break;
            case "UIN(+L)": SetText(usedMeshes[24], "ALLISON\nECKHART", 1f, 1f); break;
            case "Ultimate Cycle": SetText(usedMeshes[19], "A", 1f, 1f); SetText(usedMeshes[20], "L", 1f, 1f); SetText(usedMeshes[21], "L", 1f, 1f); SetText(usedMeshes[22], "I", 1f, 1f); SetText(usedMeshes[23], "S", 1f, 1f); SetText(usedMeshes[24], "O", 1f, 1f); SetText(usedMeshes[25], "N", 1f, 1f); SetText(usedMeshes[28], "E", 1f, 1f); SetText(usedMeshes[29], "C", 1f, 1f); SetText(usedMeshes[30], "K", 1f, 1f); SetText(usedMeshes[31], "H", 1f, 1f); SetText(usedMeshes[32], "A", 1f, 1f); SetText(usedMeshes[33], "R", 1f, 1f); SetText(usedMeshes[34], "T", 1f, 1f); break;
            case "Ultralogic": SetText(usedMeshes[3], "ALLISON\nECKHART", 1f, 1f); break;
            case "Unicode": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Unown Cipher": SetText(usedMeshes[9], "ALLISON\nECKHART", 1f, 1f); break;
            case "Wack Game of Life": SetText(usedMeshes[1], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            case "Weird Al Yankovic": SetText(usedMeshes[3], "ALLISON\nECKHART", 1f, 1f); break;
            case "Wendithap'n": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Wolf, Goat, and Cabbage": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            case "Wonder Cipher": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 1f); break;
            case "eeB gnillepS": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[3], "ECKHART", 1f, 1f); break;
            case "Ángel Hernández": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "ƎNA Cipher": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            case "Arithmetic Cipher": case "Blue Cipher": case "Blue Huffman Cipher": case "Brown Cipher": case "Cornflower Cipher": case "Crimson Cipher": case "Forest Cipher":
            case "Gray Cipher": case "Green Cipher": case "Indigo Cipher": case "Lempel-Ziv Cipher": case "Maroon Cipher": case "Orange Cipher": case "Pokemon Sprite Cipher": 
            case "Red Cipher": case "Shape Cipher": case "Violet Cipher": case "White Cipher": case "Yellow Cipher": case "Yellow Huffman Cipher": 
                SetText(usedMeshes[29], "ALLISON", 0.25f, 1f); SetText(usedMeshes[30], "ECKHART", 0.21f, 1f); break;
            default: if (debugMode) { for (int i = 0; i < usedMeshes.Length; i++) SetText(usedMeshes[i], i.ToString(), 0.5f, 0.5f); } break;

            //Unused =
            // QUIRKY //             case "8": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.18f, 0.18f); break;
            // GETS REMOVED //       case "1000 Words": SetText(usedMeshes[5], "ALLISON", 0.5f, 1f); SetText(usedMeshes[6], "ECKHART", 0.5f, 1f); break;
            // BUGGED //             case "14": SetText(usedMeshes[14], "ALLISON\nECKHART", 1f, 1f); break;
			// QUIRKY //             case "Access Codes": SetText(usedMeshes[7], "ALLISON", 0.18f, 1f); SetText(usedMeshes[8], "ECKHART", 0.18f, 1f); break;
            // BUGGED //             case "Amnesia": SetText(usedMeshes[5], "ALLISON", 1f, 1f); SetText(usedMeshes[2], "ECKHART", 1f, 1f); break;
            // GETS CHANGED //       case "Antistress": SetText(usedMeshes[5], "ALLISON", 0.1f, 1f); SetText(usedMeshes[4], "ECKHART", 0.125f, 0.75f); break;
			// QUIRKY //             case "Backdoor Hacking": SetText(usedMeshes[1], "ALLI", 1f, 1f); SetText(usedMeshes[2], "SON", 1f, 1f); SetText(usedMeshes[3], "ECK", 1f, 1f); SetText(usedMeshes[4], "HART", 1f, 1f); break;
			// QUIRKY //             case "Brainf---": SetText(usedMeshes[11], "ALLISON", 0.5f, 1f); SetText(usedMeshes[10], "ECKHART", 0.5f, 1f); break;
            // DOES NOT WORK //      case "Broken Guitar Chords": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            // GETS CHANGED //       case "Burnout": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.7f, 0.9f); break;
			// QUIRKY //             case "Castor": SetText(usedMeshes[10], "ALLISON", 1f, 1f); SetText(usedMeshes[11], "ECKHART", 0.606f, 1f); break;
            // GETS CHANGED //       case "Dialtones": SetText(usedMeshes[0], "ALLISON\nECKHART", 1f, 1f); break;
            // BREAKS MOD //         case "DNA Mutation": SetText(usedMeshes[0], "ALLI", 0.333f, 1f); SetText(usedMeshes[1], "SON", 0.333f, 1f); SetText(usedMeshes[2], "ECK", 0.333f, 1f); SetText(usedMeshes[3], "HART", 0.266f, 1f); break;
			// QUIRKY //             case "Don't Touch Anything": SetText(usedMeshes[0], "ALLISON\nECKHART", 0.455f, 1f); break;
            // BAD IDEA //           case "Dumb Waiters": SetText(usedMeshes[7], "ALLISON", 1f, 1f); SetText(usedMeshes[15], "ECKHART", 1f, 1f); break;
            // BUGGED //             case "Enigma Cycle": SetText(usedMeshes[19], "A", 1f, 1f); SetText(usedMeshes[20], "L", 1f, 1f); SetText(usedMeshes[21], "L", 1f, 1f); SetText(usedMeshes[22], "I", 1f, 1f); SetText(usedMeshes[23], "S", 1f, 1f); SetText(usedMeshes[24], "O", 1f, 1f); SetText(usedMeshes[25], "N", 1f, 1f); SetText(usedMeshes[28], "E", 1f, 1f); SetText(usedMeshes[29], "C", 1f, 1f); SetText(usedMeshes[30], "K", 1f, 1f); SetText(usedMeshes[31], "H", 1f, 1f); SetText(usedMeshes[32], "A", 1f, 1f); SetText(usedMeshes[33], "R", 1f, 1f); SetText(usedMeshes[34], "T", 1f, 1f); break;
            // NOT ALWAYS VISIBLE // case "Faulty Sink": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
            // GETS REMOVED //       case "Finite Loop": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 1f); break;
			// QUIRKY //             case "Forget Enigma": SetText(usedMeshes[0], "A", 1f, 1f); SetText(usedMeshes[1], "L", 1f, 1f); SetText(usedMeshes[2], "L", 1f, 1f); SetText(usedMeshes[3], "I", 1f, 1f); SetText(usedMeshes[4], "S", 1f, 1f); SetText(usedMeshes[5], "O", 1f, 1f); SetText(usedMeshes[6], "N", 1f, 1f); SetText(usedMeshes[7], "E", 1f, 1f); SetText(usedMeshes[8], "C", 1f, 1f); SetText(usedMeshes[9], "K", 1f, 1f); SetText(usedMeshes[10], "H", 1f, 1f); SetText(usedMeshes[11], "A", 1f, 1f); SetText(usedMeshes[12], "R", 1f, 1f); SetText(usedMeshes[13], "T", 1f, 1f); break;
			// QUIRKY //             case "Four-Card Monte": SetText(usedMeshes[1], "ALLISON\nECKHART", 0.789f, 0.806f); break;
            // BUGGED //             case "Functional Mapping": SetText(usedMeshes[6], "ALLISON", 1f, 1f); SetText(usedMeshes[7], "ECKHART", 1f, 1f); break;
			// QUIRKY //             case "LOOK AT ME": SetText(usedMeshes[0], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
			// QUIRKY //             case "Lunchtime": SetText(usedMeshes[1], "ALLISON ECKHART", 0.7f, 0.7f); break;
			// QUIRKY //             case "Password Destroyer": SetText(usedMeshes[2], "ALLISON", 1f, 1f); SetText(usedMeshes[1], "ECKHART", 1f, 1f); break;
			// QUIRKY //             case "Pollux": SetText(usedMeshes[10], "ALLISON", 1f, 1f); SetText(usedMeshes[11], "ECKHART", 0.606f, 1f); break;
			// QUIRKY //             case "Scrabble Scramble": SetText(usedMeshes[6], "ALLISON ECKHART", 1f, 1f); break;
			// AUTHOR REQUEST //     case "TetraVex": SetText(usedMeshes[36], "ALLISON\nECKHART", 1f, 1f); break;
            // QUIRKY //             case "The Swan": SetText(usedMeshes[12], "ALLISON\nECKHART", 0.6f, 0.7f); break;
			// QUIRKY //             case "Timing is Everything": SetText(usedMeshes[1], "ALLISON\nECKHART", 1f, 1f); break;
			// QUIRKY //             case "Top 10 Numbers": SetText(usedMeshes[11], "ALLISON\nECKHART", 1f, 1f); break;
			
        }
    }

    private void SetText(TextMesh tMesh, string text, float scaleX, float scaleY)
    {
        tMesh.text = text;
        tMesh.gameObject.transform.localScale = new Vector3(tMesh.gameObject.transform.localScale.x * scaleX, tMesh.gameObject.transform.localScale.y * scaleY, tMesh.gameObject.transform.localScale.z);
    }
}
