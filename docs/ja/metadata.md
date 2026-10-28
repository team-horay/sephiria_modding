# metadata.json

MODフォルダのルートにある `metadata.json` は、MODが**何であるか**と**どのように動作するか**の両方を記述する中心となるファイルです。


## 全体の構造

```json
{
  "info": {
    "title": "New Artifact Pack",
    "description": "Adds 12 artifacts.",
    "preview": "thumbnail.png"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.artifact-pack",
    "version": "0.9.1",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone" },
    "modules": [
      { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
      { "type": "localization", "path": "Localization" }
    ],
    "libraries": [
      "bin/Helper.dll"
    ],
    "dependencies": [
      { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
    ]
  },
  "tags": ["Item"]
}
```

最上位では3つの項目を定義します。

| キー | 役割 | 必須 |
|---|---|---|
| `info` | Steamワークショップとゲーム内パネルに表示されるタイトル、説明、プレビュー | 必須 |
| `tags` | ワークショップのタグ | 任意 |
| `mod` | MODの動作方法 | 必須 |

## info

### title

- MODの名前です。空の場合は、代わりに `modId` が名前として使われます。
- 言語マップを使って、言語ごとにMODの名前を定義できます。
	- 自分の言語に加えて、英語も追加することをおすすめします。
- 対応している言語コード: `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH`, `tr-TR`

```json
"info": {
  "title": { "en-US": "New Artifact Pack", "ko-KR": "새 아티팩트 팩" }
}
```

### description

- MODの説明です。
- `title` と同様に、言語ごとに説明を定義できます。

```json
"info": {
  "description": { "en-US": "Adds 12 artifacts.", "ko-KR": "아티팩트 12종을 추가합니다." }
}
```

### preview

- ワークショップにアップロードするにはプレビュー画像が必須です。(PNGまたはJPG、1MB未満)
- プレビュー画像への相対パスを入力します。
- 省略した場合、ゲームはルートフォルダ内の `thumbnail.png`、`preview.png`、`thumbnail.jpg`、`preview.jpg` をこの順に探します。


## mod

### schemaVersion

- 現在のバージョンは1です。
- 今後、MODの動作方法に大きな変更があった場合、この数字が増える可能性があります。

### modId

- MODを識別する一意の文字列です。小文字の英字、数字、ピリオド、ハイフン、アンダースコアが使えます。最大64文字です。
- **他と重複しない名前を付けてください。** `modId` が他のMODと衝突すると、正しく動作しません。先頭に制作者名を付けておくと、衝突をかなり避けられます。
(例: `horay.artifact-pack`)
- **一度公開した `modId` は変更しないでください。** 他の人が `dependencies` を使ってあなたのMODの上に子MODを作ることがあり、その際に親MODは `modId` で識別されます。


### version

- MODのバージョンを定義する文字列です。
- 他のMODが `dependencies.version` で正確なバージョンを要求する際に比較されます。
- **MODを更新するたびに、必ずバージョンを上げてください。** マルチプレイのロビーに参加するとき、ゲームはホストと参加者のMODバージョンが同じかを確認します。内容を変えたのにバージョンが同じままだと、異なる内容のMODを持つプレイヤーが同じロビーに入ってしまい、コンテンツの同期に失敗したりゲームの進行が止まったりすることがあります。
- ゲームと同じ 1.0.0 形式をおすすめします。

### author

あなたが誰なのか、みんなに知らせましょう!

### multiplayer / installation

マルチプレイのプレイヤーがこのMODを共有する必要があるかを定義します。指定できる値は次のとおりです。

| 値 | 説明 |
|---|---|
| `local_only` | 自分の画面やUIにだけ影響する(ローカライズMODや簡単なUI MODがここに該当します) |
| `everyone` | ルームの全員が同じMODを有効にしている必要がある(コンテンツを追加・変更するMODはすべてここに該当します) |
| `host_only` | ホストだけが有効にしていればよく、他の参加者側は変更なしで動作する(サーバー側の計算にだけ影響するマルチプレイMODがここに該当します) |

`installation` を `local_only` や `host_only` と宣言しているのに、MODが参加者側にも必要なコンテンツ(新しいアイテムやユニットの登録、ネットワークオブジェクト、参加者のクライアントでも実行されるコード)に触れている場合、同じロビーのプレイヤーが別々のゲームをプレイする状態になったり、コンテンツの同期に失敗して正常なゲーム進行が止まったりすることがあります。MODが実際に何をしているか、必ず確認してください。


### modules

- モジュールはMODが何をするかを定義するため、ここが**最も重要なセクション**です。
- 現在のモジュールは `localization, dll, costume, data` です。
- 以下の例のように、1つのパッケージに複数のモジュールを混在させることができます。
- 不明なモジュールが1つでもあると、パッケージ全体が「未対応」とマークされ、ロード時にそのMODはスキップされます。

```json
"modules": [
  { "type": "localization", "path": "Localization" },
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
  { "type": "costume", "path": "Costumes" },
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

#### type: localization モジュール

```json
"modules": [
  { "type": "localization", "path": "Localization" }
]
```

- `localization` モジュールは、言語ファイルを含むMOD用です。
- `path` には言語ファイルが入っているフォルダを指定します。
- `path` フォルダの中に、`ko-KR.json` や `en-US.json` のような**言語コードを名前にしたJSONファイル**を置きます。ゲームが使う言語コードとファイル形式は [ローカライズMOD](localization-mods.md) を参照してください。

#### type: dll モジュール

```json
"modules": [
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" }
]
```

- `dll` モジュールを使うと、C#コードでゲームにフックできます。
- `path` にはDLLファイルのパスを指定します。
- `entryClass` には、DLL内のエントリーポイントクラスの名前空間を含む完全名を指定します。ゲームはDLL内のエントリーポイントを探さないため、**必ず指定してください**。(ネストしたクラスの場合は `Ns.Outer+Inner` の形式を使います。)
- 2つの異なるモジュールで同じ `entryClass` を使わないでください。
- Monoビルドでのみ動作します。
- 詳細は [DLL MOD](dll-mods.md) を参照してください。

#### type: costume モジュール

```json
"modules": [
  { "type": "costume", "path": "Costumes" }
]
```

- C#コードを一切書かずに、自分のコスチュームを作成できます。
- `path` にはコスチュームファイルが入っているフォルダを指定します。
- マルチプレイでは全員がインストールしている必要があります。
- 詳細は [コスチュームMOD](costume-mods.md) を参照してください。


#### type: data モジュール

```json
"modules": [
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

- MOD用の汎用データモジュールです。通常のケースでは使いません。主にMOD間の依存関係で使います。
- `consumer` には、このデータモジュールを使うMODを指定しますが、通常は省略します。省略した場合、`dependencies` に挙げたすべてのMODがこのデータを読み取れます。
- 親MODが複数あり、データをそのうちの1つだけに渡したい場合は、そのMODの `modId` を `consumer` に入れます。その値は `dependencies` にも含まれている必要があります。

### libraries

```json
"libraries": [
  "bin/Helper.dll",
  { "path": "bin/Newtonsoft.Json.dll" }
]
```

- `dll` モジュールが参照する補助アセンブリのリストです。
- ゲームは**ここで宣言されたファイルだけ**を使って参照を解決します。DLLをフォルダに置くだけでは見つけてもらえません。
- 各エントリは文字列、または `{ "path": "..." }` オブジェクトのどちらかで記述します。
- 各エントリは `.dll` で終わる必要があり、`modules` のDLLと重複すると正しく動作しません。
- ライブラリはエントリーポイントとして扱われません。`WorkshopModBase` クラスが含まれていても呼び出されません。

### dependencies

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
]
```

- 親MODが「コンテンツ制作」用のMODである場合、子MODがその上に新しいコンテンツを追加することがあります。
- その場合、`dependencies` を使って他のMODへの依存関係を宣言できます。
- 親MODを指定すると、あなたのMODはその親MODより先にロードされなくなります。
- 親MODが存在しない、無効になっている、またはロードに失敗した場合、その子MODすべてのロードが取り消されます。
- 依存関係には次の3つのフィールドを指定できます。

| フィールド | 必須 | ルール |
|---|---|---|
| `modId` | はい | 土台にするMODの `modId` |
| `version` | いいえ | **バージョン文字列の完全一致が必要な場合にのみ使います。** バージョンが異なるとロードがスキップされるため、通常は省略するほうが安全です。 |
| `workshopId` | いいえ | ユーザーが必要なMODを見つけて購読できるようにします。 |


## tags

- ワークショップにアップロードする際に使うカテゴリです。
- 1つのMODに複数のタグを付けられます。(アイテムとコスチュームを追加するオールインワンMODなら `"tags": ["Item", "Costume"]` とします。)

| カテゴリ | 内容 |
|---|---|
| `Item` | アイテム(アーティファクト、石版、ポーションなど)、コンボ |
| `Costume` | コスチューム |
| `Weapon` | 武器 |
| `NPC` | 旅人、商人など |
| `Enemy` | 新しい敵、ボスなど |
| `Gameplay` | ゲームプレイの変更 |
| `QoL` | UI、利便性、アクセシビリティの改善 |
| `Library` | 他のMODが依存するランタイム、フレームワーク、データパックなど |
| `Localization` | 翻訳 |

## パスのルール
 
- `modules[].path`、`libraries`、`info.preview` のパスはすべて同じルールに従います。
- MODフォルダからの相対パスのみ使用できます。(絶対パス、ドライブレター、`..` は拒否されます。)
- 区切り文字には `/` を使います。
- シンボリックリンクやジャンクションを経由するパスは拒否されます。
- **大文字・小文字は実際の名前と完全に一致している必要があります。** Windowsでは `localization` と書いてもフォルダは開けますが、ゲームは `Localization` と異なるものとして拒否します。(このルールにより、Steam Deck(Linux)でも同じ動作が保証されます。)
