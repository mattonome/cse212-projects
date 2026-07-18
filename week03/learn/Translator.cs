/// <summary>
/// A simple translator that stores word translations in a dictionary.
/// Supports adding translations and translating words.
/// </summary>
public class Translator
{
    private Dictionary<string, string> _translations = new Dictionary<string, string>();

    /// <summary>
    /// Adds a word translation to the dictionary.
    /// </summary>
    /// <param name="sourceWord">The word in the source language</param>
    /// <param name="targetWord">The word in the target language</param>
    public void AddWord(string sourceWord, string targetWord)
    {
        _translations[sourceWord] = targetWord;
    }

    /// <summary>
    /// Translates a word from the source language to the target language.
    /// </summary>
    /// <param name="word">The word to translate</param>
    /// <returns>The translated word, or "???" if not found</returns>
    public string Translate(string word)
    {
        if (_translations.TryGetValue(word, out string? translation))
        {
            return translation;
        }
        else
        {
            return "???";
        }
    }
}