using UnityEngine;

public class Cup : MonoBehaviour
{
    [SerializeField] private GameObject NextLvlPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            NextLvlPanel.SetActive(true);
        }
    }
}
