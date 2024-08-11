// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Serialization;
//
// [System.Serializable]
// public class SerializableDictionary<TKey,TValue> : Dictionary<TKey,TValue>, ISerializationCallbackReceiver
// {
//     [SerializeField] private List<TKey> keys = new();
//     [SerializeField] private List<TValue> values = new();
//     
//     // Serialize dictionary to two lists 
//     public void OnBeforeSerialize()
//     {
//         keys.Clear();
//         values.Clear();
//         
//         foreach (var pair in this)
//         {
//             keys.Add(pair.Key);
//             values.Add(pair.Value);
//         }
//     }
//
//     // Deserialize dictionary from two lists
//     public void OnAfterDeserialize()
//     {
//         Clear();
//
//         if (keys.Count != values.Count)
//         {
//             Debug.LogError("Tried to deserialize to serializable dictionary, but number of keys (" 
//                            + keys.Count + ") does not match number of values (" + values.Count + ")");
//         }
//
//         for (int i = 0; i < keys.Count; i++)
//         {
//             Add(keys[i],values[i]);
//         }
//     }
// }
