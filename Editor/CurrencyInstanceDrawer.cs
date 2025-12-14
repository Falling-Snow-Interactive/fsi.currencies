using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Currencies
{
    [CustomPropertyDrawer(typeof(CurrencyInstance<,>), true)]
    public class CurrencyInstanceDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new() { style = { flexDirection = FlexDirection.Row } };

            SerializedProperty dataProp = property.FindPropertyRelative("data");
            SerializedProperty amountProp = property.FindPropertyRelative("amount");

            PropertyField dataField = new(dataProp)
                                      {
                                          label = property.displayName, name = property.displayName,
                                          style = { flexGrow = 1 }
                                      };
            PropertyField amountField = new(amountProp) { label = "" };

            root.Add(dataField);
            root.Add(amountField);
            
            return root;
        }
    }
}