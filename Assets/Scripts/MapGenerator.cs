using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Tile tilePrefab;
    public GameObject plane;
    public int width;
    public int height;

    public List<List<Tile>> tiles;
    // Start is called before the first frame update
    void Start()
    {
        tiles = new List<List<Tile>>();

        for (int i = 0; i < width; i++)
        {
            List<Tile> row = new List<Tile>();
            tiles.Add(row);
            for (int j = 0; j < height; j++)
            {
                Vector3 position = new Vector3(0.5f + i, -0.05f, -height / 2.0f + 0.5f + j);
                Tile tile = Instantiate(tilePrefab, position, tilePrefab.gameObject.transform.rotation, plane.transform);
                row.Add(tile);

                tile.coord = new Vector2Int(i, j);
            }
        }

        tilePrefab.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2Int GetTileCoord(Vector3 position)
    {
        int x = -1, y = -1;

        if (position.x < 0 || position.x > width || Mathf.Abs(position.z) > height/2.0f)
        {
            // do nothing
        }
        else
        {
            x = Mathf.FloorToInt(position.x);
            y = Mathf.FloorToInt(position.z + height / 2.0f);
        }
        return new Vector2Int(x, y);
    }

    public Tile getTile(Vector2Int coord)
    {
        if (tiles.Count == 0)
        {
            return null;
        }
        if (coord.x >= 0 && coord.y >= 0 && coord.x < tiles.Count && coord.y < tiles[0].Count)
        {
            return tiles[coord.x][coord.y];
        }
        return null;
    }

    public void SetAllTileNormal()
    {
        foreach (var row in tiles)
        {
            foreach (var tile in row)
            {
                tile.SetNormal();
            }
        }
    }
}
