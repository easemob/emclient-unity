#!/bin/zsh

function delete_symlinks {
    local dir="$1"
    for item in "$dir"/*; do
        if [[ -L "$item" ]]; then
            rm "$item"
            echo "Deleted symlink: $item"
        elif [[ -d "$item" ]]; then
            delete_symlinks "$item"  # 递归进入子目录
        fi
    done
}

# 替换为您要删除软链接的目录路径
target_directory="$1"

delete_symlinks "$target_directory"
