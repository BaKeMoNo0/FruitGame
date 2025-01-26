using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private GameObject holderNextFruit;
    [SerializeField] private GameObject saveHolderFruit;
    [SerializeField] private GameObject borders;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private TMP_Text scoreText;
    public int currentScore = 0;

    private float speed = 5f; // speed of player
    private float MaxXPos = 2.45f; // stop player at the right
    private float MinXPos = -2.15f; // stop player at the left

    private float currentMaxXPos = 2.5f; // stop player at the right
    private float currentMinXPos = -2.0f; // stop player at the left

    private float gapBiggerFruit = 0.1f;
    private float YPos = 3.8f;
   
    // Variable to follow the state of key space
    private float timePression = 0.5f;
    private bool espaceActive = true;
    private float lastPresse;

    private GameObject nextFruit;
    private int nextFruitIndex;
    private GameObject nextNextFruit;
    private int nextNextFruitIndex;
    private GameObject SaveFruit;
    private int SaveFruitIndex;
    public bool partieEnCours = true;

    public FruitsList[] fruitsPrefabs;
    public static MovePlayer instance;

    void Start() {
        gameOver.SetActive(false);
        LoadNextFruit();
    }

    void Awake() {
        instance = this;
    }

    void Update()
    {
        KeyCode ml = GameData.moveLeft;
        KeyCode mr = GameData.moveRight;
        KeyCode df = GameData.dropFruit;
        KeyCode sf = GameData.saveFruit;
        KeyCode usf =  GameData.useSaveFruit;


        if(partieEnCours) {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitGame();
            }

            if(Input.GetKey(ml) && transform.position.x > currentMinXPos) {
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            } else if(Input.GetKey(mr) && transform.position.x < currentMaxXPos) {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }
            
            if (!espaceActive && ((Time.time - lastPresse) >= timePression))
            {
                espaceActive = true;
            }

            if(Input.GetKeyDown(df) && espaceActive) {
                GameObject newFruit = Instantiate(nextFruit);
                newFruit.transform.parent = borders.transform;
                newFruit.transform.position = transform.position - new Vector3(0.1f, 1.8f, 0);

                Fruit newFruitScript = newFruit.GetComponent<Fruit>();
                newFruitScript.movePlayer = this;
                newFruitScript.fruitIndex = nextFruitIndex;

                LoadNextFruit();
                espaceActive = false;
                lastPresse = Time.time;
            }

            if(Input.GetKeyDown(sf)) {
                if(saveHolderFruit.transform.childCount == 0) {
                    SaveFruit = nextFruit;
                    SaveFruitIndex = nextFruitIndex;

                    GameObject SaveFruitPreview = Instantiate(nextFruit, saveHolderFruit.transform.position, Quaternion.identity);
                    SaveFruitPreview.transform.parent = saveHolderFruit.transform;
                    SaveFruitPreview.GetComponent<Collider2D>().isTrigger = true;
                    SaveFruitPreview.GetComponent<Rigidbody2D>().isKinematic = true;
                    SaveFruitPreview.tag = "Fruit";

                    LoadNextFruit();
                }
            }

            if(Input.GetKeyDown(usf) && saveHolderFruit.transform.childCount > 0) {
                nextNextFruit = nextFruit;
                nextNextFruitIndex = nextFruitIndex;

                if(nextNextFruit != null) {
                    Transform child = holderNextFruit.transform.GetChild(0);
                    if (child.CompareTag("Fruit")) {
                        Destroy(child.gameObject);
                    }
                }

                GameObject nextNextFruitPreview = Instantiate(nextNextFruit, holderNextFruit.transform.position, Quaternion.identity);
                nextNextFruitPreview.transform.parent = holderNextFruit.transform;
                nextNextFruitPreview.GetComponent<Collider2D>().isTrigger = true;
                nextNextFruitPreview.GetComponent<Rigidbody2D>().isKinematic = true;
                nextNextFruitPreview.tag = "Fruit";

                nextFruit = SaveFruit;
                nextFruitIndex = SaveFruitIndex;

                if(nextFruit != null) {
                    Transform child = transform.GetChild(0);
                    if (child.CompareTag("Fruit")) {
                        Destroy(child.gameObject);
                    }
                }

                GameObject nextFruitPreview = Instantiate(nextFruit, transform.position, Quaternion.identity);
                nextFruitPreview.transform.parent = transform;
                nextFruitPreview.transform.localPosition = Vector3.zero - new Vector3(0.1f, 3.8f, 0);
                nextFruitPreview.GetComponent<Collider2D>().isTrigger = true;
                nextFruitPreview.GetComponent<Rigidbody2D>().isKinematic = true;
                nextFruitPreview.tag = "Fruit";

                if(saveHolderFruit.transform.childCount > 0) {
                    Transform child = saveHolderFruit.transform.GetChild(0);
                    if (child.CompareTag("Fruit")) {
                        Destroy(child.gameObject);
                    }
                }
            }
        }
    }

    void LoadNextFruit() {

        if(nextFruit != null) {
            Transform child = transform.GetChild(0);
            if (child.CompareTag("Fruit")) {
                Destroy(child.gameObject);
            }
        }
        
        if(nextNextFruit != null) {
            Transform child = holderNextFruit.transform.GetChild(0);
            if (child.CompareTag("Fruit")) {
                Destroy(child.gameObject);
            }
        }

        if(nextNextFruit == null) {
            nextFruitIndex = Random.Range(0, 5);
            nextFruit = fruitsPrefabs[nextFruitIndex].prefabs;
        } 
        else 
        {
            nextFruitIndex = nextNextFruitIndex;
            nextFruit = nextNextFruit;
        }

        GameObject nextFruitPreview = Instantiate(nextFruit, transform.position, Quaternion.identity);
        nextFruitPreview.transform.parent = transform;
        nextFruitPreview.transform.localPosition = Vector3.zero - new Vector3(0.1f, 3.8f, 0);
        nextFruitPreview.GetComponent<Collider2D>().isTrigger = true;
        nextFruitPreview.GetComponent<Rigidbody2D>().isKinematic = true;
        nextFruitPreview.tag = "Fruit";
        
        nextNextFruitIndex = Random.Range(0, 5);
        nextNextFruit = fruitsPrefabs[nextNextFruitIndex].prefabs;

        GameObject nextNextFruitPreview = Instantiate(nextNextFruit, holderNextFruit.transform.position, Quaternion.identity);
        nextNextFruitPreview.transform.parent = holderNextFruit.transform;
        nextNextFruitPreview.GetComponent<Collider2D>().isTrigger = true;
        nextNextFruitPreview.GetComponent<Rigidbody2D>().isKinematic = true;
        nextNextFruitPreview.tag = "Fruit";

        CalculatePlayerBound();
    }

    private void CalculatePlayerBound() {
        currentMaxXPos = MaxXPos - (nextFruitIndex * gapBiggerFruit);
        currentMinXPos = MinXPos + (nextFruitIndex * gapBiggerFruit);

        if(transform.position.x < currentMinXPos) {
            transform.position = new Vector3(currentMinXPos, YPos,0);
        }
        else if(transform.position.x > currentMaxXPos) {
            transform.position = new Vector3(currentMaxXPos, YPos,0);
        }
    }

    public void IncreaseScore(int value) {
        currentScore += value;
        scoreText.text = currentScore.ToString();
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void rejouer() {
        partieEnCours = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public GameObject getGameOver() {
        return gameOver;
    }


    public GameObject getBorders() {
        return borders;
    }

    public void SwitchToParam()
    {
        SceneManager.LoadScene("Param");
    }
}

[System.Serializable]
public class FruitsList{
    public GameObject prefabs;
    public int points;
}