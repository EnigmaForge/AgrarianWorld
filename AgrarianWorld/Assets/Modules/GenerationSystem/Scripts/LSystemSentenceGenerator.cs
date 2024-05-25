using System.Linq;
using System.Text;
using UnityEngine;

namespace Modules.GenerationSystem {
    public class LSystemSentenceGenerator : IGenerator {
        private readonly VillageGenerationConfig _generationConfig;
        private readonly VillageGenerationModel _villageGenerationModel;

        public LSystemSentenceGenerator(VillageGenerationConfig generationConfig, VillageGenerationModel villageGenerationModel) {
            _generationConfig = generationConfig;
            _villageGenerationModel = villageGenerationModel;
        }

        public void Generate(int seed) {
            Random.InitState(seed);
            _villageGenerationModel.Sentence = GenerateSentence();
        }
        
        private string GenerateSentence(string word = null) {
            word ??= _generationConfig.RootSentence;
            return GrowSentence(word);
        }

        private string GrowSentence(string word, int iterationIndex = 0) {
            if (iterationIndex >= _generationConfig.IterationsLimit)
                return word;

            StringBuilder newWordBuilder = new StringBuilder();
            foreach (char letter in word) {
                newWordBuilder.Append(letter);
                ProcessRulesRecursive(newWordBuilder, letter, iterationIndex);
            }

            return newWordBuilder.ToString();
        }

        private void ProcessRulesRecursive(StringBuilder newWordBuilder, char letter, int iterationIndex) {
            LSystemRule rule = _generationConfig.Rules.FirstOrDefault(rule => rule.Letter == letter.ToString());
            if (rule != default)
                newWordBuilder.Append(GrowSentence(rule.GetResult(), iterationIndex + 1));
        }
    }
}