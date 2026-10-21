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


## uploadticket.json, mod-guard-manifest.json, mod-guard.sig.json을 잃어버렸어요.

증명 파일(`mod-guard-manifest.json`, `mod-guard.sig.json`)만 없고 `uploadticket.json`은 남아 있다면 패널에서 그 모드를 선택하고 **승인 증명 받기** 버튼을 누르면 검수 서버에서 다시 받아올 수 있습니다. 단, 포맷 등으로 `uploadticket.json`까지 잃었다면 게임은 그 폴더를 새 모드로 보기 때문에, 그대로 올리면 기존 글을 갱신하는 것이 아니라 새로운 창작마당 게시글로 생성합니다. 기존 글을 이어서 갱신하려면 다음 순서로 복구합니다.

1. 창작마당에서 자기 모드를 구독합니다.
2. Steam이 내려받은 폴더(`<Steam 라이브러리>/steamapps/workshop/content/2436940/<창작마당 번호>/`)를 통째로 `WorkshopLocal/` 아래에 복사합니다. 폴더 이름은 아무거나 괜찮습니다.
3. 복사한 폴더에 `uploadticket.json`을 아래 내용으로 만듭니다. 번호는 창작마당 페이지 URL의 `id=` 뒤 숫자를 입력하세요.
```json
{
  "modId": "yourname.first-mod",
  "publishedFileId": 123456789
}
```
4. 게임을 실행하면 패널에 "검수 요청 전"으로 보입니다. 검수 요청 → 승인 → 승인 증명 받기 → 창작마당에 올리기를 다시 거치면 **기존 글이 갱신**됩니다. (주의 : 다른 모드를 만들기 위해 기존 모드 폴더를 복제했다면 `uploadticket.json`과 증명 파일을 **반드시 지우세요.** 남겨 두면 새 모드가 원래 모드의 창작마당 글을 덮어씁니다.)