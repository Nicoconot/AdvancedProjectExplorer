using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Nicoconut.AdvancedProjectExplorer
{
    public class AdvancedProjectTab : EditorWindow
    {

        private static EditorFolder Assets;
        private Vector2 Scroll;

        private string searchInput;

        private bool isSearching = false;

        private EditorFile[] searchedFiles;


        [MenuItem("Window/Nicoconut/Advanced Project Explorer")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            AdvancedProjectTab window = (AdvancedProjectTab)GetWindow(typeof(AdvancedProjectTab), false, "Project Explorer");
            window.Show();
        }


        void OnGUI()
        {
            Scroll = GUILayout.BeginScrollView(Scroll);
            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Fetch", EditorStyles.toolbarButton))
            {
                Assets = CreateInstance<EditorFolder>();
                Assets.Setup("Assets", null, 0, position, false);
            }

            if (GUILayout.Button("Clear", EditorStyles.toolbarButton))
                Assets = null;
            GUILayout.EndHorizontal();

            if (Assets != null)
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                searchInput = GUILayout.TextField(searchInput, GUILayout.Height(20), GUILayout.Width(384));

                bool btn = GUILayout.Button("Search", GUILayout.Width(64));
                
                GUILayout.EndHorizontal();
                
                if (btn)
                {
                    if (searchInput.Length > 2) Search(searchInput);
                }

                if (isSearching)
                {
                    if (searchInput.Length > 0) DisplaySearch();
                    else isSearching = false;
                }
                else Assets.VisualizeFolder();
            }

            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }
        
        void Search(string input)
        {
            Debug.Log("searching \"" + input + "\"...");
            isSearching = true;
            var found = AssetDatabase.FindAssets(input);

            searchedFiles = new EditorFile[found.Length];

            for (int i = 0; i < found.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(found[i]);
                EditorFile file = new EditorFile(path, Assets);

                searchedFiles[i] = file;
            }
        }

        void DisplaySearch()
        {
            for (int i = 0; i < searchedFiles.Length; i++)
            {
                var file = searchedFiles[i];
                
                GUILayout.BeginHorizontal();
                GUILayout.Space(45);
                var btn = GUILayout.Button(file.FileContent, StyleReferences.FileThumbnail, GUILayout.MinWidth(256),
                    GUILayout.Height(32));
                if (btn)
                {
                    Object obj = AssetDatabase.LoadAssetAtPath<Object>(file.FileContent.tooltip);
                    Selection.activeObject = obj;
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}