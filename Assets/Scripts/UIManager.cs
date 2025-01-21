using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI collectText;
    public TextMeshProUGUI skullCounter;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleCollectText(bool show) {
        if (collectText != null) {
            collectText.enabled = show;
        }
    }
}
