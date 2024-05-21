using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class tries_counter : MonoBehaviour
{


    [SerializeField] private TMP_Text tries_text;
    private int current_tries = 0;




    // Start is called before the first frame update
    void Start()
    {
        current_tries = tries_state_saver.tries_counter;
        tries_text.text = $"Tries: {current_tries}";
    }


    public void updateTries()
    {
        current_tries++;
        tries_state_saver.tries_counter = current_tries;

        if (current_tries == 69)
        {
            tries_text.text = "hah nice";
        }
        else
        {
            tries_text.text = $"Tries: {current_tries}";
        }
    }

}
