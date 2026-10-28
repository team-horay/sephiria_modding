# 능력치 목록

코스튬 `stats`와 모드 코드에서 쓰는 스탯 ID입니다. 값은 `"ID/값"` 형식의 정수이고, 단위 열은 게임 화면에 표시될 때의 환산입니다. 예를 들어 `100 = 1%`인 스탯에 `CRITICAL/-400`을 적으면 -4%로 표시됩니다.

내장 코스튬이 실제로 쓰는 예: `MOVE_SPEED/12`, `MAX_HP/-15`, `DEFENSE/10`, `EVASION/1200`, `CRITICAL/-400`, `ATTACK_SPEED/7`, `EXP_DROP/4`, `STARTING_MONEY/800`.

| ID | 이름 | Name | 단위 |
|---|---|---|---|
| `ATTACK_SPEED` | 공격 속도 | Attack Speed | 1 = 1% |
| `BASIC_ATTACK_DAMAGE` | 일반 공격 피해 | Normal Attack Damage | 1 = 1% |
| `BOSS_REWARD_DICE` | 보스 처치 시 주사위 {VALUE}개 획득 | Acquire {VALUE} dice upon defeating a boss | 정수 |
| `BUFF_DURATION` | 자신이 부여한 버프의 지속시간 | Duration of Buffs Applied by Self | 1 = 1% |
| `BURN_DAMAGE` | &lt;tag=Burn&gt; 피해량 | &lt;tag=Burn&gt; Damage | 1 = 1% |
| `BURN_DURATION` | &lt;tag=Burn&gt; 디버프 지속시간 | &lt;tag=Burn&gt; duration | 1 = 1% |
| `BURN_SPEED` | &lt;tag=Burn&gt; 공격 속도 | Attack Speed of &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_STACK` | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | 정수 |
| `CHARGING_CHARM_BONUS` | &lt;tag=FrostRelic&gt; 충전 속도 | &lt;tag=FrostRelic&gt; Charge Speed | 1 = 1% |
| `CHARM_DAMAGE_BONUS` | &lt;tag=Artifact&gt; 피해량 | &lt;tag=Artifact&gt; Damage | 1 = 1% |
| `COMBO_BONUS_DAMAGE` | 활성화된 콤보 종류당 &lt;tag=FinalDamage&gt; 보너스 | &lt;tag=FinalDamage&gt; Bonus for each Activated Combo Type | 1 = 1% |
| `COOLDOWN_RECOVERY_SPEED` | &lt;tag=Magic_NoIcon&gt; 가속 | &lt;tag=Magic_NoIcon&gt; Haste | 정수 |
| `CRITICAL` | 치명타 확률 | Critical Chance | 100 = 1% |
| `CRITICAL_DAMAGE_RATE` | 치명타 피해 | Critical Damage | 1 = 1% |
| `CROSSBOWAMMO` | 탄창 용량 | Magazine Capacity | 정수 |
| `CROSSBOWFIRESPEED` | 석궁 발사 속도 | Crossbow Firing Speed | 1 = 1% |
| `CROSSBOW_ADDITIONAL_MAGAZINE` | 탄창 개수 | Magazine Count | 정수 |
| `CROSSBOW_RELOADSPEED` | 석궁 재장전 속도 | Crossbow Reload Speed | 정수 |
| `DARK_CLOUD_DAMAGE` | &lt;tag=DarkCloud&gt;의 추가 피해량 | Extra &lt;tag=DarkCloud&gt; damage | 1 = 1% |
| `DARK_CLOUD_RESTORE_DURING_BATTLE` | 전투 중 &lt;tag=DarkCloud&gt; 회복 속도 | &lt;tag=DarkCloud&gt; Recovery Rate during Combat | 1 = 1% |
| `DARK_CLOUD_SPEED` | &lt;tag=DarkCloud&gt;의 소모 속도 | Consumption Speed of &lt;tag=DarkCloud&gt; | 1 = 1% |
| `DASH_ATTACK_DAMAGE` | 대시 공격 피해 | Dash Attack Damage | 1 = 1% |
| `DASH_ATTACK_IGNORE_DEFENSE` | 대시 공격 방어 관통 | Dash Attack Ignore Defense | 1 = 1% |
| `DASH_COUNT` | 대시 횟수 | Dash Count | 정수 |
| `DASH_DISABLE` | 대시 불가 | Dash Disabled | 정수 |
| `DASH_INVINCIBLE_TIME_BONUS` | 대시 중 무적 시간 보너스 | Invincibility window bonus while dashing | 1000 = 1s |
| `DASH_RECOVERY_SPEED` | 대시 회복 속도 | Dash Recovery Speed | 1 = 1% |
| `DASH_SPEED` | 대시 속도 | Dash speed | 1 = 1% |
| `DEBUFF_DAMAGE` | &lt;tag=Debuff&gt;로 주는 피해량 | Damage dealt with &lt;tag=Debuff&gt; | 1 = 1% |
| `DEBUFF_DURATION` | 적에게 부여하는 모든 디버프의 지속시간 | Duration of all debuffs inflicted on enemies | 1 = 1% |
| `DEFENSE` | 방어력 | Defense | 정수 |
| `DEFENSE_TO_ATTACK` | &lt;tag=Defense&gt;에 따른 피해 감소가 적용되지 않지만, 증가된 &lt;tag=DamageReduction&gt;만큼 주는 피해 증가 | No damage reduction from &lt;tag=Defense&gt;, but increases damage dealt by increased &lt;tag=DamageReduction&gt; | 정수 |
| `DIMENSION_POCKET` | &lt;tag=TEXT:DimensionPocket&gt; 용량 | &lt;tag=TEXT:DimensionPocket&gt; Capacity | 정수 |
| `DIRECT_ATTACK_BURN` | &lt;tag=WeaponAction_DirectAttack&gt; 시 {VALUE} 확률로 &lt;tag=Burn&gt; 디버프 부여 | {VALUE} chance to inflict &lt;tag=Burn&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DIRECT_ATTACK_ELECTRIC` | &lt;tag=WeaponAction_DirectAttack&gt; 시 {VALUE} 확률로 &lt;tag=Electric&gt; 디버프 부여 | {VALUE} chance to inflict &lt;tag=Electric&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt;; | 1 = 1% |
| `DIRECT_ATTACK_FROSTBITE` | &lt;tag=WeaponAction_DirectAttack&gt; 시 {VALUE} 확률로 &lt;tag=Frostbite&gt; 디버프 부여 | {VALUE} chance to inflict &lt;tag=Frostbite&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DISABLE_DUAL_ARTIFACT` | &lt;tag=TEXT:BondArtifact&gt;(이)가 더 이상 등장하지 않음 | &lt;tag=TEXT:BondArtifact&gt; no longer appears | 정수 |
| `ELECTRIC_DAMAGE` | &lt;tag=Electric&gt; 피해량 | &lt;tag=Electric&gt; extra damage | 1 = 1% |
| `ELECTRIC_STACK` | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | 정수 |
| `ELITE_DAMAGE` | &lt;tag=EnemyType_Boss&gt;와 &lt;tag=EnemyType_Miniboss&gt;에게 주는 추가 피해량 | Bonus damage dealt to &lt;tag=EnemyType_Boss&gt; and &lt;tag=EnemyType_Miniboss&gt; | 1 = 1% |
| `EVASION` | 회피 | Evasion | 100 = 1 |
| `EVASION_DISABLE` | &lt;tag=Evasion&gt; 불가 | &lt;tag=Evasion&gt; Disabled | 1 = 1% |
| `EVASION_RATE_REDUCTION` | 회피 확률 감소 | Evasion Chance Reduction | 1 = 1% |
| `EXP_DROP` | 경험치 드롭 | Experience Drop | 1 = 1% |
| `EXTRA_ITEM_CHOICES` | 추가 아이템 선택지 | Additional Item Choices | 정수 |
| `EXTRA_LIFE` | 사망 시 60%의 체력으로 부활 ({VALUE}회) | Revive with 60% HP upon death ({VALUE} times) | 정수 |
| `EXTRA_MIRACLE_CHOICES` | 추가 기적 선택지 | Additional Miracle Choices | 정수 |
| `EXTRA_WEAPON_CHOICES` | 추가 무기 선택지 | Additional Weapon Choices | 정수 |
| `FINAL_DAMAGE` | 모든 피해 증폭 | Universal Damage Boost | 1 = 1% |
| `FINAL_HP` | 최종 HP | Final HP | 1 = 1% |
| `FINAL_MP` | 최종 MP | Final MP | 1 = 1% |
| `FINAL_WEAPONDAMAGE` | &lt;tag=Weapon&gt; 피해량 | &lt;tag=Weapon&gt; Damage | 1 = 1% |
| `FIRE_DAMAGE` | 화염속성 피해 | Fire Damage | 정수 |
| `FIRE_DAMAGE_AMP` | &lt;tag=FireDamage&gt; 증폭 | &lt;tag=FireDamage&gt; Amplification | 1 = 1% |
| `FIXED_DASH` | 대시 횟수가 {VALUE}회로 고정 | Dash count fixed at {VALUE} | 정수 |
| `FLAME_SWORD_ADDITIONAL_ATTACK` | &lt;tag=FlameSword&gt; {VALUE}회 추가 발동 | Additionally activates &lt;tag=FlameSword&gt; {VALUE} times | 정수 |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_MAGIC` | &lt;tag=Magic:noicon&gt; 피해를 입힐 시, &lt;tag=FlameSword&gt; {VALUE}회 추가 발동 | When dealing &lt;tag=Magic:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | 정수 |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_WEAPON` | &lt;tag=Weapon:noicon&gt; 피해를 입힐 시, &lt;tag=FlameSword&gt; {VALUE}회 추가 발동 | When dealing &lt;tag=Weapon:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | 정수 |
| `FLAME_SWORD_COOLDOWN` | &lt;tag=FlameSword&gt; 재사용 대기시간 | &lt;tag=FlameSword&gt; Cooldown | 1 = 1% |
| `FLAME_SWORD_CRITICAL` | &lt;tag=FlameSword&gt; 치명타 확률 | Critical Chance of &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_DAMAGE` | &lt;tag=FlameSword&gt; 피해량 | &lt;tag=FlameSword&gt; Damage | 1 = 1% |
| `FLAME_SWORD_IGNORE_DEFENSE` | &lt;tag=FlameSword&gt;이 대상의 방어력을 {VALUE} 무시 | Ignore target's defense by {VALUE} with &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_LUCK` | &lt;tag=FlameSword&gt; 투척 시 {VALUE} 확률로 크기가 커지고 피해량이 &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% 증가 | &lt;tag=FlameSword&gt; has a {VALUE} chance to grow in size and increase damage by &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% when thrown | 1 = 1% |
| `FLAME_SWORD_MAX` | &lt;tag=FlameSword&gt; 개수 상한 | &lt;tag=FlameSword&gt; Maximum Count | 정수 |
| `FOLLOWER_ATTACK_SPEED` | 동료의 공격 속도 | Companion Attack Speed | 1 = 1% |
| `FOLLOWER_CRITICAL` | 동료 공격 시 치명타 확률 | Companion Critical Chance | 100 = 1% |
| `FOLLOWER_DAMAGE` | 동료가 입히는 피해량 | Companion Damage | 1 = 1% |
| `FOLLOWER_DEFENSE` | 동료들의 방어력 | Companion Defense | 정수 |
| `FOLLOWER_MAX_HP` | 동료들의 최대 HP | Companion Max HP | 1 = 1% |
| `FOLLOWER_REVIVE` | 동료 부활 시간 가속 | Companion Revive Time Haste | 1 = 1% |
| `FREEZE_DAMAGE` | &lt;tag=Freeze&gt; 디버프 피해량 | &lt;tag=Freeze&gt; Debuff Damage | 1 = 1% |
| `FROSTBITE_DAMAGE` | &lt;tag=Frostbite&gt;에 걸린 대상은 초당 &lt;tag=IceDamage&gt;의 {VALUE}%만큼 피해 (&lt;tag=Stacks&gt;에 따라 피해량 증가) | Targets affected by &lt;tag=Frostbite&gt; take damage per second equal to {VALUE}% of &lt;tag=IceDamage&gt; (damage increases with &lt;tag=Stacks&gt;) | 정수 |
| `FROST_RELIC_CRITICAL_DAMAGE` | &lt;tag=FrostRelic&gt;의 치명타 피해 | &lt;tag=FrostRelic&gt; critical damage | 1 = 1% |
| `FROST_RELIC_DAMAGE` | &lt;tag=FrostRelic&gt;의 피해량 | &lt;tag=FrostRelic&gt; damage | 1 = 1% |
| `FRUIT_COUNT` | 과일 꼬치 상한 | Fruit Skewer Max | 정수 |
| `FURY_INVINCIBLE_TIME` | &lt;tag=WeaponAction_Fury&gt; 시 무적 시간 | Invincibility Duration During &lt;tag=WeaponAction_Fury&gt; | 10 = 1s |
| `HEALING_PENALTY` | 치유 저주: 받는 회복량이 {VALUE} 감소 | Healing Curse: Healing received decreases by {VALUE} | 1 = 1% |
| `HIGHEST_ELEMENTAL_DAMAGE` | 가장 높은 속성 피해 | Highest Elemental Damage | 정수 |
| `HP_CURSE` | 최대 HP가 {VALUE}(으)로 고정됨 | Max HP set to {VALUE} | 정수 |
| `HP_POTION_BONUS` | HP 포션 효과 | HP Potion Bonus | 100 = 1% |
| `HP_STEAL` | HP 흡수 | Life Steal | 정수 |
| `ICE_DAMAGE` | 얼음속성 피해 | Cold Damage | 정수 |
| `ICE_DAMAGE_AMP` | &lt;tag=IceDamage&gt; 증폭 | &lt;tag=IceDamage&gt; Amplification | 1 = 1% |
| `IGNORE_DEFENSE` | 방어 관통 | Ignore Defense | 1 = 1% |
| `IGNORE_SUPER_ARMOR` | 대상의 &lt;tag=SuperArmor&gt;에 추가 피해 | Extra damage to target’s &lt;tag=SuperArmor&gt; | 1 = 1% |
| `INFINITY_DASH` | 무한 대시 | Infinity Dash | 정수 |
| `INFINITY_MP` | 마력 환류 : &lt;tag=MaxMP&gt;가 &lt;tag=CONST:infinityMPMax&gt;으로 고정되고, 줄어들지 않음 | Magic Reflow: &lt;tag=MaxMP&gt; is fixed at &lt;tag=CONST:infinityMPMax&gt;, and MP does not decrease | 정수 |
| `INVENTORY_SLOT` | 인벤토리 슬롯 | Inventory Slot | 정수 |
| `LEAF_DROP` | 잎 드롭 | Leaf Drop | 1 = 1% |
| `LEVEL_UP_HEAL` | 레벨업 시 HP 회복 | HP Restored on Level Up | 1 = 1% |
| `LIGHTNING_DAMAGE` | 번개속성 피해 | Lightning Damage | 정수 |
| `LIGHTNING_DAMAGE_AMP` | &lt;tag=LightningDamage&gt; 증폭 | &lt;tag=LightningDamage&gt; Amplification | 1 = 1% |
| `LUCK` | 행운 | Luck | 정수 |
| `MAGIC_COST_REDUCE` | &lt;tag=Magic:noicon&gt;의 소모 MP 감소 | Reduce MP Cost of &lt;tag=Magic:noicon&gt; | 1 = 1% |
| `MAGIC_CRITICAL` | &lt;tag=Magic&gt;의 치명타 확률 | &lt;tag=Magic&gt; Critical Chance | 100 = 1% |
| `MAGIC_CRITICAL_DAMAGE_RATE` | &lt;tag=Magic&gt;의 치명타 피해 | &lt;tag=Magic&gt; Critical Damage | 1 = 1% |
| `MAGIC_DAMAGE_BONUS` | &lt;tag=Magic_NoIcon&gt; 피해량 | &lt;tag=Magic_NoIcon&gt; Damage | 1 = 1% |
| `MAGIC_IGNORE_DEFENSE` | &lt;tag=Magic_NoIcon&gt; 방어 관통 | &lt;tag=Magic_NoIcon&gt; Ignore Defense | 1 = 1% |
| `MAX_HP` | 최대 HP | Max HP | 정수 |
| `MAX_MP` | 최대 MP | Max MP | 정수 |
| `MIN_DARK_CLOUD` | &lt;tag=DarkCloud&gt; 용량 | &lt;tag=DarkCloud&gt; Capacity | 정수 |
| `MIN_DARK_CLOUD_AMP` | &lt;tag=DarkCloud&gt; 용량 | &lt;tag=DarkCloud&gt; Capacity | 1 = 1% |
| `MOVE_SPEED` | 이동 속도 | Move Speed | 1 = 1% |
| `MP_REGEN` | MP 재생 | MP Regeneration | 정수 |
| `MP_REGEN_KILL` | 적 처치 시 &lt;tag=MP&gt; {VALUE} 회복 | Recover &lt;tag=MP&gt; {VALUE} on Kill | 정수 |
| `MP_REGEN_MULTIPLE` | &lt;tag=MP&gt; 회복 시의 효율 | Efficiency When Recovering &lt;tag=MP&gt; | 1 = 1% |
| `MP_SKILL_DAMAGE` | MP를 소모하는 능력의 피해량 | Damage of abilities consuming MP | 1 = 1% |
| `MP_STEAL` | MP 흡수 | MP Steal | 정수 |
| `NEGOTIATION` | 협상력 | Barter | 정수 |
| `NO_DASH_INVINCIBLE` | 대시 무적 비활성화 | Disable Dash Invincibility | 정수 |
| `NO_MAGIC_COST` | 마법서가 MP를 소모하지 않음 | Grimoires do not consume MP | 정수 |
| `PARTY_BUFF` | 버프 마법 시전 시, 주변 5&lt;tag=Tiles&gt; 이내 플레이어에게도 버프 부여. 대신 버프 마법 시전 비용이 50% 증가 | Grants buffs to players within 5 &lt;tag=Tiles&gt; when casting buffs. However, the spell cost is increased by 50%. | 정수 |
| `PHYSICAL_DAMAGE` | 물리 피해 | Physical Damage | 정수 |
| `PHYSICAL_DAMAGE_AMP` | &lt;tag=PhysicalDamage&gt; 증폭 | &lt;tag=PhysicalDamage&gt; Amplification | 1 = 1% |
| `PLANET_ATTACK_SPEED` | 행성 공격 속도 | Planet Attack Speed | 1 = 1% |
| `PLANET_DAMAGE` | 행성 피해량 | Planet Damage | 1 = 1% |
| `POTION_SLOT` | 포션 가방 슬롯 | Potion Bag Slot | 정수 |
| `RECEIVED_DAMAGE_REDUCTION` | 받는 피해 감소 | Damage Taken Reduction | 1 = 1% |
| `REVIVE_PLAYER_HASTE` | 다른 플레이어를 살릴 때의 시간 {VALUE} 단축 | Reduces the time to revive another player by {VALUE} | 1 = 1% |
| `SAPPHIREBONUS` | 사파이어 획득 보너스 | Sapphire Acquisition Bonus | 1 = 1% |
| `SPECIAL_ATTACK_DAMAGE` | 특수 공격 피해 | Special Attack Damage | 1 = 1% |
| `STARTING_MONEY` | 탐험 시작 시 {VALUE} &lt;tag=Leaf:nocolor&gt; | Start with {VALUE} &lt;tag=Leaf:nocolor&gt; | 정수 |
| `SUB_BAG_SLOT` | 보조 가방 슬롯 | Side Bag Slot | 정수 |
| `THORNS` | 가시 | Thorns | 1 = 1% |
| `TOUGHNESS` | 강인함 | Toughness | 정수 |
| `TRUE_DAMAGE` | 고정 피해 | True Damage | 정수 |
| `UNLIMITED_COMBO` | 콤보 수치가 최대치를 초과하면 그 수치만큼 &lt;tag=FinalDamage&gt; | For each excess Combo, &lt;tag=FinalDamage&gt; | 1 = 1% |
| `WEAPON_CRITICAL` | &lt;tag=WeaponAction_DirectAttack&gt;의 치명타 확률 | Critical Hit Chance of &lt;tag=WeaponAction_DirectAttack&gt; | 100 = 1% |
| `WEAPON_RANGE` | 근접 공격 범위 | Melee Attack Range | 1 = 1% |
