using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Modules.SavingSystem {
    public class DataStorageService : IDataStorageService {
        private const string SAVES_FOLDER_NAME = "Saves";
        
        public void Save<TData>(string key, TData data, string group) {
            string filePath = GetFilePath(group);
            List<SavingDataHolder> saves = LoadAllSavesInFile(filePath);
            int recordedDataIndex = saves.FindIndex(dataHolder => dataHolder.Key == key);
            string jsonData = JsonUtility.ToJson(data);
            
            if (recordedDataIndex == -1)
                saves.Add(new SavingDataHolder(key, JsonUtility.ToJson(data)));
            else
                saves[recordedDataIndex].Data = jsonData;
            
            UpdateFile(filePath, saves);
        }

        public TData Load<TData>(string key, string group) {
            string filePath = GetFilePath(group);
            if (File.Exists(filePath) is false)
                return default;
            
            List<SavingDataHolder> saves = LoadAllSavesInFile(filePath);
            SavingDataHolder savingDataHolder = saves.FirstOrDefault(dataHolder => dataHolder.Key == key);

            if (savingDataHolder == null)
                return default;

            return JsonUtility.FromJson<TData>(savingDataHolder.Data);
        }

        public void Delete(string key, string group) {
            string filePath = GetFilePath(group);
            List<SavingDataHolder> saves = LoadAllSavesInFile(filePath);
            SavingDataHolder savingDataHolder = saves.FirstOrDefault(dataHolder => dataHolder.Key == key);

            if (savingDataHolder != null)
                saves.Remove(savingDataHolder);
            else
                throw new Exception($"Record does not exist in saves. File: {group}.save, key: {key}.");
            
            UpdateFile(filePath, saves);
        }

        public void DeleteGroup(string group) {
            string filePath = GetFilePath(group);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        private string GetFilePath(string group) =>
            Path.Combine(Application.persistentDataPath + $"/{SAVES_FOLDER_NAME}/", $"{group}.save");

        private List<SavingDataHolder> LoadAllSavesInFile(string filePath) {
            if (!File.Exists(filePath))
                return new List<SavingDataHolder>();

            string serializedData = File.ReadAllText(filePath);
            SavingDataHolderList holderList = JsonUtility.FromJson<SavingDataHolderList>(serializedData);

            return holderList?.Items ?? new List<SavingDataHolder>();
        }

        private void UpdateFile(string filePath, List<SavingDataHolder> data) {
            SavingDataHolderList holderList = new SavingDataHolderList {
                Items = data
            };

            string serializedData = JsonUtility.ToJson(holderList, true);

            string directoryPath = Path.Combine(Application.persistentDataPath, SAVES_FOLDER_NAME);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            File.WriteAllText(filePath, serializedData);
        }

    }
}