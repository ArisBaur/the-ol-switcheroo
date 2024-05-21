using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tries_state_saver : MonoBehaviour
{
    public static int tries_counter;
    public float incrementCooldown = 1.0f; // Adjust as needed

    private bool isCooldownActive = false;

    // Function to increment tries counter with cooldown
    public void IncrementTries()
    {
        if (!isCooldownActive)
        {
            tries_counter++;
            isCooldownActive = true;
            Invoke(nameof(ResetCooldown), incrementCooldown);
        }
    }

    // Reset cooldown after specified time
    private void ResetCooldown()
    {
        isCooldownActive = false;
    }
}
