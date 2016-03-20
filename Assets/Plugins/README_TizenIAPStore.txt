Tizen Store interface for In-App Purchase(IAP) in Unity Application
--------------------------------------------------------------
Tizen.IAP is a C# based package, that would host classes to perform in-app purchase(IAP) operations using AppControl interface. 
This package supports 2 modes :
1. Normal Mode : Used for commercial purpose
2. Developer Mode : Used by developers to test and debug

Classes supported by Tizen.IAP Package are as below :
1. StoreProvider - This is the main class of the package that provides methods and associated enumerations for all IAP operations. Client should use this singleton class to access below methods :
   a. GetCountries - To access the list of countries, where different IAP servers are hosted. Available only in Developer Mode.
   b. GetAvailableItems - To access the available items in IAP Store. Available in Normal/Developer mode.
   c. GetPurchasedItems - To access the purchased items from IAP Store. Available in Normal/Developer mode.
   d. PurchaseItem - To purchase an item from IAP Store. Available in Normal/Developer mode.
   
2. Application classes - These classes are used by application for request and response.
   a. AppRequest - App sends this object specifying all the request parameters.
   b. AppResponse - App receives this object from StoreProvider as a response. Only the requested fields will be populated and rest will be set to NULL.
   
   
STEPS TO ADD AND USE Tizen.IAP.dll
----------------------------------
1. In Monodevelop editor, 
        Right click on References -> Edit References -> Click .NET Assembly tab
		Browse to Assets/TizenIAPStore folder
		Double click Tizen.IAP.dll
		Click OK. 
		Tizen.IAP.dll would be added in References now.
2. In source code, add the namespace Tizen.IAP and use the classes for further operations.
3. Sample code is as below :
	//Code starts here
	using Tizen.IAP;
	using System;
	namespace SampleAssembly
	{
		public class SampleClass
		{
			StoreProvider storeObj = StoreProvider.Instance;

			IAP_AppRequest appReq = new IAP_AppRequest ();
			//Populate app request object
			
			storeObj.PurchaseItem(appReq);
		}
	}
	//Code ends here
	
	
STEPS TO CALL A TIZEN STORE METHOD
----------------------------------
1. Create an AppRequest object based on the criteria(Ex : GetCountries,GetAvailableItems etc.,)
2. Define a callback handler of type "Tizen.IAP.RequestCallback" to access the AppResponse.
3. Using StoreProvider Singleton, access the required method and pass the ApPRequest object.
4. Eventually, the callback handler will be triggered with the response.

