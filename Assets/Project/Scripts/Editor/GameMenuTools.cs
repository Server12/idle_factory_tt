using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    public static class GameMenuTools
    {
        [MenuItem("Tools/Clear All Prefs")]
        public static void ClearAllPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}