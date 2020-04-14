using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpawnInfo/Enemy")]
public class SpawnInfo : ScriptableObject
{
    public GameObject prefab;
    public int count;
}
