#! /bin/sh

# コマンドのステータスが0でない場合は、即時終了する
set -e

# 実行コマンドを表示
set -x

# Projectルートパス
export PROJECT_ROOT="$(git rev-parse --show-toplevel)"
# ProjectVersionファイルパス
export PROJECT_VERSION_FILE="$(find "$PROJECT_ROOT" -type f -wholename '*/ProjectSettings/ProjectVersion.txt' -print0)"

export UNITY_VERSION="2018.1.0f2"

# Unity実行ファイルパス
export UNITY_BIN="/Applications/Unity/Unity.app/Contents/MacOS/Unity"

# TODO ProjectVersionファイルの情報を元にUnity Versionを指定できるように
if [[ ! -x "$UNITY_BIN" ]] ; then
    echo "Unity is not available: $UNITY_BIN" >&2
    exit 1
fi

# TARGET_PLATFORMのチェック
case "$TARGET_PLATFORM" in
    ios | android) ;;
    "") {
        echo "#! TARGET_PLATFORM is not specified." >&2
        exit 1
    } ;;
    * ) {
        echo "#! invalid TARGET_PLATFORM: '$TARGET_PLATFORM'" >&2
        exit 1
    }
esac

# Unityプロジェクトパス
export UNITY_PROJECT_PATH="${PROJECT_ROOT}" 

# Unityバッチモードビルドの共通オプション
export UNITY_BATCH_BUILD_COMMON_OPTIONS=" \
    -batchmode \
    -quit \
    -logFile build_"${TARGET_PLATFORM}".log \
    -projectPath ${UNITY_PROJECT_PATH}"