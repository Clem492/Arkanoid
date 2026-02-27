using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AmeliorationData", menuName = "Scriptable Objects/AmeliorationData")]
public class AmeliorationData : ScriptableObject
{
    [System.Serializable]
    public class AmeliorationType
    {
        public string name;
        public string description;
        public Sprite sprite;
        public int cost;
    }

    public List<AmeliorationType> listAmeliration;
}
