using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField]
    private HexGrid hexGrid;

    [SerializeField]
    private MovementSystem movementSystem;


    public bool PlayersTurn { get; private set; } = true;

    [SerializeField]
    private Unit selectedUnit;
    private Hex previouslySelectedHex;


    public void HandleUnitSelected(GameObject unit)
    {
        Debug.Log("11111111111111111111111111111111111111111111111");

        if (PlayersTurn == false)
        {
            return;
        }

        Unit unitReference = unit.GetComponent<Unit>();


        if (CheckIfTheSameUnitSelected(unitReference))
        {
            Debug.Log("22222222222222222222222222222222222222222222222222");

            return;
        }

        PrepareUnitForMovement(unitReference);
    }



    private bool CheckIfTheSameUnitSelected(Unit unitReference)
    {
        if(this.selectedUnit == unitReference)
        {
            ClearOldSelection();
            return true;
        }
        return false;
    }

    public void HandleTerrainSelected(GameObject hexGO)
    {
        if (selectedUnit == null || PlayersTurn == false)
        {
            return;
        }
        Hex selectedHex = hexGO.GetComponent<Hex>();
        if (HandleHexOutOfRange(selectedHex.HexCoords)|| HandleSelectedHexIsInUnitHex(selectedHex.HexCoords))
        {
            return;
        }
        HandleTargetHexSelected(selectedHex);
    }

    private void PrepareUnitForMovement(Unit unitReference)
    {
        if (this.selectedUnit != null)
        {
            ClearOldSelection();
        }


        this.selectedUnit = unitReference;
        this.selectedUnit.Select();
        Debug.Log("3333333333333333333333333333333333333333333333333333");

        movementSystem.ShowRange(this.selectedUnit,this.hexGrid);
    }

    private void ClearOldSelection()
    {
        previouslySelectedHex = null;
        this.selectedUnit.Deselect();
        movementSystem.HideRange(this.hexGrid);
        this.selectedUnit = null;
    }

    private void HandleTargetHexSelected(Hex selectedHex)
    {

        //
        if (previouslySelectedHex == null || previouslySelectedHex != selectedHex)
        {
            previouslySelectedHex = selectedHex;
            movementSystem.ShowPath(selectedHex.HexCoords,this.hexGrid);
            movementSystem.MoveUnit(selectedUnit, this.hexGrid);
            PlayersTurn = false;
            selectedUnit.MovementFinished += ResetTurn;
            ClearOldSelection();
        }
        else
        {

            movementSystem.MoveUnit(selectedUnit, this.hexGrid);
            PlayersTurn = false;
            selectedUnit.MovementFinished += ResetTurn;
            ClearOldSelection();

        }
    }

    private bool HandleSelectedHexIsInUnitHex(Vector3Int hexPosition)
    {
        if (hexPosition == hexGrid.GetClosestHex(selectedUnit.transform.position))
        {
            selectedUnit.Deselect();
            ClearOldSelection();
            return true;
        }
        return false;
    }

    private bool HandleHexOutOfRange(Vector3Int hexPosition)
    {
        if (movementSystem.IsHexInRange(hexPosition) == false)
        {
            Debug.Log("hex is out of range!");
            return true;
        }
        return false;
    }

    private void ResetTurn(Unit selectedUnit)
    {
        selectedUnit.MovementFinished-= ResetTurn;
        PlayersTurn = true;
    }
}
