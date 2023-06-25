using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace Nicoconut.AdvancedProjectExplorer
{
    public class EditorFile
    {
        private string path; //Path of the file
        private string extension; //Extension of the file(cs,jpg,anim..)
        private string fileName; //Name of the file


        private EditorFolder parentFolder; //Parent folder of the file
        private Texture2D fileIcon; //Icon belonging to class of the object

        private GUIContent fileContent;

        public GUIContent FileContent => fileContent;

        public string Extension
        {
            get
            {
                if (extension == null)
                {
                    extension = GetExtension().ToLower();
                }

                return extension;
            }
        }

        public EditorFile(string filePath, EditorFolder folder)
        {
            path = filePath;
            parentFolder = folder;
            fileName = Path.GetFileName(filePath);

            Object fileobj = AssetDatabase.LoadAssetAtPath(this.path, typeof(Object));
            fileIcon = AssetPreview.GetMiniThumbnail(fileobj);

            fileContent = new GUIContent(fileName, fileIcon, path);
        }

        public void VisualizeFile()
        {
            GUILayout.BeginHorizontal(EditorStyles.helpBox, GUILayout.Width(256));
            GUILayout.Label(fileContent, GUILayout.Width(256), GUILayout.Height(32));
            GUILayout.EndHorizontal();
        }

        public void VisualizeTest()
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal(GUILayout.Width(256));
            GUILayout.Button(fileContent, GUILayout.Width(256), GUILayout.Height(32));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            VisualizeWithStyle(StyleReferences.FileThumbnail, fileContent);

            VisualizeWithStyle(EditorStyles.iconButton, fileContent);
            VisualizeWithStyle(EditorStyles.miniButton, fileContent);
            VisualizeWithStyle(EditorStyles.radioButton, fileContent);
            VisualizeWithStyle(EditorStyles.toolbarButton, fileContent);
            VisualizeWithStyle(EditorStyles.miniButtonLeft, fileContent);
            VisualizeWithStyle(EditorStyles.miniButtonMid, fileContent);
            VisualizeWithStyle(EditorStyles.miniButtonRight, fileContent);
            VisualizeWithStyle(EditorStyles.inspectorDefaultMargins, fileContent);
            VisualizeWithStyle(EditorStyles.toggle, fileContent);
            VisualizeWithStyle(EditorStyles.popup, fileContent);
            VisualizeWithStyle(EditorStyles.toolbar, fileContent);
            VisualizeWithStyle(EditorStyles.foldout, fileContent);
            VisualizeWithStyle(EditorStyles.label, fileContent);
            VisualizeWithStyle(EditorStyles.boldLabel, fileContent);
            VisualizeWithStyle(EditorStyles.colorField, fileContent);
            VisualizeWithStyle(EditorStyles.foldoutHeader, fileContent);
            VisualizeWithStyle(EditorStyles.helpBox, fileContent);
            VisualizeWithStyle(EditorStyles.largeLabel, fileContent);
            VisualizeWithStyle(EditorStyles.linkLabel, fileContent);
            VisualizeWithStyle(EditorStyles.miniLabel, fileContent);
            VisualizeWithStyle(EditorStyles.wordWrappedLabel, fileContent);
            VisualizeWithStyle(EditorStyles.centeredGreyMiniLabel, fileContent);
            VisualizeWithStyle(EditorStyles.wordWrappedMiniLabel, fileContent);
            VisualizeWithStyle(EditorStyles.numberField, fileContent);
            VisualizeWithStyle(EditorStyles.objectField, fileContent);
            VisualizeWithStyle(EditorStyles.textField, fileContent);
            VisualizeWithStyle(EditorStyles.layerMaskField, fileContent);
            VisualizeWithStyle(EditorStyles.miniTextField, fileContent);
            VisualizeWithStyle(EditorStyles.objectFieldThumb, fileContent);
            VisualizeWithStyle(EditorStyles.toolbarSearchField, fileContent);
            VisualizeWithStyle(EditorStyles.toolbarTextField, fileContent);
            VisualizeWithStyle(EditorStyles.objectFieldMiniThumb, fileContent);
            VisualizeWithStyle(EditorStyles.selectionRect, fileContent);
            VisualizeWithStyle(EditorStyles.textArea, fileContent);
            VisualizeWithStyle(EditorStyles.toggleGroup, fileContent);
            VisualizeWithStyle(EditorStyles.toolbarPopup, fileContent);
            VisualizeWithStyle(EditorStyles.toolbarDropDown, fileContent);
            VisualizeWithStyle(EditorStyles.foldoutPreDrop, fileContent);
        }

        private void VisualizeWithStyle(GUIStyle style, GUIContent content)
        {
            GUILayout.BeginVertical();
            //GUILayout.BeginHorizontal(EditorStyles.helpBox,GUILayout.Width(256));
            GUILayout.BeginHorizontal(GUILayout.Width(256));
            GUILayout.Button(content, style, GUILayout.Width(256), GUILayout.Height(32));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        private string GetExtension()
        {
            return Path.GetExtension(path);
        }
    }

}