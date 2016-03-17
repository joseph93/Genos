using UnityEngine;
using System.Collections;

public abstract class ExhibitionContent : MonoBehaviour
{

    private string contentID;
    private string title;
    private int storylineID;

    public ExhibitionContent(string title, string contentID, int storylineID)
    {
        this.title = title;
        this.contentID = contentID;
        this.storylineID = storylineID;
    }
}
