#!/usr/bin/env bash

# Construct the path to the Info.plist file
appPath="${APPCENTER_SOURCE_DIRECTORY}/src/App.iOS"

# Retrieve the short version number originally set in the Info.plist file
shortVersion=$(plutil -extract CFBundleShortVersionString xml1 -o - ${appPath}/Info.plist | sed -n "s/.*<string>\(.*\)<\/string>.*/\1/p")

# Append the MS App Center build ID to the original version number
version="${shortVersion}.${APPCENTER_BUILD_ID}"

# Update the CFBundleVersion value to contain both the original short version value and the App Center build number
plutil -replace CFBundleVersion -string $version ${appPath}/Info.plist
