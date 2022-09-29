# https://downloadsdk.easemob.com/downloads/easemob-sdk-3.9.4.zip

FILENAME=easemob-sdk-3.9.7
SDK_NAME=hyphenatechat_3.9.7

BUILDGRADLE="build.gradle"
SDK_PERFIX="hyphenatechat_"

FILE=libs/${SDK_NAME}.jar

if [ ! -f ${FILE} ]; then
  echo "$FILE not exist!! download it..."
  curl -o ${SDK_NAME}.zip https://downloadsdk.easemob.com/downloads/${FILENAME}.zip
  mv libs/classes.jar classes.jar
  rm -rf libs
  unzip -d ${SDK_NAME} ${SDK_NAME}.zip
  mv ${SDK_NAME}/libs libs
  mv classes.jar libs/classes.jar
  rm -rf ${SDK_NAME}.zip
  rm -rf ${SDK_NAME}
fi

line=$(cat ${BUILDGRADLE} | grep -n $SDK_PERFIX | awk -F ":" '{print $1}')

if [ $line ] ; then
    sed -i "" "$line d" ${BUILDGRADLE}
    sed -i "" "$line i\\"$'\n'"    implementation files('libs/$SDK_NAME.jar')
    " ${BUILDGRADLE}
fi
