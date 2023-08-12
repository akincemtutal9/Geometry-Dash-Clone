#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameAssets.Scripts.Portal
{
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(PortalBehaviour)), CanEditMultipleObjects]
    
    // By using this editor we avoid possible confusion when defining portal behaviors
    public class PortalEditor : Editor
    {
        public enum DisplayCategory
        {
            GameModes, MoveSpeed, Gravity
        }
        
        public DisplayCategory categoryToDisplay;

        bool firstTime = true;

        public override void OnInspectorGUI()
        {
            if (firstTime)
            {
            
                // Portal Behaviour int state reference
                //we match int state with the according game enums and match their id
                switch (serializedObject.FindProperty("state").intValue)
                {
                    case 0:
                        categoryToDisplay = DisplayCategory.MoveSpeed;
                        break;
                    case 1:
                        categoryToDisplay = DisplayCategory.GameModes;
                        break;
                    case 2:
                        categoryToDisplay = DisplayCategory.Gravity;
                        break;
                }
            }
            else
                categoryToDisplay = (DisplayCategory)EditorGUILayout.EnumPopup("Display", categoryToDisplay);

            EditorGUILayout.Space();

            
            // In here we take the references in portal behaviours speed gameMode gravity
            // and edit the enum values inside of them 
            // that way while testing the game it will be more effective while using portals
            switch (categoryToDisplay)
            {
                case DisplayCategory.MoveSpeed:
                    DisplayProperty("speed", 0);
                    break;
                
                case DisplayCategory.GameModes:
                    DisplayProperty("gameMode", 1);
                    break;

                case DisplayCategory.Gravity:
                    DisplayProperty("gravity", 2);
                    break;
            }

            firstTime = false;

            serializedObject.ApplyModifiedProperties();
        }

        void DisplayProperty(string property, int propNumber)
        {
            try
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(property));
            }
            catch
            {
                // ignored
            }

            serializedObject.FindProperty("state").intValue = propNumber;
        }
    }
    #endif
}
