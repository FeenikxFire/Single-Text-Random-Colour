using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleTextRandomColour : MonoBehaviour
{
    #region UI Text Components
    [Header("UI Text Components")]
    [Tooltip("Manually Filled - UI Text Elements that will random colour per letter.")]
    public Text uiText;             //Manually Filled.
    private string textToString;    //Text that changes.
    private string originalWord;    //Stored original word.
    private string[] characters;    //Stored original char
    #endregion

    #region Text Colours and Components
    [Header("Colours")]
    [Tooltip("Manually Set - Amount and Colours the text letters will randomize between.")]
    public Color[] textColours;     //Colours the text letters will randomize between.
    private string[] colours;       //Array of strings that converts the colours in textColours[] to #HexCodes http://htmlcolorcodes.com/.
    private int randTextColour;     //Number which the random colour from the textColours[] get stored.
    #endregion

    #region Hex codes set with HTML Strings
    //Unity Inspector (Rich) Text takes the string format of:
    //      <color=#HEXCODE> Text you want coloured </color>
    private string startHTMLcolour = ("<color=#");  //Stored string for start Hex code formatting. Requires ">" to finish HTML Start.
    private string endHTMLcolour = ("</color>");    //Stored string for end Hex code formatting.
    #endregion

    #region Failsafe
    //Check so awake and start don't conflict in exection order.
    private bool started = false;
    #endregion

    // Use this for initialization
    void Start()
    {
        //Store original word based of UI text.
        originalWord = uiText.text.ToString();

        //Set the size of the colours[] string to be the same size as the textColours[].
        colours = new string[textColours.Length];

        #region Store colours
        //For every Colour in textColours[], convert the #hex colour code into string form and store it.
        for (int j = 0; j < textColours.Length; j++)
        {                                                                                           //This was missing from startHMLColour, because it needed to be inserted after the #Hex colour.
            colours[j] = (startHTMLcolour + ColorUtility.ToHtmlStringRGBA(textColours[j]).ToString() + ">"); //Added > on the end for Unity UI "HTML" to do the conversion in Rich Text.
        }
        #endregion

        //Set new colours for each word.
        InitializeRandomizeTextColour();

        started = true; //Failsafe.
    }

    private void OnEnable()
    {
        if (started)
        {
            //When the object is enabled - Choose new colours.
            RerollRandomTextColour();
        }
    }

    #region Initializing Randomize Text Colour
    private void InitializeRandomizeTextColour()
    {
        textToString = originalWord;    //Current string text is the original word.
        string[] characters = new string[textToString.Length];

        uiText.text = "";

        for (int i = 0; i < textToString.Length; i++)   //While index is less than the word length - Write.
        {
            characters[i] = System.Convert.ToString(textToString[i]);
            //Set the current letter to have a random colour from available colours in textColours[].
            RollRandomTextColour();
            characters[i] = (colours[randTextColour] + characters[i] + endHTMLcolour);

            //Set the uiText to be what it currently is + the new letter. (It's been cleared already).
            uiText.text = uiText.text + characters[i];
        }
    }
    #endregion

    #region ReRoll Randomized Text Colour
    public void RerollRandomTextColour()
    {
        textToString = originalWord;
        string[] characters = new string[textToString.Length];
        uiText.text = "";

        for (int i = 0; i < textToString.Length; i++)   //While index is less than the word length - Write.
        {
            characters[i] = System.Convert.ToString(textToString[i]);

            //Set the current letter to have a random colour from available colours in textColours[].
            RollRandomTextColour();
            characters[i] = (colours[randTextColour] + characters[i] + endHTMLcolour);

            //Set the uiText to be what it currently is + the new letter. (It's been cleared already).
            uiText.text = uiText.text + characters[i];
        }
    }
    #endregion

    private void RollRandomTextColour()
    {
        randTextColour = Random.Range(0, colours.Length);
    }
}
