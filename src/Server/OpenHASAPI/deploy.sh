#!/bin/sh

VERSION=`cat src/application/package.json | grep version | cut -d ':' -f 2 | cut -d '"' -f 2`

echo "Uploading deployment version $VERSION"

aws deploy push --application-name OpenHAS --s3-location s3://api.openhas.net/application/v$VERSION.zip --source ./
