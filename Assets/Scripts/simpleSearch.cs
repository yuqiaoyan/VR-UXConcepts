using System;
using UnityEngine;
using System.Collections;

public class simpleSearch : MonoBehaviour {

    // Use this for initialization
    //public GameObject arcLayout;
    bool isGo = true;
    public float spacingDistance = .35f;
    public float resultX = -1f, resultY = .4f;
    public float startZ;
    public Transform searchResultsObj;
    private Transform[] searchResultsArray;
    private int resultsNum;
    public TextMesh debugT;
    public GameObject catalog;
    public GameObject currMenuObj;
    public TextMesh searchText;

    public arcLayout currMenu;

    public Transform[] Find(string query)
    {
        int resultsIDX = 0;

        //clean query
        query = query.Replace("\t", String.Empty);
        query = query.Replace("\n", String.Empty);
        query = query.Replace(".", String.Empty);

        for (int i = 0;i < transform.childCount; i++)
        {
            Transform item = transform.GetChild(i);
            

            String itemNameString = item.name.ToUpper();
            
            bool isMatch = itemNameString.Contains(query.ToUpper());

            //Debug.Log("name is " + itemNameString);
            //Debug.Log("query is " + query);
            //Debug.Log("isMatch returned " + isMatch.ToString());

            if (isMatch)
            {
                Debug.Log("Found " + item.name);
                searchResultsArray[resultsIDX] = item;
                resultsIDX += 1;
                
            }
        }

        if(resultsIDX == 0)
        {
            Debug.Log("results array " + searchResultsArray.ToString());
            Debug.Log("No matches");
            searchText.text = "No matches found for " + query;

        }
        else
        {
            resultsNum = resultsIDX;
            showResults(searchResultsArray);
            return searchResultsArray;
        }

        
        
        return null;
    
    }

    void setResult(Transform aResult, float resultZ)
    {
        aResult.localPosition = new Vector3(resultX, resultY, resultZ);
        aResult.SetParent(searchResultsObj);

    }

    public void showResults(Transform[] results)
    {    
        //simple left aligned search layout
        
        for (int i = 0; i < resultsNum; i++)
        {
            setResult(results[i], startZ + (spacingDistance * i));
            //setResult(results[resultIDX], spacingDistance * i *-1);
            //resultIDX +=1;

        }

        currMenu.hideMenu();

    }

    public void clearResults()
    {
        if (currMenuObj.transform.childCount == 7) //assumes our catalog only shows 5 items + 1 for arcHelper and title
        {
            //do nothing
        }
        else
        {
            int i = 0;
            while (i < searchResultsObj.childCount)
            {
                searchResultsObj.GetChild(i).SetParent(catalog.transform);
            }


            //assume 0th index is arcHelper
            int j = 2;
            while (currMenuObj.transform.childCount > 2)
            {
                Debug.Log("child name is " + currMenuObj.transform.GetChild(j).name);
                if (currMenuObj.transform.GetChild(j).tag != "menuDeco")
                {

                    currMenuObj.transform.GetChild(j).SetParent(catalog.transform);
                }
            }

            searchText.text = "Write Search";
            currMenu.createArcMenu();
        }
    }

    void Start () {
        searchResultsArray = new Transform[20];
        startZ = -0.5f;


    }
	
	// Update is called once per frame
	void Update () {
        //if (transform.childCount > 4 && isGo)
        //{
        //    showResults();
        //    isGo = false;
        //    currMenu.hideMenu();
            
            
        //}

    }
}
