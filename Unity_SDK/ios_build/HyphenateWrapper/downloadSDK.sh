SDKVERISON=3_9_6_1
FILE=HyphenateChat.framework
LOCK=.emlock

if [ ! -f ${LOCK} ]; then
  touch ${LOCK}
fi

echo "Target: ${SDKVERISON}"

for LINE in `cat ${LOCK}`
do
  echo "Current: ${LINE}"
done

if [ ! "$LINE" = "$SDKVERISON" ]; then
  rm -rf ${FILE}
fi

if [ ! -d ${FILE} ]; then
  echo "$FILE not exist!! download it..."
  curl -o HyphenateChat.zip https://download-sdk.oss-cn-beijing.aliyuncs.com/downloads/HyphenateChat${SDKVERISON}.zip
  unzip HyphenateChat.zip
  rm -rf HyphenateChat.zip
fi

echo ${SDKVERISON} > ${LOCK}
