using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject currentAmmo;

    public void setCurrentAmmo(int ammo){
        currentAmmo.GetComponent<Text>().text = ammo.ToString();
    }
}
