using UnityEngine;

public class FloatingRock : MonoBehaviour
{
    private RectTransform ThisRectTransform;
    private float InitialPositionY;
    private float FloatingOffSet;
    private float CurrentFloatingSpeed;
    private float MAX_FLOATING_SPEED = 16.0f;

    // Start is called before the first frame update
    private void Start()
    {
        ThisRectTransform = gameObject.GetComponent<RectTransform>();
        InitialPositionY = ThisRectTransform.localPosition.y;
        FloatingOffSet = ThisRectTransform.rect.height * 0.02f;
        CurrentFloatingSpeed = MAX_FLOATING_SPEED;
    }

    // Update is called once per frame
    private void Update()
    {
        ThisRectTransform.localPosition = new Vector2(ThisRectTransform.localPosition.x, ThisRectTransform.localPosition.y - (Time.deltaTime * CurrentFloatingSpeed));

        if (ThisRectTransform.localPosition.y < (InitialPositionY - FloatingOffSet))
        {
            CurrentFloatingSpeed = -MAX_FLOATING_SPEED;
        }
        else if (ThisRectTransform.localPosition.y > (InitialPositionY + FloatingOffSet))
        {
            CurrentFloatingSpeed = MAX_FLOATING_SPEED;
        }
    }
}
