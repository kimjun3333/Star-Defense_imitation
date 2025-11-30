using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    [SerializeField] private Image fill;

    public void SetHP(float cur, float max)
    {
        fill.fillAmount = cur / max;
    }
}
