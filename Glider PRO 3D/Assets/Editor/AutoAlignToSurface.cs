using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform))]
public class AutoAlignToSurface : Editor
{
    public float offsetDistance = 0.01f; // Small offset to prevent Z-fighting
    private KeyCode alignmentKey = KeyCode.F; // Set your preferred key here (e.g., A)

    private void OnSceneGUI()
    {
        // Check if the user presses the designated alignment key
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == alignmentKey)
        {
            Transform transform = (Transform)target;
            AlignToSurface(transform);
            Event.current.Use(); // Consume the event so it doesn't propagate
        }
    }

    private void AlignToSurface(Transform transform)
    {
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Undo.RecordObject(transform, "Align to Surface");

            // Move the object to the hit point
            transform.position = hit.point;

            // Get the surface normal and constrain rotation to Y-axis only
            Vector3 surfaceNormal = hit.normal;
            surfaceNormal.y = 0; // Ignore vertical components of the normal

            if (surfaceNormal != Vector3.zero) // Avoid errors when the normal is zero
            {
                // Rotate the painting to face the correct direction
                transform.rotation = Quaternion.LookRotation(-surfaceNormal);

                // Apply the same offset to prevent Z-fighting
                transform.position += surfaceNormal * offsetDistance;
            }
        }
    }
}
