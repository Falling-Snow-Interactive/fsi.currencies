using Fsi.Ui;
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
            VisualElement root = new();
            FsiUiEditorUtility.AddUss(root);
            
            root.AddToClassList("fsi-property-row");

            Label label = new(property.displayName);
            label.AddToClassList("fsi-property-label");

            root.Add(label);

            VisualElement data = new();
            data.AddToClassList("fsi-property-field");

            root.Add(data);
            
            SerializedProperty currencyProp = property.FindPropertyRelative("currency");
            SerializedProperty amountProp = property.FindPropertyRelative("amount");
            
            PropertyField currencyField = new(currencyProp)
                                      {
                                          style = { flexGrow = 1 }
                                      };
            PropertyField amountField = new(amountProp) { label = "" };
            
            data.Add(amountField);
            data.Add(currencyField);
            // data.Add(amountField);
            
            return root;
        }
    }
}