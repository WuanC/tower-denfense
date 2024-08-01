using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlaceManager : MonoBehaviour
{
    [SerializeField] private Tower towerPrefabs;
    [SerializeField] private PreviewTower previewTower;
    [SerializeField] private LayerMask placeLayer;
    [SerializeField] Tilemap placeTile;



    private void OnEnable()
    {
        if (towerPrefabs == null || previewTower == null) {
            gameObject.SetActive(false);
            return;
        }
        previewTower = Instantiate(previewTower, transform);      
    }

    private void Update()
    {
        HandlePlaceTower();
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetActive(false);
        }
    }
    private void OnDisable()
    {
        if (previewTower != null)
        {
            Destroy(previewTower.gameObject);
            previewTower = null;
        }
        if (towerPrefabs != null) towerPrefabs = null;
    }
    private void HandlePlaceTower()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = placeTile.WorldToCell(mousePosition);
        gridPosition.z = 0;

        previewTower.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        previewTower.gameObject.SetActive(true);


        if (IsPlacementValid(placeTile.GetCellCenterWorld(gridPosition)) && placeTile.HasTile(gridPosition))
        {
            previewTower.spriteRenderer.color = Color.green;
            if(Input.GetMouseButtonDown(0) && GameController.Instance.GetCurretCoins() >= towerPrefabs.GetStartCoins())
            {
                Vector3 postitionWorld = placeTile.GetCellCenterWorld(gridPosition);
                Instantiate(towerPrefabs, postitionWorld, Quaternion.identity);
                EventManager.Instance.Broadcast(EventID.OnPlaceTower, gridPosition);
                EventManager.Instance.Broadcast(EventID.OnWithDrawCoins, towerPrefabs.GetStartCoins());
                SetActive(false);
            }
        }
        else
        {
            previewTower.spriteRenderer.color = Color.red;
        }
    }
    bool IsPlacementValid(Vector3 position)
    {
        BoxCollider2D boxCollider = towerPrefabs.GetComponent<BoxCollider2D>();
        Collider2D hitCollider = Physics2D.OverlapBox(position, boxCollider.size, 0f);
        return hitCollider == null;
    }
    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
    public void SetTower(Tower tower, PreviewTower previewTower)
    {
        this.previewTower = previewTower;
        towerPrefabs = tower;
    }



}
