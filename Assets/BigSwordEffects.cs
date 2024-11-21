 using UnityEngine;

public class BigSwordEffects : MonoBehaviour
{
    [SerializeField]
    private GameObject swordTrails;

    public void ActivateSwordTrail()
    {
        if (swordTrails != null)
        {
            swordTrails.SetActive(true);
            Debug.Log("Sword trail activated");
        }
    }

    public void DeactivateSwordTrail()
    {
        if (swordTrails != null)
        {
            swordTrails.SetActive(false);
            Debug.Log("Sword trail deactivated");
        }
    }
}