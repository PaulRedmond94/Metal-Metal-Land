public static class StaticScript{

    //set of static variables to hold game details and game options for consistent values easily accessible in between scenes

    //variable which holds the name of the next scene to load, used in the loading manager script
    public static string nextSceneToLoad = "";

    //holds a reference of what characters player 1 and 2 are playing
    public static string player1Character = "";
    public static string player2Character = "";

    //holds a reference of what player 1 and player 2s scores are
    public static int player1Score = 0;
    public static int player2Score = 0;

    //holds a reference of what type of terrain to generate
    public static string terrainGenType;

    //holds reference to how many rounds are to be played up to
    public static int roundCount = 10;

    //may be redundant, come back to this later
    public static bool roundInProngress = false;

    //values for sound options
    public static int musicLevel = 100;
    public static int sfxLevel = 100;

    public static bool suddenDeathEnabled = true;
    public static bool itemsEnabled = true;

}//end static script
