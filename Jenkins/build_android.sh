#! /bin/sh

echo "build Android"

# 共通設定実行シェルのパス
COMMON_ENV_CONFIG_PATH="$(dirname "$0")/configure_common_build_env.sh"
if [[ ! -e "${COMMON_ENV_CONFIG_PATH}" ]] ; then
    echo "file could not be found: ${COMMON_ENV_CONFIG_PATH}"
    exit 1
fi

# Android, iOS共通で使う変数の設定
. "$COMMON_ENV_CONFIG_PATH"

# ビルド実行メソッドオプション
BUILD_METHOD_OPTION="-executeMethod BuildBatch.BuildAndroid" 

# ビルドオプション
UNITY_BATCH_BUILD_OPTIONS="$UNITY_BATCH_BUILD_COMMON_OPTIONS $BUILD_METHOD_OPTION"

# Android development kitのパス
# BuildBatch.cs側で指定するための設定
export ANDROID_SDK_ROOT="${HOME}/Library/Android/sdk"
export JDK_PATH="/Library/Java/JavaVirtualMachines/jdk1.8.0_101.jdk/Contents/Home/"
export ANDROID_NDK_ROOT="/Applications/android-ndk-r13b"

if [[ ! -d ${ANDROID_SDK_ROOT} ]] ; then
    echo "Invalid Andoid SDK directory path: ${ANDROID_SDK_ROOT}" >&2
fi
if [[ ! -d ${JDK_PATH} ]] ; then
    echo "Invalid JDK directory path: ${JDK_PATH}" >&2
fi
if [[ ! -d ${ANDROID_NDK_ROOT} ]] ; then
    echo "Invalid Andoid NDK directory path: ${ANDROID_NDK_ROOT}" >&2
fi

# ビルド実行
"$UNITY_BIN" $UNITY_BATCH_BUILD_OPTIONS


exit 0