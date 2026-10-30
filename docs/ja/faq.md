# FAQ


## ログはどこで見られますか?

ゲームのログは以下のファイルに書き込まれます。ゲームを起動するたびに書き直され、前回の分は `Player-prev.log` に残ります。

```
%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log
```

MOD関連の行は角かっこで囲まれたタグで始まるので、そのタグで検索してください。

| タグ | 内容 |
|---|---|
| `[WorkshopModManager]` | 購読リストの照会、ダウンロード、ローカルフォルダのスキャン、ロード計画の要約 |
| `[WorkshopModRuntime]` | DLLの有効化とローカライズのマージ結果 |
| `[WorkshopUploader]` | ワークショップへのアップロードと審査の申請 |
| `[ModGuardClient]` | 審査サーバーとの通信 |
| `[WorkshopLobbyMods]` | マルチプレイのルームにおけるMOD構成の比較 |
| `모드 내 언어 파일 발견`(MOD内の言語ファイルを発見) | 各ローカライズファイルから読み込まれたキーの数 |


## Steamワークショップパネルの「ローカルMod(AddOnsフォルダ)」とは何ですか?

パネルに「ローカルMod(AddOnsフォルダ)」という項目が表示されることがあります。これは旧式のMOD機能なので、新しいMODには使わないでください。
旧式のMODは他のプレイヤーと同期できないため、削除することをおすすめします。


## WorkshopLocal フォルダをSephiriaのインストールフォルダ以外の場所に置きたい

MODを別のドライブや作業用フォルダに置きたい場合は、起動引数でパスを渡せます。Steamライブラリで `Sephiria → プロパティ → 一般 → 起動オプション` を開き、次のように入力します。

```
-workshop_local=D:\mods\my-first-mod
```

複数のパスはセミコロンで区切り、パスにスペースが含まれる場合は全体を引用符で囲みます。

```
"-workshop_local=D:\My Mods\first;D:\My Mods\second"
```

引数で渡したフォルダは、`WorkshopLocal/` のサブフォルダとまったく同じように扱われます。


## uploadticket.json をなくしました

1.1.1以降、`mod-guard-manifest.json` と `mod-guard.sig.json` は使われなくなったため、なくしても問題ありません。必要なファイルは `uploadticket.json` だけです。

`uploadticket.json` を失った場合(たとえばフォーマット後など)、ゲームはそのフォルダを新しいMODとして扱うため、そのままアップロードすると既存のアイテムを更新するのではなく、新しいワークショップアイテムが作成されてしまいます。既存のアイテムを引き続き更新するには、次の順序で復旧してください。

1. ワークショップで自分のMODを購読します。
2. Steamがダウンロードしたフォルダ全体(`<Steamライブラリ>/steamapps/workshop/content/2436940/<ワークショップアイテムID>/`)を `WorkshopLocal/` にコピーします。フォルダ名は何でも構いません。
3. コピーしたフォルダの中に、以下の内容で `uploadticket.json` を作成します。数字には、ワークショップページのURLの `id=` に続く数字を使います。
```json
{
  "modId": "yourname.first-mod",
  "publishedFileId": 123456789
}
```
4. ゲームを起動し、パネルでそのMODを選択して **ワークショップにアップロード** を押します。**既存のアイテムが更新され**、コードを含むMODの場合は同じアイテムIDで再度審査に申請されます。(注意: 既存のMODフォルダを複製して別のMODを作った場合は、`uploadticket.json` を**必ず削除してください**。残しておくと、新しいMODが元のMODのワークショップアイテムを上書きしてしまいます。)
