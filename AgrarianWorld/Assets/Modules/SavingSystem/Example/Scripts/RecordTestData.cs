using System.Collections.Generic;
using UnityEngine;

namespace Modules.SavingSystem.Example {
    public class RecordTestData : MonoBehaviour {
        private const string TEST_SAVES_GROUP_NAME = "Test";
        private const string TEST_WORLD_NAME = "TestWorld";
        private IDataStorageService _dataStorageService;
        
        private void Awake() =>
            _dataStorageService = new DataStorageService();

        private void Start() {
            GeneratedWorldDataHolder generatedWorldDataHolder = _dataStorageService.Load<GeneratedWorldDataHolder>(TEST_WORLD_NAME, TEST_SAVES_GROUP_NAME);

            if (generatedWorldDataHolder == null)
                GenerateTestData();
        } 

        private void GenerateTestData() {
            string exampleSeed = "1234567890";
            int exampleSize = 1000;
            List<Vector3> exampleTrees = new List<Vector3> {
                new(1, 10, 30),
                new(500, 101, 3),
                new(5, 300, 900),
                new(2, 130, 67),
                new(200, 11, 45)
            };
            
            GeneratedWorldDataHolder generatedWorldDataHolder = new GeneratedWorldDataHolder(TEST_WORLD_NAME, exampleSeed, exampleSize, exampleTrees);
            _dataStorageService.Save(generatedWorldDataHolder.Name, generatedWorldDataHolder, TEST_SAVES_GROUP_NAME);
        }
    }
}
