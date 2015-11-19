#!/bin/sh

BASE_FOLDER=/opt/ha

# if the user set an input parameter for the script use it as a default folder
if [ -z "$1" ]
 then
    BASE_FOLDER=/opt/ha
 else
	BASE_FOLDER=$1
fi

REPOSITORY_FOLDER=$BASE_FOLDER/repo/ha.git
CHECKOUT_FOLDER=$BASE_FOLDER/application

PRE_RECEIVE_SCRIPT=$CHECKOUT_FOLDER/scripts/githooks/pre-receive.sh
POST_RECEIVE_SCRIPT=$CHECKOUT_FOLDER/scripts/githooks/post-receive.sh

mkdir -p -v $REPOSITORY_FOLDER
mkdir -p -v $CHECKOUT_FOLDER

# make sure the system is up-to-date
apt-get update
apt-get upgrade -y

# install git
apt-get install git-core -y

# configure the $REPOSITORY_FOLDER folder
cd $REPOSITORY_FOLDER
git init --bare --shared=group

# create pre-receive script
cat << EOF_PRIVATE >> hooks/pre-receive
#!/bin/sh

echo "Invoking pre-receive script."
if [ -f $PRE_RECEIVE_SCRIPT ]; then
	$PRE_RECEIVE_SCRIPT
fi

EOF_PRIVATE

# create post-receive script
cat << EOF_PRIVATE2 >> hooks/post-receive
#!/bin/sh

echo "Checkout the received files into $CHECKOUT_FOLDER"
GIT_WORK_TREE=$CHECKOUT_FOLDER git checkout -f

echo "Invoking post-receive script."
if [ -f $POST_RECEIVE_SCRIPT ]; then
	$POST_RECEIVE_SCRIPT
fi

EOF_PRIVATE2

# adding executable flags to the hooks
chmod a+x hooks/pre-receive
chmod a+x hooks/post-receive

# change persmissions to allow git push for others
chown -R root.users $REPOSITORY_FOLDER
chmod -R ug+w $REPOSITORY_FOLDER

# change permissions to allow non-root users change files
chown -R root.users $CHECKOUT_FOLDER
chmod -R ug+w $CHECKOUT_FOLDER
