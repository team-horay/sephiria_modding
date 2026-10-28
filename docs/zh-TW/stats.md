# 能力值清單

服裝 `stats` 與模組程式碼中使用的能力值 ID。值為 `"ID/值"` 格式的整數，「單位」欄則是在遊戲內顯示時的換算方式。例如，在 `100 = 1%` 的能力值上填寫 `CRITICAL/-400`，會顯示為 -4%。

內建服裝實際使用的範例：`MOVE_SPEED/12`、`MAX_HP/-15`、`DEFENSE/10`、`EVASION/1200`、`CRITICAL/-400`、`ATTACK_SPEED/7`、`EXP_DROP/4`、`STARTING_MONEY/800`。

| ID | 英文名稱 | 名稱 | 單位 |
|---|---|---|---|
| `ATTACK_DASH` |  |  | 100 = 1 |
| `ATTACK_SPEED` | Attack Speed | 攻擊速度 | 1 = 1% |
| `BASIC_ATTACK_DAMAGE` | Normal Attack Damage | 普通攻擊傷害 | 1 = 1% |
| `BOSS_REWARD_DICE` | Acquire {VALUE} dice upon defeating a boss | 擊敗Boss時獲得{VALUE}個骰子 | 整數 |
| `BUFF_DURATION` | Duration of Buffs Applied by Self | 自身施加的增益持續時間 | 1 = 1% |
| `BURN_DAMAGE` | &lt;tag=Burn&gt; Damage | &lt;tag=Burn&gt;異常狀態額外傷害 | 1 = 1% |
| `BURN_DURATION` | &lt;tag=Burn&gt; duration | &lt;tag=Burn&gt;負面狀態持續時間 | 1 = 1% |
| `BURN_SPEED` | Attack Speed of &lt;tag=Burn&gt; | &lt;tag=Burn&gt;的攻擊速度 | 1 = 1% |
| `BURN_STACK` | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | &lt;tag=Burn&gt;&lt;tag=Stacks&gt; | 整數 |
| `CHARGING_CHARM_BONUS` | &lt;tag=FrostRelic&gt; Charge Speed | &lt;tag=FrostRelic&gt;充能速度 | 1 = 1% |
| `CHARM_DAMAGE_BONUS` | &lt;tag=Artifact&gt; Damage | &lt;tag=Artifact&gt;傷害 | 1 = 1% |
| `COMBO_BONUS_DAMAGE` | &lt;tag=FinalDamage&gt; Bonus for each Activated Combo Type | 每種啟用的連擊類型獲得&lt;tag=FinalDamage&gt;加成 | 1 = 1% |
| `COOLDOWN_RECOVERY_SPEED` |  |  | 整數 |
| `CRITICAL` | Critical Chance | 暴擊機率 | 100 = 1% |
| `CRITICAL_DAMAGE_RATE` | Critical Damage | 暴擊傷害 | 1 = 1% |
| `CROSSBOWAMMO` | Magazine Capacity | 彈匣容量 | 整數 |
| `CROSSBOWFIRESPEED` | Crossbow Firing Speed | 弩發射速度 | 1 = 1% |
| `CROSSBOW_ADDITIONAL_MAGAZINE` | Magazine Count | 彈匣數量 | 整數 |
| `CROSSBOW_RELOADSPEED` | Crossbow Reload Speed | 弩的重新裝填速度 | 整數 |
| `DARK_CLOUD_DAMAGE` | Extra &lt;tag=DarkCloud&gt; damage | &lt;tag=DarkCloud&gt;的額外傷害 | 1 = 1% |
| `DARK_CLOUD_RESTORE_DURING_BATTLE` | &lt;tag=DarkCloud&gt; Recovery Rate during Combat | 戰鬥中&lt;tag=DarkCloud&gt;恢復速度 | 1 = 1% |
| `DARK_CLOUD_SPEED` | Consumption Speed of &lt;tag=DarkCloud&gt; | &lt;tag=DarkCloud&gt;的消耗速度 | 1 = 1% |
| `DASH_ATTACK_DAMAGE` | Dash Attack Damage | 衝刺攻擊傷害 | 1 = 1% |
| `DASH_ATTACK_IGNORE_DEFENSE` | Dash Attack Ignore Defense | 衝刺攻擊防禦穿透 | 1 = 1% |
| `DASH_COUNT` | Dash Count | 衝刺次數 | 整數 |
| `DASH_DISABLE` | Dash Disabled | 無法衝刺 | 整數 |
| `DASH_INVINCIBLE_TIME_BONUS` | Invincibility window bonus while dashing | 衝刺中無敵時間加成 | 1000 = 1s |
| `DASH_RECOVERY_SPEED` |  |  | 1 = 1% |
| `DASH_SPEED` | Dash speed | 衝刺速度 | 1 = 1% |
| `DEBUFF_DAMAGE` | Damage dealt with &lt;tag=Debuff&gt; | 使用&lt;tag=Debuff&gt;造成的傷害 | 1 = 1% |
| `DEBUFF_DURATION` | Duration of all debuffs inflicted on enemies | 施加給敵人的所有減益效果持續時間 | 1 = 1% |
| `DEFENSE` | Defense | 防禦力 | 整數 |
| `DEFENSE_TO_ATTACK` | No damage reduction from &lt;tag=Defense&gt;, but increases damage dealt by increased &lt;tag=DamageReduction&gt; | &lt;tag=Defense&gt;不套用傷害減免，但造成的傷害提高增加的&lt;tag=DamageReduction&gt;數值 | 整數 |
| `DIMENSION_POCKET` | &lt;tag=TEXT:DimensionPocket&gt; Capacity | &lt;tag=TEXT:DimensionPocket&gt;容量 | 整數 |
| `DIRECT_ATTACK_BURN` | {VALUE} chance to inflict &lt;tag=Burn&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | &lt;tag=WeaponAction_DirectAttack&gt;時，有{VALUE}的機率施加&lt;tag=Burn&gt;減益效果 | 1 = 1% |
| `DIRECT_ATTACK_ELECTRIC` | {VALUE} chance to inflict &lt;tag=Electric&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt;; | &lt;tag=WeaponAction_DirectAttack&gt;時，有{VALUE}的機率施加&lt;tag=Electric&gt;減益效果 | 1 = 1% |
| `DIRECT_ATTACK_FROSTBITE` | {VALUE} chance to inflict &lt;tag=Frostbite&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | &lt;tag=WeaponAction_DirectAttack&gt;時，有{VALUE}的機率施加&lt;tag=Frostbite&gt;減益效果 | 1 = 1% |
| `DISABLE_DUAL_ARTIFACT` | &lt;tag=TEXT:BondArtifact&gt; no longer appears | &lt;tag=TEXT:BondArtifact&gt;不再出現 | 整數 |
| `ELECTRIC_DAMAGE` | &lt;tag=Electric&gt; extra damage | &lt;tag=Electric&gt;傷害 | 1 = 1% |
| `ELECTRIC_STACK` | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | &lt;tag=Electric&gt;&lt;tag=Stacks&gt; | 整數 |
| `ELITE_DAMAGE` | Bonus damage dealt to &lt;tag=EnemyType_Boss&gt; and &lt;tag=EnemyType_Miniboss&gt; | 對&lt;tag=EnemyType_Boss&gt;與&lt;tag=EnemyType_Miniboss&gt;造成的額外傷害 | 1 = 1% |
| `EVASION` | Evasion | 閃避 | 100 = 1 |
| `EVASION_DISABLE` | &lt;tag=Evasion&gt; Disabled | 無法&lt;tag=Evasion&gt; | 1 = 1% |
| `EVASION_RATE_REDUCTION` | Evasion Chance Reduction | 閃避機率減少 | 1 = 1% |
| `EXP_DROP` | Experience Drop | 經驗值掉落 | 1 = 1% |
| `EXTRA_ITEM_CHOICES` | Additional Item Choices | 額外物品選擇 | 整數 |
| `EXTRA_LIFE` | Revive with 60% HP upon death ({VALUE} times) | 死亡時以60%生命值復活（{VALUE}次） | 整數 |
| `EXTRA_MIRACLE_CHOICES` | Additional Miracle Choices | 額外奇蹟選擇 | 整數 |
| `EXTRA_WEAPON_CHOICES` | Additional Weapon Choices | 額外武器選擇 | 整數 |
| `FINAL_DAMAGE` |  |  | 1 = 1% |
| `FINAL_HP` | Final HP | 最終HP | 1 = 1% |
| `FINAL_MP` | Final MP | 最終MP | 1 = 1% |
| `FINAL_WEAPONDAMAGE` | &lt;tag=Weapon&gt; Damage | &lt;tag=Weapon&gt;傷害 | 1 = 1% |
| `FIRE_DAMAGE` | Fire Damage | 火焰屬性傷害 | 整數 |
| `FIRE_DAMAGE_AMP` | &lt;tag=FireDamage&gt; Amplification | &lt;tag=FireDamage&gt;增幅 | 1 = 1% |
| `FIXED_DASH` | Dash count fixed at {VALUE} | 衝刺次數固定為{VALUE}次 | 整數 |
| `FLAME_SWORD_ADDITIONAL_ATTACK` | Additionally activates &lt;tag=FlameSword&gt; {VALUE} times | &lt;tag=FlameSword&gt;額外觸發{VALUE}次 | 整數 |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_MAGIC` | When dealing &lt;tag=Magic:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | 造成&lt;tag=Magic:noicon&gt;傷害時，&lt;tag=FlameSword&gt;額外觸發{VALUE}次 | 整數 |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_WEAPON` | When dealing &lt;tag=Weapon:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | 造成&lt;tag=Weapon:noicon&gt;傷害時，&lt;tag=FlameSword&gt;額外觸發{VALUE}次 | 整數 |
| `FLAME_SWORD_COOLDOWN` | &lt;tag=FlameSword&gt; Cooldown | &lt;tag=FlameSword&gt;冷卻時間 | 1 = 1% |
| `FLAME_SWORD_CRITICAL` | Critical Chance of &lt;tag=FlameSword&gt; | &lt;tag=FlameSword&gt;暴擊機率 | 1 = 1% |
| `FLAME_SWORD_DAMAGE` | &lt;tag=FlameSword&gt; Damage | &lt;tag=FlameSword&gt;傷害 | 1 = 1% |
| `FLAME_SWORD_IGNORE_DEFENSE` | Ignore target's defense by {VALUE} with &lt;tag=FlameSword&gt; | &lt;tag=FlameSword&gt;無視目標防禦力{VALUE} | 1 = 1% |
| `FLAME_SWORD_LUCK` | &lt;tag=FlameSword&gt; has a {VALUE} chance to grow in size and increase damage by &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% when thrown | &lt;tag=FlameSword&gt;投擲時有{VALUE}機率變大並增加&lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;%傷害 | 1 = 1% |
| `FLAME_SWORD_MAX` | &lt;tag=FlameSword&gt; Maximum Count | &lt;tag=FlameSword&gt;數量上限 | 整數 |
| `FOLLOWER_ATTACK_SPEED` | Companion Attack Speed | 同伴攻擊速度 | 1 = 1% |
| `FOLLOWER_CRITICAL` | Companion Critical Chance | 同伴攻擊時的暴擊機率 | 100 = 1% |
| `FOLLOWER_DAMAGE` | Companion Damage | 同伴造成的傷害 | 1 = 1% |
| `FOLLOWER_DEFENSE` | Companion Defense | 同伴的防禦力 | 整數 |
| `FOLLOWER_MAX_HP` | Companion Max HP | 同伴的最大HP | 1 = 1% |
| `FOLLOWER_REVIVE` | Companion Revive Time Haste | 同伴復活加速 | 1 = 1% |
| `FREEZE_DAMAGE` | &lt;tag=Freeze&gt; Debuff Damage | &lt;tag=Freeze&gt;減益效果傷害 | 1 = 1% |
| `FROSTBITE_DAMAGE` | Targets affected by &lt;tag=Frostbite&gt; take damage per second equal to {VALUE}% of &lt;tag=IceDamage&gt; (damage increases with &lt;tag=Stacks&gt;) | 受到&lt;tag=Frostbite&gt;影響的目標每秒受到&lt;tag=IceDamage&gt;等於{VALUE}%的傷害（隨著&lt;tag=Stacks&gt;數增加傷害） | 整數 |
| `FROST_RELIC_CRITICAL_DAMAGE` | &lt;tag=FrostRelic&gt; critical damage | &lt;tag=FrostRelic&gt;的暴擊傷害 | 1 = 1% |
| `FROST_RELIC_DAMAGE` | &lt;tag=FrostRelic&gt; damage | &lt;tag=FrostRelic&gt;的傷害 | 1 = 1% |
| `FRUIT_COUNT` | Fruit Skewer Max | 水果串上限 | 整數 |
| `FURY_INVINCIBLE_TIME` | Invincibility Duration During &lt;tag=WeaponAction_Fury&gt; | &lt;tag=WeaponAction_Fury&gt;期間無敵時間 | 10 = 1s |
| `HEALING_PENALTY` | Healing Curse: Healing received decreases by {VALUE} | 治癒詛咒：受到的回復量減少{VALUE} | 1 = 1% |
| `HIGHEST_ELEMENTAL_DAMAGE` | Highest Elemental Damage | 最高屬性傷害 | 整數 |
| `HP_CURSE` | Max HP set to {VALUE} | 最大HP固定為{VALUE} | 整數 |
| `HP_POTION_BONUS` | HP Potion Bonus | HP藥水效果 | 100 = 1% |
| `HP_STEAL` | Life Steal | HP吸取 | 整數 |
| `ICE_DAMAGE` | Cold Damage | 冰屬性傷害 | 整數 |
| `ICE_DAMAGE_AMP` | &lt;tag=IceDamage&gt; Amplification | &lt;tag=IceDamage&gt;增幅 | 1 = 1% |
| `IGNORE_DEFENSE` | Ignore Defense | 防禦穿透 | 1 = 1% |
| `IGNORE_SUPER_ARMOR` | Extra damage to target’s &lt;tag=SuperArmor&gt; | 對目標的&lt;tag=SuperArmor&gt;造成額外傷害 | 1 = 1% |
| `INFINITY_DASH` | Infinity Dash | 無限衝刺 | 整數 |
| `INFINITY_MP` | Magic Reflow: &lt;tag=MaxMP&gt; is fixed at &lt;tag=CONST:infinityMPMax&gt;, and MP does not decrease | 魔力回流：&lt;tag=MaxMP&gt;固定為&lt;tag=CONST:infinityMPMax&gt;，且不會減少 | 整數 |
| `INVENTORY_SLOT` | Inventory Slot | 背包欄位 | 整數 |
| `LEAF_DROP` | Leaf Drop | 葉子掉落 | 1 = 1% |
| `LEVEL_UP_HEAL` | HP Restored on Level Up | 升級時HP恢復 | 1 = 1% |
| `LIGHTNING_DAMAGE` | Lightning Damage | 閃電屬性傷害 | 整數 |
| `LIGHTNING_DAMAGE_AMP` | &lt;tag=LightningDamage&gt; Amplification | &lt;tag=LightningDamage&gt;增幅 | 1 = 1% |
| `LUCK` | Luck | 幸運值 | 整數 |
| `MAGIC_COST_REDUCE` | Reduce MP Cost of &lt;tag=Magic:noicon&gt; | &lt;tag=Magic:noicon&gt;的MP消耗減少 | 1 = 1% |
| `MAGIC_CRITICAL` | &lt;tag=Magic&gt; Critical Chance | &lt;tag=Magic&gt;的暴擊率 | 100 = 1% |
| `MAGIC_CRITICAL_DAMAGE_RATE` | &lt;tag=Magic&gt; Critical Damage | &lt;tag=Magic&gt;的暴擊傷害 | 1 = 1% |
| `MAGIC_DAMAGE_BONUS` | &lt;tag=Magic_NoIcon&gt; Damage | &lt;tag=Magic_NoIcon&gt;傷害 | 1 = 1% |
| `MAGIC_IGNORE_DEFENSE` | &lt;tag=Magic_NoIcon&gt; Ignore Defense | &lt;tag=Magic_NoIcon&gt;防禦穿透 | 1 = 1% |
| `MAX_HP` | Max HP | 最大HP | 整數 |
| `MAX_MP` | Max MP | 最大MP | 整數 |
| `MIN_DARK_CLOUD` | &lt;tag=DarkCloud&gt; Capacity | &lt;tag=DarkCloud&gt;容量 | 整數 |
| `MIN_DARK_CLOUD_AMP` | &lt;tag=DarkCloud&gt; Capacity | &lt;tag=DarkCloud&gt;容量 | 1 = 1% |
| `MOVE_SPEED` | Move Speed | 移動速度 | 1 = 1% |
| `MP_REGEN` | MP Regeneration | MP再生 | 整數 |
| `MP_REGEN_KILL` | Recover &lt;tag=MP&gt; {VALUE} on Kill | 擊殺敵人時恢復&lt;tag=MP&gt;{VALUE} | 整數 |
| `MP_REGEN_MULTIPLE` | Efficiency When Recovering &lt;tag=MP&gt; | &lt;tag=MP&gt;恢復時的效率 | 1 = 1% |
| `MP_SKILL_DAMAGE` | Damage of abilities consuming MP | 消耗MP的能力傷害 | 1 = 1% |
| `MP_STEAL` | MP Steal | MP吸收 | 整數 |
| `NEGOTIATION` | Barter | 談判力 | 整數 |
| `NO_DASH_INVINCIBLE` | Disable Dash Invincibility | 停用衝刺無敵 | 整數 |
| `NO_MAGIC_COST` | Grimoires do not consume MP | 魔法書不消耗MP | 整數 |
| `PARTY_BUFF` | Grants buffs to players within 5 &lt;tag=Tiles&gt; when casting buffs. However, the spell cost is increased by 50%. | 施放增益魔法時，為周圍5&lt;tag=Tiles&gt;內的玩家施加增益效果。但是，施放增益魔法的消耗增加50%。 | 整數 |
| `PHYSICAL_DAMAGE` | Physical Damage | 物理傷害 | 整數 |
| `PHYSICAL_DAMAGE_AMP` | &lt;tag=PhysicalDamage&gt; Amplification | &lt;tag=PhysicalDamage&gt;增幅 | 1 = 1% |
| `PLANET_ATTACK_SPEED` | Planet Attack Speed | 行星攻擊速度 | 1 = 1% |
| `PLANET_DAMAGE` | Planet Damage | 行星傷害 | 1 = 1% |
| `POTION_SLOT` | Potion Bag Slot | 藥水袋欄位 | 整數 |
| `RECEIVED_DAMAGE_REDUCTION` | Damage Taken Reduction | 受到傷害減少 | 1 = 1% |
| `REVIVE_PLAYER_HASTE` | Reduces the time to revive another player by {VALUE} | 復活其他玩家所需時間縮短{VALUE} | 1 = 1% |
| `SAPPHIREBONUS` | Sapphire Acquisition Bonus | 獲得藍寶石獎勵 | 1 = 1% |
| `SPECIAL_ATTACK_DAMAGE` | Special Attack Damage | 特殊攻擊傷害 | 1 = 1% |
| `STARTING_MONEY` | Start with {VALUE} &lt;tag=Leaf:nocolor&gt; | 探索開始時獲得{VALUE}&lt;tag=Leaf:nocolor&gt; | 整數 |
| `SUB_BAG_SLOT` | Side Bag Slot | 副背包欄位 | 整數 |
| `THORNS` | Thorns | 荊棘 | 1 = 1% |
| `TOUGHNESS` | Toughness | 堅韌 | 整數 |
| `TRUE_DAMAGE` | True Damage | 固定傷害 | 整數 |
| `UNLIMITED_COMBO` | For each excess Combo, &lt;tag=FinalDamage&gt; | 連擊數值超過最大值時，每超過1點&lt;tag=FinalDamage&gt; | 1 = 1% |
| `WEAPON_CRITICAL` | Critical Hit Chance of &lt;tag=WeaponAction_DirectAttack&gt; | &lt;tag=WeaponAction_DirectAttack&gt;的暴擊機率 | 100 = 1% |
| `WEAPON_RANGE` | Melee Attack Range | 近戰攻擊範圍 | 1 = 1% |
