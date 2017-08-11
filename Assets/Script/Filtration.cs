using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Filtration : MonoBehaviour {

	List<float> dataGroup = new List<float>();

	float[] array = new float[]{0,0.1f,0.5f,1,0.2f,0.6f,0.3f,2,0.5f,0.1f,2.2f,0.3f,2.8f,0.1f,0.2f,0.8f,0.1f,2f,0.5f,0.1f,1,0.3f,0.5f,1f,0.8f,0.2f,0.4f,3,0.4f,0.5f,0.1f,1,0.5f,0.6f,0.3f,0.2f,0.1f,1,0.5f,0.4f,0.2f,0.3f,2.5f,0.1f,0.3f,0.9f,2f,0.1f,0.5f,0.6f,0.1f,0.8f,1f,0.5f,0.8f,0.4f,1f,0.3f,0.1f,0.1f,0.1f,0,0,0,0.1f,0.5f,0.3f,0.8f,1,0.5f};

//0,0.1f,0.5f,1,0.2f,0.6f,0.3f,2,0.5f,0.1f,2.2f,0.3f,2.8f,0.1f,0.2f,0.8f,0.1f,2f,0.5f,0.1f,1,0.3f,0.5f,1f,0.8f,0.2f,0.4f,3,0.4f,0.5f,0.1f,1,0.5f,0.6f,0.3f,0.2f,0.1f,1,0.5f,0.4f,0.2f,0.3f,2.5f,0.1f,0.3f,0.9f,2f,0.1f,0.5f,0.6f,0.1f,0.8f,1f,0.5f,0.8f,0.4f,1f,0.3f,0.1f,0.1f,0.1f,0,0,0,0.1f,0.5f,0.3f,0.8f,1,0.5f
	public AnimationCurve curve_Old;

	public AnimationCurve curve_New;

	public Text text_Old;
	public Text text_New;

	// Use this for initialization
	void Start () {

   /*		for (int i = 0; i < 40; i++) {

			float r = Random.Range (0.01f, 3);

			text_Old.text += r;
			text_Old.text += " ; ";

			curve_Old.AddKey (i,r);
			dataGroup.Add (r);
		}  */
		for (int i = 0; i < array.Length; i++) {

			text_Old.text += array[i];
			text_Old.text += " ; ";

			curve_Old.AddKey (i,array[i]);
			dataGroup.Add (array[i]);
		}

		Debug.Log ( "Ok" + dataGroup.Count);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Ergodic()
	{
		text_New.text = "";
		for (int i = 1; i < dataGroup.Count; i++) {

			FiltrationStar (i);
		}
		int index = 0;

		foreach (float nu in dataGroup) {
			
			text_New.text += nu;
			text_New.text += " ; ";

			curve_New.AddKey (index, nu);
			index++;
		}
	}

	float Separate = 0;

	void FiltrationStar(int currentIndex)
	{
		Separate = 0;

		Separate = dataGroup [currentIndex] / 3;

		if (dataGroup.Count < currentIndex + 3) {

			dataGroup [currentIndex] = Separate;

		} else {
			
			dataGroup [currentIndex] = Separate;
			dataGroup [currentIndex + 1] += Separate;
			dataGroup [currentIndex + 2] += Separate;
		}

	}
}
