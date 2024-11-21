 using UnityEngine;

public class SmallSwordEffects : MonoBehaviour
{
    [SerializeField]
    private GameObject thrustTrail;

    public void ActivateSwordTrail()
    {
        if (thrustTrail != null)
        {
            thrustTrail.SetActive(true);
            Debug.Log("Sword trail activated");
        }
    }

    public void DeactivateSwordTrail()
    {
        if (thrustTrail != null)
        {
            thrustTrail.SetActive(false);
            Debug.Log("Sword trail deactivated");
        }
    }
}