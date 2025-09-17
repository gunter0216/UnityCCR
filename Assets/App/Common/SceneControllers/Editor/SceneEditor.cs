﻿using App.Common.SceneControllers.Runtime;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Common.SceneControllers.Editor
{
    public class SceneEditor
    {
#if UNITY_EDITOR
        [MenuItem("Helper/Scenes/StartScene", false, 1)]
        public static void GoToStartScene()
        {
            OpenScene($"Assets/Scenes/{SceneConstants.StartScene}.unity");
        }
        
        [MenuItem("Helper/Scenes/CoreScene", false, 2)]
        public static void GoToCoreScene()
        {
            OpenScene($"Assets/Scenes/{SceneConstants.CoreScene}.unity");
        }

        private static void OpenScene(string name)
        {
            if (Application.isPlaying)
            {
                Debug.Log("Open scene only in Edit mode!");
                return;
            }
            
            bool isSaved = EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), SceneManager.GetActiveScene().path);
            Debug.Log("Saved Scene " + (isSaved ? "OK" : "Error!"));
            EditorSceneManager.OpenScene(name);
        }
#endif
    }
}