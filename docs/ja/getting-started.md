# はじめてのMOD

このガイドでは、Sephiria MODの基本構造を説明し、最初のMODを作ってゲーム内で確認するまでの手順を順に追っていきます。

## 必要なもの

- 高機能なテキストエディター([Visual Studio Code](https://code.visualstudio.com/) など)

## ゲームのインストールフォルダを開く

Sephiriaのインストールフォルダを開きます。

- Steamライブラリから
  - `Steamライブラリ → Sephiriaを右クリック → 管理 → ローカルファイルを閲覧`
- Windows(既定のインストールパス)
  - `C:\Program Files (x86)\Steam\steamapps\common\Sephiria`
- macOS(既定のインストールパス)
  - `~/Library/Application Support/Steam/steamapps/common/Sephiria`(`Sephiria.app` が入っているフォルダ)

## WorkshopLocal フォルダと自分のMODフォルダを作る

1. ゲームのインストールフォルダに `WorkshopLocal/` フォルダを作成します。
2. その中に、自分のMOD用のフォルダを作成します。
   - Sephiriaは、フォルダ1つを1つのMODとして扱います。
   - ここで作成したMODはゲーム内でテストでき、Steamワークショップにアップロードすることもできます。

このガイドでは、タイトル画面の「ゲームスタート」ボタンの文字を「Hello World!」に変えるMOD `my-first-mod` を作ります。完成形の構造は次のとおりです。

```
<ゲームのインストールフォルダ>/
├── Sephiria.exe
└── WorkshopLocal/
    ├── my-first-mod/          ← MOD 1つ
    │   ├── metadata.json      ← MODを定義する中心となるファイル
    │   ├── thumbnail.png      ← MODのプレビューサムネイル(ワークショップへのアップロードに必須)
    │   └── Localization/      ← ローカライズモジュールが読み込むフォルダ
    │       └── en-US.json     ← 置き換えるテキスト
    └── another-mod/           ← 別のMOD
        └── ...
```

## metadata.json ファイル

- MODフォルダに `metadata.json` を作成し、以下の内容を貼り付けます。
- `metadata.json`: MODを定義する最も重要なファイルです。
- `yourname`: 自分のニックネームに置き換えてください。
- ファイルは **UTF-8** で保存してください。メモ帳の場合は「名前を付けて保存」のエンコードでUTF-8を選びます。
- 各フィールドの意味は [metadata.json リファレンス](metadata.md) を参照してください。

```json
{
  "info": {
    "title": "My First Mod",
    "description": "Hello World!"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.my-first-mod",
    "version": "0.0.1",
    "author": "Your Name",
    "multiplayer": { "installation": "local_only" },
    "modules": [
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```

## 言語ファイルを作る

- MODフォルダの中に `Localization/` フォルダを作成します。
- `Localization/` の中に、以下の内容で `en-US.json` ファイルを作成します。
- テスト用に、ファイル名を自分の言語コードに変えても構いません。

```json
{
  "Title_GameStart": "Hello World!"
}
```

## 動作確認

ここまでで、MODが動作するか確認できます。

1. Sephiriaを起動します。
2. ゲームの言語を英語にした状態でタイトル画面を見ると、「Start Game」ボタンが「Hello World!」になっています。`WorkshopLocal/` のMODは初期状態で有効なので、別途オンにする必要はありません。
3. タイトル画面の **Steamワークショップ** ボタンを押してワークショップパネルを開きます。「自分のMOD」セクションに「My First Mod」が表示されているはずです。

### MODが反映されない!

- JSONの文法が正しいか確認してください。
- `metadata.json` の `path` は、実際のフォルダ名と大文字・小文字まで完全に一致している必要があります。
- `metadata.json` がUTF-8で保存されているか確認してください。
- ゲームの言語が、ファイル名の言語と一致しているか確認してください。

## 次のステップ

1. [metadata.json リファレンス](metadata.md) で、MODがどのように定義されるかを学びましょう。
2. [ローカライズMOD](localization-mods.md) と [コスチュームMOD](costume-mods.md) は比較的簡単に作れるので、ここから始めるのがおすすめです。
3. [DLL MOD](dll-mods.md) は、コーディングの基礎知識がある方におすすめです。
4. [ワークショップへの公開](publishing.md) を参考に、MODをワークショップで公開しましょう。
