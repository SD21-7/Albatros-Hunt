using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAmmo : MonoBehaviour, IDragHandler{

    public void OnDrag(PointerEventData eventData){
       Debug.Log("Hoppa!");
    }
    
}
