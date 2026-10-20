# metadata.json

모드 폴더 루트의 `metadata.json` 파일은 모드가 **무엇인지**와 **어떻게 실행되는지**를 함께 담는 핵심 파일입니다.


## 전체 구조

```json
{
  "info": {
    "title": "새 아티팩트 팩",
    "description": "아티팩트 12종을 추가합니다.",
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
  "tags": ["Content"]
}
```

최상위에는 세 가지를 정의합니다.

| 키 | 역할 | 필수 |
|---|---|---|
| `info` | 창작마당과 게임 내 패널에 보이는 제목·설명·미리보기 | 필수 |
| `tags` | 창작마당 태그 | 선택 |
| `mod` | 모드의 실행 정보 | 필수 |

## info

### title

- 모드의 이름. 비어 있으면 `modId`를 이름으로 대신 사용합니다.
- 언어 맵을 통해 각 언어별 모드 이름을 정의할 수 있습니다. (본인 국가의 언어 외에도 '영어'를 추가하는 것을 권장합니다.)
- 지원 언어 코드는 다음과 같습니다: `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH`, `tr-TR`

```json
"info": {
  "title": { "en-US": "New Artifact Pack", "ko-KR": "새 아티팩트 팩" }
}
```

### description

- 모드의 설명.
- title과 동일하게 언어별 모드 설명을 정의할 수 있습니다.

```json
"info": {
  "description": { "en-US": "Adds 12 artifacts.", "ko-KR": "아티팩트 12종을 추가합니다." }
}
```

### preview

- 창작마당 업로드에는 미리보기 이미지가 필수입니다. (PNG 또는 JPG, 1MB 미만)
- 미리보기 이미지의 상대 경로를 기입하세요.
- 생략하면 루트 경로 내에서 `thumbnail.png`, `preview.png`, `thumbnail.jpg`, `preview.jpg` 파일을 순서대로 찾습니다.


## mod

### schemaVersion

- 현재 버전 1입니다.
- 향후 모드 구동과 관련하여 중대한 사항이 업데이트 되면 버전 숫자가 증가할 수 있음에 유의하세요.

### modId

- 모드를 식별하는 유일한 텍스트 입니다. 영문(소문자),숫자,점,하이픈,밑줄을 사용하세요. 최대 64자를 지원합니다.
- **고유한 이름을 지으세요.** 다른 모드와 `modId`가 중복된다면 정상적으로 실행되지 않습니다. 제작자 이름을 앞에 넣는 것도 중복 방지에 큰 도움이 될 것입니다. 
(예: `horay.artifact-pack`)
- **`modId`는 한 번 게시하면 바꾸지 마세요.** 다른 사람이 `dependencies` 기능을 통해 당신의 모드를 기반으로 한 하위 모드를 제작할 수 있습니다. 이 경우 `modId`를 통해 상위 모드를 식별합니다.


### version

- 여러분의 모드 버전을 정의할 수 있는 텍스트입니다.
- 변경해도 실제 게임에는 영향이 없습니다.
- 게임과 동일한 1.0.0 형태를 권장합니다.

### author

당신이 누군지 널리 알리세요!

### multiplayer / installation

멀티플레이 유저들이 이 모드를 공유해야하는지를 정의합니다.
기입할 수 있는 값은 다음과 같습니다.

| 값 | 내용 |
|---|---|
| `local_only` | 내 화면, UI에만 영향을 주는 모드 (번역 모드나 간단한 ui 모드가 여기에 해당) |
| `everyone` | 방 참가자 전원이 같은 모드를 켜야 함 (콘텐츠를 추가하거나 바꾸는 모드는 전부 여기에 해당) |
| `host_only` | 호스트만 켜면 되고 다른 참가자 쪽 변경 없이 동작 (멀티플레이어 모드지만 서버에만 영향이 가는 모드들이 여기에 해당) |

### modules

모듈을 통해 모드의 기능을 정의합니다. 즉 **가장 중요한 구문**입니다.
현재는 `localization, dll, costume, data` 모듈이 존재합니다. 
아래의 예시처럼 한 패키지에 여러 모듈을 섞어 넣을 수 있습니다.

```json
"modules": [
  { "type": "localization", "path": "Localization" },
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
  { "type": "costume", "path": "Costumes" },
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```
알 수 없는 `type`이 하나라도 있으면 패키지 전체가 "지원하지 않음"으로 분류되어 **다른 모듈도 실행되지 않습니다.**


#### type: localization 모듈

```json
"modules": [
  { "type": "localization", "path": "Localization" }
]
```
- `localization` 모듈은 언어파일이 포함된 모드입니다. 
- `path`에 언어 파일이 포함된 폴더를 기입합니다.
- `path` 폴더 안에 `ko-KR.json`, `en-US.json`처럼 **언어 코드 이름의 JSON 파일**을 둡니다. 게임이 쓰는 언어 코드와 파일 형식은 [번역 모드](localization-mods.md)를 확인하세요.



#### type: dll 모듈

```json
"modules": [
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
]
```
- `dll` 모듈을 통해 C# 코드를 작성하여 게임에 개입할 수 있습니다.
- `path`에 dll 파일의 경로를 기입합니다.
- `entryClass`는 dll파일의 네임스페이스를 포함한 진입점 클래스의 전체 이름을 기입하세요. 게임은 DLL 안을 뒤져서 진입점을 찾지 않으므로 **반드시 명시**해야 합니다. (중첩 클래스는 `Ns.Outer+Inner`)
- 같은 `entryClass`를 다른 두 모듈에 쓰지 마세요.
- Mono 빌드에서만 동작합니다.
- 자세한 내용은 [DLL 모드](dll-mods.md)를 확인하세요.


#### type: costume 모듈

```json
"modules": [
  { "type": "costume", "path": ? },
]
```
- C# 코드를 작성하지 않아도 자신만의 코스튬을 만들 수 있습니다.
- 멀티플레이 시 전원 설치가 필요합니다
- 자세한 내용은 [코스튬 모드](costume-mods.md)를 확인하세요.


#### type: data 모듈

```json
"modules": [
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```
- 모드에서 사용할 수 있는 범용 데이터 모듈입니다. 일반적인 경우에서는 사용되지 않고 주로 모드 끼리의 의존성을 위해 사용됩니다.
- `consumer`에 `modId`를 기입하여 어떤 모드에서 사용될 데이터인지 정의할 수 있습니다.

### libraries

```json
"libraries": [
  "bin/Helper.dll",
  { "path": "bin/Newtonsoft.Json.dll" }
]
```
- `dll` 모듈이 참조하는 보조 어셈블리 목록입니다.
- 게임은 **여기 선언된 파일만** 참조 해석에 이용합니다. 폴더에 DLL을 넣어 두기만 하면 찾지 못합니다.
- 문자열 또는 `{ "path": "..." }` 객체 모두 가능합니다.
- 각 항목은 `.dll`로 끝나야 하고, `modules`의 DLL과 겹치면 정상적으로 실행이 되지 않습니다.
- 라이브러리는 진입점으로 취급되지 않습니다. `WorkshopModBase` 클래스가 들어 있어도 호출되지 않습니다.

### dependencies

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
]
```
- 예를 들어 상위 모드가 '콘텐츠 제작기' 모드라면, 하위 모드가 그 모드를 기반으로 한 새로운 콘텐츠를 추가할 수도 있을 것입니다.
- 이 경우, `dependencies` 기능을 이용하여 다른 모드와의 의존성을 선언할 수 있습니다. 
- 의존성에는 다음 세가지 요소가 기입되어야 합니다.

| 필드 | 필수 | 규칙 |
|---|---|---|
| `modId` | 예 | 기반이 되는 모드의 `modId` |
| `version` | 아니오 | **정확히 일치하는 버전 문자열이 필요한 경우에만 사용.** 버전이 다르면 로드를 생략하므로 일반적으로는 기입하지 않는 편이 안전합니다. |
| `workshopId` | 아니오 | 사용자가 의존 모드를 찾아 구독할 수 있도록 적어 두는 용도입니다. |


## tags

창작마당에 업로드 될 때 쓰이는 카테고리 입니다.
자유롭게 지정할 수 있지만, 일반적으로 다음과 같은 태그가 있습니다. 

| 카테고리 | 콘텐츠 |
|---|---|
| `Item` | 아이템(아티팩트, 석판, 포션 등), 콤보 |
| `Costume` | 코스튬 |
| `Weapon` | 무기 |
| `Tool` | 보조 도구 |
| `Gameplay` | 게임 플레이 변경 |
| `Localization` | 번역 |
