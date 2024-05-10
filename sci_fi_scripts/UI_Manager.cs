using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private GameObject instrucionsPopUp;
    [SerializeField]
    private GameObject coin_image;
   
    public void updateAmmo(int count)
    {
        ammoText.text = "Municion: " + count;
    }
    public void setText(string text)
    {
        ammoText.text = text;
    }
    public void setPopUpVisible(bool visible)
    {
        instrucionsPopUp.SetActive(visible);
    }
    public void setCoinVisible(bool visible) { this.coin_image.SetActive(visible); }
   
}
