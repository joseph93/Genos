using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogueBox : MonoBehaviour
{
    public Text introText;
    private string testIntroText = "Welcome to the Musée des Ondes! Manipulate the 3D-outdoor to explore around the building. Follow Nipper and press Enter to discover the inside of the Museum.";
    public float speedOfText = 0.05f;
    void Awake()
    {

        //   introText.text = testIntroText;
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine("AutoType");

    }

    // Update is called once per frame
    void Update()
    {
        Touch[] touches = Input.touches;
        if (touches.Length > 0)
        {
            if (touches.Length == 2)
            {
                introText.text = testIntroText;
            }
        }
    }
    IEnumerator AutoType()
    {
        foreach (char letter in testIntroText.ToCharArray())
        {
            introText.text += letter;
            yield return new WaitForSeconds(speedOfText);
        }
    }
}
