using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private bool allOccupied = false;
    private bool closing = false;
    public bool Closing
    {
        get { return closing; }
        private set
        {
            closing = value;
            Debug.Log("Closing state changed to: " + value);
            if (value)
            {
                ActivateClosingObjects();
            }
            else
            {
                DeactivateObjectsByKeys();
                ResetActivationHistory();
            }
        }
    }

    private bool rightOrder = false;
    public bool RightOrder
    {
        get { return rightOrder; }
        private set
        {
            rightOrder = value;
            Debug.Log("Right order state changed to: " + value);
        }
    }

    private int score = 0;

    public List<GameObject> objectsToActivateByKeys = new List<GameObject>();
    public List<GameObject> objectsToActivateOnClosing = new List<GameObject>();
    public KeyCode[] activationKeys;
    public float delayToDeactivate = 0.6f;
    public GameObject orderCheckObject;

    private List<KeyCode> pressedKeys = new List<KeyCode>();

    void Update()
    {
        allOccupied = CheckAllOccupied();

        if (allOccupied)
        {
            Closing = true;
        }

        if (Closing == true)
        {
            for (int i = 0; i < activationKeys.Length; i++)
            {
                if (Input.GetKeyDown(activationKeys[i]))
                {
                    if (Closing)
                    {
                        objectsToActivateByKeys[i].SetActive(true);
                        objectsToActivateByKeys[i].transform.SetAsLastSibling();
                        SpriteRenderer renderer = objectsToActivateByKeys[i].GetComponent<SpriteRenderer>();
                        if (renderer != null)
                        {
                            renderer.sortingOrder = GetHighestSortingOrder() + 1;
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < activationKeys.Length; i++)
            {
                if (Input.GetKeyDown(activationKeys[i]))
                {
                    objectsToActivateByKeys[i].SetActive(false);
                }
            }
        }
    }

    private bool CheckAllOccupied()
    {
        foreach (Slot slot in FindObjectOfType<SlotManager>().slots)
        {
            if (slot.currentState != Slot.SlotState.Occupato)
            {
                return false;
            }
        }
        return true;
    }

    private void ActivateClosingObjects()
    {
        foreach (GameObject obj in objectsToActivateOnClosing)
        {
            obj.SetActive(true);
        }
    }

    private void DeactivateObjectsByKeys()
    {
        foreach (GameObject obj in objectsToActivateByKeys)
        {
            obj.SetActive(false);
        }
    }

    private bool CheckAllObjectsActivated()
    {
        foreach (GameObject obj in objectsToActivateByKeys)
        {
            if (!obj.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    private void CheckOrderOfActivation()
    {
        Transform firstChild = orderCheckObject.transform.GetChild(0);
        Transform secondChild = orderCheckObject.transform.GetChild(1);

        if ((firstChild == objectsToActivateByKeys[0].transform || secondChild == objectsToActivateByKeys[0].transform) &&
            (firstChild == objectsToActivateByKeys[1].transform || secondChild == objectsToActivateByKeys[1].transform))
        {
            RightOrder = true;
        }
        else
        {
            RightOrder = false;
            DeactivateObjectsByKeysWithDelay(delayToDeactivate);
        }
    }

    private void DeactivateObjectsByKeysWithDelay(float delay)
    {
        StartCoroutine(DeactivateObjectsCoroutine(delay));
    }

    private IEnumerator DeactivateObjectsCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject obj in objectsToActivateByKeys)
        {
            obj.SetActive(false);
        }
    }

    private void ResetActivationHistory()
    {
        pressedKeys.Clear();
    }

    private int GetHighestSortingOrder()
    {
        int highestSortingOrder = int.MinValue;
        foreach (GameObject obj in objectsToActivateByKeys)
        {
            if (obj.activeSelf)
            {
                SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
                if (renderer != null && renderer.sortingOrder > highestSortingOrder)
                {
                    highestSortingOrder = renderer.sortingOrder;
                }
            }
        }
        return highestSortingOrder;
    }
}
