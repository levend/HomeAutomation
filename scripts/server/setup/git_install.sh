#!/bin/sh

BASE_FOLDER=/opt/ha

REPOSITORY_FOLDER=$BASE_FOLDER/repo/ha.git
CHECKOUT_FOLDER=$BASE_FOLDER/application

mkdir -p -v $REPOSITORY_FOLDER
mkdir -p -v $CHECKOUT_FOLDER

# make sure the system is up-to-date
apt-get update
apt-get upgrade -y

# install git
apt-get install git-core -y

# configure the $REPOSITORY_FOLDER folder
cd $REPOSITORY_FOLDER
git init --bare

# create pre-receive script
cat << EOF_PRIVATE >> hooks/pre-receive
#!/bin/sh

echo "Invoking pre-receive script."
$CHECKOUT_FOLDER/scripts/githooks/pre-receive.sh

EOF_PRIVATE

# create post-receive script
cat << EOF_PRIVATE2 >> hooks/post-receive
#!/bin/sh

echo "Checkout the received files into $CHECKOUT_FOLDER"
GIT_WORK_TREE=$CHECKOUT_FOLDER git checkout -f

echo "Invoking post-receive script."
$CHECKOUT_FOLDER/scripts/githooks/post-receive.sh

EOF_PRIVATE2

# adding executable flags to the hooks
chmod a+x hooks/pre-receive
chmod a+x hooks/post-receive