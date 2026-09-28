using UnityEngine;
using UnityEngine.UI;

public class MiniMapUI : MonoBehaviour
{
    [SerializeField] private RectTransform mapContainer;
    [SerializeField] private GameObject roomIconPrefab;

    [SerializeField] private float roomDistance = 80f;

    private Vector2 currentMapPosition;

    public void AddRoom(int roomID, int doorUsed)
    {
        GameObject icon = Instantiate(roomIconPrefab, mapContainer);

        RectTransform rect = icon.GetComponent<RectTransform>();

        if (roomID == 1)
        {
            currentMapPosition = Vector2.zero;
        }

        else if (doorUsed == 1)
        {
            currentMapPosition += Vector2.right * roomDistance;
        }
        else if (doorUsed == 2) 
        {
            currentMapPosition += Vector2.up * roomDistance;
        }
        rect.anchoredPosition = currentMapPosition;
    }
}