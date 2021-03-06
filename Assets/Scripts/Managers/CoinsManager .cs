using UnityEngine;
using System.Collections;

public class CoinsManager {
		
	public const int DEFAULT_REDCOINS = 3;
	
	private  static int coins = 0;
	
	private static CoinsManager instance = new CoinsManager();

	private CoinsManager() {
		load ();
		save ();
	}
	
	public static CoinsManager getInstance(){
		return instance;
	}


	public void increaseCoins(){
		setCoins (coins + 1);
	}

	public void increaseCoins(int c){
		setCoins (coins + c);
	}

	private void setCoins(int c){
		coins = c;
		save ();
	}
	
	public int getCoins(){
		return coins;
	}

	public void spendCoins(int amount){
		load ();
		if (coins < amount) {
			Debug.LogError ("Not enough coins: you have " + this.getCoins () + " and you want spend " + amount);
			return;
		} else {
			coins -= amount;
			save ();
		}
	}

	public bool canSpendCoins(int amount){
		return coins >= amount;
	}
	

	public void save() {
		StorageManager.storeOnDisk (StorageManager.COINS, coins);
	}

	public void load() {
		setCoins	(StorageManager.loadIntFromDisk(StorageManager.COINS));
	}

}

