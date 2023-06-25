using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Nicoconut.AdvancedProjectExplorer
{
    public class EditorFolder : ScriptableObject
    {
        private string folderPath;
        private string folderName;
        private EditorFolder parentFolder;

        private List<EditorFile> childFiles;
        private List<EditorFolder> childFolders;

        private int depth; //Depth of the folder. 0 for 'Assets'.

        private EditorFile[,] groupedFiles;
        private GUIContent folderContent;
        private Texture2D folderIcon;

        private bool fold = false;
        private Rect position;

        private EditorFile tempFile;

        private int selectedFile = 0;

/*    public UnityFolder(string folderPath, UnityFolder parentFolder,int depth, Rect position, bool startFolded = true)
    {
        this.folderPath = folderPath;
        this.depth = depth;
        this.position = position;//Position is a variable of the EditorWindow.


        childFolders = FindChildFolders();
        childFiles = FindChildFiles();
       
        //Create the object by giving its path. Then get the assetpreview.
        Object folderobj = AssetDatabase.LoadAssetAtPath(this.folderPath,typeof(Object));
        folderIcon = AssetPreview.GetMiniThumbnail(folderobj);


        //Assets/New Folder-> folderName:New Folder
        string[] splitPath = this.folderPath.Split('\\');
        folderName = splitPath[splitPath.Length - 1];

        folderContent = new GUIContent(folderName,folderIcon,folderPath);

        //This is a 2D array to group files by rows of 3.
        groupedFiles = GroupChildFiles(childFiles);

        fold = !startFolded;
    }*/

        public void Setup(string folderPath, EditorFolder parentFolder, int depth, Rect position,
            bool startFolded = true)
        {
            this.folderPath = folderPath;
            this.depth = depth;
            this.position = position; //Position is a variable of the EditorWindow.


            childFolders = FindChildFolders();
            childFiles = FindChildFiles();

            //Create the object by giving its path. Then get the assetpreview.
            Object folderobj = AssetDatabase.LoadAssetAtPath(this.folderPath, typeof(Object));
            folderIcon = AssetPreview.GetMiniThumbnail(folderobj);


            //Assets/New Folder-> folderName:New Folder
            string[] splitPath = this.folderPath.Split('\\');
            folderName = splitPath[splitPath.Length - 1];

            folderContent = new GUIContent(folderName, folderIcon, folderPath);

            //This is a 2D array to group files by rows of 3.
            groupedFiles = GroupChildFiles(childFiles);

            fold = !startFolded;
        }

        private List<EditorFolder> FindChildFolders()
        {
            //GetDirectories will return all the subfolders in the given path.
            string[] dirs = Directory.GetDirectories(folderPath);
            List<EditorFolder> folders = new List<EditorFolder>();
            foreach (var directory in dirs)
            {
                //Turn all directories into our 'UnityFolder' Object.
                EditorFolder newfolder = CreateInstance<EditorFolder>();
                newfolder.Setup(directory, this, depth + 1, position);
                folders.Add(newfolder);
            }

            return folders;
        }

        private List<EditorFile> FindChildFiles()
        {
            //GetFiles is similar but returns all the files under the path(obviously)
            string[] fileNames = Directory.GetFiles(folderPath);
            List<EditorFile> files = new List<EditorFile>();
            foreach (var file in fileNames)
            {
                EditorFile newfile = new EditorFile(file, this);
                //Pass meta files.
                if (newfile.Extension.Equals(".meta"))
                    continue;
                files.Add(newfile);
                if (tempFile == null) tempFile = newfile;
            }

            return files;
        }

        public void VisualizeFolder(bool test = false)
        {
            GUILayout.BeginVertical();

            //Do this to give horizontal space

            GUILayout.BeginHorizontal();
            GUILayout.Space(15 * depth);
            fold = EditorGUILayout.Foldout(fold, folderContent, true);
            GUILayout.EndHorizontal();

            if (fold)
            {
                VisualizeChildFiles();
                foreach (var VARIABLE in childFolders)
                {
                    VARIABLE.VisualizeFolder();
                }
            }

            GUILayout.EndVertical();

            if (test)
            {
                GUILayout.Space(15);
                if (tempFile != null) tempFile.VisualizeTest();
                else Debug.Log("temp was null");
                GUILayout.Space(15);
            }
        }
        
        private EditorFile[,] GroupChildFiles(List<EditorFile> files)
        {
            //This method groups files by rows of 3. You can edit this
            //to change visuals.
            int size = files.Count;
            int rows = (size / 3) + 1;
            EditorFile[,] groupedFiles = new EditorFile[rows, 3];
            int index = 0;
            for (int i = 0; i < rows; i++)
            for (int j = 0; j < 3; j++)
                if (i * 3 + j <= size - 1)
                    groupedFiles[i, j] = files[index++];

            return groupedFiles;
        }

        private void VisualizeChildFiles()
        {
            GUIContent[] contents = childFiles.Select(x => x.FileContent).ToArray();

            for (int i = 0; i < contents.Length; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(depth * 15 + 30);
                var btn = GUILayout.Button(contents[i], StyleReferences.FileThumbnail, GUILayout.MinWidth(256),
                    GUILayout.Height(32));
                if (btn)
                {
                    Object obj = AssetDatabase.LoadAssetAtPath<Object>(contents[i].tooltip);
                    Selection.activeObject = obj;
                }
                GUILayout.EndHorizontal();
            }

           // selectedFile = GUILayout.SelectionGrid(selectedFile, contents, 3, GUILayout.MinWidth(256), GUILayout.Height(32));

        }
    }

}