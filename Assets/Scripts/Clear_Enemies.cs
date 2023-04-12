using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_Enemies : MonoBehaviour
{
    public void Hit()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject enemy in enemies)
        {
            enemy.gameObject.SendMessage("Died", SendMessageOptions.DontRequireReceiver);
        }
        Destroy(gameObject);
    }
}
