using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopLimit : MonoBehaviour{
    
    [SerializeField] private TMP_Text scoreValueGM;
    [SerializeField] private GameObject moveplayer;

    private void OnTriggerStay2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Fruit"))
        {
            Fruit fruitScript = collision.gameObject.GetComponent<Fruit>();
            if (fruitScript != null && fruitScript.hasBeenDropped)
            {
                MovePlayer.instance.partieEnCours = false;
                MovePlayer.instance.getGameOver().SetActive(true);
                scoreValueGM.text = MovePlayer.instance.currentScore.ToString();
                foreach(Transform child in MovePlayer.instance.getBorders().transform) {
                        Destroy(child.gameObject);
                }
                if(moveplayer.transform.childCount > 0) {
                    Transform child = moveplayer.transform.GetChild(0);
                        Destroy(child.gameObject);
                }
            }
        }
    }
}
