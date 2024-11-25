 using UnityEngine;

public class SmallSwordEffects : MonoBehaviour
{
    [SerializeField]
    private GameObject thrustTrail;

    public void ActivateSwordTrail()
    {
        if (thrustTrail != null)
        {
            thrustTrail.SetActive(false);
            thrustTrail.SetActive(true);
  
        }
    }

    public void DeactivateSwordTrail()
    {
        if (thrustTrail != null)
        {
            thrustTrail.SetActive(false);
    
        }
    }
}