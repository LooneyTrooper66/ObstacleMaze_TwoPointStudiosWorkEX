using UnityEngine;

public class MazeCellLogic : MonoBehaviour
{
    [SerializeField] GameObject frontWall;
    [SerializeField] GameObject backWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject unvisitedWall;

    public bool isVisited { get; private set; }


    public void Visit()
    {
        isVisited = true;
        unvisitedWall.SetActive(false);
    }

    public void ClearFrontWall()
    {
        frontWall.SetActive(false);
    }
    public void ClearBackWall()
    {
        backWall.SetActive(false);
    }
    public void ClearLeftWall()
    {
        leftWall.SetActive(false);
    }
    public void ClearRightWall()
    {
        rightWall.SetActive(false);
    }
}
