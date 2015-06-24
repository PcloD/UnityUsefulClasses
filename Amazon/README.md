## Amazon
Some Amazon AWS services wrappers

#### AnalyticsManager
AnalyticsManager is a singleton and must be added only once

##### How to use: 
1. Download [Amazon Unity SDK](https://s3.amazonaws.com/aws-unity-sdk/latest/aws-unity-sdk.zip)
2. Import **aws-sdk-unity-mobile-analytics-x.x.x.x.unitypackage**
3. Add **AWSPrefab** and **AnalyticsManager script** to your start scene
4. In **AnalyticsManager** set **IdentityPoolId**(created in Amazon Cognito Console) and **AppId**(created in Amazon Mobile Analyics Console)
5. Use manager in any scene of your game

Example usage:

```c#
class AnalyticsController
{
	public const string GAME_MODE_SELECTION_EVENT   = "PlayerGameMode";
	public const string GAME_MODE_ATTR_NAME         = "Mode";

	public static void playerGameModeEvent()
	{
	    CustomEvent customEvent = new CustomEvent(GAME_MODE_SELECTION_EVENT);
	    customEvent.AddAttribute(GAME_MODE_ATTR_NAME, "EndlessMode");
	    AnalyticsManager.Get.RecordEvent(customEvent);
	}
}
```

#### AmazonEventBuilder
Wrapped Amazon's custom event class.

Example usage:

```c#
AmazonEventBuilder eventBuilder = new AmazonEventBuilder("TestEvent");
eventBuilder.AddAttribute("FistName", "John").AddAttribute("LastName", "Smith").AddMetric("Age", 31);
AnalyticsManager.Get.RecordEvent(eventBuilder);
```

#### DynamoDbBase

Class provides parent class for classes that work with DynamoDB. 
Detailed description will be added soon.