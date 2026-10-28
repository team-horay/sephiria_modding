# 属性列表

服装 `stats` 和模组代码中使用的属性 ID。值为 `"ID/值"` 格式的整数，“单位”列表示它们在游戏内显示时的换算方式。例如，对于 `100 = 1%` 的属性，写 `CRITICAL/-400` 会显示为 -4%。

内置服装实际使用的示例：`MOVE_SPEED/12`、`MAX_HP/-15`、`DEFENSE/10`、`EVASION/1200`、`CRITICAL/-400`、`ATTACK_SPEED/7`、`EXP_DROP/4`、`STARTING_MONEY/800`。

| ID | 英文名 | 名称 | 单位 |
|---|---|---|---|
| `ATTACK_DASH` |  |  | 100 = 1 |
| `ATTACK_SPEED` | Attack Speed | 攻击速度 | 1 = 1% |
| `BASIC_ATTACK_DAMAGE` | Normal Attack Damage | 普通攻击伤害 | 1 = 1% |
| `BOSS_REWARD_DICE` | Acquire {VALUE} dice upon defeating a boss | 击败Boss时获得{VALUE}个骰子 | 整数 |
| `BUFF_DURATION` | Duration of Buffs Applied by Self | 自身施加的增益持续时间 | 1 = 1% |
| `BURN_DAMAGE` | &lt;tag=Burn&gt; Damage | &lt;tag=Burn&gt;异常状态额外伤害 | 1 = 1% |
| `BURN_DURATION` | &lt;tag=Burn&gt; duration | &lt;tag=Burn&gt;负面状态持续时间 | 1 = 1% |
| `BURN_SPEED` | Attack Speed of &lt;tag=Burn&gt; | &lt;tag=Burn&gt;的攻击速度 | 1 = 1% |
| `BURN_STACK` | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | &lt;tag=Burn&gt;&lt;tag=Stacks&gt; | 整数 |
| `CHARGING_CHARM_BONUS` | &lt;tag=FrostRelic&gt; Charge Speed | &lt;tag=FrostRelic&gt;充能速度 | 1 = 1% |
| `CHARM_DAMAGE_BONUS` | &lt;tag=Artifact&gt; Damage | &lt;tag=Artifact&gt;伤害 | 1 = 1% |
| `COMBO_BONUS_DAMAGE` | &lt;tag=FinalDamage&gt; Bonus for each Activated Combo Type | 每种激活的连击类型获得&lt;tag=FinalDamage&gt;奖励 | 1 = 1% |
| `COOLDOWN_RECOVERY_SPEED` |  |  | 整数 |
| `CRITICAL` | Critical Chance | 暴击几率 | 100 = 1% |
| `CRITICAL_DAMAGE_RATE` | Critical Damage | 暴击伤害 | 1 = 1% |
| `CROSSBOWAMMO` | Magazine Capacity | 弹匣容量 | 整数 |
| `CROSSBOWFIRESPEED` | Crossbow Firing Speed | 弩发射速度 | 1 = 1% |
| `CROSSBOW_ADDITIONAL_MAGAZINE` | Magazine Count | 弹匣数量 | 整数 |
| `CROSSBOW_RELOADSPEED` | Crossbow Reload Speed | 弩的重装速度 | 整数 |
| `DARK_CLOUD_DAMAGE` | Extra &lt;tag=DarkCloud&gt; damage | &lt;tag=DarkCloud&gt;的额外伤害 | 1 = 1% |
| `DARK_CLOUD_RESTORE_DURING_BATTLE` | &lt;tag=DarkCloud&gt; Recovery Rate during Combat | 战斗中&lt;tag=DarkCloud&gt;恢复速度 | 1 = 1% |
| `DARK_CLOUD_SPEED` | Consumption Speed of &lt;tag=DarkCloud&gt; | &lt;tag=DarkCloud&gt;的消耗速度 | 1 = 1% |
| `DASH_ATTACK_DAMAGE` | Dash Attack Damage | 冲刺攻击伤害 | 1 = 1% |
| `DASH_ATTACK_IGNORE_DEFENSE` | Dash Attack Ignore Defense | 冲刺攻击无视防御 | 1 = 1% |
| `DASH_COUNT` | Dash Count | 冲刺次数 | 整数 |
| `DASH_DISABLE` | Dash Disabled | 无法冲刺 | 整数 |
| `DASH_INVINCIBLE_TIME_BONUS` | Invincibility window bonus while dashing | 冲刺中无敌时间奖励 | 1000 = 1s |
| `DASH_RECOVERY_SPEED` |  |  | 1 = 1% |
| `DASH_SPEED` | Dash speed | 冲刺速度 | 1 = 1% |
| `DEBUFF_DAMAGE` | Damage dealt with &lt;tag=Debuff&gt; | 使用&lt;tag=Debuff&gt;造成的伤害量 | 1 = 1% |
| `DEBUFF_DURATION` | Duration of all debuffs inflicted on enemies | 施加给敌人的所有减益效果的持续时间 | 1 = 1% |
| `DEFENSE` | Defense | 防御力 | 整数 |
| `DEFENSE_TO_ATTACK` | No damage reduction from &lt;tag=Defense&gt;, but increases damage dealt by increased &lt;tag=DamageReduction&gt; | &lt;tag=Defense&gt;不适用伤害减免，但提高与增加&lt;tag=DamageReduction&gt;相同的伤害 | 整数 |
| `DIMENSION_POCKET` | &lt;tag=TEXT:DimensionPocket&gt; Capacity | &lt;tag=TEXT:DimensionPocket&gt;容量 | 整数 |
| `DIRECT_ATTACK_BURN` | {VALUE} chance to inflict &lt;tag=Burn&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | &lt;tag=WeaponAction_DirectAttack&gt;时，有{VALUE}的概率施加&lt;tag=Burn&gt;减益效果 | 1 = 1% |
| `DIRECT_ATTACK_ELECTRIC` | {VALUE} chance to inflict &lt;tag=Electric&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt;; | &lt;tag=WeaponAction_DirectAttack&gt;时，有{VALUE}的概率施加&lt;tag=Electric&gt;减益效果 | 1 = 1% |
| `DIRECT_ATTACK_FROSTBITE` | {VALUE} chance to inflict &lt;tag=Frostbite&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | &lt;tag=WeaponAction_DirectAttack&gt;时，有{VALUE}的概率施加&lt;tag=Frostbite&gt;减益效果 | 1 = 1% |
| `DISABLE_DUAL_ARTIFACT` | &lt;tag=TEXT:BondArtifact&gt; no longer appears | &lt;tag=TEXT:BondArtifact&gt;不再出现 | 整数 |
| `ELECTRIC_DAMAGE` | &lt;tag=Electric&gt; extra damage | &lt;tag=Electric&gt;伤害量 | 1 = 1% |
| `ELECTRIC_STACK` | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | &lt;tag=Electric&gt;&lt;tag=Stacks&gt; | 整数 |
| `ELITE_DAMAGE` | Bonus damage dealt to &lt;tag=EnemyType_Boss&gt; and &lt;tag=EnemyType_Miniboss&gt; | 对&lt;tag=EnemyType_Boss&gt;与&lt;tag=EnemyType_Miniboss&gt;造成的额外伤害量 | 1 = 1% |
| `EVASION` | Evasion | 闪避 | 100 = 1 |
| `EVASION_DISABLE` | &lt;tag=Evasion&gt; Disabled | &lt;tag=Evasion&gt;不可 | 1 = 1% |
| `EVASION_RATE_REDUCTION` | Evasion Chance Reduction | 闪避几率减少 | 1 = 1% |
| `EXP_DROP` | Experience Drop | 经验值掉落 | 1 = 1% |
| `EXTRA_ITEM_CHOICES` | Additional Item Choices | 额外物品选择 | 整数 |
| `EXTRA_LIFE` | Revive with 60% HP upon death ({VALUE} times) | 死亡时以60%生命值复活（{VALUE}次） | 整数 |
| `EXTRA_MIRACLE_CHOICES` | Additional Miracle Choices | 额外奇迹选择 | 整数 |
| `EXTRA_WEAPON_CHOICES` | Additional Weapon Choices | 额外武器选择 | 整数 |
| `FINAL_DAMAGE` |  |  | 1 = 1% |
| `FINAL_HP` | Final HP | 最终HP | 1 = 1% |
| `FINAL_MP` | Final MP | 最终MP | 1 = 1% |
| `FINAL_WEAPONDAMAGE` | &lt;tag=Weapon&gt; Damage | &lt;tag=Weapon&gt;伤害 | 1 = 1% |
| `FIRE_DAMAGE` | Fire Damage | 火焰属性伤害 | 整数 |
| `FIRE_DAMAGE_AMP` | &lt;tag=FireDamage&gt; Amplification | &lt;tag=FireDamage&gt;增幅 | 1 = 1% |
| `FIXED_DASH` | Dash count fixed at {VALUE} | 冲刺次数固定为{VALUE}次 | 整数 |
| `FLAME_SWORD_ADDITIONAL_ATTACK` | Additionally activates &lt;tag=FlameSword&gt; {VALUE} times | &lt;tag=FlameSword&gt;额外触发{VALUE}次 | 整数 |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_MAGIC` | When dealing &lt;tag=Magic:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | 造成&lt;tag=Magic:noicon&gt;伤害时，&lt;tag=FlameSword&gt;额外触发{VALUE}次 | 整数 |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_WEAPON` | When dealing &lt;tag=Weapon:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | 造成&lt;tag=Weapon:noicon&gt;伤害时，&lt;tag=FlameSword&gt;额外触发{VALUE}次 | 整数 |
| `FLAME_SWORD_COOLDOWN` | &lt;tag=FlameSword&gt; Cooldown | &lt;tag=FlameSword&gt;冷却时间 | 1 = 1% |
| `FLAME_SWORD_CRITICAL` | Critical Chance of &lt;tag=FlameSword&gt; | &lt;tag=FlameSword&gt;暴击几率 | 1 = 1% |
| `FLAME_SWORD_DAMAGE` | &lt;tag=FlameSword&gt; Damage | &lt;tag=FlameSword&gt;伤害 | 1 = 1% |
| `FLAME_SWORD_IGNORE_DEFENSE` | Ignore target's defense by {VALUE} with &lt;tag=FlameSword&gt; | &lt;tag=FlameSword&gt;无视目标防御力{VALUE} | 1 = 1% |
| `FLAME_SWORD_LUCK` | &lt;tag=FlameSword&gt; has a {VALUE} chance to grow in size and increase damage by &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% when thrown | &lt;tag=FlameSword&gt;投掷时有{VALUE}概率增大并增加&lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;%伤害 | 1 = 1% |
| `FLAME_SWORD_MAX` | &lt;tag=FlameSword&gt; Maximum Count | &lt;tag=FlameSword&gt;数量上限 | 整数 |
| `FOLLOWER_ATTACK_SPEED` | Companion Attack Speed | 同伴攻击速度 | 1 = 1% |
| `FOLLOWER_CRITICAL` | Companion Critical Chance | 同伴攻击时的暴击几率 | 100 = 1% |
| `FOLLOWER_DAMAGE` | Companion Damage | 同伴造成的伤害 | 1 = 1% |
| `FOLLOWER_DEFENSE` | Companion Defense | 同伴的防御力 | 整数 |
| `FOLLOWER_MAX_HP` | Companion Max HP | 同伴的最大HP | 1 = 1% |
| `FOLLOWER_REVIVE` | Companion Revive Time Haste | 同伴复活加速 | 1 = 1% |
| `FREEZE_DAMAGE` | &lt;tag=Freeze&gt; Debuff Damage | &lt;tag=Freeze&gt;减益效果伤害 | 1 = 1% |
| `FROSTBITE_DAMAGE` | Targets affected by &lt;tag=Frostbite&gt; take damage per second equal to {VALUE}% of &lt;tag=IceDamage&gt; (damage increases with &lt;tag=Stacks&gt;) | 受到&lt;tag=Frostbite&gt;影响的目标每秒受到&lt;tag=IceDamage&gt;等于{VALUE}%的伤害（随着&lt;tag=Stacks&gt;数增加伤害） | 整数 |
| `FROST_RELIC_CRITICAL_DAMAGE` | &lt;tag=FrostRelic&gt; critical damage | &lt;tag=FrostRelic&gt;的暴击伤害 | 1 = 1% |
| `FROST_RELIC_DAMAGE` | &lt;tag=FrostRelic&gt; damage | &lt;tag=FrostRelic&gt;的伤害 | 1 = 1% |
| `FRUIT_COUNT` | Fruit Skewer Max | 水果串上限 | 整数 |
| `FURY_INVINCIBLE_TIME` | Invincibility Duration During &lt;tag=WeaponAction_Fury&gt; | &lt;tag=WeaponAction_Fury&gt;期间无敌时间 | 10 = 1s |
| `HEALING_PENALTY` | Healing Curse: Healing received decreases by {VALUE} | 治愈诅咒：受到的回复量减少{VALUE} | 1 = 1% |
| `HIGHEST_ELEMENTAL_DAMAGE` | Highest Elemental Damage | 最高元素伤害 | 整数 |
| `HP_CURSE` | Max HP set to {VALUE} | 最大HP固定为{VALUE} | 整数 |
| `HP_POTION_BONUS` | HP Potion Bonus | HP药水效果 | 100 = 1% |
| `HP_STEAL` | Life Steal | HP偷取 | 整数 |
| `ICE_DAMAGE` | Cold Damage | 冰属性伤害 | 整数 |
| `ICE_DAMAGE_AMP` | &lt;tag=IceDamage&gt; Amplification | &lt;tag=IceDamage&gt;增幅 | 1 = 1% |
| `IGNORE_DEFENSE` | Ignore Defense | 无视防御 | 1 = 1% |
| `IGNORE_SUPER_ARMOR` | Extra damage to target’s &lt;tag=SuperArmor&gt; | 对目标的&lt;tag=SuperArmor&gt;造成额外伤害 | 1 = 1% |
| `INFINITY_DASH` | Infinity Dash | 无限冲刺 | 整数 |
| `INFINITY_MP` | Magic Reflow: &lt;tag=MaxMP&gt; is fixed at &lt;tag=CONST:infinityMPMax&gt;, and MP does not decrease | 魔力回流：&lt;tag=MaxMP&gt;固定为&lt;tag=CONST:infinityMPMax&gt;，且不减少 | 整数 |
| `INVENTORY_SLOT` | Inventory Slot | 背包空间 | 整数 |
| `LEAF_DROP` | Leaf Drop | 叶子掉落 | 1 = 1% |
| `LEVEL_UP_HEAL` | HP Restored on Level Up | 升级时HP恢复 | 1 = 1% |
| `LIGHTNING_DAMAGE` | Lightning Damage | 闪电属性伤害 | 整数 |
| `LIGHTNING_DAMAGE_AMP` | &lt;tag=LightningDamage&gt; Amplification | &lt;tag=LightningDamage&gt;增幅 | 1 = 1% |
| `LUCK` | Luck | 幸运值 | 整数 |
| `MAGIC_COST_REDUCE` | Reduce MP Cost of &lt;tag=Magic:noicon&gt; | &lt;tag=Magic:noicon&gt;的MP消耗减少 | 1 = 1% |
| `MAGIC_CRITICAL` | &lt;tag=Magic&gt; Critical Chance | &lt;tag=Magic&gt;的暴击率 | 100 = 1% |
| `MAGIC_CRITICAL_DAMAGE_RATE` | &lt;tag=Magic&gt; Critical Damage | &lt;tag=Magic&gt;的暴击伤害 | 1 = 1% |
| `MAGIC_DAMAGE_BONUS` | &lt;tag=Magic_NoIcon&gt; Damage | &lt;tag=Magic_NoIcon&gt;伤害 | 1 = 1% |
| `MAGIC_IGNORE_DEFENSE` | &lt;tag=Magic_NoIcon&gt; Ignore Defense | &lt;tag=Magic_NoIcon&gt;无视防御 | 1 = 1% |
| `MAX_HP` | Max HP | 最大HP | 整数 |
| `MAX_MP` | Max MP | 最大MP | 整数 |
| `MIN_DARK_CLOUD` | &lt;tag=DarkCloud&gt; Capacity | &lt;tag=DarkCloud&gt;容量 | 整数 |
| `MIN_DARK_CLOUD_AMP` | &lt;tag=DarkCloud&gt; Capacity | &lt;tag=DarkCloud&gt;容量 | 1 = 1% |
| `MOVE_SPEED` | Move Speed | 移动速度 | 1 = 1% |
| `MP_REGEN` | MP Regeneration | MP再生 | 整数 |
| `MP_REGEN_KILL` | Recover &lt;tag=MP&gt; {VALUE} on Kill | 击杀敌人时恢复&lt;tag=MP&gt;{VALUE} | 整数 |
| `MP_REGEN_MULTIPLE` | Efficiency When Recovering &lt;tag=MP&gt; | &lt;tag=MP&gt;恢复时的效率 | 1 = 1% |
| `MP_SKILL_DAMAGE` | Damage of abilities consuming MP | 消耗MP的能力伤害 | 1 = 1% |
| `MP_STEAL` | MP Steal | MP吸收 | 整数 |
| `NEGOTIATION` | Barter | 谈判力 | 整数 |
| `NO_DASH_INVINCIBLE` | Disable Dash Invincibility | 禁用冲刺无敌 | 整数 |
| `NO_MAGIC_COST` | Grimoires do not consume MP | 魔法书不消耗MP | 整数 |
| `PARTY_BUFF` | Grants buffs to players within 5 &lt;tag=Tiles&gt; when casting buffs. However, the spell cost is increased by 50%. | 施放增益魔法时，为周围5&lt;tag=Tiles&gt;内的玩家施加增益效果。但是，施放增益魔法的消耗增加50%。 | 整数 |
| `PHYSICAL_DAMAGE` | Physical Damage | 物理伤害 | 整数 |
| `PHYSICAL_DAMAGE_AMP` | &lt;tag=PhysicalDamage&gt; Amplification | &lt;tag=PhysicalDamage&gt;增幅 | 1 = 1% |
| `PLANET_ATTACK_SPEED` | Planet Attack Speed | 行星攻击速度 | 1 = 1% |
| `PLANET_DAMAGE` | Planet Damage | 行星伤害 | 1 = 1% |
| `POTION_SLOT` | Potion Bag Slot | 药水袋栏位 | 整数 |
| `RECEIVED_DAMAGE_REDUCTION` | Damage Taken Reduction | 受到伤害减少 | 1 = 1% |
| `REVIVE_PLAYER_HASTE` | Reduces the time to revive another player by {VALUE} | 复活其他玩家所需时间缩短{VALUE} | 1 = 1% |
| `SAPPHIREBONUS` | Sapphire Acquisition Bonus | 获得蓝宝石奖励 | 1 = 1% |
| `SPECIAL_ATTACK_DAMAGE` | Special Attack Damage | 特殊攻击伤害 | 1 = 1% |
| `STARTING_MONEY` | Start with {VALUE} &lt;tag=Leaf:nocolor&gt; | 探索开始时获得{VALUE}&lt;tag=Leaf:nocolor&gt; | 整数 |
| `SUB_BAG_SLOT` | Side Bag Slot | 副背包栏位 | 整数 |
| `THORNS` | Thorns | 荆棘 | 1 = 1% |
| `TOUGHNESS` | Toughness | 坚韧 | 整数 |
| `TRUE_DAMAGE` | True Damage | 无视防御伤害 | 整数 |
| `UNLIMITED_COMBO` | For each excess Combo, &lt;tag=FinalDamage&gt; | 连击数值超过最大值时，每超过1点&lt;tag=FinalDamage&gt; | 1 = 1% |
| `WEAPON_CRITICAL` | Critical Hit Chance of &lt;tag=WeaponAction_DirectAttack&gt; | &lt;tag=WeaponAction_DirectAttack&gt;的暴击几率 | 100 = 1% |
| `WEAPON_RANGE` | Melee Attack Range | 近战攻击范围 | 1 = 1% |
