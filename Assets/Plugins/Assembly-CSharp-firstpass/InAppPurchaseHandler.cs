using System.Runtime.InteropServices;
using UnityEngine;

public class InAppPurchaseHandler
{
	private static bool initializedForPurchase;

	private static bool initializedForProductRequest;

	private static string editorPurchaseGameObjectName;

	private static string editorProductRequestGameObjectName;

	private static string editorOnPurchaseSuccessMethodName;

	private static string editorOnProductRequestSuccessMethodName;

	public static bool isInitializedForPurchase()
	{
		return initializedForPurchase;
	}

	public static bool isInitializedForProductRequest()
	{
		return initializedForProductRequest;
	}

	public static string parseProductIdFromCallbackString(string transactionAndProductId)
	{
		return transactionAndProductId.Split(',')[1];
	}

	private static bool purchaseHandlerCanMakePayments() { return default(bool); }

	public static bool canMakePayments()
	{
		return true;
	}

	private static void purchaseHandlerInitPurchase(string gameobjectName, string onSuccessMethodName, string onFailureMethodName) { }

	public static void initPurchase(string gameobjectName, string onSuccessMethodName, string onFailureMethodName)
	{
		if (initializedForPurchase)
		{
			Debug.LogError("PurchaseHandler already initialized for purchase");
			return;
		}
		editorPurchaseGameObjectName = gameobjectName;
		editorOnPurchaseSuccessMethodName = onSuccessMethodName;
		initializedForPurchase = true;
	}

	private static void purchaseHandlerResetForPurchase() { }

	public static void resetForPurchase()
	{
		editorPurchaseGameObjectName = null;
		editorOnPurchaseSuccessMethodName = null;
		initializedForPurchase = false;
	}

	private static void purchaseHandlerInitProductRequest(string gameobjectName, string onSuccessMethodName, string onFailureMethodName) { }

	public static void initProductRequest(string gameobjectName, string onSuccessMethodName, string onFailureMethodName)
	{
		if (initializedForProductRequest)
		{
			Debug.LogError("PurchaseHandler already initialized for purchase");
			return;
		}
		editorProductRequestGameObjectName = gameobjectName;
		editorOnProductRequestSuccessMethodName = onSuccessMethodName;
		initializedForProductRequest = true;
	}

	private static void purchaseHandlerResetForProductRequest() { }

	public static void resetForProductRequest()
	{
		editorProductRequestGameObjectName = null;
		editorOnProductRequestSuccessMethodName = null;
		initializedForProductRequest = false;
	}

	private static void purchaseHandlerStartPurchase(string productIdentifier) { }

	public static void startPurchase(string productIdentifier)
	{
		Debug.Log("PurchaseHandler.startPurchase(" + productIdentifier + ")");
		if (!initializedForPurchase)
		{
			Debug.LogError("PurchaseHandler not initialized for purchase");
		}
		else
		{
			GameObject.Find(editorPurchaseGameObjectName).SendMessage(editorOnPurchaseSuccessMethodName, "," + productIdentifier);
		}
	}

	private static void purchaseHandlerCallbackHasBeenHandled(string transactionAndProductId) { }

	public static void callbackHasBeenHandled(string transactionAndProductIdentifier)
	{
		if (!initializedForPurchase)
		{
			Debug.LogError("PurchaseHandler not initialized for purchase");
		}
	}

	private static void purchaseHandlerQueryProducts(string productIds) { }

	public static void queryProducts(string productIds)
	{
		if (!initializedForProductRequest)
		{
			Debug.LogError("PurchaseHandler not initialized for product request");
			return;
		}
		string[] array = productIds.Split(',');
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			if (i > 0)
			{
				text += ";";
			}
			text = text + array[i] + ";0,99GBP";
		}
		GameObject.Find(editorProductRequestGameObjectName).SendMessage(editorOnProductRequestSuccessMethodName, text);
	}
}
