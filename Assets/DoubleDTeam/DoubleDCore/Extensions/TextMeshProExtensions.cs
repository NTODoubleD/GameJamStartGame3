using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace DoubleDCore.Extensions
{
    public static class TextMeshProExtensions
    {
        public static void StartRevealCharactersAnim(this TMP_Text textComponent, float animationDelay,
            Action endAction = null)
        {
            textComponent.StartCoroutine(RevealCharacters(textComponent, animationDelay, endAction));
        }

        public static void StartRevealWordsAnim(this TMP_Text textComponent, float animationDelay,
            Action endAction = null)
        {
            textComponent.StartCoroutine(RevealWords(textComponent, animationDelay, endAction));
        }

        private static IEnumerator RevealCharacters(TMP_Text textComponent, float timeDelay, Action endAction)
        {
            textComponent.ForceMeshUpdate();

            TMP_TextInfo textInfo = textComponent.textInfo;

            int totalVisibleCharacters = textInfo.characterCount;
            int visibleCount = 0;

            while (true)
            {
                if (visibleCount > totalVisibleCharacters)
                {
                    endAction?.Invoke();
                    yield break;
                }

                textComponent.maxVisibleCharacters = visibleCount;

                visibleCount += 1;

                yield return new WaitForSeconds(timeDelay);
            }
        }

        private static IEnumerator RevealWords(TMP_Text textComponent, float timeDelay, Action endAction)
        {
            textComponent.ForceMeshUpdate();

            int totalWordCount = textComponent.textInfo.wordCount;
            int totalVisibleCharacters = textComponent.textInfo.characterCount;

            int counter = 0;
            int visibleCount = 0;

            while (true)
            {
                var currentWord = counter % (totalWordCount + 1);

                if (currentWord == 0)
                    visibleCount = 0;
                else if (currentWord < totalWordCount)
                    visibleCount = textComponent.textInfo.wordInfo[currentWord - 1].lastCharacterIndex + 1;
                else if (currentWord == totalWordCount)
                    visibleCount = totalVisibleCharacters;

                textComponent.maxVisibleCharacters = visibleCount;

                if (visibleCount >= totalVisibleCharacters)
                {
                    endAction?.Invoke();
                    yield break;
                }

                counter += 1;

                yield return new WaitForSeconds(timeDelay);
            }
        }
    }
}