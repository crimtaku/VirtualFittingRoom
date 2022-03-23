using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choosecloth : MonoBehaviour
{
    List<string> paidat = new List<string>() { "Remove Shirt", "huppari", "clothshirt", "mekko"};
    List<string> housut = new List<string>() { "Remove Pants", "housut"};

    public string chosenShirt;
    public string chosenPants;
    public Dropdown shirtdropdown;
    public Dropdown pantsdropdown;

    // Start is called before the first frame update
    void Start()
    {
        //list?container = containernamehere
        
        //täytetään listat
        shirtdropdown.AddOptions(paidat);
        pantsdropdown.AddOptions(housut);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Changeshirt(int val)
    {
        chosenShirt = paidat[val];
    }
    public void Changepants(int val)
    {
        chosenPants= housut[val];
    }
}
