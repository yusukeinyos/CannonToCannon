#! /bin/sh

echo "build iOS"

# 共通設定実行シェルのパス
COMMON_ENV_CONFIG_PATH="$(dirname "$0")/configure_common_build_env.sh"
if [[ ! -e "${COMMON_ENV_CONFIG_PATH}" ]] ; then
    echo "file could not be found: ${COMMON_ENV_CONFIG_PATH}"
    exit 1
fi

# Android, iOS共通で使う変数の設定
TARGET_PLATFORM=ios . "$COMMON_ENV_CONFIG_PATH"

# ビルド実行メソッドオプション
BUILD_METHOD_OPTION="-executeMethod BuildBatch.BuildiOS"

# ビルドオプション
UNITY_BATCH_BUILD_OPTIONS="$UNITY_BATCH_BUILD_COMMON_OPTIONS $BUILD_METHOD_OPTION"

# TeamID
export IOS_DEVELOPMENT_TEAM="4D5YMKTU24"

# ビルド実行
"$UNITY_BIN" $UNITY_BATCH_BUILD_OPTIONS

echo "Xcode project build is completed!"

# ====================================== Xcode アプリビルド ======================================
# Xcodeビルドで必要な値
export PROJECT_NAME=Unity-iPhone
export XCODE_WORKSPACE=${UNITY_PROJECT_PATH}/build
export XCODE_PROJECT=${XCODE_WORKSPACE}/${PROJECT_NAME}.xcodeproj

APP_NAME="arkWildcard" # CannonToCannon

# Xcodeプロジェクトのクリーン
xcodebuild clean -project ${XCODE_PROJECT}

# buildターゲットと設定をリストアップ
xcodebuild -project ${XCODE_PROJECT} -list

# Xcodeビルドarhiveの作成
xcodebuild \
    -project ${XCODE_PROJECT} \
    -scheme ${PROJECT_NAME} \
    -configuration Release \
    -archivePath ${XCODE_PROJECT}/${APP_NAME}.xcarchive \
    archive

# plist作成
EXPORT_OPTIONS_PLIST="ExportOptions.plist"
cat <<_EOF > ${EXPORT_OPTIONS_PLIST}
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
  <dict>
    <key>method</key>
    <string>development</string>
    <key>teamID</key>
    <string>${IOS_DEVELOPMENT_TEAM}</string>
    <key>compileBitcode</key>
    <false/>
    <key>provisioningProfiles</key>
    <dict>
      <key>jp.co.sumzap.ark.ep.arkWildcard</key>
      <string>arkWildcard.iOSAppDevelopment</string>
    </dict>
  </dict>
</plist>
_EOF

echo "## export options plist ##"
cat ${EXPORT_OPTIONS_PLIST}

# ipaファイル出力
xcodebuild \
    -exportArchive \
    -archivePath ${XCODE_PROJECT}/${APP_NAME}.xcarchive \
    -exportPath $HOME/Export \
    -exportOptionsPlist ${EXPORT_OPTIONS_PLIST}

exit 0
