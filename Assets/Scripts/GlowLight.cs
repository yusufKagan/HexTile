using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowLight : MonoBehaviour
{
    Dictionary<Renderer, Material[]> glowMaterialDictionary= new Dictionary<Renderer, Material[]>();
    Dictionary<Renderer, Material[]> originalMaterialDictionary = new Dictionary<Renderer, Material[]>();
    Dictionary<Color, Material> cachedGlowMaterials = new Dictionary<Color, Material>();

    public Material glowMaterial;
    private Color validSpaceColor = Color.green;
    private Color originalGlowColor;
    //private bool isGlowing = true;
    private bool isGlowing=false;
    private void Awake()
    {
        PrepareMaterialDictionaries();
    }

    private void PrepareMaterialDictionaries()
    {
        foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            Material[] originalMaterials = renderer.materials;
            originalMaterialDictionary.Add(renderer, originalMaterials);

            Material[] newMaterials = new Material[renderer.materials.Length];

            for (int i = 0; i < originalMaterials.Length; i++)
            {
                Material mat = null;
                if (cachedGlowMaterials.TryGetValue(originalMaterials[i].color,out mat) == false)
                {
                    mat = new Material(glowMaterial);
                    //By default, Unity considers a color with a property name "_Color" to be the main color
                    mat.color = originalMaterials[i].color;
                    cachedGlowMaterials.Add(originalMaterials[i].color, mat);
                }
                newMaterials[i] = mat;
            }
            glowMaterialDictionary.Add(renderer, newMaterials);
        }
    }
    public void ToggleGlow()
    {
        if (isGlowing == false)
        {
            ResetGlowGlowHighlight();
            foreach(Renderer renderer in originalMaterialDictionary.Keys)
            {
                renderer.materials = glowMaterialDictionary[renderer];
            }
        }
        else
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                renderer.materials = originalMaterialDictionary[renderer];
            }
        }
        isGlowing = !isGlowing;
    }
    public void ToggleGlow(bool state)
    {
        if(isGlowing == state)
        {
            return;
        }
        isGlowing=!state;
        ToggleGlow();
    }
    public void ResetGlowGlowHighlight()
    {
        foreach (Renderer renderer in originalMaterialDictionary.Keys)
        {
            renderer.materials = originalMaterialDictionary[renderer];
        }
    }
    public void HighLightValidPath()
    {
        return;
    }
}
