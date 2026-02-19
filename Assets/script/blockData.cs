using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "blockData", menuName = "Scriptable Objects/blockData")]
public class blockData : ScriptableObject
{
    [System.Serializable]
    public class BlockType
    {
        public string name;
        public int MoneyValue;
        public int Dommage;
        public int rebondSpeed;
        public Color color;
        public GameObject prefab;
    }

    public List<BlockType> blockTypes;
}
