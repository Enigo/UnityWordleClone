using System;
using System.Text;
using UnityEngine;

public static class PlayerPrefsUtils
{
    private const string LastPlayed = "LastPlayed";
    private const string DateFormat = "dd/MM/yyyy";

    public static bool WasPlayedToday()
    {
        return PlayerPrefs.HasKey(LastPlayed) &&
               FromBase64(PlayerPrefs.GetString(LastPlayed)).Equals(DateTime.Now.ToString(DateFormat));
    }

    public static void SetPlayedToday()
    {
        PlayerPrefs.SetString(LastPlayed, ToBase64(DateTime.Now.ToString(DateFormat)));
    }

    private static string ToBase64(string value)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
    }

    private static string FromBase64(string value)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(value));
    }
}