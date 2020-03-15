using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWall : MonoBehaviour
{
    [SerializeField] GameObject blockPrefab;
    [SerializeField] int xSize, zSize;
    [SerializeField] float offset; 

    private void Start()
    {
        float x_offset = -offset;
        float z_offset = -offset + blockPrefab.transform.localScale.x;
        for (int x = 0; x < xSize; x++)
        {
            Vector3 spawnPosition = new Vector3(x_offset, blockPrefab.transform.position.y, offset);
            SpawnBlock(spawnPosition);
            spawnPosition = new Vector3(x_offset, blockPrefab.transform.position.y, -offset);
            SpawnBlock(spawnPosition);

            x_offset += blockPrefab.transform.localScale.x;
        }

        for (int z = 0; z < zSize; z++)
        {
            Vector3 spawnPosition = new Vector3(offset, blockPrefab.transform.position.y, z_offset);
            SpawnBlock(spawnPosition);
            spawnPosition = new Vector3(-offset, blockPrefab.transform.position.y, z_offset);
            SpawnBlock(spawnPosition);

            z_offset += blockPrefab.transform.localScale.x;
        }

        void SpawnBlock(Vector3 spawnPosition)
        {
            Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
