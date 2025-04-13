using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject card;
    [SerializeField] private Vector3[] cards;
    [SerializeField] private Texture2D[] images;
    [SerializeField] private float startX;
    [SerializeField] private float startY;
    [SerializeField] private float planeZ;
    [SerializeField] private float deltaX = 1.1f;
    [SerializeField] private float deltaY = 1.1f;
    [SerializeField] private int columns = 5;
    [SerializeField] int rows = 6;

    [SerializeField] private GameObject winUI;

    [SerializeField] private AudioSource matchedPairAudioSource;

    private int pairs;
    
    InteractiveCard selectedCard1;
    InteractiveCard selectedCard2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winUI.SetActive(false);
        if (rows * columns != images.Length * 2)
        {
            Debug.LogWarning("Number of r*c is not equal to provided cards, quit...");

            return;
        }

        pairs = columns * rows / 2;
        
        System.Random random = new System.Random();
        images = images.OrderBy(x => random.Next()).ToArray();
        
        cards = new Vector3[rows*columns];

        float dx = startX;
        float dy = startY;

        int counter = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                cards[counter++] = new Vector3(dx, dy, planeZ);
                dx += deltaX;
            }
            
            dx = startX;
            dy += deltaY;
        }
        
        cards = cards.OrderBy(x => random.Next()).ToArray();
        
        //Start creating (instantiate) cards, setting images etc
        counter = 0;

        int row = 0;

        foreach (Vector3 pos in cards)
        {
            GameObject go = Instantiate(card);
            
            go.SetActive(true);
            
            go.transform.position = pos;
            
            go.GetComponent<MeshRenderer>().material.SetTexture("_MainTexture",images[row]);

            go.GetComponent<InteractiveCard>().onClicked += SelectedCard;
            
            go.GetComponent<InteractiveCard>().imageName = images[row].name;

            counter++;

            if (counter % 2 == 0)
            {
                row++;
            }
        }
    }

    private void SelectedCard(InteractiveCard card, bool selected)
    {
        if (selectedCard1 == null && selected)
        {
            selectedCard1 = card;
        }
        
        else if (selectedCard1 == card && !selected)
        {
            selectedCard1.ResetMe();
            selectedCard1 = null;
        }
        else if (selectedCard2 != null && card == selectedCard2 && !selected)
        {
            selectedCard2.ResetMe();
            selectedCard2 = null;
        }
        else if (selectedCard2 == null && card != selectedCard1 && selected)
        {
            selectedCard2 = card;

            if (selectedCard1.Compare(selectedCard2))
            {
                //ok match!
                
                matchedPairAudioSource.Play();
                
                selectedCard1.HideAndDestroy();
                selectedCard2.HideAndDestroy();
                
                selectedCard1 = null;
                selectedCard2 = null;

                pairs--;

                if (pairs == 0)
                {
                    // Gameover
                    winUI.SetActive(true);
                    winUI.GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                //flip back
                
                selectedCard1.ResetMe();
                selectedCard2.ResetMe();
                
                selectedCard1 = null;
                selectedCard2 = null;
            }
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
