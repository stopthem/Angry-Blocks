using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBallBar : MonoBehaviour
{

    public RectTransform extraBallInner;

    private GameController gameController;

    private float currentWidth, addWidth, totalWidth;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Start()
    {
        totalWidth = 5;
        currentWidth = 0;
    }

    private void Update()
    {
        if (currentWidth >= totalWidth)
        {
            gameController.ballsCount++;
            gameController.ballsCountText.text = gameController.ballsCount.ToString();
            currentWidth = 0;
        }

        if (currentWidth >= addWidth)
        {
            addWidth += 1;
            extraBallInner.sizeDelta = new Vector2(addWidth, 1);
        }
        else
        {
            addWidth = currentWidth;
        }
    }

    public void IncreaseCurrentWidth()
    {
        int addRandom = Random.Range(1, 3);
        currentWidth = addRandom + currentWidth % 6;
    }
}
