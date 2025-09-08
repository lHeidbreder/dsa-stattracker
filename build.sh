#!/bin/bash

PLATFORMS="linux-x64 win-x64"

for p in $PLATFORMS
do
    echo $p
    dotnet publish \
        -c Release \
        -r $p \
        --self-contained false \
        -p:IncludeNativeLibrariesForSelfExtract=true
        
    #TODO: package?
done
