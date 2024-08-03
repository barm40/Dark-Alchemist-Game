// using System.Collections.Generic;
// using UnityEngine;
// using System.Linq;
// using UnityEngine.SceneManagement;
//
// public class SaveDataManager : MonoBehaviour
// {
//     [Header("Debug")] [SerializeField] 
//     private bool initializeDataIfNull;
//     
//     // define a file name for save data 
//     [Header("Save Data Storage")] [SerializeField]
//     private string saveFileName;
//     
//     private LevelLoader _levelLoader;
//     
//     private GameData _gameData;
//     private List<ISaveData> _saveDataObjects = new();
//     private SaveDataHandler _saveDataHandler;
//     
//     public static SaveDataManager Instance { get; private set; }
//
//     private void Awake()
//     {
//         if (Instance != null)
//         {
//             Debug.Log("An instance of the save manager already exists, destroying the newest one");
//             Destroy(gameObject);
//             return;
//         }
//         Instance = this;
//         
//         DontDestroyOnLoad(gameObject);
//         
//         // create a save data handler with the application data path
//         _saveDataHandler = new SaveDataHandler(Application.persistentDataPath, saveFileName);
//         _levelLoader = FindObjectOfType<LevelLoader>();
//     }
//
//     private void OnEnable()
//     {
//         SceneManager.sceneLoaded += OnSceneLoaded;
//     }
//
//     private void OnDisable()
//     {
//         SceneManager.sceneLoaded -= OnSceneLoaded;
//     }
//
//     private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//     {
//         // initialize a list of all objects that are of type save data 
//         _saveDataObjects = FindAllSaveDataObjects();
//         LoadGame();
//     }
//
//     private void Update()
//     {
//         DontDestroyOnLoad(gameObject);
//     }
//
//     public void NewGame()
//     {
//         _gameData = new GameData();
//     }
//     
//     public void SaveGame()
//     {
//         if (_gameData == null)
//         {
//             Debug.Log("No save data, please start a new game");
//             return;
//         }
//         
//         // pass data to other scripts so that the data can be updated by them
//         foreach (var savaDataObj in _saveDataObjects)
//         {
//             savaDataObj.SaveData(_gameData);
//         }
//         
//         // save game data to file
//         _saveDataHandler.SaveData(_gameData);
//     }
//
//     public void LoadGame()
//     {
//         // load data from file
//         _gameData = _saveDataHandler.LoadData();
//         
//         // if no data, start new game
//         if (_gameData == null)
//         {
//             if (initializeDataIfNull)
//             {
//                 NewGame();
//             }
//             Debug.Log("No save data, cannot load, starting new game.");
//             return;
//         }
//         
//         // pass the data to all other objects, so they can be updated by it
//         foreach (var savaDataObj in _saveDataObjects)
//         {
//             savaDataObj.LoadData(_gameData);
//         }
//         
//         if (_levelLoader.CurrSceneIndex != _gameData.sceneIndex)
//             _levelLoader.LoadNextLevel(_gameData.sceneIndex);
//     }
//
//     private static List<ISaveData> FindAllSaveDataObjects()
//     {
//         var saveDataObjects = FindObjectsOfType<MonoBehaviour>()
//             .OfType<ISaveData>();
//         
//         return new List<ISaveData>(saveDataObjects);
//     }
//
//     private void OnApplicationQuit()
//     {
//         SaveGame();
//     }
// }
