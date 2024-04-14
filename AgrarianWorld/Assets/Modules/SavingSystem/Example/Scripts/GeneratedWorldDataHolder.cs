using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.SavingSystem.Example {
    [Serializable]
    public class GeneratedWorldDataHolder {
        public string Name;
        public string Seed;
        public int Size;
        public List<Vector3> Trees;

        public GeneratedWorldDataHolder(string name, string seed, int size, List<Vector3> trees) {
            Name = name;
            Seed = seed;
            Size = size;
            Trees = trees;
        }
    }
}