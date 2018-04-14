public class Character {
    /* Class which holds information and structure for each character
       Primarily used in the character select screen
       
     */

    private string name;
    private string nationality;
    private string genre;

    public Character(string name, string nationality, string genre)
    {
        this.name = name;
        this.nationality = nationality;
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
        return "Name: " + name + ", Nationality: " + nationality + ", Genre: " + genre;

    }//end toString


}
