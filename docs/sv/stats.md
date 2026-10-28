# Statlista

Stat-ID:n som används i en dräkts `stats` och i modkod. Värdena är heltal i formatet `"ID/värde"`, och kolumnen Enhet visar hur de omvandlas när de visas i spelet. Om du till exempel skriver `CRITICAL/-400` för en stat där `100 = 1%` visas det som -4 %.

Exempel som faktiskt används av de inbyggda dräkterna: `MOVE_SPEED/12`, `MAX_HP/-15`, `DEFENSE/10`, `EVASION/1200`, `CRITICAL/-400`, `ATTACK_SPEED/7`, `EXP_DROP/4`, `STARTING_MONEY/800`.

| ID | Engelskt namn | Namn | Enhet |
|---|---|---|---|
| `ATTACK_DASH` |  |  | 100 = 1 |
| `ATTACK_SPEED` | Attack Speed | Attackhastighet | 1 = 1% |
| `BASIC_ATTACK_DAMAGE` | Normal Attack Damage | Normal attackskada | 1 = 1% |
| `BOSS_REWARD_DICE` | Acquire {VALUE} dice upon defeating a boss | Få {VALUE} tärning(ar) när du besegrar en boss | Heltal |
| `BUFF_DURATION` | Duration of Buffs Applied by Self | Varaktighet för buffar som du tillämpar | 1 = 1% |
| `BURN_DAMAGE` | &lt;tag=Burn&gt; Damage | &lt;tag=Burn&gt;-skada | 1 = 1% |
| `BURN_DURATION` | &lt;tag=Burn&gt; duration | &lt;tag=Burn&gt;-varaktighet | 1 = 1% |
| `BURN_SPEED` | Attack Speed of &lt;tag=Burn&gt; | Attackhastighet för &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_STACK` | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | Heltal |
| `CHARGING_CHARM_BONUS` | &lt;tag=FrostRelic&gt; Charge Speed | Laddningshastighet för &lt;tag=FrostRelic&gt; | 1 = 1% |
| `CHARM_DAMAGE_BONUS` | &lt;tag=Artifact&gt; Damage | &lt;tag=Artifact&gt;-skada | 1 = 1% |
| `COMBO_BONUS_DAMAGE` | &lt;tag=FinalDamage&gt; Bonus for each Activated Combo Type | &lt;tag=FinalDamage&gt;-bonus för varje aktiverad kombinationstyp | 1 = 1% |
| `COOLDOWN_RECOVERY_SPEED` |  |  | Heltal |
| `CRITICAL` | Critical Chance | Kritisk träffchans | 100 = 1% |
| `CRITICAL_DAMAGE_RATE` | Critical Damage | Kritisk skada | 1 = 1% |
| `CROSSBOWAMMO` | Magazine Capacity | Magasinskapacitet | Heltal |
| `CROSSBOWFIRESPEED` | Crossbow Firing Speed | Armborstets eldhastighet | 1 = 1% |
| `CROSSBOW_ADDITIONAL_MAGAZINE` | Magazine Count | Magasinsantal | Heltal |
| `CROSSBOW_RELOADSPEED` | Crossbow Reload Speed | Armborstets omladdningshastighet | Heltal |
| `DARK_CLOUD_DAMAGE` | Extra &lt;tag=DarkCloud&gt; damage | Extra &lt;tag=DarkCloud&gt;-skada | 1 = 1% |
| `DARK_CLOUD_RESTORE_DURING_BATTLE` | &lt;tag=DarkCloud&gt; Recovery Rate during Combat | Återhämtningshastighet för &lt;tag=DarkCloud&gt; under strid | 1 = 1% |
| `DARK_CLOUD_SPEED` | Consumption Speed of &lt;tag=DarkCloud&gt; | Förbrukningshastighet för &lt;tag=DarkCloud&gt; | 1 = 1% |
| `DASH_ATTACK_DAMAGE` | Dash Attack Damage | Rusningsattacksskada | 1 = 1% |
| `DASH_ATTACK_IGNORE_DEFENSE` | Dash Attack Ignore Defense | Rusningsattack: ignorera försvar | 1 = 1% |
| `DASH_COUNT` | Dash Count | Antal rusningar | Heltal |
| `DASH_DISABLE` | Dash Disabled | Rusning inaktiverad | Heltal |
| `DASH_INVINCIBLE_TIME_BONUS` | Invincibility window bonus while dashing | Bonus till osårbarhetsfönstret vid rusning | 1000 = 1s |
| `DASH_RECOVERY_SPEED` |  |  | 1 = 1% |
| `DASH_SPEED` | Dash speed | Rusningshastighet | 1 = 1% |
| `DEBUFF_DAMAGE` | Damage dealt with &lt;tag=Debuff&gt; | Skada orsakad med &lt;tag=Debuff&gt; | 1 = 1% |
| `DEBUFF_DURATION` | Duration of all debuffs inflicted on enemies | Varaktighet för alla debuffar som tillfogas fiender | 1 = 1% |
| `DEFENSE` | Defense | Försvar | Heltal |
| `DEFENSE_TO_ATTACK` | No damage reduction from &lt;tag=Defense&gt;, but increases damage dealt by increased &lt;tag=DamageReduction&gt; | Ingen skademinskning från &lt;tag=Defense&gt;, men ökad &lt;tag=DamageReduction&gt; ökar utdelad skada | Heltal |
| `DIMENSION_POCKET` | &lt;tag=TEXT:DimensionPocket&gt; Capacity | Kapacitet för &lt;tag=TEXT:DimensionPocket&gt; | Heltal |
| `DIRECT_ATTACK_BURN` | {VALUE} chance to inflict &lt;tag=Burn&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | {VALUE} chans att tillfoga &lt;tag=Burn&gt;-debuff vid &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DIRECT_ATTACK_ELECTRIC` | {VALUE} chance to inflict &lt;tag=Electric&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt;; | {VALUE} chans att tillfoga &lt;tag=Electric&gt;-debuff vid &lt;tag=WeaponAction_DirectAttack&gt;; | 1 = 1% |
| `DIRECT_ATTACK_FROSTBITE` | {VALUE} chance to inflict &lt;tag=Frostbite&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | {VALUE} chans att tillfoga &lt;tag=Frostbite&gt;-debuff vid &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DISABLE_DUAL_ARTIFACT` | &lt;tag=TEXT:BondArtifact&gt; no longer appears | &lt;tag=TEXT:BondArtifact&gt; dyker inte längre upp | Heltal |
| `ELECTRIC_DAMAGE` | &lt;tag=Electric&gt; extra damage | Extra &lt;tag=Electric&gt;-skada | 1 = 1% |
| `ELECTRIC_STACK` | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | &lt;tag=Electric&gt;-&lt;tag=Stacks&gt; | Heltal |
| `ELITE_DAMAGE` | Bonus damage dealt to &lt;tag=EnemyType_Boss&gt; and &lt;tag=EnemyType_Miniboss&gt; | Bonusskada mot &lt;tag=EnemyType_Boss&gt; och &lt;tag=EnemyType_Miniboss&gt; | 1 = 1% |
| `EVASION` | Evasion | Undvikande | 100 = 1 |
| `EVASION_DISABLE` | &lt;tag=Evasion&gt; Disabled | &lt;tag=Evasion&gt; inaktiverad | 1 = 1% |
| `EVASION_RATE_REDUCTION` | Evasion Chance Reduction | Minskad undvikandechans | 1 = 1% |
| `EXP_DROP` | Experience Drop | Erfarenhetsdrop | 1 = 1% |
| `EXTRA_ITEM_CHOICES` | Additional Item Choices | Extra föremålsval | Heltal |
| `EXTRA_LIFE` | Revive with 60% HP upon death ({VALUE} times) | Återuppstå med 60 % HP vid död ({VALUE} gång(er)) | Heltal |
| `EXTRA_MIRACLE_CHOICES` | Additional Miracle Choices | Extra mirakelval | Heltal |
| `EXTRA_WEAPON_CHOICES` | Additional Weapon Choices | Extra vapenval | Heltal |
| `FINAL_DAMAGE` |  |  | 1 = 1% |
| `FINAL_HP` | Final HP | Slutligt HP | 1 = 1% |
| `FINAL_MP` | Final MP | Slutlig MP | 1 = 1% |
| `FINAL_WEAPONDAMAGE` | &lt;tag=Weapon&gt; Damage | &lt;tag=Weapon&gt;-skada | 1 = 1% |
| `FIRE_DAMAGE` | Fire Damage | Eldskada | Heltal |
| `FIRE_DAMAGE_AMP` | &lt;tag=FireDamage&gt; Amplification | &lt;tag=FireDamage&gt;-förstärkning | 1 = 1% |
| `FIXED_DASH` | Dash count fixed at {VALUE} | Antal rusningar låst till {VALUE} | Heltal |
| `FLAME_SWORD_ADDITIONAL_ATTACK` | Additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Aktiverar &lt;tag=FlameSword&gt; ytterligare {VALUE} gång(er) | Heltal |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_MAGIC` | When dealing &lt;tag=Magic:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | När &lt;tag=Magic:noicon&gt;-skada orsakas aktiveras &lt;tag=FlameSword&gt; ytterligare {VALUE} gång(er) | Heltal |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_WEAPON` | When dealing &lt;tag=Weapon:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | När &lt;tag=Weapon:noicon&gt;-skada orsakas aktiveras &lt;tag=FlameSword&gt; ytterligare {VALUE} gång(er) | Heltal |
| `FLAME_SWORD_COOLDOWN` | &lt;tag=FlameSword&gt; Cooldown | Nedkylning för &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_CRITICAL` | Critical Chance of &lt;tag=FlameSword&gt; | Kritisk chans för &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_DAMAGE` | &lt;tag=FlameSword&gt; Damage | &lt;tag=FlameSword&gt;-skada | 1 = 1% |
| `FLAME_SWORD_IGNORE_DEFENSE` | Ignore target's defense by {VALUE} with &lt;tag=FlameSword&gt; | &lt;tag=FlameSword&gt; ignorerar {VALUE} av målets försvar | 1 = 1% |
| `FLAME_SWORD_LUCK` | &lt;tag=FlameSword&gt; has a {VALUE} chance to grow in size and increase damage by &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% when thrown | &lt;tag=FlameSword&gt; har {VALUE} chans att växa i storlek och öka skadan med &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt; % när den kastas | 1 = 1% |
| `FLAME_SWORD_MAX` | &lt;tag=FlameSword&gt; Maximum Count | Maxantal &lt;tag=FlameSword&gt; | Heltal |
| `FOLLOWER_ATTACK_SPEED` | Companion Attack Speed | Följeslagares attackhastighet | 1 = 1% |
| `FOLLOWER_CRITICAL` | Companion Critical Chance | Följeslagares kritchans | 100 = 1% |
| `FOLLOWER_DAMAGE` | Companion Damage | Följeslagares skada | 1 = 1% |
| `FOLLOWER_DEFENSE` | Companion Defense | Följeslagares försvar | Heltal |
| `FOLLOWER_MAX_HP` | Companion Max HP | Följeslagares max HP | 1 = 1% |
| `FOLLOWER_REVIVE` | Companion Revive Time Haste | Följeslagares återupplivningshastighet | 1 = 1% |
| `FREEZE_DAMAGE` | &lt;tag=Freeze&gt; Debuff Damage | Debuffskada från &lt;tag=Freeze&gt; | 1 = 1% |
| `FROSTBITE_DAMAGE` | Targets affected by &lt;tag=Frostbite&gt; take damage per second equal to {VALUE}% of &lt;tag=IceDamage&gt; (damage increases with &lt;tag=Stacks&gt;) | Mål som påverkas av &lt;tag=Frostbite&gt; tar varje sekund skada motsvarande {VALUE} % av &lt;tag=IceDamage&gt; (skadan ökar med &lt;tag=Stacks&gt;) | Heltal |
| `FROST_RELIC_CRITICAL_DAMAGE` | &lt;tag=FrostRelic&gt; critical damage | &lt;tag=FrostRelic&gt;-kritisk skada | 1 = 1% |
| `FROST_RELIC_DAMAGE` | &lt;tag=FrostRelic&gt; damage | &lt;tag=FrostRelic&gt;-skada | 1 = 1% |
| `FRUIT_COUNT` | Fruit Skewer Max | Maxantal fruktspett | Heltal |
| `FURY_INVINCIBLE_TIME` | Invincibility Duration During &lt;tag=WeaponAction_Fury&gt; | Oövervinnlighetstid under &lt;tag=WeaponAction_Fury&gt; | 10 = 1s |
| `HEALING_PENALTY` | Healing Curse: Healing received decreases by {VALUE} | Läkningsförbannelse: Mottagen läkning minskar med {VALUE} | 1 = 1% |
| `HIGHEST_ELEMENTAL_DAMAGE` | Highest Elemental Damage | Högsta elementskada | Heltal |
| `HP_CURSE` | Max HP set to {VALUE} | Max-HP sätts till {VALUE} | Heltal |
| `HP_POTION_BONUS` | HP Potion Bonus | HP-drycksbonus | 100 = 1% |
| `HP_STEAL` | Life Steal | Livsstöld | Heltal |
| `ICE_DAMAGE` | Cold Damage | Köldskada | Heltal |
| `ICE_DAMAGE_AMP` | &lt;tag=IceDamage&gt; Amplification | Förstärkning av &lt;tag=IceDamage&gt; | 1 = 1% |
| `IGNORE_DEFENSE` | Ignore Defense | Ignorera försvar | 1 = 1% |
| `IGNORE_SUPER_ARMOR` | Extra damage to target’s &lt;tag=SuperArmor&gt; | Extra skada på målets &lt;tag=SuperArmor&gt; | 1 = 1% |
| `INFINITY_DASH` | Infinity Dash | Oändlig rusning | Heltal |
| `INFINITY_MP` | Magic Reflow: &lt;tag=MaxMP&gt; is fixed at &lt;tag=CONST:infinityMPMax&gt;, and MP does not decrease | Magiskt återflöde: &lt;tag=MaxMP&gt; är låst till &lt;tag=CONST:infinityMPMax&gt;, och MP minskar inte | Heltal |
| `INVENTORY_SLOT` | Inventory Slot | Inventarieplats | Heltal |
| `LEAF_DROP` | Leaf Drop | Lövfall | 1 = 1% |
| `LEVEL_UP_HEAL` | HP Restored on Level Up | HP återställs vid nivåökning | 1 = 1% |
| `LIGHTNING_DAMAGE` | Lightning Damage | Blixtskada | Heltal |
| `LIGHTNING_DAMAGE_AMP` | &lt;tag=LightningDamage&gt; Amplification | Förstärkning av &lt;tag=LightningDamage&gt; | 1 = 1% |
| `LUCK` | Luck | Tur | Heltal |
| `MAGIC_COST_REDUCE` | Reduce MP Cost of &lt;tag=Magic:noicon&gt; | Minska MP-kostnaden för &lt;tag=Magic:noicon&gt; | 1 = 1% |
| `MAGIC_CRITICAL` | &lt;tag=Magic&gt; Critical Chance | Kritisk träffchans för &lt;tag=Magic&gt; | 100 = 1% |
| `MAGIC_CRITICAL_DAMAGE_RATE` | &lt;tag=Magic&gt; Critical Damage | Kritisk skada för &lt;tag=Magic&gt; | 1 = 1% |
| `MAGIC_DAMAGE_BONUS` | &lt;tag=Magic_NoIcon&gt; Damage | Skada från &lt;tag=Magic_NoIcon&gt; | 1 = 1% |
| `MAGIC_IGNORE_DEFENSE` | &lt;tag=Magic_NoIcon&gt; Ignore Defense | Ignorera försvar för &lt;tag=Magic_NoIcon&gt; | 1 = 1% |
| `MAX_HP` | Max HP | Max HP | Heltal |
| `MAX_MP` | Max MP | Max MP | Heltal |
| `MIN_DARK_CLOUD` | &lt;tag=DarkCloud&gt; Capacity | Kapacitet för &lt;tag=DarkCloud&gt; | Heltal |
| `MIN_DARK_CLOUD_AMP` | &lt;tag=DarkCloud&gt; Capacity | Kapacitet för &lt;tag=DarkCloud&gt; | 1 = 1% |
| `MOVE_SPEED` | Move Speed | Rörelsehastighet | 1 = 1% |
| `MP_REGEN` | MP Regeneration | MP-återhämtning | Heltal |
| `MP_REGEN_KILL` | Recover &lt;tag=MP&gt; {VALUE} on Kill | Återställ &lt;tag=MP&gt; {VALUE} vid dödande | Heltal |
| `MP_REGEN_MULTIPLE` | Efficiency When Recovering &lt;tag=MP&gt; | Effektivitet vid återställning av &lt;tag=MP&gt; | 1 = 1% |
| `MP_SKILL_DAMAGE` | Damage of abilities consuming MP | Skada från förmågor som förbrukar MP | 1 = 1% |
| `MP_STEAL` | MP Steal | MP-stöld | Heltal |
| `NEGOTIATION` | Barter | Byteshandel | Heltal |
| `NO_DASH_INVINCIBLE` | Disable Dash Invincibility | Inaktivera odödlighet vid rusning | Heltal |
| `NO_MAGIC_COST` | Grimoires do not consume MP | Grimoarer förbrukar inte MP | Heltal |
| `PARTY_BUFF` | Grants buffs to players within 5 &lt;tag=Tiles&gt; when casting buffs. However, the spell cost is increased by 50%. | Ger buffar till spelare inom 5 &lt;tag=Tiles&gt; när du kastar buffar. Men besvärjelsekostnaden ökar med 50 %. | Heltal |
| `PHYSICAL_DAMAGE` | Physical Damage | Fysisk skada | Heltal |
| `PHYSICAL_DAMAGE_AMP` | &lt;tag=PhysicalDamage&gt; Amplification | &lt;tag=PhysicalDamage&gt;-förstärkning | 1 = 1% |
| `PLANET_ATTACK_SPEED` | Planet Attack Speed | Planeternas attackhastighet | 1 = 1% |
| `PLANET_DAMAGE` | Planet Damage | Planetskada | 1 = 1% |
| `POTION_SLOT` | Potion Bag Slot | Plats i dryckesväska | Heltal |
| `RECEIVED_DAMAGE_REDUCTION` | Damage Taken Reduction | Minskad mottagen skada | 1 = 1% |
| `REVIVE_PLAYER_HASTE` | Reduces the time to revive another player by {VALUE} | Minskar tiden för att återuppliva en annan spelare med {VALUE} | 1 = 1% |
| `SAPPHIREBONUS` | Sapphire Acquisition Bonus | Bonus för safirinsamling | 1 = 1% |
| `SPECIAL_ATTACK_DAMAGE` | Special Attack Damage | Specialattackskada | 1 = 1% |
| `STARTING_MONEY` | Start with {VALUE} &lt;tag=Leaf:nocolor&gt; | I början av expeditionen {VALUE} &lt;tag=Leaf:nocolor&gt; | Heltal |
| `SUB_BAG_SLOT` | Side Bag Slot | Plats i sidoväska | Heltal |
| `THORNS` | Thorns | Törnen | 1 = 1% |
| `TOUGHNESS` | Toughness | Tålighet | Heltal |
| `TRUE_DAMAGE` | True Damage | Ren skada | Heltal |
| `UNLIMITED_COMBO` | For each excess Combo, &lt;tag=FinalDamage&gt; | För varje överskjutande kombo, &lt;tag=FinalDamage&gt; | 1 = 1% |
| `WEAPON_CRITICAL` | Critical Hit Chance of &lt;tag=WeaponAction_DirectAttack&gt; | Kritisk träffchans för &lt;tag=WeaponAction_DirectAttack&gt; | 100 = 1% |
| `WEAPON_RANGE` | Melee Attack Range | Närstridsräckvidd | 1 = 1% |
