// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.IO;
// using UnityEngine;
//
// public class SaveDataHandler
// {
//     private readonly string _saveFilePath;
//     private readonly string _saveFileName;
//     
//     public SaveDataHandler(string saveFilePath, string saveFileName)
//     {
//         _saveFileName = saveFileName;
//         _saveFilePath = saveFilePath;
//     }
//
//     public GameData LoadData()
//     {
//         // combine paths regardless of os 
//         var fullPath = Path.Combine(_saveFilePath, _saveFileName);
//         
//         GameData loadedData = null;
//
//         if (File.Exists(fullPath))
//         {
//             try
//             {
//                 var dataToLoad = File.ReadAllText(fullPath);
//                 
//                 // deseralize object from json
//                 loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
//                 
//             }
//             catch (Exception e)
//             {
//                 Debug.LogError("Error when trying to load data from, file: " + fullPath + "\n" + e);
//             }
//         }
//
//         return loadedData;
//     }
//
//     public void SaveData(GameData data)
//     {
//         // combine paths regardless of os 
//         var fullPath = Path.Combine(_saveFilePath, _saveFileName);
//
//         try
//         {
//             // create directory if it doesn't exist
//             Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
//
//             // serialize object to json
//             var dataToStore = JsonUtility.ToJson(data, true); 
//
//             // write the json to file
//             File.WriteAllText(fullPath, dataToStore);
//         }
//         catch (Exception e)
//         {
//             Debug.LogError("Error when trying to save data to file: " + fullPath + "\n" + e);
//         }
//     }
// }
