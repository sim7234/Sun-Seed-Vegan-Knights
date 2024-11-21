 using UnityEngine;

public class BigSwordEffects : MonoBehaviour
{
    [SerializeField]
    private GameObject swordTrails;

    public void ActivateBigSwordTrail()
    {
        if (swordTrails != null)
        {
            swordTrails.SetActive(false);
            swordTrails.SetActive(true);
            Debug.Log("Sword trail activated");
        }
    }

    public void DeactivateBigSwordTrail()
    {
        if (swordTrails != null)
        {
            swordTrails.SetActive(false);
            Debug.Log("Sword trail deactivated");
        }
    }
}