﻿	using UnityEngine;
	using UnityEngine.UI;
	using System.Collections;

	public class ClickToBuy : MonoBehaviour, Observer {

		private string BUY_OK = "Your gadget has " + '\n' + "been bought! :D";
		private string BUY_ERROR = "Sorry, can't buy " + '\n' + " the gadget :(";
		private string ALREADY_BOUGHT = "You already have" + '\n' + " this gadget!";

		private float scaleFactor = 1.1f;
		private string itemName;
		private string dialogText;
		private bool clicked = false;

		public GameObject dialogBox;
		public GameObject messageBox;

		public Image buttonBackground;

		private float dialogX = -0.48f;
		private float dialogY = -15f;

		private bool messageShowed = false;
		private static GameObject refereceToDialogBox;

		void Start () {

			Transform[] tList = GetComponentsInChildren<Transform> ();
			foreach (Transform t in tList) {
				if(t.name == "name") {
					itemName = t.gameObject.GetComponent<Text>().text;
				}
			}
		}
		
		void Update () {
			// when click is finished
			if (Input.GetMouseButtonUp(0)) {
				if(Utility.checkInput(gameObject)) {
					showDialogBox();
				}
			}
		}


		private IEnumerator click() {
			PlayClickSound.play();
			transform.localScale /= scaleFactor;
			yield return new WaitForSeconds (0.1f);
			transform.localScale *= scaleFactor;
		}
		
		
		
		public void notify (bool state) {
			if (state) {// YES has been clicked

				string itemName = transform.name;
				ShopItem item = ShopManager.getInstance().getItem(itemName);

				bool canBuy = ShopManager.getInstance().canBuyItem(itemName);
			
				if (canBuy) {
					// if buy action has success then activate the gadget
					if (ShopManager.getInstance ().buyItem (itemName)) {
						GadgetActivator.getInstance().activate (itemName);
					}
				}
				GameObject.Destroy(GameObject.FindGameObjectWithTag(GameTags.dialogBoxOk));

				string msgState = canBuy ? BUY_OK : BUY_ERROR;
				StartCoroutine(showMessageBox(msgState));

				GetComponent<GraphicButtonManager>().update(item);
			}
		}



		private void showDialogBox() {
			// this branch is active when user click on an item after
			// he already has clicked an item. Then two dialog boxes can appear.
			// This statement allow to cancel the first click.

			GameObject dialogOk = GameObject.FindGameObjectWithTag (GameTags.dialogBoxOk);
			GameObject dialogYesNo = GameObject.FindGameObjectWithTag (GameTags.dialogBoxYesNo);

			if (dialogOk != null || dialogYesNo != null) {
				GameObject.Destroy(dialogOk);
				GameObject.Destroy(dialogYesNo);
				return;
			}

			messageShowed = false;
			string itemName = transform.name;
			ShopItem item = ShopManager.getInstance ().getItem (itemName);

			int lvlToUnlock = item.lvlToUnlock;
			int playerLevel = LevelManager.getInstance ().getLevel ();
			//if the item has already been bought you can not buy it
			bool alreadyBought = item.activatable;

			if (alreadyBought) {
				StartCoroutine(showMessageBox (ALREADY_BOUGHT));
			} 

			//if cannot buy because of the low level
			else if (lvlToUnlock > playerLevel) {
			StartCoroutine(showMessageBox (BUY_ERROR));
		
			} else {

				string dialogtext = "Do you want to buy" + '\n' + itemName + "?";

				GameObject refereceToDialogBox = (GameObject)Instantiate (dialogBox, new Vector3 (dialogX, dialogY, -5), Quaternion.identity);
				refereceToDialogBox.GetComponent<DialogBoxYesNo> ().setDialogText (dialogtext);
				refereceToDialogBox.GetComponent<DialogBoxYesNo> ().register (this);
			}

		}


		private IEnumerator showMessageBox(string msg) {
		yield return new WaitForSeconds (0.2f);
			GameObject refereceToMsgBox = (GameObject) Instantiate (messageBox, new Vector3(dialogX, dialogY, -5), Quaternion.identity);
			refereceToMsgBox.GetComponent<DialogBoxOk> ().setDialogText (msg);
		}

	}
