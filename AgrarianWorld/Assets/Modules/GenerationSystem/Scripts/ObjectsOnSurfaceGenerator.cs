using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Modules.GenerationSystem {
    public class ObjectsOnSurfaceGenerator : IObjectsOnSurfaceGenerator {
        private readonly DiContainer _diContainer;
        private HashSet<GenerationObjectData> _generationObjects;
        private List<Vector3> _generationPoints;
        private float _minHeight;
        private float _maxHeight;
        private float _range;
        private Vector3 _center;

        public ObjectsOnSurfaceGenerator(DiContainer diContainer) =>
            _diContainer = diContainer;

        public void Generate(int seed) {
            Random.InitState(seed);
            GenerateObjectsInRandomPoints();
        }

        private void GenerateObjectsInRandomPoints() {
            FilterPoints();
            
            foreach (GenerationObjectData objectData in _generationObjects) {
                for (int objectIndex = 0; objectIndex < objectData.Count; objectIndex++) {
                    Vector3 spawnPoint = GetAndRemovePoint();
                    GameObject instantiatedObject = _diContainer.InstantiatePrefab(objectData.Prefab);
                    instantiatedObject.transform.position = spawnPoint;
                    instantiatedObject.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
                }
            }
        }

        private void FilterPoints() {
            int count = _generationPoints.Count;
            List<Vector3> filteredPoints = new();
            for (int pointIndex = 0; pointIndex < count; pointIndex++) {
                Vector3 point = _generationPoints[pointIndex];
                if (PointOutOfRange(point) is false)
                    filteredPoints.Add(point);
            }
            _generationPoints = filteredPoints;
        }

        private bool PointOutOfRange(Vector3 point) =>
            (point - _center).magnitude > _range || point.y < _minHeight || point.y > _maxHeight;

        private Vector3 GetAndRemovePoint() {
            int pointIndex = Random.Range(0, _generationPoints.Count);
            Vector3 point = _generationPoints[pointIndex];
            _generationPoints.RemoveAt(pointIndex);
            return point;
        }

        public IObjectsGenerator SetObjects(HashSet<GenerationObjectData> objects) {
            _generationObjects = objects;
            return this;
        }

        public IObjectsOnSurfaceGenerator SetGenerationPoints(List<Vector3> generationPoints) {
            _generationPoints = generationPoints;
            return this;
        }

        public IObjectsOnSurfaceGenerator SetGenerationRange(float minHeight, float maxHeight, float range, Vector3 center) {
            _minHeight = minHeight;
            _maxHeight = maxHeight;
            _range = range;
            _center = center;
            return this;
        }
    }
}