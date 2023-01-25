using System.Collections.Generic;
using UnityEngine;

public class TEST_StarsBehaviour : MonoBehaviour
{
    private const float Speed = 10.0f;
    private float ScreenEdge;
    List<RectTransform> StarsTransforms = new List<RectTransform>();

    private void Start()
    {
        // Get background RectTransform and calcucate screen edge,
        // it is half size of screen width, as the background image is centered into middle of screen
        ScreenEdge = gameObject.GetComponent<RectTransform>().rect.width * 0.5f;

        // Get each child RectTransforms
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            StarsTransforms.Add(gameObject.transform.GetChild(i).GetComponent<RectTransform>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (RectTransform Star in StarsTransforms)
        {
            // Move the stars
            Star.localPosition = new Vector3(Star.localPosition.x + (Time.deltaTime * Speed), Star.localPosition.y, 0.0f);

            // Set new star position if the star position is out of the screen
            if (Star.localPosition.x > ScreenEdge)
            {
                Star.localPosition = new Vector3(-ScreenEdge, Star.localPosition.y, Star.localPosition.z);
            }
        }
    }
}
