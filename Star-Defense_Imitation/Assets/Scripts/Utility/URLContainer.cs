using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

public static class URLContainer
{
    public static string towerURL = "https://docs.google.com/spreadsheets/d/1MX7Pi7p9tuYxsoqtnmjGidSzMs3Wwyu7h0MkwYgALRo/edit?usp=sharing";     
    public static string enemyURL = "https://docs.google.com/spreadsheets/d/1MX7Pi7p9tuYxsoqtnmjGidSzMs3Wwyu7h0MkwYgALRo/edit?usp=sharing";
    public static string StageURL = string.Empty;
    public static string WaveURL = string.Empty;

    public static readonly Dictionary<Type, (Type dataType, string url)> SheetMap = new()
    {
        { typeof(TowerSO), (typeof(TowerSheetData), towerURL) },
        { typeof(EnemySO), (typeof(EnemySheetData), enemyURL) },

    };
}
