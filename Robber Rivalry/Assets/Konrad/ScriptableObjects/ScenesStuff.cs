using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scene Management", menuName = "ScriptableObjects/SceneManager")]
public class ScenesStuff : ScriptableObject
{
    public int mainMenuIndex;
    public int smallMapIndex;
    public int originalMapIndex;
    public int mapsIndex;
    public int optionsIndex;
    public int characterSelectIndex;
    public int controlsScreenIndex;
    public int tutorialScreenIndex;
}
