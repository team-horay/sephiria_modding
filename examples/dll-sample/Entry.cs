using UnityEngine;

namespace PhysicalDamagePlus
{
    /// <summary>
    /// 모드 진입점. metadata.json의 entryClass가 이 클래스의 전체 이름("PhysicalDamagePlus.Entry")을 가리킵니다.
    ///
    /// 플레이어 아바타가 서버에서 스폰될 때 물리 피해 커스텀 스탯을 5 더하는 최소 예제입니다.
    /// customStats는 네트워크 동기화 딕셔너리라 서버에서 더하면 모든 클라이언트에 전달됩니다.
    /// 그래서 게임 상태를 바꾸는 일은 Serverside 이벤트에서만 하고, Clientside 이벤트는 확인 로그만 남깁니다.
    ///
    /// 확인: 게임을 시작해 스탯 창을 열면 물리 피해가 +5 되어 있고, Player.log에 "[PhysicalDamagePlus]" 로그가 남습니다.
    /// </summary>
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // 프로세스당 한 번 호출됩니다. 이벤트 구독은 여기서 한 번만 하세요.
        protected override void OnModLoaded()
        {
            Debug.Log($"[PhysicalDamagePlus] OnModLoaded: {metadata.modName} v{metadata.modVersion} by {metadata.modAuthor} " +
                      $"| package={Context?.PackageRoot}");

            HorayModAPI.OnPlayerAvatarSpawnedServerside += HandlePlayerSpawnedServer;
            HorayModAPI.OnPlayerAvatarSpawnedClientside += HandlePlayerSpawnedClient;
        }

        protected override void OnModUnloaded()
        {
            HorayModAPI.OnPlayerAvatarSpawnedServerside -= HandlePlayerSpawnedServer;
            HorayModAPI.OnPlayerAvatarSpawnedClientside -= HandlePlayerSpawnedClient;
        }

        // 호스트(서버)에서만 호출됩니다. 런을 다시 시작할 때마다 새 아바타에 대해 다시 호출됩니다.
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
            Debug.Log($"[PhysicalDamagePlus] 서버: {player.name} PhysicalDamage +{Bonus} → {player.GetCustomStat(ECustomStat.PhysicalDamage)}");
        }

        // 모든 참가자(호스트 포함)에서 호출됩니다. 여기서는 동기화된 값을 확인만 합니다.
        private static void HandlePlayerSpawnedClient(PlayerAvatar player)
        {
            if (player == null)
                return;
            Debug.Log($"[PhysicalDamagePlus] 클라: {player.name} (local={player.isLocalPlayer}) PhysicalDamage={player.GetCustomStat(ECustomStat.PhysicalDamage)}");
        }
    }
}
