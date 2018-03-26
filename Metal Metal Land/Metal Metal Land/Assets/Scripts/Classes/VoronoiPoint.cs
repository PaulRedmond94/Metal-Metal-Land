using UnityEngine;
using System.Collections;

//class to hold voronoi point object
class VoronoiPoint
{
    private int x;
    private int y;
    private string cellType;

    public VoronoiPoint(int x, int y, string cellType)
    {
        this.x = x;
        this.y = y;
        this.cellType = cellType;


    }//end constructor

    //setters and getters
    public void setX(int x)
    {
        this.x = x;

    }//end set X

    public int getX()
    {
        return x;

    }//end getX

    public void setY(int y)
    {
        this.y = y;

    }//end setY

    public int getY()
    {
        return y;

    }//end getY

    public void setCellType(string cellType)
    {
        this.cellType = cellType;

    }//end set CellType

    public string getCellType()
    {
        return cellType;

    }//end getCellType

    public string toString()
    {
        return "X CoOrd: " + x + "  yCoOrd: " + y + "  Terrain: " + cellType;

    }


    public int getManhattanDistance(int xVal, int yVal)
    {
        int absXValue = System.Math.Abs(xVal - x);
        int absYValue = System.Math.Abs(yVal - y);

        return absXValue + absYValue;

    }//end getManhattanDistance

}//end voronoiPoint class