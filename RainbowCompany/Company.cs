using System;
using System.Linq;
using UnityEngine;

namespace RainbowCompany;

public class Company : MonoBehaviour
{
    private float hue;
    private float colorTimer = 0.05f;

    private void Update()
    {
        if (colorTimer >= 0)
        {
            colorTimer -= Time.deltaTime;
        }
        else
        {
            hue += Time.deltaTime;
            if (hue > 1.0f) hue -= 1.0f;
            var newColor = Color.HSVToRGB(hue, 0.5f, 0.5f);
            foreach (var material in Rainbow.sceneMaterials.Where(m => m)) material.color = newColor;
            
            colorTimer = 0.05f;
        }
    }
}