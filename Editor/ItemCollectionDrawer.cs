using Reshyl.Inventory;
using UnityEditor;
using UnityEngine;

namespace ReshylEditor.Inventory
{
    [CustomPropertyDrawer(typeof(ItemCollection))]
    public class ItemCollectionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (Application.isPlaying)
                GUI.enabled = false;

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var itemProp = property.FindPropertyRelative("item");
            var amountProp = property.FindPropertyRelative("amount");

            Rect itemRect = new Rect(position.x, position.y, position.width * 0.65f, position.height);
            Rect amountRect = new Rect(position.x + position.width * 0.68f, position.y, position.width * 0.32f, position.height);

            EditorGUI.PropertyField(itemRect, itemProp, GUIContent.none);
            EditorGUI.PropertyField(amountRect, amountProp, GUIContent.none);

            GUI.enabled = true;
            EditorGUI.EndProperty();
        }
    }

}
