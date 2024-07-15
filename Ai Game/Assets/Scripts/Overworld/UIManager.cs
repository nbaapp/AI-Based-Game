using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI AIText;
    public GameObject northButton;
    public GameObject southButton;
    public GameObject eastButton;
    public GameObject westButton;

    private void Start()
    {
        DisableDirections();
    }

    public void EnableDirections()
    {
        northButton.SetActive(true);
        southButton.SetActive(true);
        eastButton.SetActive(true);
        westButton.SetActive(true);
    }

    public void DisableDirections()
    {
        northButton.SetActive(false);
        southButton.SetActive(false);
        eastButton.SetActive(false);
        westButton.SetActive(false);
    }

    public void EnableDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                northButton.SetActive(true);
                break;
            case Direction.South:
                southButton.SetActive(true);
                break;
            case Direction.East:
                eastButton.SetActive(true);
                break;
            case Direction.West:
                westButton.SetActive(true);
                break;
        }
    }

    public void DisableDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                northButton.SetActive(false);
                break;
            case Direction.South:
                southButton.SetActive(false);
                break;
            case Direction.East:
                eastButton.SetActive(false);
                break;
            case Direction.West:
                westButton.SetActive(false);
                break;
        }
    }

    public void SetAIText(string text)
    {
        AIText.text = text;
    }

}
