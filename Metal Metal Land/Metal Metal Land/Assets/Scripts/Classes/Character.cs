public class Character {
    /* Class which holds information and structure for each character
       Primarily used in the character select screen
       
     */

    private string name;
    private string nationality;
    //private string bio;
    private string genre;

    public Character(string name, string nationality, /*string bio,*/ string genre)
    {
        this.name = name;
        this.nationality = nationality;
        //this.bio = bio;
        this.genre = genre;

    }//end constructor

    //setters & getters
    public void setName(string name)
    {
        this.name = name;

    }//end setName

    public string getName()
    {
        return this.name;

    }//end getName

    public void setNationality(string nationality)
    {
        this.nationality = nationality;

    }//end setNationality

    public string getNationality()
    {
        return nationality;

    }//end string
    /*
    public void setBio(string bio)
    {
        this.bio = bio;

    }//end setBio

    public string getBio()
    {
        return bio;

    }//end getBio
    */
    public void setgenre(string genre)
    {
        this.genre = genre;

    }//end setgenre

    public string getGenre()
    {
        return genre;

    }//end getgenre

    public string toString()
    {
        return "Name: " + name + /*", Bio: " + bio +*/ ", Nationality: " + nationality + ", Genre: " + genre;

    }//end toString


}
