using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SelectionBase]
public class Hex : MonoBehaviour
{
    private HexCoordinates hexCoordinates;
    [SerializeField]
    private GlowLight highlight;

    [SerializeField]
    private HexType hexType;
    public Vector3Int HexCoords => hexCoordinates.GetHexCoords();

    public int GetCost() => hexType switch
    {
        HexType.Difficult => 20,
        HexType.Default => 10,
        HexType.Road => 5,
        _ => throw new Exception($"Hex Of Type {hexType} not supported")
    };
    public bool IsObstacle()
    {
        return this.hexType == HexType.Obstacle;
    }
    private void Awake()
    {
        hexCoordinates = GetComponent<HexCoordinates>();
        highlight = GetComponent<GlowLight>();
    }
    public void EnableHighlight()
    {
        highlight.ToggleGlow(true);
    }
    public void DisableHighlight()
    {
        highlight.ToggleGlow(false);
    }
    public void ResetHighlight()
    {
        highlight.ResetGlowGlowHighlight();
    }
    public void HighlightPath()
    {
        highlight.HighLightValidPath();
    }
}
public enum HexType
{
    None,
    Default,
    Difficult,
    Road,
    Water,
    Obstacle,
}