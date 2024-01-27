using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectionManager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    public LayerMask selectionMask;
    //public HexGrid hexGrid;
    //List<Vector3Int> neighbours=new List<Vector3Int>();

    public UnityEvent<GameObject> OnUnitSelected;
    public UnityEvent<GameObject> TerrainSelected;

    private void Awake()
    {
        if(mainCamera == null)
            mainCamera = Camera.main;
    }
    public void HandleClick(Vector3 mousePos)
    {
        Debug.Log("HandleClick");
        GameObject result;
        if(FindTarget(mousePos,out result))
        {
            if (UnitSelected(result))
            {
                OnUnitSelected?.Invoke(result);
            }
            else
            {
                TerrainSelected?.Invoke(result);
            }
            /*
            Hex selectedHex= result.GetComponent<Hex>();
            selectedHex.DisableHighlight();
            foreach(Vector3Int neighbour in neighbours)
            {
                hexGrid.GetTileAt(neighbour).DisableHighlight();
            }
            ///List<Vector3Int> neighbours = hexGrid.GetNeighboursFor(selectedHex.HexCoords);
            BFSResult bfsResult = GraphSearch.BFSRange(hexGrid, selectedHex.HexCoords, 20);
            neighbours = new List<Vector3Int>(bfsResult.GetRangePositions());

            foreach (Vector3Int neighbour in neighbours)
            {
                hexGrid.GetTileAt(neighbour).EnableHighlight();
            }
            /*
            Debug.Log($"neigbours of hex in {selectedHex.HexCoords} are: ");
            foreach (Vector3Int neighbourPos in neighbours)
            {
                Debug.Log(neighbourPos);
            }
            */
        }
    }
    private bool UnitSelected(GameObject result)
    {
        return result.GetComponent<Unit>()!=null;
    }
    public bool FindTarget(Vector3 mousePosition, out GameObject result)
    {
        RaycastHit hit;
        Ray ray=mainCamera.ScreenPointToRay(mousePosition);
        if(Physics.Raycast(ray, out hit,100, selectionMask))
        {
            result = hit.collider.gameObject;
            return true;
        }
        result = null;
        return false;
    }
}
