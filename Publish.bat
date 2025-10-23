#!/bin/bash

# Dòng này thông báo cho biết script bắt đầu chạy
echo "Bat dau Publish du an (Single File, Windows x64)..."

# Lệnh publish
dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true

# Dòng này thông báo khi chạy xong
echo "Da Publish xong!"

pause