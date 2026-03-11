using UnityEngine;
using UnityEngine.EventSystems;

public class Placement : MonoBehaviour, IPointerClickHandler
{
    //class used to manage the placement of the Turrets in game

    [SerializeField] bool isPlaced = false; //bool for checking if the Slot is Empty
    
    /*When the Mouse sends the input to the collider:
     * checks if it's in the BuildMode & the Slot is Empty;
     * if it's true, spawn the turret selected in the slot clicked;
     * change the color of the Slot;
     * subtract the Money based on the price & update the Money Counter;
     * Adds the slot to the counter in UIManager.cs;
     * and flag the Slot so it can't be selected again.
     * exit the Build mode
    */
    public void OnPointerClick(PointerEventData eventData)
    {
        if (UIManager.Instance.buildMode && isPlaced == false)
        {
            Instantiate(UIManager.Instance.selectedTur, transform.position, transform.rotation, transform);
            transform.GetComponent<Renderer>().material.color = Color.blue;
            UIManager.Instance.money -= UIManager.Instance.selectedPrice;
            UIManager.Instance.UpdateCounter();
            isPlaced = true;
            UIManager.Instance.usedSlots++;
        }
        UIManager.Instance.buildMode = false;
    }
}
