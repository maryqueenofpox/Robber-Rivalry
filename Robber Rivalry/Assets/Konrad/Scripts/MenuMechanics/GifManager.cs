using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GifManager : MonoBehaviour
{
    [SerializeField] List<GameObject> gifs = new List<GameObject>();

    int index;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject item in gifs)
        {
            item.SetActive(false);
        }

        index = 0;
        gifs[0].SetActive(true);
    }

    public void NextButton()
    {
        gifs[index].SetActive(false);
        index++;

        if (index > gifs.Count - 1)
            index = 0;

        gifs[index].SetActive(true);
    }

    public void BackButton()
    {
        gifs[index].SetActive(false);
        index--;

        if (index < gifs.Count - 1)
            index = 12;

        gifs[index].SetActive(true);
    }
}
