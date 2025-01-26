using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextReveal : MonoBehaviour
{
    public GameObject next;
    public float speed;

    public TextMeshProUGUI _text;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        _text.maxVisibleCharacters = 0;
        yield return new WaitForSecondsRealtime(0.1f);
        int count = _text.textInfo.characterCount;
        int iterator = 0;

        // Reveal the text character by character
        while (iterator < count)
        {
            iterator++;
            Debug.Log(iterator);
            _text.maxVisibleCharacters = iterator;

            // If any key is pressed, reveal all text immediately
            if (Input.anyKeyDown)
            {
                iterator = count; // Set iterator to the total count to finish the reveal
                _text.maxVisibleCharacters = count;
            }

            yield return new WaitForSeconds(1f / speed);
        }

        yield return new WaitForSecondsRealtime(0.2f);

        // Wait for a key press to proceed to the next step
        while (!Input.anyKeyDown)
        {
            yield return null; // Wait until a key is pressed
        }

        // Hide current text and show the next object
        next.SetActive(true);
        gameObject.SetActive(false);
    }
}