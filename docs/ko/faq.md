# FAQ


## 로그는 어떻게 볼 수 있나요?

게임 로그는 아래 파일에 기록됩니다. 게임을 실행할 때마다 새로 쓰이고, 직전 실행분은 `Player-prev.log`에 남습니다.

```
%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log
```

모드 관련 줄은 대괄호 태그로 시작하니 그 태그로 검색하면 됩니다.

| 태그 | 내용 |
|---|---|
| `[WorkshopModManager]` | 구독 목록 조회, 다운로드, 로컬 폴더 수집, 로딩 계획 요약 |
| `[WorkshopModRuntime]` | DLL 활성화와 번역 병합 결과 |
| `[WorkshopUploader]` | 검수 제출과 창작마당 업로드 |
| `[ModGuardClient]` | 검수 서버 통신 |
| `[WorkshopLobbyMods]` | 멀티플레이 방의 모드 집합 비교 |
| `모드 내 언어 파일 발견` | 번역 파일별로 읽힌 키 수 |


## 창작마당에 "로컬 모드 (AddOns 폴더)"는 뭔가요?

패널에 "로컬 모드 (AddOns 폴더)" 항목이 보일 수 있습니다. 구버전 모딩 기능이므로 새 모드에는 쓰지 마세요.
구버전 모드는 다른 플레이어와의 모드 동기화가 불가능하므로 삭제를 권합니다.


## WorkshopLocal 폴더를 세피리아 설치 폴더가 아니라 다른 폴더로 옮기고 싶어요.

모드를 다른 드라이브나 작업 폴더에 두고 싶다면 실행 인자로 경로를 넘길 수 있습니다. Steam 라이브러리에서 세피리아 → 속성 → 일반 → 시작 옵션에 적습니다.

```
-workshop_local=D:\mods\my-first-mod
```

여러 개는 세미콜론으로 잇고, 경로에 공백이 있으면 전체를 따옴표로 감쌉니다.

```
"-workshop_local=D:\My Mods\first;D:\My Mods\second"
```

인자로 넘긴 폴더는 `WorkshopLocal/` 하위 폴더와 똑같이 취급됩니다.





## 자주 하는 실수 (미정리)

| 증상 | 원인 |
|---|---|
| 패널에 모드가 보이지만 아무것도 적용되지 않음 | `mod` 키가 없음(없음으로 분류). 또는 사용자가 꺼 둠 |
| "schemaVersion은 정수여야 함" | `"schemaVersion": "1"`처럼 따옴표를 씀 |
| "대소문자가 실제 파일명과 다름" | `path`의 대소문자가 실제 파일과 다름. 탐색기가 보여 주는 이름과 똑같이 적을 것 |
| "폴더여야 하는데 파일임" | `localization` 모듈의 `path`에 `.json` 파일을 직접 적음. 폴더를 적어야 함 |
| "version '>=1.0'은 지원하지 않는 형식" | `dependencies[].version`에 범위 표기를 씀. 정확한 버전 하나만 가능 |
| "알 수 없는 모듈 type" | 오타, 또는 게임이 아직 지원하지 않는 모듈 타입을 적음 |
| 모드 두 개가 둘 다 제외됨 | 같은 `modId`를 씀. 템플릿을 복사한 뒤 `modId`를 바꾸지 않은 경우가 대부분 |
| `multiplayer` 경고가 로그에 남음 | `installation`·`support` 값 오타. 거부되진 않지만 `unknown`·`untested`로 처리됨 |
