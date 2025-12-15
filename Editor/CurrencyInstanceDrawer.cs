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
            root.AddToClassList("fsi-property-row");
            FsiUiEditorUtility.AddUss(root);

            VisualElement data = new();
            data.AddToClassList("fsi-property-row");

            root.Add(data);
            
            SerializedProperty currencyProp = property.FindPropertyRelative("currency");
            SerializedProperty amountProp = property.FindPropertyRelative("amount");

            PropertyField currencyField = new(currencyProp);
            currencyField.AddToClassList("fsi-property-field");
            
            IntegerField amountField = new() { label = property.displayName };
            amountField.BindProperty(amountProp);
            amountField.AddToClassList("fsi-property-field");
            amountField.RegisterValueChangedCallback(_ =>
                                                     {
                                                         amountField.label = property.displayName;
                                                     });
            
            data.Add(amountField);
            data.Add(currencyField);
            
            return root;
        }
    }
}