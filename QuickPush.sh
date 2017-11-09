MESSAGE ="Quick update from script - "
DATE=`date '+%d-%m-%Y %H:%M:%S'`

git add .
git commit -m $MESSAGE$DATE
git push