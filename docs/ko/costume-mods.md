# 코스튬 모드

코드 없이 PNG와 JSON만으로 코스튬 추가해 봅시다. 이 모드로 추가할 수 있는 건 다음 두 경우입니다.

- **새 코스튬**: 새로운 코스튬을 생성합니다. 외형, 스탯, 시작 아이템 등을 정할 수 있습니다.
- **기존 코스튬의 외형**: 분홍 토끼 같은 기존 코스튬에 외형을 추가합니다.

이 문서는 [`examples/costume-sample`](../../examples/costume-sample) 예제를 기준으로 설명합니다. 예제를 내려받아 모드 구조를 파악하세요.

## 모드 구조

```
costume-sample/
├── metadata.json
├── thumbnail.png
├── Costumes/                  ← 코스튬 폴더
│   └── WhiteCat/              ← 폴더 하나 = 코스튬 하나
│       ├── costume.json
│       ├── icon.png
│       ├── idle.png
│       ├── move.png
│       ├── idle_back.png
│       ├── move_back.png
│       ├── attack.png
│       └── ...                ← 그 밖의 모션 PNG
└── Localization/              ← 코스튬 이름, 설명 텍스트
    ├── ko-KR.json
    ├── en-US.json
    └── ...
```


## metadata.json

예제를 바탕으로 자기 모드를 만들 때에는 `metadata.json` 파일의 `modId`와 `author`를 수정해야 합니다. (자세한 내용은 [`metadata.json`](metadata.md) 참고)

```json
{
  "info": {
    "title": { "ko-KR": "하얀 고양이 코스튬", "en-US": "White Cat Costume" },
    "description": { "ko-KR": "하얀 고양이 코스튬을 추가합니다.", "en-US": "Adds the White Cat costume." }
  },
  "tags": ["Costume"],
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.costume-whitecat",
    "version": "1.0.0",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone" },
    "modules": [
      { "type": "costume", "path": "Costumes" },
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```


## 스프라이트 애니메이션

- 코스튬에 필요한 모든 애니메이션을 나열하면 준비해야 할 종류가 많습니다.
- 처음 만들어 보는 분들은 `idle, move, airborne, fall`만으로 간단한 코스튬 제작을 시도해 보세요.

| 모션 | 내용 | 필수 여부 |
|---|---|---|
| `idle` | 대기 | 필수 |
| `move` | 이동 | 필수 |
| `airborne` | 공중에 뜸 | 선택 |
| `fall` | 쓰러짐 | 선택 |
| `attack` | 공격 | 선택 |
| `greatsword_lower` | 회전하며 휘두르기 | 선택 |
| `greatsword_sweep` | 빙글 돌기 | 선택 |
| `hitfeedback` | 피격 | 선택 |

### 뒷모습 애니메이션

- 더욱 자연스러운 코스튬을 위해 뒷모습을 그릴 수 있습니다. 이는 선택 사항입니다.
- 뒷모습을 추가하기로 결정했다면, `idle_back, move_back` 모션이 필수로 포함되어야 합니다.
- 뒷모습 애니메이션은 다음과 같이 정의됩니다.

| 모션 | 내용 |
|---|---|
| `idle_back` | 대기 (뒷모습) |
| `move_back` | 이동 (뒷모습) |
| `attack_back` | 공격 (뒷모습) |
| `greatsword_upper` | 회전하며 휘두르기 (뒷모습) |

## 그림 준비하기

- 한 동작의 프레임을 가로로 이어 붙인 스프라이트 PNG 시트를 제작하여 각 모션별로 준비하세요.
- 그림 높이는 프레임 높이와 같고, 폭은 프레임 폭 × 프레임 수입니다. 프레임 사이에 여백을 두지 마세요.
- 모든 개별 이미지는 동일한 사이즈를 기반으로 작업하는 것이 권장됩니다.
- 개별 스프라이트 사이즈가 20×16인 하얀 고양이 예제의 이미지 크기는 다음과 같습니다.

| 파일 | 크기 | 프레임 수 |
|---|---|---|
| `idle.png` | 120×16 | 6 |
| `move.png` | 160×16 | 8 |
| `attack.png` | 60×16 | 3 |
| `fall.png` | 20×16 | 1 |

*이미지 예시*


### 새 코스튬 정보 작성하기

`costume.json` 파일을 작성하여 자신만의 코스튬을 정의합니다.

예제 `Costumes/WhiteCat/costume.json`:

```json
{
  "schemaVersion": 1,
  "id": "whitecat",
  "name": "Costume_WhiteCat_Name",
  "flavorText": "Costume_WhiteCat_FlavorText",
  "icon": "icon.png",

  "frameSize": [20, 16],
  "pivot": [10, 2],
  "pixelsPerUnit": 16,

  "animations": {
    "idle":             { "file": "idle.png", "fps": 12 },
    "idle_back":        { "file": "idle_back.png", "fps": 12 },
    "move":             { "file": "move.png", "fps": 4, "footstepFrames": [3, 7] },
    "move_back":        { "file": "move_back.png", "fps": 4, "footstepFrames": [3, 7] },
    "attack":           { "file": "attack.png", "fps": 20, "loop": false },
    "attack_back":      { "file": "attack_back.png", "fps": 20, "loop": false },
    "greatsword_lower": { "file": "greatsword_lower.png", "fps": 20, "loop": false },
    "greatsword_upper": { "file": "greatsword_upper.png", "fps": 20, "loop": false },
    "greatsword_sweep": { "file": "greatsword_sweep.png", "fps": 30, "loop": false },
    "airborne":         { "file": "airborne.png", "fps": 12 },
    "fall":             { "file": "fall.png", "fps": 1 },
    "hitfeedback":      { "file": "hitfeedback.png", "fps": 20, "loop": false }
  },

  "stats": ["DEFENSE/-10", "EVASION/1200"],
  "startingItems": [],
  "onlyDefaultWeapon": false
}
```

| 필드 | 필수 | 내용 |
|---|---|---|
| `schemaVersion` | 예 | 구분자. 정수 `1`을 입력하세요. |
| `id` | 예 | 소문자 영문, 숫자, 밑줄, 하이픈, 1~31자를 사용하세요. 충돌 방지를 위해 게임 안에서는 `<modId>:<id>`로 등록됩니다. |
| `name` | 예 | 코스튬 이름의 **번역 키**입니다. 아래 [번역 파일 준비](#번역-파일-준비) 참고. |
| `flavorText` | 아니오 | 코스튬 설명의 **번역 키**입니다. 아래 [번역 파일 준비](#번역-파일-준비) 참고. |
| `icon` | 예 | 코스튬 미리보기 아이콘 PNG 경로입니다. |
| `face` | 아니오 | 멀티플레이 HUD UI 얼굴 PNG 경로입니다. |
| `pixelsPerUnit` | 아니오 | 더 세밀한 그림을 추가하려면 이 값을 수정하세요. 값이 커질수록 세밀한 그림으로 표현됩니다. 기본 ppu는 16입니다. |
| `frameSize` | 아니오 | 각 스프라이트의 `[가로, 세로]` 크기입니다. 기본값은 `[16, 16]`입니다. |
| `pivot` | 아니오 | 각 스프라이트의 중심점. 왼쪽 아래가 `[0, 0]` 입니다. 기본값은 `[가로/2, 0]`입니다. |
| `animations` | 예 | 동작별 그림 정보입니다. [animations](#animations) 참고. |
| `stats` | 아니오 | 지정할 스탯 목록입니다. [능력치 목록](stats.md) 참고. |
| `startingItems` | 아니오 | 시작 아이템 번호 배열입니다. [아이템 목록](item-table.md) 참고. |
| `body` | 아니오 | 피격 판정 범위를 정합니다. [body](#body) 참고. |
| `skins` | 아니오 | 같은 코스튬의 추가 외형입니다. [skins](#skins) 참고. |

### animations

```json
  "animations": {
    "idle":      { "file": "idle.png", "fps": 12 },
    "move":      { "file": "move.png", "fps": 4, "footstepFrames": [3, 7] },
    "attack":    { "file": "attack.png", "fps": 20, "loop": false },
    "fall":      { "file": "fall.png", "fps": 1 },
    "hitfeedback": { "file": "hitfeedback.png", "fps": 20, "loop": false, "frameSize": [24, 16], "pivot": [12, 2] }
  }
```

- 위에서 준비한 애니메이션을 `{ "file", "fps", "loop", "footstepFrames" }` 네 가지 속성과 함께 정의할 수 있습니다.
- 동작마다 `frameSize`와 `pivot`을 따로 설정할 수도 있습니다.
- 마지막 줄처럼 한 동작만 다른 크기로 그렸다면 그 동작에 `frameSize`와 `pivot`을 따로 적습니다.

#### 구성 요소

| 필드 | 필수 | 내용 |
|---|---|---|
| `file` | 예 | 재생할 파일 이름을 입력합니다. |
| `fps` | 아니오 | 애니메이션이 재생될 속도입니다. (기본값: 12) |
| `loop` | 아니오 | 애니메이션의 반복 여부를 정합니다. (기본값: true, 공격 애니메이션의 경우 false) |
| `footstepFrames` | 아니오 | 0부터 시작되는 프레임 번호를 입력합니다. 해당 프레임이 실행될 때 발소리를 재생합니다. |


### body

```json
"body": { "hitboxRadius": 4, "hitboxOffset": [0, 4], "movementRadius": 0.25, "bodyScale": 1 }
```

- 캐릭터의 이동과 피격 충돌 범위를 정의합니다.
- 내장 코스튬은 이 수치가 기본값으로 설정되어 있습니다.
- 특별한 의도가 있는 것이 아니라면 생략하는 것을 권장합니다.

#### 구성 요소

| 필드 | 필수 | 내용 |
|---|---|---|
| `hitboxRadius` | 아니오 | 피격 판정 원의 반지름. 픽셀 단위, 0.5~64. 기본 `4`. |
| `hitboxOffset` | 아니오 | 피격 판정 원의 중심 위치 `[x, y]`. 픽셀 단위, pivot 기준으로 x는 오른쪽·y는 위가 양수, ±64. 기본 `[0, 4]`. |
| `movementRadius` | 아니오 | 벽·장애물과 부딪히는 이동 충돌 원의 반지름. 유닛 단위(16픽셀 = 1), 0.05~2. 기본 `0.25`. |
| `bodyScale` | 아니오 | 무기를 쥐는 어깨 위치의 배율. 0.1~4. 기본 `1`. 몸집이 큰 코스튬에서 무기가 몸에 파묻힐 때 올립니다. |



### skins

```json
"skins": [
  { "id": "winter", "name": "Skin_WhiteTiger_Winter_Name", "folder": "skins/Winter", "icon": "icon.png",
    "animations": { "idle": "idle.png", "move": "move.png" } }
]
```

- 같은 코스튬에 추가 외형을 지정할 수 있습니다.
- 애니메이션은 위의 형식을 그대로 따릅니다.
- `frameSize`, `pivot`을 생략하면 코스튬 본체의 값을 따릅니다.




## 번역 파일 준비

`Localization/ko-KR.json`:

```json
{
  "Costume_WhiteCat_Name": "하얀 고양이",
  "Costume_WhiteCat_FlavorText": "줄무늬가 인상적입니다."
}
```

`Localization/en-US.json`:

```json
{
  "Costume_WhiteCat_Name": "White Cat",
  "Costume_WhiteCat_FlavorText": "The stripes are rather impressive."
}
```

- `name`과 `flavorText`에는 글 대신 **번역 키**를 적고, 글은 같은 모드의 `Localization/` 폴더에 언어별로 둡니다.
- 키는 공백 없이 영문, 숫자, 밑줄, 하이픈을 사용하세요. 다른 모드와 겹치지 않게 모드 이름을 섞어 짓는 것이 좋습니다.
- 키가 **어느 언어 파일에도 없으면** 그 코스튬은 로드되지 않습니다. 최소 한 언어는 입력해야 합니다.
- 보다 많은 사람들이 즐길 수 있도록 영어(`en-US`)를 함께 넣는 것을 권장합니다.
- `metadata.json`의 `modules`에 `localization` 모듈을 함께 선언하는 것을 잊지 마세요.



## 확인하기

1. 모드 폴더를 `WorkshopLocal/`에 두고 게임을 실행합니다.
2. 게임을 시작해 마을의 코스튬 변경 창을 열면 새 코스튬이 추가되었는지 확인하세요.
3. 코스튬 내용에 변경이 있다면 게임을 재시작해야 합니다.



## 추가: 기존 코스튬에 스킨만 추가하는 방법

`costume.json` 대신 `skin.json`을 만들어 기존 코스튬의 외형을 만들 수도 있습니다.

```
Costumes/
└── PinkRabbit_Pajama/
    ├── skin.json
    ├── icon.png
    ├── idle.png
    └── move.png
```

```json
{
  "schemaVersion": 1,
  "costume": "PinkRabbit",
  "id": "pajama",
  "name": "Skin_PinkRabbit_Pajama_Name",
  "icon": "icon.png",
  "animations": { "idle": "idle.png", "move": "move.png" }
}
```

- `costume`은 대상 코스튬의 id입니다. 내장 코스튬 id(`PinkRabbit` 등) 또는 다른 모드 코스튬의 모드명이 포함된 런타임 id를 기재해야 합니다. (예제의 하얀 고양이라면 `yourname.costume-whitecat:whitecat`)
- `stats`, `startingItems`, `body`, `skins`, `flavorText`는 기존 코스튬의 내용을 그대로 사용하므로 별도로 지정할 수 없습니다.
- 내장 코스튬의 기본 외형을 덮어쓰는 기능은 지원하지 않습니다.