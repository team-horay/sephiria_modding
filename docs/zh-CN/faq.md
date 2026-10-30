# 常见问题


## 如何查看日志？

游戏日志写入下面的文件。每次启动游戏时都会重写，上一次运行的日志保留在 `Player-prev.log` 中。

```
%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log
```

与模组相关的行以方括号中的标签开头，搜索该标签即可。

| 标签 | 内容 |
|---|---|
| `[WorkshopModManager]` | 订阅列表查询、下载、本地文件夹扫描、加载计划摘要 |
| `[WorkshopModRuntime]` | DLL 激活和本地化合并结果 |
| `[WorkshopUploader]` | 创意工坊上传和审核提交 |
| `[ModGuardClient]` | 与审核服务器的通信 |
| `[WorkshopLobbyMods]` | 多人游戏房间中模组集合的比较 |
| `모드 내 언어 파일 발견` | 从每个本地化文件读取的键数量 |


## Steam 创意工坊面板中的“本地模组（AddOns文件夹）”是什么？

你可能会在面板中看到“本地模组（AddOns文件夹）”这一项。这是旧版的模组功能，新模组请不要使用它。
旧版模组无法与其他玩家同步，建议移除。


## 我想把 WorkshopLocal 文件夹放到 Sephiria 安装文件夹以外的地方。

如果想把模组放在其他驱动器或工作文件夹中，可以通过启动参数传入路径。在 Steam 库中依次打开 Sephiria → 属性 → 通用 → 启动选项，然后输入：

```
-workshop_local=D:\mods\my-first-mod
```

多个路径用分号分隔；如果路径包含空格，请用引号把整体括起来。

```
"-workshop_local=D:\My Mods\first;D:\My Mods\second"
```

通过参数传入的文件夹会被当作 `WorkshopLocal/` 的子文件夹来处理。


## 我丢失了 uploadticket.json。

从 1.1.1 起不再使用 `mod-guard-manifest.json` 和 `mod-guard.sig.json`，丢失了也没关系。唯一需要的文件是 `uploadticket.json`。

如果丢失了 `uploadticket.json`（例如重装系统之后），游戏会把该文件夹视为新模组，直接上传会创建一个新的创意工坊物品，而不是更新原有的物品。若要继续更新原有物品，请按以下顺序恢复：

1. 在创意工坊订阅你自己的模组。
2. 把 Steam 下载的整个文件夹（`<Steam 库>/steamapps/workshop/content/2436940/<创意工坊物品 ID>/`）复制到 `WorkshopLocal/` 中。文件夹名随意。
3. 在复制的文件夹中创建 `uploadticket.json`，内容如下。数字请使用创意工坊页面 URL 中 `id=` 之后的数字。
```json
{
  "modId": "yourname.first-mod",
  "publishedFileId": 123456789
}
```
4. 启动游戏，在面板中选择该模组并点击**上传到创意工坊**。**原有物品就会被更新**，如果是代码模组，还会以同一物品 ID 重新提交审核。（注意：如果你是复制了现有模组的文件夹来制作另一个不同的模组，**必须删除** `uploadticket.json`。否则新模组会覆盖原模组的创意工坊物品。）
