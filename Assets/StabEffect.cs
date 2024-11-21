using UnityEngine;

public class StabEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject stabSprite;

    public void ActivateStabSprite()
    {
        if (stabSprite != null)
        {
            stabSprite.SetActive(true);
            Debug.Log("Activating sprite");
        }
        else
        {
            Debug.LogWarning("Stab sprite not assigned!");
        }
    }

    public void DeactivateStabSprite()
    {
        if (stabSprite != null)
        {
            stabSprite.SetActive(false);
            Debug.Log("Deactivating sprite");
        }
    }
}