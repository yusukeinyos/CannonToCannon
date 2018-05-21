#! /bin/sh

set -e

# TODO gitから取得
# Projectルートパス
export PROJECT_ROOT="$(git rev-parse --show-toplevel)"
# ProjectVersionファイルパス
PROJECT_VERSION_FILE="$(find "$PROJECT_ROOT" -type f -wholename '*/ProjectSettings/ProjectVersion.txt' -print0)"

UNITY_VERSION="2018.1.0f2"

# Unity実行ファイルパス
export UNITY_BIN="/Applications/Unity/Unity.app/Contents/MacOS/Unity"

# TODO ProjectVersionファイルの情報を元にUnity Versionを指定できるように
if [[ ! -x "$UNITY_BIN" ]] ; then
    echo "Unity is not available: $UNITY_BIN" >&2
    exit 1
fi

# Unityプロジェクトパス
export UNITY_PROJECT_PATH="${PROJECT_ROOT}"

# ビルド実行メソッド
BUILD_METHOD="BuildBatch.BuildAndroid"

# Unityバッチモードビルドのオプション
export UNITY_BATCH_BUILD_OPTIONS=" \
    -batchmode \
    -quit \
    -logFile build.log \
    -projectPath ${UNITY_PROJECT_PATH} \
    -executeMethod ${BUILD_METHOD}" 

# Android development kit path
ANDROID_SDK_PATH="/Users/a14753/Library/Android/sdk"
JDK_PATH="/Library/Java/JavaVirtualMachines/jdk1.8.0_101.jdk/Contents/Home/"
ANDROID_NDK_PATH="/Applications/android-ndk-r13b"

if [[ ! -d ${ANDROID_SDK_PATH} ]] ; then
    echo "Invalid Andoid SDK directory path: ${ANDROID_SDK_PATH}" >&2
fi
if [[ ! -d ${JDK_PATH} ]] ; then
    echo "Invalid JDK directory path: ${JDK_PATH}" >&2
fi
if [[ ! -d ${ANDROID_NDK_PATH} ]] ; then
    echo "Invalid Andoid NDK directory path: ${ANDROID_NDK_PATH}" >&2
fi

# ビルド実行
"$UNITY_BIN" $UNITY_BATCH_BUILD_OPTIONS


exit 0