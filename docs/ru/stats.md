# Список характеристик

ID характеристик, используемые в `stats` костюма и в коде модов. Значения являются целыми числами в формате `"ID/значение"`, а столбец «Единица» показывает, как они преобразуются при отображении в игре. Например, запись `CRITICAL/-400` для характеристики, у которой `100 = 1%`, отображается как -4%.

Примеры, реально используемые встроенными костюмами: `MOVE_SPEED/12`, `MAX_HP/-15`, `DEFENSE/10`, `EVASION/1200`, `CRITICAL/-400`, `ATTACK_SPEED/7`, `EXP_DROP/4`, `STARTING_MONEY/800`.

| ID | Английское название | Название | Единица |
|---|---|---|---|
| `ATTACK_DASH` |  |  | 100 = 1 |
| `ATTACK_SPEED` | Attack Speed | Скорость атаки | 1 = 1% |
| `BASIC_ATTACK_DAMAGE` | Normal Attack Damage | Урон от обычной атаки | 1 = 1% |
| `BOSS_REWARD_DICE` | Acquire {VALUE} dice upon defeating a boss | За победу над боссом вы получаете кубики ×{VALUE} | Целое число |
| `BUFF_DURATION` | Duration of Buffs Applied by Self | Длительность собственных баффов | 1 = 1% |
| `BURN_DAMAGE` | &lt;tag=Burn&gt; Damage | Урон от эффекта «&lt;tag=Burn&gt;» | 1 = 1% |
| `BURN_DURATION` | &lt;tag=Burn&gt; duration | Длительность эффекта «&lt;tag=Burn&gt;» | 1 = 1% |
| `BURN_SPEED` | Attack Speed of &lt;tag=Burn&gt; | Скорость атаки эффекта «&lt;tag=Burn&gt;» | 1 = 1% |
| `BURN_STACK` | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | &lt;tag=Stack&gt;и эффекта «&lt;tag=Burn&gt;» | Целое число |
| `CHARGING_CHARM_BONUS` | &lt;tag=FrostRelic&gt; Charge Speed | Скорость зарядки предметов «&lt;tag=FrostRelic&gt;» | 1 = 1% |
| `CHARM_DAMAGE_BONUS` | &lt;tag=Artifact&gt; Damage | Урон &lt;tag=Artifact&gt; | 1 = 1% |
| `COMBO_BONUS_DAMAGE` | &lt;tag=FinalDamage&gt; Bonus for each Activated Combo Type | Бонусный &lt;tag=FinalDamage&gt; за каждый активированный тип комбо | 1 = 1% |
| `COOLDOWN_RECOVERY_SPEED` |  |  | Целое число |
| `CRITICAL` | Critical Chance | Шанс критического удара | 100 = 1% |
| `CRITICAL_DAMAGE_RATE` | Critical Damage | Критический урон | 1 = 1% |
| `CROSSBOWAMMO` | Magazine Capacity | Ёмкость магазина | Целое число |
| `CROSSBOWFIRESPEED` | Crossbow Firing Speed | Скорострельность арбалета | 1 = 1% |
| `CROSSBOW_ADDITIONAL_MAGAZINE` | Magazine Count | Количество магазинов | Целое число |
| `CROSSBOW_RELOADSPEED` | Crossbow Reload Speed | Скорость перезарядки арбалета | Целое число |
| `DARK_CLOUD_DAMAGE` | Extra &lt;tag=DarkCloud&gt; damage | &lt;tag=DarkCloud&gt;: дополнительный урон | 1 = 1% |
| `DARK_CLOUD_RESTORE_DURING_BATTLE` | &lt;tag=DarkCloud&gt; Recovery Rate during Combat | &lt;tag=DarkCloud&gt;: скорость восстановления в бою | 1 = 1% |
| `DARK_CLOUD_SPEED` | Consumption Speed of &lt;tag=DarkCloud&gt; | &lt;tag=DarkCloud&gt;: скорость расхода | 1 = 1% |
| `DASH_ATTACK_DAMAGE` | Dash Attack Damage | Урон атак с рывка | 1 = 1% |
| `DASH_ATTACK_IGNORE_DEFENSE` | Dash Attack Ignore Defense | Игнорирование защиты атаками с рывка | 1 = 1% |
| `DASH_COUNT` | Dash Count | Количество рывков | Целое число |
| `DASH_DISABLE` | Dash Disabled | Рывок недоступен | Целое число |
| `DASH_INVINCIBLE_TIME_BONUS` | Invincibility window bonus while dashing | Бонус ко времени неуязвимости при рывке | 1000 = 1s |
| `DASH_RECOVERY_SPEED` |  |  | 1 = 1% |
| `DASH_SPEED` | Dash speed | Скорость рывка | 1 = 1% |
| `DEBUFF_DAMAGE` | Damage dealt with &lt;tag=Debuff&gt; | Урон эффектов категории &lt;tag=Debuff&gt; | 1 = 1% |
| `DEBUFF_DURATION` | Duration of all debuffs inflicted on enemies | Длительность всех дебаффов, наложенных на врагов | 1 = 1% |
| `DEFENSE` | Defense | DEF | Целое число |
| `DEFENSE_TO_ATTACK` | No damage reduction from &lt;tag=Defense&gt;, but increases damage dealt by increased &lt;tag=DamageReduction&gt; | &lt;tag=Defense&gt; не снижает получаемый урон, но увеличение &lt;tag=DamageReduction&gt; повышает наносимый урон | Целое число |
| `DIMENSION_POCKET` | &lt;tag=TEXT:DimensionPocket&gt; Capacity | &lt;tag=TEXT:DimensionPocket&gt;: вместимость | Целое число |
| `DIRECT_ATTACK_BURN` | {VALUE} chance to inflict &lt;tag=Burn&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | &lt;tag=WeaponAction_DirectAttack&gt; с вероятностью {VALUE} накладывает &lt;tag=Burn&gt; | 1 = 1% |
| `DIRECT_ATTACK_ELECTRIC` | {VALUE} chance to inflict &lt;tag=Electric&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt;; | &lt;tag=WeaponAction_DirectAttack&gt; с вероятностью {VALUE} накладывает &lt;tag=Electric&gt; | 1 = 1% |
| `DIRECT_ATTACK_FROSTBITE` | {VALUE} chance to inflict &lt;tag=Frostbite&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | &lt;tag=WeaponAction_DirectAttack&gt; с вероятностью {VALUE} накладывает &lt;tag=Frostbite&gt; | 1 = 1% |
| `DISABLE_DUAL_ARTIFACT` | &lt;tag=TEXT:BondArtifact&gt; no longer appears | &lt;tag=TEXT:BondArtifact&gt; больше не появляется | Целое число |
| `ELECTRIC_DAMAGE` | &lt;tag=Electric&gt; extra damage | Дополнительный урон от эффекта &lt;tag=Electric&gt; | 1 = 1% |
| `ELECTRIC_STACK` | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | &lt;tag=Stack&gt;и эффекта «&lt;tag=Electric&gt;» | Целое число |
| `ELITE_DAMAGE` | Bonus damage dealt to &lt;tag=EnemyType_Boss&gt; and &lt;tag=EnemyType_Miniboss&gt; | Бонусный урон по врагам типов &lt;tag=EnemyType_Boss&gt; и &lt;tag=EnemyType_Miniboss&gt; | 1 = 1% |
| `EVASION` | Evasion | Уклонение | 100 = 1 |
| `EVASION_DISABLE` | &lt;tag=Evasion&gt; Disabled | &lt;tag=Evasion&gt; недоступно | 1 = 1% |
| `EVASION_RATE_REDUCTION` | Evasion Chance Reduction | Снижение шанса уклонения | 1 = 1% |
| `EXP_DROP` | Experience Drop | Получаемый опыт | 1 = 1% |
| `EXTRA_ITEM_CHOICES` | Additional Item Choices | Дополнительные варианты предметов | Целое число |
| `EXTRA_LIFE` | Revive with 60% HP upon death ({VALUE} times) | Возрождение с 60% HP после смерти ({VALUE} раз) | Целое число |
| `EXTRA_MIRACLE_CHOICES` | Additional Miracle Choices | Дополнительные варианты чудес | Целое число |
| `EXTRA_WEAPON_CHOICES` | Additional Weapon Choices | Дополнительные варианты оружия | Целое число |
| `FINAL_DAMAGE` |  |  | 1 = 1% |
| `FINAL_HP` | Final HP | Общее количество HP | 1 = 1% |
| `FINAL_MP` | Final MP | Общее количество MP | 1 = 1% |
| `FINAL_WEAPONDAMAGE` | &lt;tag=Weapon&gt; Damage | Урон, который наносит &lt;tag=Weapon&gt; | 1 = 1% |
| `FIRE_DAMAGE` | Fire Damage | Урон от огня | Целое число |
| `FIRE_DAMAGE_AMP` | &lt;tag=FireDamage&gt; Amplification | &lt;tag=FireDamage&gt;: увеличение | 1 = 1% |
| `FIXED_DASH` | Dash count fixed at {VALUE} | Фиксированное количество рывков: {VALUE} | Целое число |
| `FLAME_SWORD_ADDITIONAL_ATTACK` | Additionally activates &lt;tag=FlameSword&gt; {VALUE} times | &lt;tag=FlameSword&gt; срабатывает дополнительно {VALUE} раз | Целое число |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_MAGIC` | When dealing &lt;tag=Magic:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | При нанесении урона &lt;tag=Magic:noicon&gt; &lt;tag=FlameSword&gt; срабатывает дополнительно {VALUE} раз | Целое число |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_WEAPON` | When dealing &lt;tag=Weapon:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | При нанесении урона &lt;tag=Weapon:noicon&gt; &lt;tag=FlameSword&gt; срабатывает дополнительно {VALUE} раз | Целое число |
| `FLAME_SWORD_COOLDOWN` | &lt;tag=FlameSword&gt; Cooldown | Перезарядка &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_CRITICAL` | Critical Chance of &lt;tag=FlameSword&gt; | Шанс того, что &lt;tag=FlameSword&gt; нанесёт критический удар | 1 = 1% |
| `FLAME_SWORD_DAMAGE` | &lt;tag=FlameSword&gt; Damage | Урон, который наносит &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_IGNORE_DEFENSE` | Ignore target's defense by {VALUE} with &lt;tag=FlameSword&gt; | &lt;tag=FlameSword&gt; игнорирует {VALUE} защиты цели | 1 = 1% |
| `FLAME_SWORD_LUCK` | &lt;tag=FlameSword&gt; has a {VALUE} chance to grow in size and increase damage by &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% when thrown | При броске &lt;tag=FlameSword&gt; может увеличиться в размерах с шансом {VALUE} и получить &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% к урону | 1 = 1% |
| `FLAME_SWORD_MAX` | &lt;tag=FlameSword&gt; Maximum Count | &lt;tag=FlameSword&gt;: максимальное количество | Целое число |
| `FOLLOWER_ATTACK_SPEED` | Companion Attack Speed | Скорость атаки спутника | 1 = 1% |
| `FOLLOWER_CRITICAL` | Companion Critical Chance | Шанс критического удара спутника | 100 = 1% |
| `FOLLOWER_DAMAGE` | Companion Damage | Урон спутника | 1 = 1% |
| `FOLLOWER_DEFENSE` | Companion Defense | Защита спутника | Целое число |
| `FOLLOWER_MAX_HP` | Companion Max HP | Максимальное HP спутника | 1 = 1% |
| `FOLLOWER_REVIVE` | Companion Revive Time Haste | Скорость возрождения спутника | 1 = 1% |
| `FREEZE_DAMAGE` | &lt;tag=Freeze&gt; Debuff Damage | Урон от эффекта «&lt;tag=Freeze&gt;» | 1 = 1% |
| `FROSTBITE_DAMAGE` | Targets affected by &lt;tag=Frostbite&gt; take damage per second equal to {VALUE}% of &lt;tag=IceDamage&gt; (damage increases with &lt;tag=Stacks&gt;) | Цели под действием эффекта «&lt;tag=Frostbite&gt;» каждую секунду получают &lt;tag=IceDamage&gt; в размере {VALUE}%. (Урон увеличивается за каждый &lt;tag=Stack&gt;) | Целое число |
| `FROST_RELIC_CRITICAL_DAMAGE` | &lt;tag=FrostRelic&gt; critical damage | Критический урон предметов «&lt;tag=FrostRelic&gt;» | 1 = 1% |
| `FROST_RELIC_DAMAGE` | &lt;tag=FrostRelic&gt; damage | Урон предметов «&lt;tag=FrostRelic&gt;» | 1 = 1% |
| `FRUIT_COUNT` | Fruit Skewer Max | Максимум фруктовых шпажек | Целое число |
| `FURY_INVINCIBLE_TIME` | Invincibility Duration During &lt;tag=WeaponAction_Fury&gt; | Длительность неуязвимости, которую даёт &lt;tag=WeaponAction_Fury&gt; | 10 = 1s |
| `HEALING_PENALTY` | Healing Curse: Healing received decreases by {VALUE} | Проклятие исцеления: получаемое исцеление уменьшается на {VALUE} | 1 = 1% |
| `HIGHEST_ELEMENTAL_DAMAGE` | Highest Elemental Damage | Наибольший стихийный урон | Целое число |
| `HP_CURSE` | Max HP set to {VALUE} | Максимальное HP устанавливается на {VALUE} | Целое число |
| `HP_POTION_BONUS` | HP Potion Bonus | Бонус к эффекту зелья здоровья | 100 = 1% |
| `HP_STEAL` | Life Steal | Похищение HP | Целое число |
| `ICE_DAMAGE` | Cold Damage | Урон от холода | Целое число |
| `ICE_DAMAGE_AMP` | &lt;tag=IceDamage&gt; Amplification | &lt;tag=IceDamage&gt;: усиление | 1 = 1% |
| `IGNORE_DEFENSE` | Ignore Defense | Игнорирование защиты | 1 = 1% |
| `IGNORE_SUPER_ARMOR` | Extra damage to target’s &lt;tag=SuperArmor&gt; | Дополнительный урон по показателю «&lt;tag=SuperArmor&gt;» цели | 1 = 1% |
| `INFINITY_DASH` | Infinity Dash | Бесконечный рывок | Целое число |
| `INFINITY_MP` | Magic Reflow: &lt;tag=MaxMP&gt; is fixed at &lt;tag=CONST:infinityMPMax&gt;, and MP does not decrease | Магический круговорот: &lt;tag=MaxMP&gt; фиксируется на значении &lt;tag=CONST:infinityMPMax&gt;, а MP не расходуется | Целое число |
| `INVENTORY_SLOT` | Inventory Slot | Ячейка инвентаря | Целое число |
| `LEAF_DROP` | Leaf Drop | Выпадение листьев | 1 = 1% |
| `LEVEL_UP_HEAL` | HP Restored on Level Up | Восстановление HP при повышении уровня | 1 = 1% |
| `LIGHTNING_DAMAGE` | Lightning Damage | Урон от молнии | Целое число |
| `LIGHTNING_DAMAGE_AMP` | &lt;tag=LightningDamage&gt; Amplification | &lt;tag=LightningDamage&gt;: усиление | 1 = 1% |
| `LUCK` | Luck | Удача | Целое число |
| `MAGIC_COST_REDUCE` | Reduce MP Cost of &lt;tag=Magic:noicon&gt; | &lt;tag=Magic:noicon&gt;: снижение затрат MP | 1 = 1% |
| `MAGIC_CRITICAL` | &lt;tag=Magic&gt; Critical Chance | &lt;tag=Magic&gt;: шанс критического удара | 100 = 1% |
| `MAGIC_CRITICAL_DAMAGE_RATE` | &lt;tag=Magic&gt; Critical Damage | &lt;tag=Magic&gt;: критический урон | 1 = 1% |
| `MAGIC_DAMAGE_BONUS` | &lt;tag=Magic_NoIcon&gt; Damage | &lt;tag=Magic_NoIcon&gt;: урон | 1 = 1% |
| `MAGIC_IGNORE_DEFENSE` | &lt;tag=Magic_NoIcon&gt; Ignore Defense | &lt;tag=Magic_NoIcon&gt;: игнорирование защиты | 1 = 1% |
| `MAX_HP` | Max HP | Максимальное HP | Целое число |
| `MAX_MP` | Max MP | Максимальное MP | Целое число |
| `MIN_DARK_CLOUD` | &lt;tag=DarkCloud&gt; Capacity | &lt;tag=DarkCloud&gt;: вместимость | Целое число |
| `MIN_DARK_CLOUD_AMP` | &lt;tag=DarkCloud&gt; Capacity | &lt;tag=DarkCloud&gt;: вместимость | 1 = 1% |
| `MOVE_SPEED` | Move Speed | Скорость передвижения | 1 = 1% |
| `MP_REGEN` | MP Regeneration | Восстановление MP | Целое число |
| `MP_REGEN_KILL` | Recover &lt;tag=MP&gt; {VALUE} on Kill | При убийстве восстанавливается {VALUE} &lt;tag=MP&gt; | Целое число |
| `MP_REGEN_MULTIPLE` | Efficiency When Recovering &lt;tag=MP&gt; | Эффективность восстановления &lt;tag=MP&gt; | 1 = 1% |
| `MP_SKILL_DAMAGE` | Damage of abilities consuming MP | Урон способностей, расходующих MP | 1 = 1% |
| `MP_STEAL` | MP Steal | Похищение MP | Целое число |
| `NEGOTIATION` | Barter | Торговля | Целое число |
| `NO_DASH_INVINCIBLE` | Disable Dash Invincibility | Отключение неуязвимости при рывке | Целое число |
| `NO_MAGIC_COST` | Grimoires do not consume MP | Гримуары не расходуют MP | Целое число |
| `PARTY_BUFF` | Grants buffs to players within 5 &lt;tag=Tiles&gt; when casting buffs. However, the spell cost is increased by 50%. | При применении баффов они также накладываются на игроков в радиусе (&lt;tag=Tiles&gt;: 5). Однако затраты MP на заклинание увеличиваются на 50%. | Целое число |
| `PHYSICAL_DAMAGE` | Physical Damage | Физический урон | Целое число |
| `PHYSICAL_DAMAGE_AMP` | &lt;tag=PhysicalDamage&gt; Amplification | &lt;tag=PhysicalDamage&gt;: усиление | 1 = 1% |
| `PLANET_ATTACK_SPEED` | Planet Attack Speed | Скорость атаки планет | 1 = 1% |
| `PLANET_DAMAGE` | Planet Damage | Урон планет | 1 = 1% |
| `POTION_SLOT` | Potion Bag Slot | Ячейка сумки для зелий | Целое число |
| `RECEIVED_DAMAGE_REDUCTION` | Damage Taken Reduction | Снижение получаемого урона | 1 = 1% |
| `REVIVE_PLAYER_HASTE` | Reduces the time to revive another player by {VALUE} | Сокращает время на возрождение другого игрока на {VALUE} | 1 = 1% |
| `SAPPHIREBONUS` | Sapphire Acquisition Bonus | Бонус на получение сапфиров | 1 = 1% |
| `SPECIAL_ATTACK_DAMAGE` | Special Attack Damage | Урон особых атак | 1 = 1% |
| `STARTING_MONEY` | Start with {VALUE} &lt;tag=Leaf:nocolor&gt; | В начале экспедиции {VALUE} &lt;tag=Leaf:nocolor&gt; | Целое число |
| `SUB_BAG_SLOT` | Side Bag Slot | Ячейка подсумка | Целое число |
| `THORNS` | Thorns | Шипы | 1 = 1% |
| `TOUGHNESS` | Toughness | Стойкость | Целое число |
| `TRUE_DAMAGE` | True Damage | Истинный урон | Целое число |
| `UNLIMITED_COMBO` | For each excess Combo, &lt;tag=FinalDamage&gt; | За каждое комбо сверх лимита &lt;tag=FinalDamage&gt; | 1 = 1% |
| `WEAPON_CRITICAL` | Critical Hit Chance of &lt;tag=WeaponAction_DirectAttack&gt; | &lt;tag=WeaponAction_DirectAttack&gt;: шанс критического удара | 100 = 1% |
| `WEAPON_RANGE` | Melee Attack Range | Дальность ближней атаки | 1 = 1% |
