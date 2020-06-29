
using System.Collections.Generic;
public class VoterPreferences
{
    public List<int> Preferences { get; set; }

    public int GetTopPick()
    {
        if (Preferences.Count > 0)
        {
            return Preferences[0];
        }
        else
        {
            return -1;
        }
    }

    public void RemoveTopPick()
    {
        Preferences.RemoveAt(0);
    }

    public int GetLength()
    {
        return Preferences.Count;
    }

    public List<int> GetAlteredPreferences()
    {
        List<int> prefs = Preferences;
        if (prefs.Count > 0)
        {
            prefs.RemoveAt(0);
        }
        return prefs;
    }
}