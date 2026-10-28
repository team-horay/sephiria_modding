# ステータス一覧

コスチュームの `stats` とMODのコードで使うステータスIDです。値は `"ID/値"` 形式の整数で、単位列はゲーム画面に表示されるときの換算を示します。たとえば `100 = 1%` のステータスに `CRITICAL/-400` と書くと、-4% と表示されます。

内蔵コスチュームが実際に使っている例: `MOVE_SPEED/12`, `MAX_HP/-15`, `DEFENSE/10`, `EVASION/1200`, `CRITICAL/-400`, `ATTACK_SPEED/7`, `EXP_DROP/4`, `STARTING_MONEY/800`。

| ID | 英語名 | 名前 | 単位 |
|---|---|---|---|
| `ATTACK_DASH` |  |  | 100 = 1 |
| `ATTACK_SPEED` | Attack Speed | 攻撃速度 | 1 = 1% |
| `BASIC_ATTACK_DAMAGE` | Normal Attack Damage | 通常攻撃ダメージ | 1 = 1% |
| `BOSS_REWARD_DICE` | Acquire {VALUE} dice upon defeating a boss | ボスを倒した時、サイコロを{VALUE}個獲得 | 整数 |
| `BUFF_DURATION` | Duration of Buffs Applied by Self | 自己が付与したバフの持続時間 | 1 = 1% |
| `BURN_DAMAGE` | &lt;tag=Burn&gt; Damage | &lt;tag=Burn&gt;デバフのダメージ | 1 = 1% |
| `BURN_DURATION` | &lt;tag=Burn&gt; duration | &lt;tag=Burn&gt;デバフの持続時間 | 1 = 1% |
| `BURN_SPEED` | Attack Speed of &lt;tag=Burn&gt; | &lt;tag=Burn&gt;の攻撃速度 | 1 = 1% |
| `BURN_STACK` | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | &lt;tag=Burn&gt;&lt;tag=Stacks&gt; | 整数 |
| `CHARGING_CHARM_BONUS` | &lt;tag=FrostRelic&gt; Charge Speed | &lt;tag=FrostRelic&gt;チャージ速度 | 1 = 1% |
| `CHARM_DAMAGE_BONUS` | &lt;tag=Artifact&gt; Damage | &lt;tag=Artifact&gt;ダメージ | 1 = 1% |
| `COMBO_BONUS_DAMAGE` | &lt;tag=FinalDamage&gt; Bonus for each Activated Combo Type | アクティブなコンボタイプごとに&lt;tag=FinalDamage&gt;ボーナス | 1 = 1% |
| `COOLDOWN_RECOVERY_SPEED` |  |  | 整数 |
| `CRITICAL` | Critical Chance | クリティカル確率 | 100 = 1% |
| `CRITICAL_DAMAGE_RATE` | Critical Damage | クリティカルダメージ | 1 = 1% |
| `CROSSBOWAMMO` | Magazine Capacity | マガジン容量 | 整数 |
| `CROSSBOWFIRESPEED` | Crossbow Firing Speed | クロスボウ発射速度 | 1 = 1% |
| `CROSSBOW_ADDITIONAL_MAGAZINE` | Magazine Count | マガジン数 | 整数 |
| `CROSSBOW_RELOADSPEED` | Crossbow Reload Speed | クロスボウ再装填速度 | 整数 |
| `DARK_CLOUD_DAMAGE` | Extra &lt;tag=DarkCloud&gt; damage | &lt;tag=DarkCloud&gt;の追加ダメージ | 1 = 1% |
| `DARK_CLOUD_RESTORE_DURING_BATTLE` | &lt;tag=DarkCloud&gt; Recovery Rate during Combat | 戦闘中の&lt;tag=DarkCloud&gt;回復速度 | 1 = 1% |
| `DARK_CLOUD_SPEED` | Consumption Speed of &lt;tag=DarkCloud&gt; | &lt;tag=DarkCloud&gt;の消費速度 | 1 = 1% |
| `DASH_ATTACK_DAMAGE` | Dash Attack Damage | ダッシュ攻撃ダメージ | 1 = 1% |
| `DASH_ATTACK_IGNORE_DEFENSE` | Dash Attack Ignore Defense | ダッシュ攻撃防御貫通 | 1 = 1% |
| `DASH_COUNT` | Dash Count | ダッシュ可能回数 | 整数 |
| `DASH_DISABLE` | Dash Disabled | ダッシュ不可 | 整数 |
| `DASH_INVINCIBLE_TIME_BONUS` | Invincibility window bonus while dashing | ダッシュ中の無敵時間ボーナス | 1000 = 1s |
| `DASH_RECOVERY_SPEED` |  |  | 1 = 1% |
| `DASH_SPEED` | Dash speed | ダッシュ速度 | 1 = 1% |
| `DEBUFF_DAMAGE` | Damage dealt with &lt;tag=Debuff&gt; | &lt;tag=Debuff&gt;で与えるダメージ量 | 1 = 1% |
| `DEBUFF_DURATION` | Duration of all debuffs inflicted on enemies | 敵に付与するすべてのデバフの持続時間 | 1 = 1% |
| `DEFENSE` | Defense | 防御力 | 整数 |
| `DEFENSE_TO_ATTACK` | No damage reduction from &lt;tag=Defense&gt;, but increases damage dealt by increased &lt;tag=DamageReduction&gt; | &lt;tag=Defense&gt;によるダメージ軽減は適用されませんが、増加した&lt;tag=DamageReduction&gt;分だけ与えるダメージが増加 | 整数 |
| `DIMENSION_POCKET` | &lt;tag=TEXT:DimensionPocket&gt; Capacity | &lt;tag=TEXT:DimensionPocket&gt;容量 | 整数 |
| `DIRECT_ATTACK_BURN` | {VALUE} chance to inflict &lt;tag=Burn&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | &lt;tag=WeaponAction_DirectAttack&gt;時、{VALUE}の確率で&lt;tag=Burn&gt;デバフを付与 | 1 = 1% |
| `DIRECT_ATTACK_ELECTRIC` | {VALUE} chance to inflict &lt;tag=Electric&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt;; | &lt;tag=WeaponAction_DirectAttack&gt;時、{VALUE}の確率で&lt;tag=Electric&gt;デバフを付与 | 1 = 1% |
| `DIRECT_ATTACK_FROSTBITE` | {VALUE} chance to inflict &lt;tag=Frostbite&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | &lt;tag=WeaponAction_DirectAttack&gt;時、{VALUE}の確率で&lt;tag=Frostbite&gt;デバフを付与 | 1 = 1% |
| `DISABLE_DUAL_ARTIFACT` | &lt;tag=TEXT:BondArtifact&gt; no longer appears | もう&lt;tag=TEXT:BondArtifact&gt;が出現しない | 整数 |
| `ELECTRIC_DAMAGE` | &lt;tag=Electric&gt; extra damage | &lt;tag=Electric&gt;デバフ追加ダメージ量 | 1 = 1% |
| `ELECTRIC_STACK` | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | &lt;tag=Electric&gt;&lt;tag=Stacks&gt; | 整数 |
| `ELITE_DAMAGE` | Bonus damage dealt to &lt;tag=EnemyType_Boss&gt; and &lt;tag=EnemyType_Miniboss&gt; | &lt;tag=EnemyType_Boss&gt;と&lt;tag=EnemyType_Miniboss&gt;に与える追加ダメージ量 | 1 = 1% |
| `EVASION` | Evasion | 回避 | 100 = 1 |
| `EVASION_DISABLE` | &lt;tag=Evasion&gt; Disabled | &lt;tag=Evasion&gt;不可 | 1 = 1% |
| `EVASION_RATE_REDUCTION` | Evasion Chance Reduction | 回避率減少 | 1 = 1% |
| `EXP_DROP` | Experience Drop | 経験値ドロップ | 1 = 1% |
| `EXTRA_ITEM_CHOICES` | Additional Item Choices | 追加アイテムの選択肢 | 整数 |
| `EXTRA_LIFE` | Revive with 60% HP upon death ({VALUE} times) | 倒れた時、最大HPの60%で復活（{VALUE}回） | 整数 |
| `EXTRA_MIRACLE_CHOICES` | Additional Miracle Choices | 追加の奇跡の選択肢 | 整数 |
| `EXTRA_WEAPON_CHOICES` | Additional Weapon Choices | 追加武器の選択肢 | 整数 |
| `FINAL_DAMAGE` |  |  | 1 = 1% |
| `FINAL_HP` | Final HP | 最終HP | 1 = 1% |
| `FINAL_MP` | Final MP | 最終MP | 1 = 1% |
| `FINAL_WEAPONDAMAGE` | &lt;tag=Weapon&gt; Damage | &lt;tag=Weapon&gt;ダメージ | 1 = 1% |
| `FIRE_DAMAGE` | Fire Damage | 火属性ダメージ | 整数 |
| `FIRE_DAMAGE_AMP` | &lt;tag=FireDamage&gt; Amplification | &lt;tag=FireDamage&gt;増幅 | 1 = 1% |
| `FIXED_DASH` | Dash count fixed at {VALUE} | ダッシュ可能回数が{VALUE}回に固定 | 整数 |
| `FLAME_SWORD_ADDITIONAL_ATTACK` | Additionally activates &lt;tag=FlameSword&gt; {VALUE} times | &lt;tag=FlameSword&gt;が{VALUE}回追加発動 | 整数 |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_MAGIC` | When dealing &lt;tag=Magic:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | &lt;tag=Magic:noicon&gt;ダメージを与えた時、&lt;tag=FlameSword&gt;が{VALUE}回追加発動 | 整数 |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_WEAPON` | When dealing &lt;tag=Weapon:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | &lt;tag=Weapon:noicon&gt;ダメージを与えた時、&lt;tag=FlameSword&gt;が{VALUE}回追加発動 | 整数 |
| `FLAME_SWORD_COOLDOWN` | &lt;tag=FlameSword&gt; Cooldown | &lt;tag=FlameSword&gt;再使用待機時間 | 1 = 1% |
| `FLAME_SWORD_CRITICAL` | Critical Chance of &lt;tag=FlameSword&gt; | &lt;tag=FlameSword&gt;クリティカル確率 | 1 = 1% |
| `FLAME_SWORD_DAMAGE` | &lt;tag=FlameSword&gt; Damage | &lt;tag=FlameSword&gt;ダメージ | 1 = 1% |
| `FLAME_SWORD_IGNORE_DEFENSE` | Ignore target's defense by {VALUE} with &lt;tag=FlameSword&gt; | &lt;tag=FlameSword&gt;によって対象の防御力を{VALUE}無視 | 1 = 1% |
| `FLAME_SWORD_LUCK` | &lt;tag=FlameSword&gt; has a {VALUE} chance to grow in size and increase damage by &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% when thrown | &lt;tag=FlameSword&gt;投げると{VALUE}の確率でサイズが大きくなり、ダメージが&lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;%増加 | 1 = 1% |
| `FLAME_SWORD_MAX` | &lt;tag=FlameSword&gt; Maximum Count | &lt;tag=FlameSword&gt;最大数 | 整数 |
| `FOLLOWER_ATTACK_SPEED` | Companion Attack Speed | 仲間の攻撃速度 | 1 = 1% |
| `FOLLOWER_CRITICAL` | Companion Critical Chance | 仲間が攻撃する時のクリティカル確率 | 100 = 1% |
| `FOLLOWER_DAMAGE` | Companion Damage | 仲間が与えるダメージ | 1 = 1% |
| `FOLLOWER_DEFENSE` | Companion Defense | 仲間の防御力 | 整数 |
| `FOLLOWER_MAX_HP` | Companion Max HP | 仲間の最大HP | 1 = 1% |
| `FOLLOWER_REVIVE` | Companion Revive Time Haste | 仲間復活時間加速 | 1 = 1% |
| `FREEZE_DAMAGE` | &lt;tag=Freeze&gt; Debuff Damage | &lt;tag=Freeze&gt;デバフダメージ | 1 = 1% |
| `FROSTBITE_DAMAGE` | Targets affected by &lt;tag=Frostbite&gt; take damage per second equal to {VALUE}% of &lt;tag=IceDamage&gt; (damage increases with &lt;tag=Stacks&gt;) | &lt;tag=Frostbite&gt;状態の対象は1秒ごとに&lt;tag=IceDamage&gt;の{VALUE}%分のダメージを受ける（&lt;tag=Stacks&gt;に応じてダメージが増加） | 整数 |
| `FROST_RELIC_CRITICAL_DAMAGE` | &lt;tag=FrostRelic&gt; critical damage | &lt;tag=FrostRelic&gt;のクリティカルダメージ | 1 = 1% |
| `FROST_RELIC_DAMAGE` | &lt;tag=FrostRelic&gt; damage | &lt;tag=FrostRelic&gt;のダメージ | 1 = 1% |
| `FRUIT_COUNT` | Fruit Skewer Max | フルーツ串上限 | 整数 |
| `FURY_INVINCIBLE_TIME` | Invincibility Duration During &lt;tag=WeaponAction_Fury&gt; | &lt;tag=WeaponAction_Fury&gt;中の無敵時間 | 10 = 1s |
| `HEALING_PENALTY` | Healing Curse: Healing received decreases by {VALUE} | 治癒の呪い：受ける回復量が{VALUE}減少 | 1 = 1% |
| `HIGHEST_ELEMENTAL_DAMAGE` | Highest Elemental Damage | 最も高い属性ダメージ | 整数 |
| `HP_CURSE` | Max HP set to {VALUE} | 最大HPが{VALUE}に固定される | 整数 |
| `HP_POTION_BONUS` | HP Potion Bonus | HPポーションの効果 | 100 = 1% |
| `HP_STEAL` | Life Steal | HP吸収 | 整数 |
| `ICE_DAMAGE` | Cold Damage | 氷属性ダメージ | 整数 |
| `ICE_DAMAGE_AMP` | &lt;tag=IceDamage&gt; Amplification | &lt;tag=IceDamage&gt;増幅 | 1 = 1% |
| `IGNORE_DEFENSE` | Ignore Defense | 防御貫通 | 1 = 1% |
| `IGNORE_SUPER_ARMOR` | Extra damage to target’s &lt;tag=SuperArmor&gt; | 対象の&lt;tag=SuperArmor&gt;への追加ダメージ | 1 = 1% |
| `INFINITY_DASH` | Infinity Dash | インフィニティダッシュ | 整数 |
| `INFINITY_MP` | Magic Reflow: &lt;tag=MaxMP&gt; is fixed at &lt;tag=CONST:infinityMPMax&gt;, and MP does not decrease | 魔力還流：&lt;tag=MaxMP&gt;が&lt;tag=CONST:infinityMPMax&gt;に固定され、減少しない | 整数 |
| `INVENTORY_SLOT` | Inventory Slot | バッグの枠 | 整数 |
| `LEAF_DROP` | Leaf Drop | リーフドロップ | 1 = 1% |
| `LEVEL_UP_HEAL` | HP Restored on Level Up | レベルアップ時HP回復 | 1 = 1% |
| `LIGHTNING_DAMAGE` | Lightning Damage | 雷属性ダメージ | 整数 |
| `LIGHTNING_DAMAGE_AMP` | &lt;tag=LightningDamage&gt; Amplification | &lt;tag=LightningDamage&gt;増幅 | 1 = 1% |
| `LUCK` | Luck | 幸運 | 整数 |
| `MAGIC_COST_REDUCE` | Reduce MP Cost of &lt;tag=Magic:noicon&gt; | &lt;tag=Magic:noicon&gt;の消費MP減少 | 1 = 1% |
| `MAGIC_CRITICAL` | &lt;tag=Magic&gt; Critical Chance | &lt;tag=Magic&gt;のクリティカル率 | 100 = 1% |
| `MAGIC_CRITICAL_DAMAGE_RATE` | &lt;tag=Magic&gt; Critical Damage | &lt;tag=Magic&gt;のクリティカルダメージ | 1 = 1% |
| `MAGIC_DAMAGE_BONUS` | &lt;tag=Magic_NoIcon&gt; Damage | &lt;tag=Magic_NoIcon&gt;ダメージ | 1 = 1% |
| `MAGIC_IGNORE_DEFENSE` | &lt;tag=Magic_NoIcon&gt; Ignore Defense | &lt;tag=Magic_NoIcon&gt;防御貫通 | 1 = 1% |
| `MAX_HP` | Max HP | 最大HP | 整数 |
| `MAX_MP` | Max MP | 最大MP | 整数 |
| `MIN_DARK_CLOUD` | &lt;tag=DarkCloud&gt; Capacity | &lt;tag=DarkCloud&gt;容量 | 整数 |
| `MIN_DARK_CLOUD_AMP` | &lt;tag=DarkCloud&gt; Capacity | &lt;tag=DarkCloud&gt;容量 | 1 = 1% |
| `MOVE_SPEED` | Move Speed | 移動速度 | 1 = 1% |
| `MP_REGEN` | MP Regeneration | MP再生 | 整数 |
| `MP_REGEN_KILL` | Recover &lt;tag=MP&gt; {VALUE} on Kill | 敵を倒すと&lt;tag=MP&gt;{VALUE}回復 | 整数 |
| `MP_REGEN_MULTIPLE` | Efficiency When Recovering &lt;tag=MP&gt; | &lt;tag=MP&gt;回復時の効率 | 1 = 1% |
| `MP_SKILL_DAMAGE` | Damage of abilities consuming MP | MPを消費する能力のダメージ | 1 = 1% |
| `MP_STEAL` | MP Steal | MP吸収 | 整数 |
| `NEGOTIATION` | Barter | 交渉力 | 整数 |
| `NO_DASH_INVINCIBLE` | Disable Dash Invincibility | ダッシュ無敵が無効化 | 整数 |
| `NO_MAGIC_COST` | Grimoires do not consume MP | 魔法書がMPを消費しない | 整数 |
| `PARTY_BUFF` | Grants buffs to players within 5 &lt;tag=Tiles&gt; when casting buffs. However, the spell cost is increased by 50%. | バフ魔法を発動すると、周りの5&lt;tag=Tiles&gt;内のプレイヤーにもバフを付与。ただし、バフ魔法の使用コストが50％増加 | 整数 |
| `PHYSICAL_DAMAGE` | Physical Damage | 物理ダメージ | 整数 |
| `PHYSICAL_DAMAGE_AMP` | &lt;tag=PhysicalDamage&gt; Amplification | &lt;tag=PhysicalDamage&gt;増幅 | 1 = 1% |
| `PLANET_ATTACK_SPEED` | Planet Attack Speed | 惑星攻撃速度 | 1 = 1% |
| `PLANET_DAMAGE` | Planet Damage | 惑星ダメージ | 1 = 1% |
| `POTION_SLOT` | Potion Bag Slot | ポーションバッグ枠 | 整数 |
| `RECEIVED_DAMAGE_REDUCTION` | Damage Taken Reduction | 被ダメージ減少 | 1 = 1% |
| `REVIVE_PLAYER_HASTE` | Reduces the time to revive another player by {VALUE} | 他のプレイヤーを復活させる時の時間を{VALUE}短縮 | 1 = 1% |
| `SAPPHIREBONUS` | Sapphire Acquisition Bonus | サファイア獲得ボーナス | 1 = 1% |
| `SPECIAL_ATTACK_DAMAGE` | Special Attack Damage | 特殊攻撃ダメージ | 1 = 1% |
| `STARTING_MONEY` | Start with {VALUE} &lt;tag=Leaf:nocolor&gt; | 探索開始時{VALUE}&lt;tag=Leaf:nocolor&gt; | 整数 |
| `SUB_BAG_SLOT` | Side Bag Slot | サブバッグ枠 | 整数 |
| `THORNS` | Thorns | 棘 | 1 = 1% |
| `TOUGHNESS` | Toughness | 強靭さ | 整数 |
| `TRUE_DAMAGE` | True Damage | 固定ダメージ | 整数 |
| `UNLIMITED_COMBO` | For each excess Combo, &lt;tag=FinalDamage&gt; | コンボ数が最大値を超えると、超過した数に応じて&lt;tag=FinalDamage&gt; | 1 = 1% |
| `WEAPON_CRITICAL` | Critical Hit Chance of &lt;tag=WeaponAction_DirectAttack&gt; | &lt;tag=WeaponAction_DirectAttack&gt;のクリティカルヒット率 | 100 = 1% |
| `WEAPON_RANGE` | Melee Attack Range | 近接攻撃範囲 | 1 = 1% |
