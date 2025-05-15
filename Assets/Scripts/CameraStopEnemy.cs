using UnityEngine;

// This script should be attached to an enemy that should stop the camera when the player is near.
// The enemy GameObject also needs a trigger collider (e.g., BoxCollider2D or CircleCollider2D)
// set to "Is Trigger" to define the area where the camera should stop.
public class CameraStopEnemy : MonoBehaviour
{
    private SideScrollingCamera sideScrollingCamera;
    private bool cameraStopped = false; // Flag to track if THIS script stopped the camera

    private void Awake()
    {
        // Find the SideScrollingCamera component on the Main Camera
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            sideScrollingCamera = mainCamera.GetComponent<SideScrollingCamera>();
            if (sideScrollingCamera == null)
            {
                Debug.LogWarning("CameraStopEnemy: SideScrollingCamera component not found on Main Camera.");
            }
        }
        else
        {
            Debug.LogWarning("CameraStopEnemy: Main Camera not found.");
        }
    }

    // Called when another collider enters the trigger collider attached to this GameObject
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering collider belongs to the Player
        if (other.CompareTag("Player") && sideScrollingCamera != null && sideScrollingCamera.enabled)
        {
            // Disable the camera script to stop its movement
            sideScrollingCamera.enabled = false;
            cameraStopped = true; // Mark that we stopped the camera
            Debug.Log($"CameraStopEnemy ({gameObject.name}): Player entered trigger. Camera stopped.");
        }
    }

    // Called when another collider exits the trigger collider attached to this GameObject
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the exiting collider belongs to the Player AND we were the one who stopped the camera
        if (other.CompareTag("Player") && sideScrollingCamera != null && cameraStopped)
        {
            // Enable the camera script to resume its movement
            sideScrollingCamera.enabled = true;
            cameraStopped = false; // Mark that the camera is now moving again
            Debug.Log($"CameraStopEnemy ({gameObject.name}): Player exited trigger. Camera resumed.");
        }
    }

    // This method should be called by the enemy script when the enemy dies
    public void OnEnemyDeath()
    {
        // Ensure the camera is re-enabled if it was stopped by this script
        if (sideScrollingCamera != null && cameraStopped)
        {
            sideScrollingCamera.enabled = true;
            cameraStopped = false;
            Debug.Log($"CameraStopEnemy ({gameObject.name}): Enemy died. Camera resumed.");
        }
        // Note: OnTriggerExit2D might not be called reliably when the enemy object is destroyed,
        // so calling OnEnemyDeath is the failsafe to ensure the camera resumes.
    }

    // Ensure camera resumes if this script is disabled for any reason (e.g., scene change)
    private void OnDisable()
    {
        if (sideScrollingCamera != null && cameraStopped)
        {
            sideScrollingCamera.enabled = true;
            cameraStopped = false;
            Debug.Log($"CameraStopEnemy ({gameObject.name}): Script disabled. Camera resumed.");
        }
    }
}
