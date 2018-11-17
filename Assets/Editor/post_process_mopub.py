import os
from sys import argv
from mod_pbxproj import XcodeProject

path = argv[1]

print('----------------------------------preparing to execute our magic scripts to tweak xcode project----------------------------------')
#path: /Users/khurram/Desktop/output
print('post_process.py xcode build path --> ' + path)
    
print('Step 1: start add libraries ')
project = XcodeProject.Load(path +'/Unity-iPhone.xcodeproj/project.pbxproj')

project.add_file('System/Library/Frameworks/CFNetwork.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/Security.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/AddressBook.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/AddressBookUI.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/Twitter.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/StoreKit.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/AdSupport.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/AudioToolbox.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/AVFoundation.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/CoreLocation.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/CoreTelephony.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/EventKit.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/EventKitUI.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/iAd.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/MediaPlayer.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/MediaToolbox.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/MessageUI.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/MobileCoreServices.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/PassKit.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/QuartzCore.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/Social.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/SystemConfiguration.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/UIKit.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/Foundation.framework', tree='SDKROOT', weak=True)
project.add_file('System/Library/Frameworks/CoreGraphics.framework', tree='SDKROOT', weak=True)

project.add_file('usr/lib/libsqlite3.dylib', tree='SDKROOT')
project.add_file('usr/lib/libz.dylib', tree='SDKROOT')

print('Step 2: change build setting')
project.add_other_buildsetting('GCC_ENABLE_OBJC_EXCEPTIONS', 'YES')
project.add_other_ldflags('-ObjC')
project.add_other_ldflags('-all_load')

if project.modified:
  project.backup()
  project.saveFormat3_2()

print('----------------------------------end for excuting our magic scripts to tweak our xcode ----------------------------------')
