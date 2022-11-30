using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShovelCard : MonoBehaviour
    , IPointerDownHandler
    , IPointerUpHandler
{
    public bool isHovering;
    public MapGenerator mapGenerator;
    public Tile curTile;

    public void OnPointerDown(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Plant the tree");
        mapGenerator.SetAllTileNormal();
        isHovering = false;

        if (curTile != null)
        {
            curTile.SetNormal();
            curTile.RemovePlant();
        }
        curTile = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        isHovering = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHovering)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, new Vector3(0, 0, 0));
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector2Int tileCoord = mapGenerator.GetTileCoord(ray.GetPoint(distance));
                Tile tile = mapGenerator.getTile(tileCoord);

                if (curTile != null)
                {
                    curTile.SetNormal();
                }

                if (tile != null && tile.curPlant != null)
                {
                    tile.SetGlow();
                    curTile = tile;
                }
            }

        }
    }
}
