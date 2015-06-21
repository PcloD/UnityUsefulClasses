####IOS Build
This section explains how to create IOS build from unity.

##Exporting from unity
1. Go to Build Settings -> Player Settings -> Other Settings
2. In ** Configuration ** section 
	1. Choose *Scripting Backend* to ** IL2CPP **
	2. Choose *Architecture* to **Universal**
3. Press Build


##XCode build
1. Open exported unity project
2. Open **General** section
	1. In *Linked frameworks and Libraries* check if following frameworks available
		*CoreTelephony
		*iAd
		*EventKit
		*EventKitui
		*Adsupport
		*AVFoundaion
		*AudioToolbox
		*CoreGraphics
		*CoreMedia
		*MessageUi
		*StoreKit
		*SystemConfiguration

		*If you use ```GoogleMobileAds```* add  [GoogleMobileAds.framework](https://developers.google.com/mobile-ads-sdk/download#downloadios) 
	
3. Open ** Build Settings **
	1. *Linking*
		* * Other Linker Flags * set ** -Objc **
	2. *Apple LLVM 6.1 - Language - Modules* set ** Enable Modules(C and Objective C) **
		* *If you use ```GoogleMobileAds```* or got *error ** Module GoogleMobileAds *not found* ** then *Allow non-modular Includes in Framework Modules* to **YES**
	3. *Architectures*
		* *Build Active Architecture Only* - **NO**