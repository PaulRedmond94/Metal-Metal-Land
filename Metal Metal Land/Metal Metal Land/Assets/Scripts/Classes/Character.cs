public class Character {

    private string name;
    private string nationality;
    private string bio;
    private string age;

    public Character(string name, string nationality, string bio, string age)
    {
        this.name = name;
        this.nationality = nationality;
        this.bio = bio;
        this.age = age;

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

    public void setBio(string bio)
    {
        this.bio = bio;

    }//end setBio

    public string getBio()
    {
        return bio;

    }//end getBio

    public void setAge(string age)
    {
        this.age = age;

    }//end setAge

    public string getAge()
    {
        return age;

    }//end getAge

    public string toString()
    {
        return "Name: " + name + ", Bio: " + bio + ", Nationality: " + nationality + ", Age: " + age;

    }//end toString


}
