using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [HideInInspector] public MovePlayer movePlayer;
    public int fruitIndex;
    public bool hasBeenDropped = false;

    private void OnCollisionEnter2D(Collision2D collision) {

        hasBeenDropped = true;
        
        if(collision.gameObject.CompareTag("Fruit")) {

            Fruit collidedFruit = collision.gameObject.GetComponent<Fruit>();
            if(collidedFruit.fruitIndex == fruitIndex) {
                if(!gameObject.activeSelf || !collision.gameObject.activeSelf) 
                    return;
                
                collision.gameObject.SetActive(false);
                Destroy(collision.gameObject);

                if((fruitIndex + 1) < movePlayer.fruitsPrefabs.Length) {
                    GameObject newFruit = Instantiate(movePlayer.fruitsPrefabs[fruitIndex + 1].prefabs);
                    newFruit.transform.parent = MovePlayer.instance.getBorders().transform;
                    MovePlayer.instance.IncreaseScore(MovePlayer.instance.fruitsPrefabs[fruitIndex + 1].points);
                    newFruit.transform.position = transform.position;

                    Fruit newFruitSc = newFruit.GetComponent<Fruit>();
                    newFruitSc.movePlayer = MovePlayer.instance;
                    newFruitSc.fruitIndex = fruitIndex + 1;
                    
                    gameObject.SetActive(false);
                    Destroy(gameObject);
                }
            }
        }
    }
}
