using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Tori.UI
{
    [ExecuteInEditMode]
    public static class OptimizedScrollViewMenuEditor
    {
        const string PrefabPath = "Assets/Optimized Scorll View/Prefab/Optimized Scroll View.prefab";
        [MenuItem("GameObject/UI/Optimized Scroll View")]
        private static void CreateRecyclableScrollView()
        {
            GameObject selected = Selection.activeGameObject;

            if (!selected || !(selected.transform is RectTransform))
            {
                selected = GameObject.FindObjectOfType<Canvas>().gameObject;
            }

            if (!selected) return;

            GameObject asset = AssetDatabase.LoadAssetAtPath(PrefabPath, typeof(GameObject)) as GameObject;

            GameObject item = Object.Instantiate(asset);
            item.name = "Optimized Scroll View";

            item.transform.SetParent(selected.transform);
            item.transform.localPosition = Vector3.zero;
            Selection.activeGameObject = item;
            Undo.RegisterCreatedObjectUndo(item, "Create Optimized Scroll view");
        }
    }

}