using System;
using HalfDiggers.Runner;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PickableObjectMarker))]
    public class PickableObjectSpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(PickableObjectMarker pickableObjectMarker, GizmoType gizmo)
        {
            Gizmos.color = new Color32(255, 0,255, 128);
            Vector3 position = pickableObjectMarker.transform.position;
            Gizmos.DrawSphere(position, .5f);
           

            Vector3 labelPosition = new(position.x - 0.5f, position.y + 1f, position.z);
            string text =
                $"{Enum.GetName(typeof(GameObjectsTypeId), pickableObjectMarker.PickableObjectStaticData.GameObjectsTypeId)?.ToUpper()}";
            Handles.Label(labelPosition, text);
        }
    }
}