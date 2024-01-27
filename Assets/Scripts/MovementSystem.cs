using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    private BFSResult movementRange = new BFSResult();
    private List<Vector3Int> currentPath = new List<Vector3Int>();

    public void HideRange(HexGrid hexGrid)
    {
        foreach (Vector3Int hexPosition in movementRange.GetRangePositions())
        {
            hexGrid.GetTileAt(hexPosition).DisableHighlight();
        }
        movementRange = new BFSResult();
    }

    public void ShowRange(Unit selectedUnit,HexGrid hexGrid)
    {
        CalculateRange(selectedUnit, hexGrid);
        foreach (Vector3Int hexPosition in movementRange.GetRangePositions())
        {

            hexGrid.GetTileAt(hexPosition).EnableHighlight();
        }
    }
    public void CalculateRange(Unit selectedUnit,HexGrid hexGrid)
    {
        movementRange = GraphSearch.BFSRange(hexGrid,hexGrid.GetClosestHex(selectedUnit.transform.position), selectedUnit.MovementPoints);
    }

    public void ShowPath(Vector3Int selectedHehPosition,HexGrid hexGrid)
    {
        if (movementRange.GetRangePositions().Contains(selectedHehPosition))
        {
            foreach (Vector3Int hexPosition in currentPath)
            {
                //hexGrid.GetTileAt(hexPosition).ResetHighlight();
            }
            currentPath = movementRange.GetPathTo(selectedHehPosition);
            foreach (Vector3Int hexPosition in currentPath)
            {
                hexGrid.GetTileAt(hexPosition).HighlightPath();
            }
        }
    }
    public void MoveUnit(Unit selectedUnit,HexGrid hexGrid)
    {
        Debug.Log("Moving unit: " + selectedUnit.name);
        selectedUnit.MoveThrouhPath(currentPath.Select(pos => hexGrid.GetTileAt(pos).transform.position).ToList());
    }
    public bool IsHexInRange(Vector3Int hexPosition)
    {
        return movementRange.IsHexPositionInRange(hexPosition);
    }

}
