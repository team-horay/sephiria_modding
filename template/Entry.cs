using UnityEngine;

namespace MyFirstMod
{
    /// <summary>
    /// 모드 진입점. metadata.json의 entryClass가 이 클래스의 전체 이름("MyFirstMod.Entry")을 가리킵니다.
    /// 타이틀 메뉴에 버튼 하나를 추가하고 로그를 남기는 최소 예제입니다.
    /// 로드 확인은 Player.log의 "[MyFirstMod]" 로그로 합니다.
    /// </summary>
    public class Entry : WorkshopModBase
    {
        private int clickCount;

        // 프로세스당 한 번 호출됩니다. 이벤트 구독은 여기서 한 번만 하세요.
        protected override void OnModLoaded()
        {
            Debug.Log($"[MyFirstMod] OnModLoaded: {metadata.modName} v{metadata.modVersion} by {metadata.modAuthor} " +
                      $"| package={Context?.PackageRoot}");

            HorayModAPI.OnLocalizationReady += HandleLocalizationReady;
            HorayModAPI.OnTitleMenuReady += HandleTitleMenuReady;
            HorayModAPI.OnAllDatabasesReady += HandleAllDatabasesReady;
        }

        protected override void OnModUnloaded()
        {
            HorayModAPI.OnLocalizationReady -= HandleLocalizationReady;
            HorayModAPI.OnTitleMenuReady -= HandleTitleMenuReady;
            HorayModAPI.OnAllDatabasesReady -= HandleAllDatabasesReady;
        }

        // 버튼 라벨 같은 짧은 텍스트는 코드로 등록할 수 있습니다. 양이 많으면 localization 모듈을 쓰세요.
        private void HandleLocalizationReady(HorayModLocalizationContext loc)
        {
            loc.AddText("ko-KR", "MyFirstMod_Hello", "안녕, 세피리아!");
            loc.AddText("en-US", "MyFirstMod_Hello", "Hello, Sephiria!");
        }

        // 타이틀 씬이 만들어질 때마다 호출됩니다. 돌려받은 버튼을 다른 씬까지 들고 가지 마세요.
        private void HandleTitleMenuReady(UI_TitleLobby title)
        {
            title.AddTitleSubMenu("MyFirstMod_Hello", () =>
            {
                clickCount++;
                Debug.Log($"[MyFirstMod] 타이틀 버튼 클릭 {clickCount}회");
            });
        }

        // 월드에 들어가 모든 데이터베이스가 초기화된 뒤 호출됩니다. 런을 다시 시작할 때마다 다시 호출됩니다.
        private void HandleAllDatabasesReady()
        {
            Debug.Log("[MyFirstMod] OnAllDatabasesReady");
        }
    }
}
