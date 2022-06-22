using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] private int dotsNumber;
    [SerializeField] private GameObject dotsParent;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private float dotSpacing;
    [SerializeField] [Range(0.01f,0.3f)] private float dotMinScale;
    [SerializeField] [Range(0.3f,1f)] private float dotMaxScale;
     
    private Transform[] dotsList;
    private Vector2 pos;
    private float timeStamp;

    private void Start()
    {
        Hide();
        PreparateDots();
    }  
    private void PreparateDots()
    {
        dotsList = new Transform[dotsNumber];
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

        float scale = dotMaxScale;
        float scaleFactore = scale / dotsNumber;
        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, null).transform;
            dotsList[i].parent = dotsParent.transform;

            dotsList[i].localScale = Vector3.one * scale;
            if (scale > dotMinScale)
            {
                scale -= scaleFactore;
            }
        }
    } 
    public void UpdateDots(Vector3 ballPos, Vector2 force)
    {
        timeStamp = dotSpacing;
        for (int i = 0; i < dotsNumber; i++)
        {
            pos.x = (ballPos.x + force.x * timeStamp); 
            pos.y = (ballPos.y + force.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2;
            dotsList[i].position = pos;
            timeStamp += dotSpacing;
        }
    }
    public void Show()
    {
        dotsParent.SetActive(true);
    } 
    public void Hide()
    {
        dotsParent.SetActive(false);
    }
}
