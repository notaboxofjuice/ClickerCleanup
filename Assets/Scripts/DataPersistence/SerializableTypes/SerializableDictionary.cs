using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new();
    [SerializeField] private List<TValue> values = new();
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }
    public void OnAfterDeserialize()
    {
        Clear();
        if (keys.Count != values.Count) Debug.LogError("Serialized dictionary data mismatch");
        for (int i = 0; i <keys.Count; i++) Add(keys[i], values[i]);
    }
}