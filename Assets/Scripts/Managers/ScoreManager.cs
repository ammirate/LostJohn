using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
		
	public const int DEFAULT_MULTIPLIER = 1;

	private static int multiplier = 1;

	private static int score = 0;

	private static int coins = 0;

	private static int bestScore = 0;

	public static bool isLastScoreBest = false;
	
	private static ScoreManager instance = null; 

	private ScoreManager(){
	}

	public static ScoreManager getInstance(){
		if(instance == null){
			instance = new ScoreManager();
			instance.load ();
			//instance.save ();
		}
		return instance;
	}
	

	public void resetScore() {
		score = 0;
		coins = 0;
	}
	
	public void increaseScore(){
		setScore (score + (1 * multiplier));
	}

	public void increaseActualCoin() {
		coins++;
	}

	public void increaseScore(int s){
		Debug.Log("Increasing of " + s + " * " + multiplier); 
		setScore (score + (s * multiplier));
	}

	private void setScore(int s){
		score = s;
	}

	public int getScore(){
		return score;
	}

	public int getActualCoins() {
		return coins;
	}

	public void setBestScore(int bs){
		if (bs > bestScore) {
			bestScore = bs;
			isLastScoreBest = true;
		} else {
			isLastScoreBest = false;
		}
	}

	public bool checkBestScore() {
		if (score > bestScore) {
			setBestScore (score);
			isLastScoreBest = true;
		} else {
			isLastScoreBest = false;
		}
		return isLastScoreBest;
	}

	public int getBestScore(){
		return bestScore;
	}


	public void increaseMultiplier(){
		multiplier++;
	}

	public void setMultiplier(int m){
		multiplier = m;
	}

	public void restoreMultiplier(){
		multiplier = DEFAULT_MULTIPLIER;
	}

	public void save() {
		StorageManager.storeOnDisk (StorageManager.SCORE, score);
		StorageManager.storeOnDisk (StorageManager.BEST_SCORE, bestScore);
	}
	
	public void load() {
		setScore	(StorageManager.loadIntFromDisk (StorageManager.SCORE));
		setBestScore(StorageManager.loadIntFromDisk (StorageManager.BEST_SCORE));
	}
}

