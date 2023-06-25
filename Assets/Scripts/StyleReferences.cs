using UnityEditor;
using UnityEngine;

namespace Nicoconut.AdvancedProjectExplorer
{
    public static class StyleReferences
    {
        private static GUIStyle fileThumbnail;

        public static GUIStyle FileThumbnail
        {
            get
            {
                if (fileThumbnail == null)
                {
                    Debug.Log("Recreating bg");
                    var btnskin = GUI.skin.button;
                    var labelskin = GUI.skin.label;

                    fileThumbnail = new GUIStyle(btnskin)
                    {
                        alignment = TextAnchor.MiddleLeft
                    };
                }

                return fileThumbnail;
            }
        }
        
        private static GUIStyle rightAlignedTextField;

        public static GUIStyle RightAlignedTextField
        {
            get
            {
                if (rightAlignedTextField == null)
                {
                    rightAlignedTextField = new GUIStyle(EditorStyles.textField)
                    {
                        alignment = TextAnchor.MiddleRight
                    };
                }

                return rightAlignedTextField;
            }
        }

        private static Texture2D MakeTransparent(Texture2D tex)
        {
            var pixels = tex.GetPixels();

            for (int i = 0; i < pixels.Length; i++)
            {
                if (pixels[i].a != 0) pixels[i].a = 0.1f;
            }
            
            tex.SetPixels(pixels);
            tex.Apply();

            return tex;
        }

    }
}