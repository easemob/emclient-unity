SDKVERISON=3_9_4

FILE=HyphenateChat.framework

cd ../HyphenateWrapper
if [ ! -d ${FILE} ]; then
  echo "$FILE not exist!! download it..."
  curl -o HyphenateChat.zip https://download-sdk.oss-cn-beijing.aliyuncs.com/downloads/HyphenateChat${SDKVERISON}.zip
  unzip HyphenateChat.zip
  rm -rf HyphenateChat.zip
fi

