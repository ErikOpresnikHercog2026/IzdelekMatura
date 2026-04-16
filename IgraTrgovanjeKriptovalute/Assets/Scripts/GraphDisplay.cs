using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class GraphDisplay : MonoBehaviour
{
    [SerializeField]
    private Sprite grafTocka;
    [SerializeField]
    private RectTransform graphContainer;

    [SerializeField]
    private RectTransform labelTemplateX;
    [SerializeField]
    private RectTransform labelTemplateY;

    [SerializeField]
    private RectTransform graphOuter;

    private GameObject NarisiTockoGrafa(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = grafTocka;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    public void PokaziGraf(List<int> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float xSize = 50f;
        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject CircleGameObject = NarisiTockoGrafa(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                PoveziTockeGrafa(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, CircleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = CircleGameObject;

            /*RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphOuter);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -20f);
            labelX.GetComponent<TMP_Text>().text = i.ToString();*/
        }

        int separatorCount = 10;

        for (int i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphOuter);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(960f, normalizedValue * graphHeight);
            labelY.GetComponent<TMP_Text>().text = Mathf.RoundToInt (normalizedValue * yMaximum).ToString();
        }

    }

    private void PoveziTockeGrafa(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);

        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        Vector2 dir = (dotPositionB - dotPositionA).normalized;

        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        rectTransform.sizeDelta = new Vector2(distance, 3f);

        rectTransform.anchoredPosition = dotPositionA + dir * distance * 0.5f;

        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }
}
