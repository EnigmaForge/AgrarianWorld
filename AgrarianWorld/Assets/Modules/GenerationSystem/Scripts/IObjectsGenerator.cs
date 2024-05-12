using System.Collections.Generic;

namespace Modules.GenerationSystem {
    public interface IObjectsGenerator : IGenerator {
        public IObjectsGenerator SetObjects(HashSet<GenerationObjectData> objects);
    }
}