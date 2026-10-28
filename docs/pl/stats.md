# Lista statystyk

ID statystyk używane w `stats` stroju i w kodzie modów. Wartości to liczby całkowite w formacie `"ID/wartość"`, a kolumna Jednostka pokazuje, jak są przeliczane przy wyświetlaniu w grze. Na przykład wpisanie `CRITICAL/-400` dla statystyki, w której `100 = 1%`, wyświetla się jako -4%.

Przykłady faktycznie używane przez wbudowane stroje: `MOVE_SPEED/12`, `MAX_HP/-15`, `DEFENSE/10`, `EVASION/1200`, `CRITICAL/-400`, `ATTACK_SPEED/7`, `EXP_DROP/4`, `STARTING_MONEY/800`.

| ID | Nazwa angielska | Nazwa | Jednostka |
|---|---|---|---|
| `ATTACK_DASH` |  |  | 100 = 1 |
| `ATTACK_SPEED` | Attack Speed | Szybkość ataku | 1 = 1% |
| `BASIC_ATTACK_DAMAGE` | Normal Attack Damage | Obrażenia normalnego ataku | 1 = 1% |
| `BOSS_REWARD_DICE` | Acquire {VALUE} dice upon defeating a boss | Po pokonaniu bossa zyskaj kości. Liczba kości: {VALUE} | Liczba całkowita |
| `BUFF_DURATION` | Duration of Buffs Applied by Self | Czas trwania wzmocnień nakładanych przez siebie | 1 = 1% |
| `BURN_DAMAGE` | &lt;tag=Burn&gt; Damage | Obrażenia &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_DURATION` | &lt;tag=Burn&gt; duration | Czas trwania &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_SPEED` | Attack Speed of &lt;tag=Burn&gt; | Szybkość zadawania &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_STACK` | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | Liczba całkowita |
| `CHARGING_CHARM_BONUS` | &lt;tag=FrostRelic&gt; Charge Speed | Szybkość ładowania &lt;tag=FrostRelic&gt; | 1 = 1% |
| `CHARM_DAMAGE_BONUS` | &lt;tag=Artifact&gt; Damage | Obrażenia &lt;tag=Artifact&gt; | 1 = 1% |
| `COMBO_BONUS_DAMAGE` | &lt;tag=FinalDamage&gt; Bonus for each Activated Combo Type | Premia &lt;tag=FinalDamage&gt; za każdy aktywowany typ kombinacji | 1 = 1% |
| `COOLDOWN_RECOVERY_SPEED` |  |  | Liczba całkowita |
| `CRITICAL` | Critical Chance | Szansa na trafienie krytyczne | 100 = 1% |
| `CRITICAL_DAMAGE_RATE` | Critical Damage | Obrażenia krytyczne | 1 = 1% |
| `CROSSBOWAMMO` | Magazine Capacity | Pojemność magazynka | Liczba całkowita |
| `CROSSBOWFIRESPEED` | Crossbow Firing Speed | Szybkostrzelność kuszy | 1 = 1% |
| `CROSSBOW_ADDITIONAL_MAGAZINE` | Magazine Count | Liczba magazynków | Liczba całkowita |
| `CROSSBOW_RELOADSPEED` | Crossbow Reload Speed | Szybkość przeładowania kuszy | Liczba całkowita |
| `DARK_CLOUD_DAMAGE` | Extra &lt;tag=DarkCloud&gt; damage | Dodatkowe obrażenia od &lt;tag=DarkCloud&gt; | 1 = 1% |
| `DARK_CLOUD_RESTORE_DURING_BATTLE` | &lt;tag=DarkCloud&gt; Recovery Rate during Combat | Szybkość odzyskiwania &lt;tag=DarkCloud&gt; podczas walki | 1 = 1% |
| `DARK_CLOUD_SPEED` | Consumption Speed of &lt;tag=DarkCloud&gt; | Szybkość zużywania &lt;tag=DarkCloud&gt; | 1 = 1% |
| `DASH_ATTACK_DAMAGE` | Dash Attack Damage | Obrażenia ataku podczas doskoku | 1 = 1% |
| `DASH_ATTACK_IGNORE_DEFENSE` | Dash Attack Ignore Defense | Ignorowanie obrony ataku podczas doskoku | 1 = 1% |
| `DASH_COUNT` | Dash Count | Liczba doskoków | Liczba całkowita |
| `DASH_DISABLE` | Dash Disabled | Doskok wyłączony | Liczba całkowita |
| `DASH_INVINCIBLE_TIME_BONUS` | Invincibility window bonus while dashing | Premia do okna niewrażliwości podczas doskoku | 1000 = 1s |
| `DASH_RECOVERY_SPEED` |  |  | 1 = 1% |
| `DASH_SPEED` | Dash speed | Szybkość doskoku | 1 = 1% |
| `DEBUFF_DAMAGE` | Damage dealt with &lt;tag=Debuff&gt; | Obrażenia zadawane za pomocą &lt;tag=Debuff&gt; | 1 = 1% |
| `DEBUFF_DURATION` | Duration of all debuffs inflicted on enemies | Czas trwania wszystkich osłabień nakładanych na wrogów | 1 = 1% |
| `DEFENSE` | Defense | Obrona | Liczba całkowita |
| `DEFENSE_TO_ATTACK` | No damage reduction from &lt;tag=Defense&gt;, but increases damage dealt by increased &lt;tag=DamageReduction&gt; | Brak redukcji obrażeń z &lt;tag=Defense&gt;, ale zwiększa zadawane obrażenia o wartość zwiększonej &lt;tag=DamageReduction&gt; | Liczba całkowita |
| `DIMENSION_POCKET` | &lt;tag=TEXT:DimensionPocket&gt; Capacity | Pojemność &lt;tag=TEXT:DimensionPocket&gt; | Liczba całkowita |
| `DIRECT_ATTACK_BURN` | {VALUE} chance to inflict &lt;tag=Burn&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | {VALUE} szansy na nałożenie osłabienia &lt;tag=Burn&gt; przy &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DIRECT_ATTACK_ELECTRIC` | {VALUE} chance to inflict &lt;tag=Electric&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt;; | {VALUE} szansy na nałożenie osłabienia &lt;tag=Electric&gt; przy &lt;tag=WeaponAction_DirectAttack&gt;; | 1 = 1% |
| `DIRECT_ATTACK_FROSTBITE` | {VALUE} chance to inflict &lt;tag=Frostbite&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | {VALUE} szansy na nałożenie osłabienia &lt;tag=Frostbite&gt; przy &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DISABLE_DUAL_ARTIFACT` | &lt;tag=TEXT:BondArtifact&gt; no longer appears | &lt;tag=TEXT:BondArtifact&gt; nie pojawia się już | Liczba całkowita |
| `ELECTRIC_DAMAGE` | &lt;tag=Electric&gt; extra damage | Dodatkowe obrażenia &lt;tag=Electric&gt; | 1 = 1% |
| `ELECTRIC_STACK` | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | Liczba całkowita |
| `ELITE_DAMAGE` | Bonus damage dealt to &lt;tag=EnemyType_Boss&gt; and &lt;tag=EnemyType_Miniboss&gt; | Dodatkowe obrażenia zadawane &lt;tag=EnemyType_Boss&gt; i &lt;tag=EnemyType_Miniboss&gt; | 1 = 1% |
| `EVASION` | Evasion | Unik | 100 = 1 |
| `EVASION_DISABLE` | &lt;tag=Evasion&gt; Disabled | &lt;tag=Evasion&gt; wyłączony | 1 = 1% |
| `EVASION_RATE_REDUCTION` | Evasion Chance Reduction | Redukcja szansy na unik | 1 = 1% |
| `EXP_DROP` | Experience Drop | Zdobywane doświadczenie | 1 = 1% |
| `EXTRA_ITEM_CHOICES` | Additional Item Choices | Dodatkowe opcje przedmiotów | Liczba całkowita |
| `EXTRA_LIFE` | Revive with 60% HP upon death ({VALUE} times) | Wskrzesza z 60% PŻ po śmierci (liczba użyć: {VALUE}) | Liczba całkowita |
| `EXTRA_MIRACLE_CHOICES` | Additional Miracle Choices | Dodatkowe opcje cudów | Liczba całkowita |
| `EXTRA_WEAPON_CHOICES` | Additional Weapon Choices | Dodatkowe opcje broni | Liczba całkowita |
| `FINAL_DAMAGE` |  |  | 1 = 1% |
| `FINAL_HP` | Final HP | Końcowe PŻ | 1 = 1% |
| `FINAL_MP` | Final MP | Końcowe PM | 1 = 1% |
| `FINAL_WEAPONDAMAGE` | &lt;tag=Weapon&gt; Damage | Obrażenia &lt;tag=Weapon&gt; | 1 = 1% |
| `FIRE_DAMAGE` | Fire Damage | Obrażenia od ognia | Liczba całkowita |
| `FIRE_DAMAGE_AMP` | &lt;tag=FireDamage&gt; Amplification | Wzmocnienie &lt;tag=FireDamage&gt; | 1 = 1% |
| `FIXED_DASH` | Dash count fixed at {VALUE} | Liczba doskoków ustawiona na {VALUE} | Liczba całkowita |
| `FLAME_SWORD_ADDITIONAL_ATTACK` | Additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Aktywuje dodatkowe &lt;tag=FlameSword&gt;. Liczba dodatkowych aktywacji: {VALUE} | Liczba całkowita |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_MAGIC` | When dealing &lt;tag=Magic:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Gdy zostaną zadane obrażenia &lt;tag=Magic:noicon&gt;, aktywuje dodatkowe &lt;tag=FlameSword&gt;. Liczba dodatkowych aktywacji: {VALUE} | Liczba całkowita |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_WEAPON` | When dealing &lt;tag=Weapon:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Gdy zostaną zadane obrażenia &lt;tag=Weapon:noicon&gt;, aktywuje dodatkowe &lt;tag=FlameSword&gt;. Liczba dodatkowych aktywacji: {VALUE} | Liczba całkowita |
| `FLAME_SWORD_COOLDOWN` | &lt;tag=FlameSword&gt; Cooldown | Czas odnowienia &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_CRITICAL` | Critical Chance of &lt;tag=FlameSword&gt; | Szansa na trafienie krytyczne &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_DAMAGE` | &lt;tag=FlameSword&gt; Damage | Obrażenia &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_IGNORE_DEFENSE` | Ignore target's defense by {VALUE} with &lt;tag=FlameSword&gt; | Ignoruje obronę celu o {VALUE} przy &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_LUCK` | &lt;tag=FlameSword&gt; has a {VALUE} chance to grow in size and increase damage by &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% when thrown | &lt;tag=FlameSword&gt; ma {VALUE} szansy na zwiększenie rozmiaru i obrażeń o &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% po rzuceniu | 1 = 1% |
| `FLAME_SWORD_MAX` | &lt;tag=FlameSword&gt; Maximum Count | Maksymalna liczba &lt;tag=FlameSword&gt; | Liczba całkowita |
| `FOLLOWER_ATTACK_SPEED` | Companion Attack Speed | Szybkość ataku towarzysza | 1 = 1% |
| `FOLLOWER_CRITICAL` | Companion Critical Chance | Szansa na trafienie krytyczne towarzysza | 100 = 1% |
| `FOLLOWER_DAMAGE` | Companion Damage | Obrażenia towarzysza | 1 = 1% |
| `FOLLOWER_DEFENSE` | Companion Defense | Obrona towarzysza | Liczba całkowita |
| `FOLLOWER_MAX_HP` | Companion Max HP | Maks. PŻ towarzysza | 1 = 1% |
| `FOLLOWER_REVIVE` | Companion Revive Time Haste | Przyspieszenie czasu wskrzeszenia towarzysza | 1 = 1% |
| `FREEZE_DAMAGE` | &lt;tag=Freeze&gt; Debuff Damage | Obrażenia od osłabienia &lt;tag=Freeze&gt; | 1 = 1% |
| `FROSTBITE_DAMAGE` | Targets affected by &lt;tag=Frostbite&gt; take damage per second equal to {VALUE}% of &lt;tag=IceDamage&gt; (damage increases with &lt;tag=Stacks&gt;) | Cele dotknięte &lt;tag=Frostbite&gt; otrzymują co sekundę &lt;tag=IceDamage&gt; równe {VALUE}% (obrażenia rosną wraz z &lt;tag=Stacks&gt;) | Liczba całkowita |
| `FROST_RELIC_CRITICAL_DAMAGE` | &lt;tag=FrostRelic&gt; critical damage | Obrażenia krytyczne &lt;tag=FrostRelic&gt; | 1 = 1% |
| `FROST_RELIC_DAMAGE` | &lt;tag=FrostRelic&gt; damage | Obrażenia &lt;tag=FrostRelic&gt; | 1 = 1% |
| `FRUIT_COUNT` | Fruit Skewer Max | Maksymalna liczba owocowych szaszłyków | Liczba całkowita |
| `FURY_INVINCIBLE_TIME` | Invincibility Duration During &lt;tag=WeaponAction_Fury&gt; | Czas trwania niewrażliwości podczas &lt;tag=WeaponAction_Fury&gt; | 10 = 1s |
| `HEALING_PENALTY` | Healing Curse: Healing received decreases by {VALUE} | Klątwa leczenia: otrzymywane leczenie zmniejsza się o {VALUE} | 1 = 1% |
| `HIGHEST_ELEMENTAL_DAMAGE` | Highest Elemental Damage | Najwyższe obrażenia od żywiołów | Liczba całkowita |
| `HP_CURSE` | Max HP set to {VALUE} | Maks. PŻ ustawione na {VALUE} | Liczba całkowita |
| `HP_POTION_BONUS` | HP Potion Bonus | Bonus do mikstury PŻ | 100 = 1% |
| `HP_STEAL` | Life Steal | Kradzież życia | Liczba całkowita |
| `ICE_DAMAGE` | Cold Damage | Obrażenia od zimna | Liczba całkowita |
| `ICE_DAMAGE_AMP` | &lt;tag=IceDamage&gt; Amplification | Wzmocnienie &lt;tag=IceDamage&gt; | 1 = 1% |
| `IGNORE_DEFENSE` | Ignore Defense | Ignorowanie obrony | 1 = 1% |
| `IGNORE_SUPER_ARMOR` | Extra damage to target’s &lt;tag=SuperArmor&gt; | Dodatkowe obrażenia przeciwko celowi z &lt;tag=SuperArmor&gt; | 1 = 1% |
| `INFINITY_DASH` | Infinity Dash | Nieskończony doskok | Liczba całkowita |
| `INFINITY_MP` | Magic Reflow: &lt;tag=MaxMP&gt; is fixed at &lt;tag=CONST:infinityMPMax&gt;, and MP does not decrease | Przepływ magii: wartość &lt;tag=MaxMP&gt; wynosi &lt;tag=CONST:infinityMPMax&gt; i nie spada | Liczba całkowita |
| `INVENTORY_SLOT` | Inventory Slot | Miejsce w ekwipunku | Liczba całkowita |
| `LEAF_DROP` | Leaf Drop | Opad liści | 1 = 1% |
| `LEVEL_UP_HEAL` | HP Restored on Level Up | Odzyskiwanie PŻ przy awansie | 1 = 1% |
| `LIGHTNING_DAMAGE` | Lightning Damage | Obrażenia od błyskawic | Liczba całkowita |
| `LIGHTNING_DAMAGE_AMP` | &lt;tag=LightningDamage&gt; Amplification | Wzmocnienie &lt;tag=LightningDamage&gt; | 1 = 1% |
| `LUCK` | Luck | Szczęście | Liczba całkowita |
| `MAGIC_COST_REDUCE` | Reduce MP Cost of &lt;tag=Magic:noicon&gt; | Zmniejszenie kosztu PM &lt;tag=Magic:noicon&gt; | 1 = 1% |
| `MAGIC_CRITICAL` | &lt;tag=Magic&gt; Critical Chance | Szansa na trafienie krytyczne &lt;tag=Magic&gt; | 100 = 1% |
| `MAGIC_CRITICAL_DAMAGE_RATE` | &lt;tag=Magic&gt; Critical Damage | Obrażenia krytyczne &lt;tag=Magic&gt; | 1 = 1% |
| `MAGIC_DAMAGE_BONUS` | &lt;tag=Magic_NoIcon&gt; Damage | Obrażenia &lt;tag=Magic_NoIcon&gt; | 1 = 1% |
| `MAGIC_IGNORE_DEFENSE` | &lt;tag=Magic_NoIcon&gt; Ignore Defense | Ignorowanie obrony &lt;tag=Magic_NoIcon&gt; | 1 = 1% |
| `MAX_HP` | Max HP | Maks. PŻ | Liczba całkowita |
| `MAX_MP` | Max MP | Maks. PM | Liczba całkowita |
| `MIN_DARK_CLOUD` | &lt;tag=DarkCloud&gt; Capacity | Pojemność &lt;tag=DarkCloud&gt; | Liczba całkowita |
| `MIN_DARK_CLOUD_AMP` | &lt;tag=DarkCloud&gt; Capacity | Pojemność &lt;tag=DarkCloud&gt; | 1 = 1% |
| `MOVE_SPEED` | Move Speed | Szybkość ruchu | 1 = 1% |
| `MP_REGEN` | MP Regeneration | Regeneracja PM | Liczba całkowita |
| `MP_REGEN_KILL` | Recover &lt;tag=MP&gt; {VALUE} on Kill | Odzyskuj &lt;tag=MP&gt; {VALUE} po zabiciu | Liczba całkowita |
| `MP_REGEN_MULTIPLE` | Efficiency When Recovering &lt;tag=MP&gt; | Skuteczność odzyskiwania &lt;tag=MP&gt; | 1 = 1% |
| `MP_SKILL_DAMAGE` | Damage of abilities consuming MP | Obrażenia umiejętności zużywających PM | 1 = 1% |
| `MP_STEAL` | MP Steal | Kradzież PM | Liczba całkowita |
| `NEGOTIATION` | Barter | Targowanie | Liczba całkowita |
| `NO_DASH_INVINCIBLE` | Disable Dash Invincibility | Wyłącz niewrażliwość podczas doskoku | Liczba całkowita |
| `NO_MAGIC_COST` | Grimoires do not consume MP | Grymuary nie zużywają PM | Liczba całkowita |
| `PARTY_BUFF` | Grants buffs to players within 5 &lt;tag=Tiles&gt; when casting buffs. However, the spell cost is increased by 50%. | Podczas rzucania wzmocnień zapewnia je także pobliskim graczom. Zasięg: &lt;tag=Tiles&gt; × 5. Koszt zaklęcia wzrasta o 50%. | Liczba całkowita |
| `PHYSICAL_DAMAGE` | Physical Damage | Obrażenia fizyczne | Liczba całkowita |
| `PHYSICAL_DAMAGE_AMP` | &lt;tag=PhysicalDamage&gt; Amplification | Wzmocnienie &lt;tag=PhysicalDamage&gt; | 1 = 1% |
| `PLANET_ATTACK_SPEED` | Planet Attack Speed | Szybkość ataku planet | 1 = 1% |
| `PLANET_DAMAGE` | Planet Damage | Obrażenia planet | 1 = 1% |
| `POTION_SLOT` | Potion Bag Slot | Miejsce w torbie na mikstury | Liczba całkowita |
| `RECEIVED_DAMAGE_REDUCTION` | Damage Taken Reduction | Redukcja otrzymywanych obrażeń | 1 = 1% |
| `REVIVE_PLAYER_HASTE` | Reduces the time to revive another player by {VALUE} | Skraca czas wskrzeszania innego gracza o {VALUE} | 1 = 1% |
| `SAPPHIREBONUS` | Sapphire Acquisition Bonus | Premia do zdobywanych szafirów | 1 = 1% |
| `SPECIAL_ATTACK_DAMAGE` | Special Attack Damage | Obrażenia ataku specjalnego | 1 = 1% |
| `STARTING_MONEY` | Start with {VALUE} &lt;tag=Leaf:nocolor&gt; | Na początku wyprawy — zasób „&lt;tag=Leaf:nocolor&gt;”: {VALUE} | Liczba całkowita |
| `SUB_BAG_SLOT` | Side Bag Slot | Miejsce w bocznej torbie | Liczba całkowita |
| `THORNS` | Thorns | Ciernie | 1 = 1% |
| `TOUGHNESS` | Toughness | Wytrzymałość | Liczba całkowita |
| `TRUE_DAMAGE` | True Damage | Obrażenia prawdziwe | Liczba całkowita |
| `UNLIMITED_COMBO` | For each excess Combo, &lt;tag=FinalDamage&gt; | Za każde kombo ponad limit &lt;tag=FinalDamage&gt; | 1 = 1% |
| `WEAPON_CRITICAL` | Critical Hit Chance of &lt;tag=WeaponAction_DirectAttack&gt; | Szansa na trafienie krytyczne &lt;tag=WeaponAction_DirectAttack&gt; | 100 = 1% |
| `WEAPON_RANGE` | Melee Attack Range | Zasięg ataku wręcz | 1 = 1% |
