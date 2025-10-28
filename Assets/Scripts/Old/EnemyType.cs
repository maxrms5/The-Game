using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    [SerializeField]
    private List<string> type = new List<string>();

    public bool IsType(string type)
    {
        return type.Contains(type);
        
    }

    public IEnumerable<string> GetTypes()
    {
        return type;
    }

    public void Rename(int index, string typeName)
    {
        type[index] = typeName;
    }

    public string GetTypeAtIndex(int index)
    {
        return type[index];
    }

    public int Count
    {
        get { return type.Count; }
    }
}
