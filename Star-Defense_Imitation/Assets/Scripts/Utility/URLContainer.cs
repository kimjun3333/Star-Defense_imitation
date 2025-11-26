using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

public static class URLContainer
{
    public static string towerURL = "https://opensheet.elk.sh/1MX7Pi7p9tuYxsoqtnmjGidSzMs3Wwyu7h0MkwYgALRo/Tower";     
    public static string enemyURL = "https://opensheet.elk.sh/1MX7Pi7p9tuYxsoqtnmjGidSzMs3Wwyu7h0MkwYgALRo/Enemy";
    public static string StageURL = "https://opensheet.elk.sh/1MX7Pi7p9tuYxsoqtnmjGidSzMs3Wwyu7h0MkwYgALRo/Stage";
    public static string WaveURL = "https://opensheet.elk.sh/1MX7Pi7p9tuYxsoqtnmjGidSzMs3Wwyu7h0MkwYgALRo/Wave";

    public static readonly Dictionary<Type, (Type dataType, string url)> SheetMap = new()
    {
        { typeof(TowerSO), (typeof(TowerSheetData), towerURL) },
        { typeof(EnemySO), (typeof(EnemySheetData), enemyURL) },
        { typeof(StageSO), (typeof(StageSheetData), StageURL) },
        { typeof(WaveSO), (typeof(WaveSheetData), WaveURL) },
    };
}
