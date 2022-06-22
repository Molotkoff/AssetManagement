using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace Molotkoff.AssetManagment
{
    [CustomPropertyDrawer(typeof(Container<>), true)]
    public class ContainerPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position = DrawContainer(position, property);
        }

        private Rect DrawContainer(Rect position, SerializedProperty property)
        { //TO-DO: NICE DISPSLAY OF PROPERTY
            const float CONTAINER_NAME_WIDTH = 200;
            const float HEIGHT = 15;
            
            var containerNameProperty = property.FindPropertyRelative("_id");

            var containerNameRect = new Rect(position.position, new Vector2(CONTAINER_NAME_WIDTH, HEIGHT));
            EditorGUI.LabelField(containerNameRect, property.displayName);

            //popup container choose
            var containersScheme = AssetManagment.instance.Scheme();

            var containerType = TypeUtil.GetFieldTypeFromSerializibleProperty(property).GetGenericArguments()[0];
            var allContainers = containersScheme.GetContainers(containerType);

            var myContainer = containerNameProperty.stringValue;
            var myContainerIndex = Array.IndexOf(allContainers, myContainer);

            var containerPopupPosition = new Rect(position.position + new Vector2(CONTAINER_NAME_WIDTH + 10, 0), 
                                                  new Vector2(CONTAINER_NAME_WIDTH, HEIGHT));
            
            var newIndex = EditorGUI.Popup(containerPopupPosition, myContainerIndex, allContainers);

            if (newIndex != myContainerIndex)
            {
                containerNameProperty.stringValue = allContainers[newIndex];
                //property.serializedObject.ApplyModifiedProperties();
            }

            return new Rect(position.position + new Vector2(0, HEIGHT), position.size);
        }
    }
}